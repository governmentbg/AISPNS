using AISTN.Common.Helper;
using AISTN.Common.Models.PageResult;
using AISTN.Common.Services;
using AISTN.Data.DataModel;
using AISTN.ExternalAppAPI.Helper;
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

namespace AISTN.ExternalAppAPI.Services
{
    [Injectable]
    public class PropertyService : ServiceBase
    {
        private readonly IGenericRepository<Property> _propertyRepository;
        private readonly IGenericRepository<Blob> _blobRepository;
        private readonly IGenericRepository<Case> _caseRepository;
        private readonly IGenericRepository<DocumentContent> _documentContentRepository;
        private readonly IGenericRepository<TemplateDocument> _templateDocumentRepository;

        public PropertyService(UserService userService,
                               IHttpContextAccessor contextAccessor,
                               IMapper mapper,
                               ExceptionLogger logger,
                               ExcelGenerator excelGenerator,
                               IGenericRepository<Property> propertyRepository,
                               IGenericRepository<Blob> blobRepository,
                               IGenericRepository<Case> caseRepository,
                               IGenericRepository<DocumentContent> documentContentRepository,
                               IGenericRepository<TemplateDocument> templateDocumentRepository
                               )
                : base(mapper, logger, excelGenerator)
        {
            SetCurrentUser(userService.GetCurrentUser(contextAccessor.HttpContext!).ResultData);
            _propertyRepository = propertyRepository;
            _blobRepository = blobRepository;
            _caseRepository = caseRepository;
            _documentContentRepository = documentContentRepository;
            _templateDocumentRepository = templateDocumentRepository;
        }

        public OperationResult<PagedList<PropertyIndexDTO>> SearchProperty(int pageNumber, int pageSize, PropertySearchFilter filter)
        {
            try
            {
                var query = GetPropertyQuery(filter);
                return Success(PagedList<PropertyIndexDTO>.ToPagedList(query.ProjectTo<PropertyIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<PropertyIndexDTO>>(ex);
            }
        }

        public OperationResult<PagedList<PropertyIndexDTO>> GetPropertiesByClassKind(int pageNumber, int pageSize, PropertyCaseFilter filter)
        {
            try
            {
                var query = default(IQueryable);
                switch (filter.kind)
                {
                    case PropertyClassKind.Things:
                        query = GetPropertyQueryByKind(PropertyClassKindIds.Things, filter.CaseId);
                        return Success(PagedList<PropertyIndexDTO>.ToPagedList(query.ProjectTo<PropertyIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
                    case PropertyClassKind.Patents:
                        query = GetPropertyQueryByKind(PropertyClassKindIds.Patents, filter.CaseId);
                        return Success(PagedList<PropertyIndexDTO>.ToPagedList(query.ProjectTo<PropertyIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
                    case PropertyClassKind.Shares:
                        query = GetPropertyQueryByKind(PropertyClassKindIds.Shares, filter.CaseId);
                        return Success(PagedList<PropertyIndexDTO>.ToPagedList(query.ProjectTo<PropertyIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
                    case PropertyClassKind.Receivables:
                        query = GetPropertyQueryByKind(PropertyClassKindIds.Receivables, filter.CaseId);
                        return Success(PagedList<PropertyIndexDTO>.ToPagedList(query.ProjectTo<PropertyIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
                    default:
                        _logger.LogException(new ArgumentOutOfRangeException(nameof(filter.kind), filter.kind, null));
                        return Exception<PagedList<PropertyIndexDTO>>(new Exception("Обекта не можа да бъде взет."));
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<PropertyIndexDTO>>(ex);
            }
        }

        public OperationResult<SavePropertyDTO> GetPropertyById(Guid id)
        {
            try
            {
                var property = _propertyRepository.GetById(id, src => src.Include(x => x.Case)
                                                                     .Include(x => x.Entity)
                                                                     .Include(x => x.Person)
                                                                     .Include(x => x.PropertyClass)
                                                                     .Include(x => x.PropertyKind)
                                                                     .Include(x => x.PropertyType)
                                                                     .Include(x => x.Address));

                return Success(_mapper.Map<SavePropertyDTO>(property));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SavePropertyDTO>(ex);
            }
        }

        public OperationResult<Guid> CreateProperty(SavePropertyDTO propertyDTO)
        {
            try
            {
                var property = _mapper.Map<Property>(propertyDTO);
                property.Address = _mapper.Map<Address>(propertyDTO.Address);

                _propertyRepository.Add(property);
                _propertyRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.CreateProperty));

                return Success(property.Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }

        public OperationResult<SavePropertyDTO> UpdateProperty(SavePropertyDTO propertyDTO)
        {
            try
            {
                var property = _propertyRepository.GetById(propertyDTO.Id.Value);

                if (property == null)
                {
                    return Exception<SavePropertyDTO>(new Exception("Няма намерена вещ."));
                }

                _mapper.Map(propertyDTO, property);

                _propertyRepository.Update(property);
                _propertyRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.UpdateProperty));

                return Success(_mapper.Map<SavePropertyDTO>(property));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SavePropertyDTO>(ex);
            }
        }

        public OperationResult<Guid> DeleteProperty(Guid id)
        {
            try
            {
                var property = _propertyRepository.GetById(id);

                if (property == null)
                {
                    return Exception<Guid>(new Exception("Няма намерена вещ."));
                }

                _propertyRepository.Remove(property.Id);
                _propertyRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.DeleteProperty));

                return Success(property.Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }

        private IQueryable<Property> GetPropertyQuery(PropertySearchFilter filter)
        {
            return _propertyRepository.Get(filter: x => (filter.CaseNumber == null || x.Case.Number == filter.CaseNumber)
                                                 && (filter.CaseYear == null || x.Case.Year == filter.CaseYear)
                                                 && (filter.CourtName == null || x.Case.Court.Name == filter.CourtName)
                                                 && (filter.DebtorName == null || x.Case.Sides.Any(x => x.IsDebtor == true && x.Entity.Name.Contains(filter.DebtorName)))
                                                 && (filter.FromDate == null || x.DateCreated == filter.FromDate)
                                                 && (filter.ToDate == null || x.DateCreated == filter.ToDate),
                                                     source => source.Include(x => x.Case)
                                                                     .Include(x => x.Entity)
                                                                     .Include(x => x.Person)
                                                                     .Include(x => x.PropertyClass)
                                                                     .Include(x => x.PropertyKind)
                                                                     .Include(x => x.PropertyType)
                                                                     .Include(x => x.Address))
                                                                  .AsQueryable()
                                                                  .OrderBy(x => x.Id);
        }

        private IQueryable<Property> GetPropertyQueryByKind(Guid kindId, Guid caseId)
        {
            return _propertyRepository.Get(x => (x.PropertyClassId == kindId) && (x.CaseId == caseId),
                                                     source => source.Include(x => x.Case)
                                                                     .Include(x => x.Entity)
                                                                     .Include(x => x.Person)
                                                                     .Include(x => x.PropertyClass)
                                                                     .Include(x => x.PropertyKind)
                                                                     .Include(x => x.PropertyType)
                                                                     .Include(x => x.Address)
                                                                        .ThenInclude(x => x.Municipality)
                                                                     .Include(x => x.Address)
                                                                        .ThenInclude(x => x.Region)
                                                                     .Include(x => x.Address)
                                                                        .ThenInclude(x => x.Settlement))
                                                                  .AsQueryable()
                                                                  .OrderBy(x => x.Id);
        }

        public TemplateDownloadModel GeneratePropertyDocument(Guid caseId)
        {
            var templateDocument = _templateDocumentRepository.Get(x => x.Type == TemplateDocumentType.Bankruptcy.ToString(),
                                                                                            src => src.Include(x => x.DocumentCollection)
                                                                                                        .ThenInclude(x => x.DocumentContents)
                                                                                                        .ThenInclude(x => x.Blob))
                                                                                                           .FirstOrDefault();
            var caseRepo = _caseRepository.GetById(caseId,
                                                   source => source.Include(x => x.Court)
                                                                   .Include(x => x.Sides).ThenInclude(x => x.Entity));

            var debtor = caseRepo.Sides.FirstOrDefault(x => x.IsDebtor == true)?.Entity;


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

                    body.InnerXml = body.InnerXml.Replace(PropertyTemplateModel.CaseNumber, caseRepo.Number);
                    body.InnerXml = body.InnerXml.Replace(PropertyTemplateModel.CaseYear, caseRepo.Year.ToString());
                    body.InnerXml = body.InnerXml.Replace(PropertyTemplateModel.CourtName, caseRepo.Court.Name);
                    body.InnerXml = body.InnerXml.Replace(PropertyTemplateModel.DebtorName, debtor?.Name ?? null);
                    body.InnerXml = body.InnerXml.Replace(PropertyTemplateModel.DebtorIdentifier, debtor?.Bulstat ?? null);
                    body.InnerXml = body.InnerXml.Replace(PropertyTemplateModel.SyndicFullName, _currentUser.FullName);


                    var objects = _propertyRepository.Get(x => x.PropertyClass.Description == "Вещи" && x.CaseId == caseId, source => source.Include(x => x.PropertyType)
                                                                                                                                            .Include(x => x.PropertyKind)
                                                                                                                                            .Include(x => x.Address).ThenInclude(x => x.Settlement)
                                                                                                                                            .Include(x => x.Address).ThenInclude(x => x.Municipality)
                                                                                                                                            .Include(x => x.Address).ThenInclude(x => x.Region));
                    var takings = _propertyRepository.Get(x => x.PropertyClass.Description == "Взимания" && x.CaseId == caseId, source => source.Include(x => x.PropertyType)
                                                                                                                                                .Include(x => x.Entity));
                    var shares = _propertyRepository.Get(x => x.PropertyClass.Description == "Дялове" && x.CaseId == caseId, source => source.Include(x => x.PropertyType)
                                                                                                                                             .Include(x => x.Entity));
                    var patent = _propertyRepository.Get(x => x.PropertyClass.Description == "Патенти" && x.CaseId == caseId, source => source.Include(x => x.PropertyType));

                    #region Tables Properties

                    // Calculate the page width in twips for letter size page with 0.5" margins
                    //const double letterPageWidthInInches = 8.5; // Letter page width in inches
                    //const double letterPageHeightInInches = 11; // Letter page height in inches
                    //const double marginInInches = 0.50; // Margin width in inches
                    //const int dpi = 72; // Dots per inch (standard for Word)
                    //const int twipsPerInch = 1440; // Twips per inch

                    //int pageWidthInTwips = (int)((letterPageWidthInInches - (2 * marginInInches)) * dpi * twipsPerInch);
                    //int pageHeightInTwips = (int)((letterPageHeightInInches - (2 * marginInInches)) * dpi * twipsPerInch);


                    #endregion


                    #region ObjectsTable

                    if (objects != null && objects.Any())
                    {
                        // Create a table using the Open XML SDK
                        Table objectsTable = new Table();

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
                        objectsTable.Append(objectsTableProperties);

                        // Define table columns
                        TableGrid objectsTableGrid = new TableGrid(
                            new GridColumn(),
                            new GridColumn(),
                            new GridColumn(),
                            new GridColumn(),
                            new GridColumn(),
                            new GridColumn(),
                            new GridColumn()
                        );
                        objectsTable.Append(objectsTableGrid);

                        // Add table headers
                        TableRow objectsTableHeaderRow = new TableRow();
                        objectsTableHeaderRow.Append(
                            new TableCell(new Paragraph(new Run(new Text("Тип")))),
                            new TableCell(new Paragraph(new Run(new Text("Вид")))),
                            new TableCell(new Paragraph(new Run(new Text("Адрес")))),
                            new TableCell(new Paragraph(new Run(new Text("Състояние")))),
                            new TableCell(new Paragraph(new Run(new Text("Бележки")))),
                            new TableCell(new Paragraph(new Run(new Text("Стойност"))))
                        );
                        objectsTable.Append(objectsTableHeaderRow);

                        // Iterate through the list of invoices and add rows for each invoice
                        foreach (var obj in objects)
                        {
                            TableRow row = new TableRow();
                            row.Append(
                                new TableCell(new Paragraph(new Run(new Text(obj.PropertyType.Description)))),
                                new TableCell(new Paragraph(new Run(new Text(obj.PropertyKind.Description)))),
                                new TableCell(new Paragraph(new Run(new Text(GetConcatenatedAddress(obj.Address))))),
                                new TableCell(new Paragraph(new Run(new Text(obj.State)))),
                                new TableCell(new Paragraph(new Run(new Text(obj.Notes)))),
                                new TableCell(new Paragraph(new Run(new Text(obj.Value))))
                            );
                            objectsTable.Append(row);
                        }

                        // Find and replace the placeholder in the Word document
                        var objectsTablePlaceholder = body.Descendants<Text>().FirstOrDefault(t => t.Text.Contains(PropertyTemplateModel.ObjectsTable));

                        try
                        {
                            // Attempt to insert the table  
                            objectsTablePlaceholder.Text = string.Empty;
                            int index = body.Descendants<Paragraph>().ToList().IndexOf(objectsTablePlaceholder.Ancestors<Paragraph>().First());
                            body.InsertAt(objectsTable, index);
                        }
                        catch (Exception ex)
                        {
                            // Handle and log the exception
                            Console.WriteLine("Error while inserting ObjectsTable: " + ex.Message);
                        }
                    }
                    else
                    {
                        body.InnerXml = body.InnerXml.Replace(PropertyTemplateModel.ObjectsTable, null);
                    }

                    #endregion

                    #region TakingsTable
                    if (takings != null && takings.Any())
                    {
                        // Create a table using the Open XML SDK
                        Table takingsTable = new Table();

                        // Define table properties (optional)
                        TableProperties takingsTableProperties = new TableProperties(
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
                        takingsTable.Append(takingsTableProperties);

                        // Define table columns
                        TableGrid takingsTableGrid = new TableGrid(
                            new GridColumn(),
                            new GridColumn(),
                            new GridColumn(),
                            new GridColumn(),
                            new GridColumn(),
                            new GridColumn(),
                            new GridColumn()
                        );
                        takingsTable.Append(takingsTableGrid);

                        // Add table headers
                        TableRow takingsTableHeaderRow = new TableRow();
                        takingsTableHeaderRow.Append(
                            new TableCell(new Paragraph(new Run(new Text("Вид")))),
                            new TableCell(new Paragraph(new Run(new Text("Ю/ФЛ Име")))),
                            new TableCell(new Paragraph(new Run(new Text("ЕИК/ЕГН")))),
                            new TableCell(new Paragraph(new Run(new Text("Описание")))),
                            new TableCell(new Paragraph(new Run(new Text("Бележки")))),
                            new TableCell(new Paragraph(new Run(new Text("Стойност"))))
                        );
                        takingsTable.Append(takingsTableHeaderRow);

                        // Iterate through the list of invoices and add rows for each invoice
                        foreach (var obj in takings)
                        {
                            TableRow row = new TableRow();
                            row.Append(
                                new TableCell(new Paragraph(new Run(new Text(obj.PropertyType.Description)))),
                                new TableCell(new Paragraph(new Run(new Text(obj.Entity.Name)))),
                                new TableCell(new Paragraph(new Run(new Text(obj.Entity.Bulstat)))),
                                new TableCell(new Paragraph(new Run(new Text(obj.State)))),
                                new TableCell(new Paragraph(new Run(new Text(obj.Notes)))),
                                new TableCell(new Paragraph(new Run(new Text(obj.Value))))
                            );
                            takingsTable.Append(row);
                        }

                        // Find and replace the placeholder in the Word document
                        var takingsTableParagraph = body.Descendants<Paragraph>().FirstOrDefault(p => p.InnerText.Contains("TAKINGS_TABLE"));

                        // After inserting the first table
                        body = wordDoc.MainDocumentPart.Document.Body;

                        try
                        {
                            InsertTable(takingsTable, takingsTableParagraph, body);
                        }
                        catch (Exception ex)
                        {
                            // Handle and log the exception
                            Console.WriteLine("Error while inserting TAKINGS_TABLE: " + ex.Message);
                        }
                    }
                    else
                    {
                        body.InnerXml = body.InnerXml.Replace(PropertyTemplateModel.TakingsTable, null);
                    }

                    #endregion

                    #region SharesTable
                    if (shares != null && shares.Any())
                    {
                        // Create a table using the Open XML SDK
                        Table sharesTable = new Table();

                        // Define table properties (optional)
                        TableProperties sharesTableProperties = new TableProperties(
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
                        sharesTable.Append(sharesTableProperties);

                        // Define table columns
                        TableGrid sharesTableGrid = new TableGrid(
                            new GridColumn(),
                            new GridColumn(),
                            new GridColumn(),
                            new GridColumn(),
                            new GridColumn(),
                            new GridColumn(),
                            new GridColumn()
                        );
                        sharesTable.Append(sharesTableGrid);

                        // Add table headers
                        TableRow sharesTableHeaderRow = new TableRow();
                        sharesTableHeaderRow.Append(
                            new TableCell(new Paragraph(new Run(new Text("Вид")))),
                            new TableCell(new Paragraph(new Run(new Text("ЮЛ Име")))),
                            new TableCell(new Paragraph(new Run(new Text("ЕИК")))),
                            new TableCell(new Paragraph(new Run(new Text("Описание")))),
                            new TableCell(new Paragraph(new Run(new Text("Бележки")))),
                            new TableCell(new Paragraph(new Run(new Text("Стойност"))))
                        );
                        sharesTable.Append(sharesTableHeaderRow);

                        // Iterate through the list of invoices and add rows for each invoice
                        foreach (var obj in shares)
                        {
                            TableRow row = new TableRow();
                            row.Append(
                                new TableCell(new Paragraph(new Run(new Text(obj.PropertyType.Description)))),
                                new TableCell(new Paragraph(new Run(new Text(obj.Entity.Name)))),
                                new TableCell(new Paragraph(new Run(new Text(obj.Entity.Bulstat)))),
                                new TableCell(new Paragraph(new Run(new Text(obj.State)))),
                                new TableCell(new Paragraph(new Run(new Text(obj.Notes)))),
                                new TableCell(new Paragraph(new Run(new Text(obj.Value))))
                            );
                            sharesTable.Append(row);
                        }

                        // Find and replace the placeholder in the Word document
                        var sharesTablePlaceholder = body.Descendants<Text>().FirstOrDefault(t => t.Text.Contains(PropertyTemplateModel.SharesTable));

                        // After inserting the first table
                        body = wordDoc.MainDocumentPart.Document.Body;

                        // Find the paragraph containing the placeholder
                        var sharesTableParagraph = sharesTablePlaceholder.Ancestors<Paragraph>().FirstOrDefault();

                        try
                        {
                            if (sharesTableParagraph != null)
                            {
                                // Attempt to insert the table  
                                int index = body.Elements<Paragraph>().ToList().IndexOf(sharesTableParagraph);
                                body.InsertAt(sharesTable, index);
                                sharesTablePlaceholder.Remove(); // Remove the placeholder after inserting the table
                            }
                            else
                            {
                                throw new Exception("Paragraph containing the sharesTable placeholder not found.");
                            }
                        }
                        catch (Exception ex)
                        {
                            // Handle and log the exception
                            Console.WriteLine("Error while inserting SharesTable: " + ex.Message);
                        }
                    }
                    else
                    {
                        body.InnerXml = body.InnerXml.Replace(PropertyTemplateModel.SharesTable, null);
                    }

                    #endregion

                    #region PatentTable
                    if (patent != null && patent.Any())
                    {
                        // Create a table using the Open XML SDK
                        Table patentTable = new Table();

                        // Define table properties (optional)
                        TableProperties patentTableProperties = new TableProperties(
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
                        patentTable.Append(patentTableProperties);

                        // Define table columns
                        TableGrid patentTableGrid = new TableGrid(
                            new GridColumn(),
                            new GridColumn(),
                            new GridColumn(),
                            new GridColumn(),
                            new GridColumn(),
                            new GridColumn(),
                            new GridColumn()
                        );
                        patentTable.Append(patentTableGrid);

                        // Add table headers
                        TableRow patentTableHeaderRow = new TableRow();
                        patentTableHeaderRow.Append(
                            new TableCell(new Paragraph(new Run(new Text("Вид")))),
                            new TableCell(new Paragraph(new Run(new Text("Описание")))),
                            new TableCell(new Paragraph(new Run(new Text("Бележки")))),
                            new TableCell(new Paragraph(new Run(new Text("Стойност"))))
                        );
                        patentTable.Append(patentTableHeaderRow);

                        // Iterate through the list of invoices and add rows for each invoice
                        foreach (var obj in patent)
                        {
                            TableRow row = new TableRow();
                            row.Append(
                                new TableCell(new Paragraph(new Run(new Text(obj.PropertyType.Description)))),
                                new TableCell(new Paragraph(new Run(new Text(obj.State)))),
                                new TableCell(new Paragraph(new Run(new Text(obj.Notes)))),
                                new TableCell(new Paragraph(new Run(new Text(obj.Value))))
                            );
                            patentTable.Append(row);
                        }

                        // Find and replace the placeholder in the Word document
                        var patentTablePlaceholder = body.Descendants<Text>().FirstOrDefault(t => t.Text.Contains(PropertyTemplateModel.PatentTable));

                        // After inserting the first table
                        body = wordDoc.MainDocumentPart.Document.Body;

                        // Find the paragraph containing the placeholder
                        var patentTableParagraph = patentTablePlaceholder.Ancestors<Paragraph>().FirstOrDefault();

                        try
                        {
                            if (patentTableParagraph != null)
                            {
                                // Attempt to insert the table  
                                int index = body.Elements<Paragraph>().ToList().IndexOf(patentTableParagraph);
                                body.InsertAt(patentTable, index);
                                patentTablePlaceholder.Remove(); // Remove the placeholder after inserting the table
                            }
                            else
                            {
                                throw new Exception("Paragraph containing the patentTable placeholder not found.");
                            }
                        }
                        catch (Exception ex)
                        {
                            // Handle and log the exception
                            Console.WriteLine("Error while inserting PatentTable: " + ex.Message);
                        }
                    }
                    else
                    {
                        body.InnerXml = body.InnerXml.Replace(PropertyTemplateModel.PatentTable, null);
                    }

                    #endregion
                }

                return new TemplateDownloadModel()
                {
                    FileName = "Маса на несъстоятелността " + caseRepo.Number + "_" + DateTime.Now.ToShortDateString() + ".docx",
                    MimeType = templateDocument.DocumentCollection.DocumentContents.FirstOrDefault().ContentMimeType,
                    BlobContent = mem.ToArray()
                };
            }
        }


        private void InsertTable(Table table, Paragraph paragraph, Body body)
        {
            if (paragraph != null)
            {
                // Attempt to insert the table  
                int index = body.Elements<Paragraph>().ToList().IndexOf(paragraph);
                body.InsertAt(table, index);
                paragraph.Remove(); // Remove the paragraph containing the placeholder after inserting the table
            }
            else
            {
                throw new Exception("Paragraph containing the placeholder not found.");
            }
        }

        private static string GetConcatenatedAddress(Address source)
        {
            if (source == null) return null;

            List<string> addressParts = new List<string>();
            if (source.Region != null && !string.IsNullOrWhiteSpace(source.Region.Name))
                addressParts.Add(source.Region.Name);
            if (source.Settlement != null && !string.IsNullOrWhiteSpace(source.Settlement.Name))
                addressParts.Add(source.Settlement.Name);
            if (source.Municipality != null && !string.IsNullOrWhiteSpace(source.Municipality.Name))
                addressParts.Add(source.Municipality.Name);
            if (!string.IsNullOrWhiteSpace(source.PostCode))
                addressParts.Add(source.PostCode);
            if (!string.IsNullOrWhiteSpace(source.StreetName))
                addressParts.Add(source.StreetName);
            if (!string.IsNullOrWhiteSpace(source.StreetNumber))
                addressParts.Add(source.StreetNumber);
            if (!string.IsNullOrWhiteSpace(source.BuildingNumber))
                addressParts.Add(source.BuildingNumber);
            if (!string.IsNullOrWhiteSpace(source.EntranceNumber))
                addressParts.Add(source.EntranceNumber);
            if (!string.IsNullOrWhiteSpace(source.FloorNumber))
                addressParts.Add(source.FloorNumber);
            if (!string.IsNullOrWhiteSpace(source.ApartmentNumber))
                addressParts.Add(source.ApartmentNumber);


            return string.Join(", ", addressParts);
        }
    }
}
