using AISTN.Common.Helper;
using AISTN.Common.Models.PageResult;
using AISTN.Common.Services;
using AISTN.Data.DataModel;
using AISTN.ExternalAppAPI.Helper;
using AISTN.ExternalAppAPI.Models.Details;
using AISTN.ExternalAppAPI.Models.Filter;
using AISTN.ExternalAppAPI.Models.Index;
using AISTN.ExternalAppAPI.Models.Save;
using AISTN.Repository;
using AISTN.Repository.Attributes;
using AISTN.Repository.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AISTN.ExternalAppAPI.Services;

[Injectable]
public class ActivityService : ServiceBase
{
    private readonly IGenericRepository<Activity> _activityRepository;
    private readonly IGenericRepository<Blob> _blobRepository;
    private readonly IGenericRepository<Case> _caseRepository;
    private readonly IConfiguration _configuration;
    private readonly IGenericRepository<DocumentContent> _documentContentRepository;
    private readonly IGenericRepository<TemplateDocument> _templateDocumentRepository;
    private readonly Guid? _userId;
    private readonly IGenericRepository<Syndic> _syndicRepository;
    private readonly Exception _invalidUserException = new Exception("Невалиден потребител.");

    public ActivityService(IMapper mapper, ExceptionLogger logger, ExcelGenerator excelGenerator,
        IGenericRepository<Activity> activityRepository,
        IGenericRepository<Blob> blobRepository,
        IGenericRepository<Case> caseRepository,
        IGenericRepository<DocumentContent> documentContentRepository,
        IConfiguration configuration,
        IGenericRepository<Syndic> syndicRepository,
        IGenericRepository<TemplateDocument> templateDocumentRepository,
        UserService userService,
        IHttpContextAccessor contextAccessor)
        : base(mapper, logger, excelGenerator)
    {
        SetCurrentUser(userService.GetCurrentUser(contextAccessor.HttpContext!).ResultData);
        _activityRepository = activityRepository;
        _blobRepository = blobRepository;
        _caseRepository = caseRepository;
        _configuration = configuration;
        _documentContentRepository = documentContentRepository;
        _syndicRepository = syndicRepository;
        _templateDocumentRepository = templateDocumentRepository;
        _userId = _currentUser.UserId;
    }

    public OperationResult<PagedList<ActivityIndexDTO>> GetAllActivities(int pageNumber, int pageSize)
    {
        try
        {
            var activities = _activityRepository.Get(x => x.Syndic.UserId == _userId.Value,
                src => src.Include(x => x.Syndic)).AsQueryable();

            return Success(PagedList<ActivityIndexDTO>.ToPagedList(activities.ProjectTo<ActivityIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
        }
        catch (Exception ex)
        {
            _logger.LogException(ex);
            return Exception<PagedList<ActivityIndexDTO>>(ex);
        }
    }

    public OperationResult<PagedList<ActivityIndexDTO>> SearchActivities(int pageNumber, int pageSize,
        ActivitySearchFilter filter)
    {
        try
        {
            var query = GetActivitiesFiltered(filter);
            return Success(PagedList<ActivityIndexDTO>.ToPagedList(
                query.ProjectTo<ActivityIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
        }
        catch (Exception ex)
        {
            _logger.LogException(ex);
            return Exception<PagedList<ActivityIndexDTO>>(ex);
        }
    }

    public OperationResult<Guid> CreteActivity(SaveActivityDTO activityDTO)
    {
        try
        {
            var syndic = _syndicRepository.Get(x => x.UserId == _userId).FirstOrDefault();

            if (syndic?.UserId != _userId || IsInvalidUser())
            {
                return Exception<Guid>(_invalidUserException);
            }

            activityDTO.SyndicId = syndic.Id;

            var activity = _mapper.Map<Activity>(activityDTO);
            _activityRepository.Add(activity);
            _activityRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.CreateActivity));

            return Success(activity.Id);
        }
        catch (Exception ex)
        {
            _logger.LogException(ex);
            return Exception<Guid>(ex);
        }
    }

    public OperationResult<SaveActivityDTO> UpdateActivity(SaveActivityDTO activityDTO)
    {
        try
        {
            var activity = _activityRepository.GetById(activityDTO.Id.Value);

            if (activity == null)
            {
                return Exception<SaveActivityDTO>(new Exception("Няма намерен дневник."));
            }

            _mapper.Map(activityDTO, activity);

            _activityRepository.Update(activity);
            _activityRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.UpdateActivity));

            return Success(_mapper.Map<SaveActivityDTO>(activity));
        }
        catch (Exception ex)
        {
            _logger.LogException(ex);
            return Exception<SaveActivityDTO>(ex);
        }
    }

    public OperationResult<ActivityIndexDTO> GetActivityById(Guid activityId)
    {
        try
        {
            var activity = _activityRepository.GetById(activityId, source => source.Include(x => x.Syndic));
            var activityDTO = _mapper.Map<ActivityIndexDTO>(activity);

            return Success(activityDTO);
        }
        catch (Exception ex)
        {
            _logger.LogException(ex);
            return Exception<ActivityIndexDTO>(ex);
        }
    }

    public OperationResult<bool> DeleteActivity(Guid id)
    {
        try
        {
            var activity = _activityRepository.GetById(id, source => source.Include(x => x.Syndic));

            if (activity == null)
            {
                return Exception<bool>(new Exception("Няма намерен дневник."));
            }

            if (activity.Syndic.UserId != _userId)
            {
                return Exception<bool>(new Exception("Действието не е разрешено"));
            }


            _activityRepository.Remove(activity.Id);
            _activityRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.DeleteActivity));

            return Success(true);
        }
        catch (Exception ex)
        {
            _logger.LogException(ex);
            return Exception<bool>(ex);
        }
    }

    private IQueryable<Activity> GetActivitiesFiltered(ActivitySearchFilter filter)
    {
        return _activityRepository.Get(filter: x => (filter.CaseId == null || x.CaseId == filter.CaseId)
                                                    && x.Syndic.UserId == _userId,
                                              source => source.Include(x => x.Case)
                                                        .Include(x => x.Syndic)).AsQueryable()
                                                        .OrderBy(x => x.Id);
    }

    public TemplateDownloadModel GenerateActivityDocument(Guid caseId)
    {
        var templateDocument = _templateDocumentRepository.Get(x => x.Type == TemplateDocumentType.Journal.ToString(),
                                                                                            src => src.Include(x => x.DocumentCollection)
                                                                                                        .ThenInclude(x => x.DocumentContents)
                                                                                                        .ThenInclude(x => x.Blob))
                                                                                                           .FirstOrDefault();
        var caseRepo = _caseRepository.GetById(caseId,
                                               source => source.Include(x => x.Court)
                                                               .Include(x => x.Sides).ThenInclude(x => x.Entity));

        var debtor = caseRepo.Sides.FirstOrDefault(x => x.IsDebtor == true)?.Entity;

        var syndic = _syndicRepository.Get(x => x.UserId == _userId).FirstOrDefault();

        var syndicData = GetSyndicById(syndic.Id);

        byte[] docBlob = null;

        if (templateDocument.DocumentCollection.DocumentContents.FirstOrDefault().BlobId.HasValue)
        {
            docBlob = _blobRepository.GetById(templateDocument.DocumentCollection.DocumentContents.FirstOrDefault().BlobId.Value).DocumentContent;
        }

        using (MemoryStream mem = new MemoryStream())
        {
            mem.Write(docBlob, 0, (int)docBlob.Length);

            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(mem, true))
            {
                Body body = wordDoc.MainDocumentPart.Document.Body;

                body.InnerXml = body.InnerXml.Replace(ActivityTemplateModel.SyndicFullName, _currentUser.FullName);
                body.InnerXml = body.InnerXml.Replace(ActivityTemplateModel.DebtorName, debtor?.Name ?? null);
                body.InnerXml = body.InnerXml.Replace(ActivityTemplateModel.DebtorIdentifier, debtor?.Bulstat ?? null);
                body.InnerXml = body.InnerXml.Replace(ActivityTemplateModel.CaseNumber, caseRepo.Number);
                body.InnerXml = body.InnerXml.Replace(ActivityTemplateModel.CaseYear, caseRepo.Year.ToString());
                body.InnerXml = body.InnerXml.Replace(ActivityTemplateModel.CourtName, caseRepo.Court.Name);
                body.InnerXml = body.InnerXml.Replace(ActivityTemplateModel.SyndicAddress, syndicData.ResultData.SyndicAddress);


                var activities = _activityRepository.Get(x => x.CaseId == caseId);

                #region Activities Table

                if (activities != null && activities.Any())
                {
                    // Create a table using the Open XML SDK
                    Table activitiesTable = new Table();

                    // Define table properties (optional)
                    TableProperties objectsTableProperties = new TableProperties(
                         new TableWidth { Width = "5000", Type = TableWidthUnitValues.Pct },
                        new TableBorders(
                            new TopBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 1 }, // Top border
                            new BottomBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 1 }, // Bottom border
                            new LeftBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 1 }, // Left border
                            new RightBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 1 }, // Right border
                            new InsideHorizontalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 1 }, // Inside horizontal border
                            new InsideVerticalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 1 } // Inside vertical border
                         ),
                         new InsideHorizontalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 1 }, // Inside horizontal border (between rows)
                         new InsideVerticalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 1 } // Inside vertical border (between columns)
                    );
                    activitiesTable.Append(objectsTableProperties);

                    // Define table columns
                    TableGrid objectsTableGrid = new TableGrid(
                        new GridColumn(),
                        new GridColumn(),
                        new GridColumn(),
                        new GridColumn()
                    );
                    activitiesTable.Append(objectsTableGrid);

                    // Add table headers
                    TableRow objectsTableHeaderRow = new TableRow();
                    objectsTableHeaderRow.Append(
                        new TableCell(new Paragraph(new Run(new Text("Дата на вписване")))),
                        new TableCell(new Paragraph(new Run(new Text("Пореден № на действието ")))),
                        new TableCell(new Paragraph(new Run(new Text("Описание на извършеното действие ")))),
                        new TableCell(new Paragraph(new Run(new Text("Забележка"))))
                    );
                    activitiesTable.Append(objectsTableHeaderRow);

                    // Iterate through the list of invoices and add rows for each invoice
                    foreach (var activity in activities)
                    {
                        TableRow row = new TableRow();
                        row.Append(
                            new TableCell(new Paragraph(new Run(new Text(activity.ActivityDate.ToString("dd-MM-yyyy"))))),
                            new TableCell(new Paragraph(new Run(new Text(activity.SerialNumber)))),
                            new TableCell(new Paragraph(new Run(new Text(activity.Description)))),
                            new TableCell(new Paragraph(new Run(new Text(activity.Notes))))
                        );
                        activitiesTable.Append(row);
                    }

                    // Find and replace the placeholder in the Word document
                    var activitiesTablePlaceholder = body.Descendants<Text>().FirstOrDefault(t => t.Text.Contains(ActivityTemplateModel.ActivitiesTable));

                    try
                    {
                        // Attempt to insert the table  
                        activitiesTablePlaceholder.Text = string.Empty;
                        int index = body.Descendants<Paragraph>().ToList().IndexOf(activitiesTablePlaceholder.Ancestors<Paragraph>().First());
                        body.InsertAt(activitiesTable, index);
                    }
                    catch (Exception ex)
                    {
                        // Handle and log the exception
                        Console.WriteLine("Error while inserting ObjectsTable: " + ex.Message);
                    }
                }
                else
                {
                    body.InnerXml = body.InnerXml.Replace(ActivityTemplateModel.ActivitiesTable, null);
                }

                #endregion

            }

            return new TemplateDownloadModel()
            {
                FileName = "Дневник на синдика " + caseRepo.Number + "_" + DateTime.Now.ToShortDateString() + ".docx",
                MimeType = templateDocument.DocumentCollection.DocumentContents.FirstOrDefault().ContentMimeType,
                BlobContent = mem.ToArray()
            };
        }
    }

    public OperationResult<DetailsSyndicAdminTemplateDTO> GetSyndicById(Guid id)
    {
        try
        {
            var syndic = _syndicRepository.GetById(id, include: source => source.Include(x => x.Syndic2Addresses)
                                                                                    .ThenInclude(x => x.Address!)
                                                                                    .ThenInclude(x => x.Region)
                                                                                 .Include(x => x.Syndic2Addresses)
                                                                                    .ThenInclude(x => x.Address!)
                                                                                    .ThenInclude(x => x.Settlement)
                                                                                 .Include(x => x.Syndic2Addresses)
                                                                                    .ThenInclude(x => x.Address!)
                                                                                    .ThenInclude(x => x.Municipality!));
            return Success(_mapper.Map<DetailsSyndicAdminTemplateDTO>(syndic));
        }
        catch (Exception ex)
        {
            _logger.LogException(ex);
            return Exception<DetailsSyndicAdminTemplateDTO>(ex);
        }

    }

    private bool IsInvalidUser()
    {
        return _userId is null || _userId.Value == Guid.Empty;
    }
}