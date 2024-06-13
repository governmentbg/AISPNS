using AISTN.Common.Helper;
using AISTN.Common.Models;
using AISTN.Common.Models.PageResult;
using AISTN.Common.Services;
using AISTN.Data.DataModel;
using AISTN.ExternalAppAPI.Helper;
using AISTN.ExternalAppAPI.Models.Details;
using AISTN.ExternalAppAPI.Models.Save;
using AISTN.Repository;
using AISTN.Repository.Attributes;
using AISTN.Repository.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;

namespace AISTN.ExternalAppAPI.Services
{
    [Injectable]
    public class TemplateService : ServiceBase
    {
        private readonly IGenericRepository<Template> _templateRepository;
        private readonly IGenericRepository<AdminTemplate> _adminTemplateRepository;
        private readonly IGenericRepository<Blob> _blobRepository;
        private readonly IGenericRepository<DocumentContent> _documentContentRepository;
        private readonly IGenericRepository<Syndic> _syndicRepository;
        private readonly Guid? _userId;

        public TemplateService(IMapper mapper, ExceptionLogger logger,
                               ExcelGenerator excelGenerator,
                               IGenericRepository<Template> templateRepository, 
                               IGenericRepository<AdminTemplate> adminTemplateRepository,
                               IGenericRepository<Blob> blobRepository,
                               IGenericRepository<DocumentContent> documentContentRepository,
                               IGenericRepository<Syndic> syndicRepository,
                               UserService userService,
                               IHttpContextAccessor contextAccessor)
            : base(mapper, logger, excelGenerator)
        {
            SetCurrentUser(userService.GetCurrentUser(contextAccessor.HttpContext!).ResultData);
            _templateRepository = templateRepository;
            _adminTemplateRepository = adminTemplateRepository;
            _blobRepository = blobRepository;
            _documentContentRepository = documentContentRepository;
            _syndicRepository = syndicRepository;
            _userId = _currentUser.UserId;
        }

        public OperationResult<PagedList<TemplateIndexDTO>> GetAllTemplates(int pageNumber, int pageSize)
        {
            try
            {
                var templates = _templateRepository.Get(x => x.Syndic.UserId == _userId.Value, include: src => src.Include(x => x.TemplateKind)
                                                                              .Include(x => x.DocumentCollection)
                                                                                .ThenInclude(x => x.DocumentContents))
                                                                              .AsQueryable();

                return Success(PagedList<TemplateIndexDTO>.ToPagedList(templates.ProjectTo<TemplateIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<TemplateIndexDTO>>(ex);
            }
        }

        public OperationResult<PagedList<AdminTemplateIndexDTO>> GetAllAdminTemplates(int pageNumber, int pageSize)
        {
            try
            {
                var adminTemplates = _adminTemplateRepository.GetAll(include: src => src.Include(x => x.TemplateKind)
                                                                                        .Include(x => x.DocumentCollection)
                                                                                            .ThenInclude(x => x.DocumentContents))
                                                                                        .AsQueryable();

                return Success(PagedList<AdminTemplateIndexDTO>.ToPagedList(adminTemplates.ProjectTo<AdminTemplateIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {

                _logger.LogException(ex);
                return Exception<PagedList<AdminTemplateIndexDTO>>(ex);
            }
        }

        public OperationResult<Guid> CreateTemplate(SaveTemplateDTO templateDTO)
        {
            try
            {
                var syndic = _syndicRepository.Get(x => x.UserId == _userId).FirstOrDefault();
                if (syndic == null)
                {
                    return Exception<Guid>(new Exception("Няма намерен синдик."));
                }

                templateDTO.SyndicId = syndic.Id;


                var templateEntity = _mapper.Map<Template>(templateDTO);

                _templateRepository.Add(templateEntity);
                _templateRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.CreateSyndicTemplate));

                return Success(templateEntity.Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }

        public OperationResult<SaveTemplateDTO> UpdateTemplate(SaveTemplateDTO templateDTO)
        {
            try
            {
                var templateEntity = _templateRepository.GetById(templateDTO.Id.Value);

                if (templateEntity == null)
                {
                    return Exception<SaveTemplateDTO>(new Exception("Няма намерен образец."));
                }

                templateEntity = _mapper.Map(templateDTO, templateEntity);
                _templateRepository.Update(templateEntity);
                _templateRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.UpdateSyndicTemplate));

                return Success(_mapper.Map<SaveTemplateDTO>(templateEntity));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SaveTemplateDTO>(ex);
            }
        }

        public OperationResult<SaveTemplateDTO> GetTemplateById(Guid id)
        {
            try {
                var templateEntity = _templateRepository.GetById(id, src => src.Include(x => x.TemplateKind));
                return Success(_mapper.Map<SaveTemplateDTO>(templateEntity));
            } 
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SaveTemplateDTO>(ex);
            }
        }

        public TemplateDownloadModel GenerateAdminTemplate(Guid templateId)
        {
            var adminTemplate = _adminTemplateRepository.GetById(templateId);
            var documentContent = _documentContentRepository.Get(x => x.DocumentCollectionId == adminTemplate.DocumentCollectionId).FirstOrDefault();
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
                    FileName = adminTemplate.TemplateName + DateTime.Now.ToShortDateString() + ".docx",
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
