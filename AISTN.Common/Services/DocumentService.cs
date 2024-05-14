using AISTN.Common.Helper;
using AISTN.Common.Models;
using AISTN.Common.Models.PageResult;
using AISTN.Data.DataModel;
using AISTN.Repository;
using AISTN.Repository.Attributes;
using AISTN.Repository.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Object = AISTN.Data.DataModel.Object;


namespace AISTN.Common.Services
{
    [Injectable]
    public class DocumentService : ServiceBase
    {
        private readonly IGenericRepository<DocumentContent> _documentContentRepository;
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<Installment> _installmentRepository;
        private readonly IGenericRepository<Syndic> _syndicRepository;
        private readonly IGenericRepository<StatisticalReport> _statisticalReportRepository;
        private readonly IGenericRepository<Announcement> _announcementRepository;
        private readonly IGenericRepository<Object> _objectRepository;
        private readonly IGenericRepository<Signal> _signalRepository;
        private readonly IGenericRepository<TemplateArticle28> _templateArticle28;
        private readonly IGenericRepository<Inspection> _inspection;
        private readonly IGenericRepository<Course> _course;
        private readonly IGenericRepository<Plea> _plea;
        private readonly IGenericRepository<AdminTemplate> _adminTemplate;
        private readonly IGenericRepository<ReportTemplate> _reportTemplate;
        private readonly IGenericRepository<Template> _templateRepository;
        private readonly IGenericRepository<Report> _reportRepository;
        private readonly IGenericRepository<Activity> _activityRepository;
        private readonly IGenericRepository<Property> _propertyRepository;
        private readonly IGenericRepository<TemplateDocument> _templateDocumentRepository;
        private readonly IGenericRepository<DocumentLegalBasis> _documentLegalBasisRepo;
        private readonly IGenericRepository<ActAnnouncement> _actAnnouncementRepo;
        private readonly IGenericRepository<RegisterEntry> _registerEntry;


        public DocumentService(
            IMapper mapper,
            ExceptionLogger logger,
            IGenericRepository<DocumentContent> documentContentRepository,
            IGenericRepository<Order> orderRepository,
            IGenericRepository<Installment> installmentRepository,
            IGenericRepository<Syndic> syndicRepository,
            IGenericRepository<StatisticalReport> statisticalReportRepository,
            IGenericRepository<Signal> signalRepository,
            ExcelGenerator excelGenerator,
            IGenericRepository<Announcement> announcementRepository,
            IGenericRepository<Data.DataModel.Object> objectRepository,
            IGenericRepository<TemplateArticle28> templateArticle28,
            IGenericRepository<Inspection> inspection,
            IGenericRepository<Course> course,
            IGenericRepository<Plea> plea,
            IGenericRepository<AdminTemplate> adminTemplate,
            IGenericRepository<ReportTemplate> reportTemplate,
            IGenericRepository<Template> templateRepository,
            IGenericRepository<Report> reportRepository,
            IGenericRepository<Activity> activityRepository,
            IGenericRepository<Property> propertyRepository,
            IGenericRepository<TemplateDocument> templateDocumentRepository,
            IGenericRepository<DocumentLegalBasis> documentLegalBasisRepo,
            IGenericRepository<ActAnnouncement> actAnnouncementRepo,
            IGenericRepository<RegisterEntry> registerEntry) : base(mapper, logger, excelGenerator)
        {
            _documentContentRepository = documentContentRepository;
            _orderRepository = orderRepository;
            _installmentRepository = installmentRepository;
            _syndicRepository = syndicRepository;
            _statisticalReportRepository = statisticalReportRepository;
            _announcementRepository = announcementRepository;
            _objectRepository = objectRepository;
            _signalRepository = signalRepository;
            _templateArticle28 = templateArticle28;
            _inspection = inspection;
            _course = course;
            _plea = plea;
            _adminTemplate = adminTemplate;
            _reportTemplate = reportTemplate;
            _templateRepository = templateRepository;
            _reportRepository = reportRepository;
            _activityRepository = activityRepository;
            _propertyRepository = propertyRepository;
            _templateDocumentRepository = templateDocumentRepository;
            _documentLegalBasisRepo = documentLegalBasisRepo;
            _actAnnouncementRepo = actAnnouncementRepo;
            _registerEntry = registerEntry;
        }

        public OperationResult<DocumentContentDTO> AttachDocumentFile(FileDTO file)
        {
            try
            {
                if (file.AttachedDocumentCollectionId == null)
                {
                    var document = InsertDocuments(file);

                    if (AttachedDocumentParent.Installment == file.DocumentContentType)
                    {
                        if (UpdateInstallmentDocumentWithAttachedFile(document.DocumentCollectionId, file.Id))
                        {
                            return Success(_mapper.Map<DocumentContentDTO>(document), "Успешно качване");
                        }
                    }
                    else if (AttachedDocumentParent.Order == file.DocumentContentType)
                    {
                        if (UpdateOrderDocumentWithAttachedFile(document.DocumentCollectionId, file.Id))
                        {
                            return Success(_mapper.Map<DocumentContentDTO>(document), "Успешно качване");
                        }
                    }
                    else if (AttachedDocumentParent.Syndic == file.DocumentContentType)
                    {
                        if (UpdateSyndicDocumentWithAttachedFile(document.DocumentCollectionId, file.Id))
                        {
                            return Success(_mapper.Map<DocumentContentDTO>(document), "Успешно качване");
                        }
                    }
                    else if (AttachedDocumentParent.Template == file.DocumentContentType)
                    {
                        if (UpdateTemplateDocumentWithAttachedFile(document.DocumentCollectionId, file.Id))
                        {
                            return Success(_mapper.Map<DocumentContentDTO>(document), "Успешно качване");
                        }
                    }
                    else if (AttachedDocumentParent.StatisticalReport == file.DocumentContentType)
                    {
                        if (UpdateStatisticalReportDocumentWithAttachedFile(document.DocumentCollectionId, file.Id))
                        {
                            return Success(_mapper.Map<DocumentContentDTO>(document), "Успешно качване");
                        }
                    }
                    else if (DocumentCollectionType.WebsiteDocument == file.DocumentContentType)
                    {
                        if (UpdateWebsiteDocumentWithAttachedFile(document))
                        {
                            return Success(_mapper.Map<DocumentContentDTO>(document), "Успешно качване");
                        }
                    }
                    else if (AttachedDocumentParent.Announcement == file.DocumentContentType)
                    {
                        if (UpdateAnnouncementAttachedFile(document.DocumentCollectionId, file.Id))
                        {
                            return Success(_mapper.Map<DocumentContentDTO>(document), "Успешно качване");
                        }
                    }
                    else if (AttachedDocumentParent.Object == file.DocumentContentType)
                    {
                        if (UpdateObjectAttachedFile(document.DocumentCollectionId, file.Id))
                        {
                            return Success(_mapper.Map<DocumentContentDTO>(document), "Успешно качване");
                        }
                    }
                    else if (AttachedDocumentParent.Signal == file.DocumentContentType)
                    {
                        if (UpdateSignalAttachedFile(document.DocumentCollectionId, file.Id))
                        {
                            return Success(_mapper.Map<DocumentContentDTO>(document), "Успешно качване");
                        }
                    }
                    else if (AttachedDocumentParent.TemplateArticle28 == file.DocumentContentType)
                    {
                        if (UpdateSignalTemplateArticle28(document.DocumentCollectionId, file.Id))
                        {
                            return Success(_mapper.Map<DocumentContentDTO>(document), "Успешно качване");
                        }
                    }
                    else if (AttachedDocumentParent.Inspection == file.DocumentContentType)
                    {
                        if (UpdateInspection(document.DocumentCollectionId, file.Id))
                        {
                            return Success(_mapper.Map<DocumentContentDTO>(document), "Успешно качване");
                        }
                    }
                    else if (AttachedDocumentParent.Course == file.DocumentContentType)
                    {
                        if (UpdateCourse(document.DocumentCollectionId, file.Id))
                        {
                            return Success(_mapper.Map<DocumentContentDTO>(document), "Успешно качване");
                        }
                    }
                    else if (AttachedDocumentParent.SyndicPlea == file.DocumentContentType)
                    {
                        if (UpdatePlea(document.DocumentCollectionId, file.Id))
                        {
                            return Success(_mapper.Map<DocumentContentDTO>(document), "Успешно качване");
                        }
                    }
                    else if (AttachedDocumentParent.AdminTemplate == file.DocumentContentType)
                    {
                        if (UpdateAdminTemplate(document.DocumentCollectionId, file.Id))
                        {
                            return Success(_mapper.Map<DocumentContentDTO>(document), "Успешно качване");
                        }
                    }
                    else if (AttachedDocumentParent.Message == file.DocumentContentType)
                    {
                        return Success(_mapper.Map<DocumentContentDTO>(document), "Успешно качване");
                    }
                    else if (AttachedDocumentParent.ReportTemplate == file.DocumentContentType)
                    {
                        if (UpdateReportTemplate(document.DocumentCollectionId, file.Id))
                        {
                            return Success(_mapper.Map<DocumentContentDTO>(document), "Успешно качване");
                        }
                    }
                    else if (AttachedDocumentParent.SyndicTemplate == file.DocumentContentType)
                    {
                        if (UpdateSyndicTemplate(document.DocumentCollectionId, file.Id))
                        {
                            return Success(_mapper.Map<DocumentContentDTO>(document), "Успешно качване");
                        }
                    }
                    else if (AttachedDocumentParent.SyndicReportTemplate == file.DocumentContentType)
                    {
                        if (UpdateSyndicReportTemplate(document.DocumentCollectionId, file.Id))
                        {
                            return Success(_mapper.Map<DocumentContentDTO>(document), "Успешно качване");
                        }
                    } 
                    else if (AttachedDocumentParent.Activity == file.DocumentContentType)
                    {
                        if (UpdateActivity(document.DocumentCollectionId, file.Id))
                        {
                            return Success(_mapper.Map<DocumentContentDTO>(document), "Успешно качване");
                        }
                    }
                    else if (AttachedDocumentParent.Property == file.DocumentContentType)
                    {
                        if (UpdateProperty(document.DocumentCollectionId, file.Id))
                        {
                            return Success(_mapper.Map<DocumentContentDTO>(document), "Успешно качване");
                        }
                    }
                    else if (AttachedDocumentParent.TemplateDocument == file.DocumentContentType)
                    {
                        if (UpdateTemplateDocument(document.DocumentCollectionId, file.Id))
                        {
                            return Success(_mapper.Map<DocumentContentDTO>(document), "Успешно качване");
                        }
                    }
                    else if (AttachedDocumentParent.DocumentLegalBasis == file.DocumentContentType)
                    {
                        if (UpdateDocumentLegalBasis(document.DocumentCollectionId, file.Id))
                        {
                            return Success(_mapper.Map<DocumentContentDTO>(document), "Успешно качване");
                        }
                    }
                    else if (AttachedDocumentParent.ActAnnouncement == file.DocumentContentType)
                    {
                        if (UpdateActAnnouncement(document.DocumentCollectionId, file.Id))
                        {
                            return Success(_mapper.Map<DocumentContentDTO>(document), "Успешно качване");
                        }
                    }
                    else if (AttachedDocumentParent.RegisterEntry == file.DocumentContentType)
                    {
                        if (UpdateRegisterEntry(document.DocumentCollectionId, file.Id))
                        {
                            return Success(_mapper.Map<DocumentContentDTO>(document), "Успешно качване");
                        }
                    }
                }
                else if (file.AttachedDocumentCollectionId.HasValue)
                {
                    var document = InsertDocuments(file);

                    return Success(_mapper.Map<DocumentContentDTO>(document), "Успешно качване");

                }
                return Error<DocumentContentDTO>("Неуспешно качване");
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<DocumentContentDTO>(ex);
            }
        }

        private bool UpdateInstallmentDocumentWithAttachedFile(Guid? collectionId, Guid id)
        {
            var installment = _installmentRepository.GetById(id);

            installment.DocumentCollectionId = collectionId;
            _installmentRepository.Update(installment);
            _installmentRepository.Save();
            return true;
        }

        private bool UpdateStatisticalReportDocumentWithAttachedFile(Guid? collectionId, Guid id)
        {
            var statisticalReport = _statisticalReportRepository.GetById(id);

            statisticalReport.DocumentCollectionId = collectionId;
            _statisticalReportRepository.Update(statisticalReport);
            _statisticalReportRepository.Save();
            return true;
        }

        private bool UpdateOrderDocumentWithAttachedFile(Guid? collectionId, Guid id)
        {
            var order = _orderRepository.GetById(id);

            order.DocumentCollectionId = collectionId;
            _orderRepository.Update(order);
            _orderRepository.Save();
            return true;
        }

        private bool UpdateSyndicDocumentWithAttachedFile(Guid? collectionId, Guid id)
        {
            var appeal = _syndicRepository.GetById(id);

            appeal.DocumentCollectionId = collectionId;
            _syndicRepository.Update(appeal);
            _syndicRepository.Save();
            return true;
        }

        private bool UpdateTemplateDocumentWithAttachedFile(Guid? collectionId, Guid id)
        {
            var template = _templateRepository.GetById(id);

            template.DocumentCollectionId = collectionId;
            _templateRepository.Update(template);
            _templateRepository.Save();
            return true;
        }

        private bool UpdateWebsiteDocumentWithAttachedFile(DocumentContent document)
        {
            document.DocumentCollectionId = DocumentCollectionType.WebsiteDocument;

            _documentContentRepository.Update(document);
            _documentContentRepository.Save();
            return true;
        }

        private bool UpdateAnnouncementAttachedFile(Guid? collectionId, Guid id)
        {
            var announcement = _announcementRepository.GetById(id);

            announcement.DocumentCollectionId = collectionId;
            _announcementRepository.Update(announcement);
            _announcementRepository.Save();
            return true;
        }

        private bool UpdateObjectAttachedFile(Guid? collectionId, Guid id)
        {
            var announcement = _objectRepository.GetById(id);

            announcement.DocumentCollectionId = collectionId;
            _objectRepository.Update(announcement);
            _objectRepository.Save();
            return true;
        }

        private bool UpdateSignalAttachedFile(Guid? collectionId, Guid id)
        {
            var signal = _signalRepository.GetById(id);

            signal.DocumentCollectionId = collectionId;
            _signalRepository.Update(signal);
            _signalRepository.Save();
            return true;
        }

        private bool UpdateSignalTemplateArticle28(Guid? collectionId, Guid id)
        {
            var templateArticle28 = _templateArticle28.GetById(id);

            templateArticle28.DocumentCollectionId = collectionId;
            _templateArticle28.Update(templateArticle28);
            _templateArticle28.Save();
            return true;
        }

        private bool UpdateInspection(Guid? collectionId, Guid id)
        {
            var inspection = _inspection.GetById(id);

            inspection.DocumentCollectionId = collectionId;
            _inspection.Update(inspection);
            _inspection.Save();
            return true;
        }

        private bool UpdateCourse(Guid? collectionId, Guid id)
        {
            var course = _course.GetById(id);

            course.DocumentCollectionId = collectionId;
            _course.Update(course);
            _course.Save();
            return true;
        }

        private bool UpdatePlea(Guid? collectionId, Guid id)
        {
            var plea = _plea.GetById(id);

            plea.DocumentCollectionId = collectionId;
            _plea.Update(plea);
            _plea.Save();
            return true;
        }

        private bool UpdateAdminTemplate(Guid? collectionId, Guid id)
        {
            var adminTemplate = _adminTemplate.GetById(id);

            adminTemplate.DocumentCollectionId = collectionId;
            _adminTemplate.Update(adminTemplate);
            _adminTemplate.Save();
            return true;
        }

        private bool UpdateReportTemplate(Guid? collectionId, Guid id)
        {
            var reportTemplate = _reportTemplate.GetById(id);

            reportTemplate.DocumentCollectionId = collectionId;
            _reportTemplate.Update(reportTemplate);
            _reportTemplate.Save();
            return true;
        }

        private bool UpdateSyndicTemplate(Guid? collectionId, Guid id)
        {
            var syndicTemplate = _templateRepository.GetById(id);

            syndicTemplate.DocumentCollectionId = collectionId;
            _templateRepository.Update(syndicTemplate);
            _templateRepository.Save();
            return true;
        }

        private bool UpdateSyndicReportTemplate(Guid? collectionId, Guid id)
        {
            var syndicReportTemplate = _reportRepository.GetById(id);

            syndicReportTemplate.DocumentCollectionId = collectionId;
            _reportRepository.Update(syndicReportTemplate);
            _reportRepository.Save();
            return true;
        }

        private bool UpdateActivity(Guid? collectionId, Guid id)
        {
            var activity = _activityRepository.GetById(id);

            activity.DocumentCollectionId = collectionId;
            _activityRepository.Update(activity);
            _activityRepository.Save();
            return true;
        }

        private bool UpdateProperty(Guid? collectionId, Guid id)
        {
            var property = _propertyRepository.GetById(id);

            property.DocumentCollectionId = collectionId;
            _propertyRepository.Update(property);
            _propertyRepository.Save();
            return true;
        }

        private bool UpdateTemplateDocument(Guid? collectionId, Guid id)
        {
            var templateDocument = _templateDocumentRepository.GetById(id);

            templateDocument.DocumentCollectionId = collectionId;
            _templateDocumentRepository.Update(templateDocument);
            _templateDocumentRepository.Save();
            return true;
        }

        private bool UpdateDocumentLegalBasis(Guid? collectionId, Guid id)
        {
            var documentLegalBasis = _documentLegalBasisRepo.GetById(id);

            documentLegalBasis.DocumentCollectionId = collectionId;
            _documentLegalBasisRepo.Update(documentLegalBasis);
            _documentLegalBasisRepo.Save();
            return true;
        }

        private bool UpdateActAnnouncement(Guid? collectionId, Guid id)
        {
            var actAnnouncement = _actAnnouncementRepo.GetById(id);

            actAnnouncement.DocumentCollectionId = collectionId;
            _actAnnouncementRepo.Update(actAnnouncement);
            _actAnnouncementRepo.Save();
            return true;
        }

        private bool UpdateRegisterEntry(Guid? collectionId, Guid id)
        {
            var registerEntry = _registerEntry.GetById(id);

            registerEntry.DocumentCollectionId = collectionId;
            _registerEntry.Update(registerEntry);
            _registerEntry.Save();
            return true;
        }

        public OperationResult<PagedList<DocumentContentDTO>> GetAllDocumentFiles(int pageNumber, int pageSize, List<Guid> guids)
        {
            try
            {
                var result = _documentContentRepository.Get(x => guids.Contains(x.DocumentCollectionId!.Value),
                                                                 opt => opt.Include(x => x.SignalDocumentKind)
                                                                           .Include(x => x.AttachedDocumentKind)
                                                                           .Include(x => x.Blob))
                                                                           .OrderByDescending(x => x.DocumentDate)
                                                                           .AsQueryable();

                return Success(PagedList<DocumentContentDTO>.ToPagedList(result.ProjectTo<DocumentContentDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<DocumentContentDTO>>(ex);
            }
        }

        public OperationResult<string?> GetFileName(Guid id)
        {
            try
            {
                var fileName = "";
                var file = _documentContentRepository.Get(x => x.DocumentCollectionId == id).FirstOrDefault();
                if (file != null)
                {
                    fileName = file.FileName;
                }
                return Success(fileName);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<string?>(ex);
            }
        }

        public OperationResult<bool> DeleteDocument(Guid id)
        {
            try
            {
                var document = _documentContentRepository.Get(x => x.Id == id).FirstOrDefault();
                _documentContentRepository.Remove(document!.Id);
                _documentContentRepository.Save();
                return Success(true, "Успешно изтрит файл");
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<bool>(ex);
            }
        }

        #region process insert
        private DocumentContent InsertDocuments(FileDTO doc)
        {
            var document = new DocumentContent
            {
                FileName = doc.File.FileName,
                ContentMimeType = doc.File.ContentType,
                FileSize = doc.File.Length,
                BlobId = new Guid(),
                Blob = ProcessNewBlob(doc.File),
                Description = doc.Description,
                Notes = doc.Notes,
                DocumentDate = doc.DocumentDate,
                AttachedDocumentKindId = doc.AttachedDocumentKindId,
                SignalDocumentKindId = doc.SignalDocumentKindId
            };

            if (doc.AttachedDocumentCollectionId.HasValue)
            {
                document.DocumentCollectionId = doc.AttachedDocumentCollectionId.Value;
            }
            else
            {
                document.DocumentCollection = new DocumentCollection();
            }

            _documentContentRepository.Add(document);
            _documentContentRepository.Save();

            return document;
        }

        public Blob ProcessNewBlob(IFormFile file)
        {
            try
            {
                var dataFile = new Blob();

                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    var content = memoryStream.ToArray();
                    dataFile.DocumentContent = content;
                    dataFile.DateCreated = DateTime.Now;
                }
                return dataFile;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                throw;
            }
        }

        #endregion

        public OperationResult<bool> AttachMultipleFiles(Message message, List<FileDTO> files)
        {
            try
            {
                if(message.DocumentCollection == null) { 
                    message.DocumentCollection = new DocumentCollection();
                }

                foreach (var file in files)
                {
                    var documentContent = AttachDocumentFile(file);

                    if (documentContent.Type == ResultType.Success)
                    {
                        message.DocumentCollection.DocumentContents.Add(_documentContentRepository.GetById(documentContent.ResultData.Id));
                    }
                }

                return Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<bool>(ex);
            }
        }

        #region download files
        public DocumentContent GetAttachedDocuments(Guid id)
        {
            try
            {
                return _documentContentRepository.GetById(id, source => source.Include(x => x.Blob));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                throw;
            }
        }
        #endregion

        public OperationResult<SaveDocumentContentDTO> GetDocumentContentById(Guid id)
        {
            try
            {
                var document = _documentContentRepository.GetById(id);

                return Success(_mapper.Map<SaveDocumentContentDTO>(document));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SaveDocumentContentDTO>(ex);
            }
        }


        public OperationResult<SaveDocumentContentDTO> UpdateDocumentContent(SaveDocumentContentDTO documentContent)
        {
            try
            {
                var document = _documentContentRepository.GetById(documentContent.Id);

                if (document == null)
                {
                    return Exception<SaveDocumentContentDTO>(new Exception("Няма намерен документ."));
                }

                document.Description = documentContent.Description;
                document.Notes = documentContent.Notes;
                document.AttachedDocumentKindId = documentContent.AttachedDocumentKindId;
                document.SignalDocumentKindId = documentContent.SignalDocumentKindId;
                document.DocumentDate = documentContent.DocumentDate;
                _documentContentRepository.Update(document);
                _documentContentRepository.Save();

                return Success(_mapper.Map<SaveDocumentContentDTO>(document));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                throw;
            }
        }

    }
}
