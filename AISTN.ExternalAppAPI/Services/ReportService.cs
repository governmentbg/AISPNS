using AISTN.Common.Helper;
using AISTN.Data.DataModel;
using AISTN.Common.Models.PageResult;
using AISTN.Common.Services;
using AISTN.ExternalAppAPI.Models.Filter;
using AISTN.ExternalAppAPI.Models.Index;
using AISTN.ExternalAppAPI.Models.Save;
using AISTN.Repository;
using AISTN.Repository.Attributes;
using AISTN.Repository.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using AISTN.ExternalAppAPI.Helper;
using AISTN.ExternalAppAPI.Models.Details;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace AISTN.ExternalAppAPI.Services
{
    [Injectable]
    public class ReportService : ServiceBase
    {
        private readonly IGenericRepository<Report> _reportRepository;
        private readonly IGenericRepository<ReportTemplate> _reportTemplateRepository;
        private readonly IGenericRepository<Blob> _blobRepository;
        private readonly IGenericRepository<DocumentContent> _documentContentRepository;
        private readonly IGenericRepository<Syndic> _syndicRepository;
        private readonly Guid? _userId;

        public ReportService(IMapper mapper, ExceptionLogger logger,
                               ExcelGenerator excelGenerator,
                               IGenericRepository<Report> reportRepository,
                               IGenericRepository<Blob> blobRepository,
                               IGenericRepository<DocumentContent> documentContentRepository,
                               IGenericRepository<ReportTemplate> reportTemplateRepository,
                               IGenericRepository<Syndic> syndicRepository,
                               UserService userService,
                               IHttpContextAccessor contextAccessor)
            : base(mapper, logger, excelGenerator)
        {
            SetCurrentUser(userService.GetCurrentUser(contextAccessor.HttpContext!).ResultData);
            _reportRepository = reportRepository;
            _reportTemplateRepository = reportTemplateRepository;
            _blobRepository = blobRepository;
            _documentContentRepository = documentContentRepository;
            _syndicRepository = syndicRepository;
            _userId = _currentUser.UserId;
        }

        public OperationResult<PagedList<ReportIndexDTO>> SearchReports(int pageNumber, int pageSize, ReportSearchFilter filter)
        {
            try
            {
                var reports = _reportRepository.Get(filter: x => ((filter.CaseNumber == null) || x.Case.Number.Contains(filter.CaseNumber))
                                                                  && ((filter.CaseYear == null) || x.Case.Year == filter.CaseYear)
                                                                  && ((filter.CourtNumber == null) || x.Case.Court.Number == filter.CourtNumber)
                                                                  && (filter.DebtorName == null || x.Case.Sides.Any(x => x.IsDebtor == true && x.Entity.Name.Contains(filter.DebtorName)))
                                                                  && (filter.DebtorIdentifier == null || x.Case.Sides.Any(x => x.IsDebtor == true && x.Entity.Bulstat.Contains(filter.DebtorIdentifier)))
                                                                  && ((filter.SyndicFirstName == null) || x.Syndic.FirstName.Contains(filter.SyndicFirstName))
                                                                  && ((filter.SyndicLastName == null) || x.Syndic.LastName.Contains(filter.SyndicLastName)),
                    
                                                                    include: source => (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Report, object>)source.Include(x => x.ReportType) 
                                                                                                .Include(x => x.Case)
                                                                                                    .ThenInclude(x => x.Court)
                                                                                                .Include(x => x.Case)
                                                                                                    .ThenInclude(x => x.Sides.Where(x => x.IsDebtor == true))
                                                                                                        .ThenInclude(x => x.Entity)
                                                                                                .Include(x => x.Syndic)
                                                                                                .Include(x => x.Case)
                                                                                                    .ThenInclude(x => x.Court))
                                                                                                .AsQueryable();

                return Success(PagedList<ReportIndexDTO>.ToPagedList(reports.ProjectTo<ReportIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<ReportIndexDTO>>(ex);
            }
        }

        public OperationResult<Guid> CreateReport(SaveReportDTO reportDTO)
        {
            try
            {
                var syndic = _syndicRepository.Get(x => x.UserId == _userId).FirstOrDefault();
                if (syndic == null)
                {
                    return Exception<Guid>(new Exception("Няма намерен синдик."));
                }

                reportDTO.SyndicId = syndic.Id;


                var reportEntity = _mapper.Map<Report>(reportDTO);

                _reportRepository.Add(reportEntity);
                _reportRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.CreateSyndicReport));

                return Success(reportEntity.Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }

        public OperationResult<SaveReportDTO> UpdateReport(SaveReportDTO reportDTO)
        {
            try
            {
                var reportEntity = _reportRepository.GetById(reportDTO.Id.Value);

                if (reportEntity == null)
                {
                    return Exception<SaveReportDTO>(new Exception("Няма намерен отчет."));
                }

                reportEntity = _mapper.Map(reportDTO, reportEntity);
                _reportRepository.Update(reportEntity);
                _reportRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.UpdateSyndicTemplate));

                return Success(_mapper.Map<SaveReportDTO>(reportEntity));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SaveReportDTO>(ex);
            }
        }

        public OperationResult<SaveReportDTO> GetReportById(Guid id)
        {
            try
            {
                var reportEntity = _reportRepository.GetById(id, src => src.Include(x => x.ReportType)
                                                                            .Include(x => x.Syndic));
                return Success(_mapper.Map<SaveReportDTO>(reportEntity));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SaveReportDTO>(ex);
            }
        }

        public TemplateDownloadModel GenerateReportTemplate(Guid reportTypeId)
        {
            var reportTemplate = _reportTemplateRepository.Get(x => x.ReportTypeId == reportTypeId).FirstOrDefault();
            var documentContent = _documentContentRepository.Get(x => x.DocumentCollectionId == reportTemplate.DocumentCollectionId).FirstOrDefault();
            var syndic = _syndicRepository.Get(x => x.UserId == _userId).FirstOrDefault();

            var syndicData = GetSyndicById(syndic.Id);

            byte[] docBlob = null;

            if (documentContent.BlobId.HasValue)
            {
                docBlob = _blobRepository.GetById(documentContent.BlobId.Value).DocumentContent;
            }

            using (MemoryStream mem = new MemoryStream())
            {
                mem.Write(docBlob, 0, (int)docBlob.Length);

                using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(mem, true))
                {
                    Body body = wordDoc.MainDocumentPart.Document.Body;

                    body.InnerXml = body.InnerXml.Replace(SyndicTemplateModel.SyndicFullName, syndicData.ResultData.SyndicFullName);
                    body.InnerXml = body.InnerXml.Replace(SyndicTemplateModel.SyndicAddress, syndicData.ResultData.SyndicAddress);
                    body.InnerXml = body.InnerXml.Replace(SyndicTemplateModel.SyndicIdentifier, syndicData.ResultData.SyndicIdentifier);
                    body.InnerXml = body.InnerXml.Replace(SyndicTemplateModel.SyndicEmail, syndicData.ResultData.SyndicEmail);
                    body.InnerXml = body.InnerXml.Replace(SyndicTemplateModel.SyndicPhone, syndicData.ResultData.SyndicPhone);
                }

                return new TemplateDownloadModel()
                {
                    FileName = reportTemplate.TemplateName + DateTime.Now.ToShortDateString() + ".docx",
                    MimeType = documentContent.ContentMimeType,
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
    }
}
