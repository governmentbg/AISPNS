using AISTN.Common.Helper;
using AISTN.Common.Models.PageResult;
using AISTN.Common.Services;
using AISTN.Data.DataModel;
using AISTN.InternalAppAPI.Helper;
using AISTN.InternalAppAPI.Models;
using AISTN.InternalAppAPI.Models.Details;
using AISTN.InternalAppAPI.Models.Filter;
using AISTN.InternalAppAPI.Models.Index;
using AISTN.InternalAppAPI.Models.Save;
using AISTN.Repository;
using AISTN.Repository.Attributes;
using AISTN.Repository.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using NPOI.XWPF.UserModel;

namespace AISTN.InternalAppAPI.Services
{
    [Injectable]
    public class InspectionService : ServiceBase
    {
        private readonly IGenericRepository<Inspection> _inspectionRepository;
        private readonly IGenericRepository<TemplateDocument> _templateDocumentRepository;
        private readonly IGenericRepository<Blob> _blobRepository;
        public InspectionService(IMapper mapper,
                             ExceptionLogger logger,
                             IGenericRepository<Inspection> inspectionRepository,
                             IGenericRepository<TemplateDocument> templateDocumentRepository,
                             IGenericRepository<Blob> blobRepository,
                             UserService userService,
                             IHttpContextAccessor contextAccessor,
                             ExcelGenerator excelGenerator)
            : base(mapper, logger, excelGenerator)
        {
            SetCurrentUser(userService.GetCurrentUser(contextAccessor.HttpContext!).ResultData);
            _inspectionRepository = inspectionRepository;
            _templateDocumentRepository = templateDocumentRepository;
            _blobRepository = blobRepository;
        }

        public OperationResult<SaveInspectionDTO> GetInspectionById(Guid id)
        {
            try
            {
                var inspection = _inspectionRepository.GetById(id);
                return Success(_mapper.Map<SaveInspectionDTO>(inspection));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SaveInspectionDTO>(ex);
            }
        }

        public OperationResult<PagedList<InspectionIndexDTO>> SearchCurrentInspection(int pageNumber, int pageSize, InspectionSearchFilter filter)
        {
            try
            {
                DateTime currentTime = DateTime.Now;
                var query = SearchCurrentInspectionQuery(filter, currentTime);
                return Success(PagedList<InspectionIndexDTO>.ToPagedList(query.ProjectTo<InspectionIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<InspectionIndexDTO>>(ex);
            }
        }

        private IQueryable<Inspection> SearchCurrentInspectionQuery(InspectionSearchFilter filter, DateTime currentTime)
        {
            return _inspectionRepository.Get(filter: x => ((filter.SyndicID == null) || x.SyndicId == filter.SyndicID)
                                                            && ((filter.InspectionTypeID == null) || x.InspectionTypeId == filter.InspectionTypeID)
                                                            && ((filter.InspectorName == null) || x.InspectorName == filter.InspectorName)
                                                            && ((filter.FromDate == null) || x.InspectionDate >= filter.FromDate)
                                                            && ((filter.ToDate == null) || x.InspectionDate >= filter.ToDate))
                                                            .AsQueryable().OrderBy(x => x.InspectionDate)
                                                            .Where(x => x.InspectionDate >= currentTime);
        }

        public OperationResult<PagedList<InspectionIndexDTO>> SearchFinishedInspection(int pageNumber, int pageSize, InspectionSearchFilter filter)
        {
            try
            {
                DateTime currentTime = DateTime.Now;
                var query = SearchFinishedInspectionQuery(filter, currentTime);
                return Success(PagedList<InspectionIndexDTO>.ToPagedList(query.ProjectTo<InspectionIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<InspectionIndexDTO>>(ex);
            }
        }

        private IQueryable<Inspection> SearchFinishedInspectionQuery(InspectionSearchFilter filter, DateTime currentTime)
        {
            return _inspectionRepository.Get(filter: x => ((filter.SyndicID == null) || x.SyndicId == filter.SyndicID)
                                                            && ((filter.InspectionTypeID == null) || x.InspectionTypeId == filter.InspectionTypeID)
                                                            && ((filter.InspectorName == null) || x.InspectorName == filter.InspectorName)
                                                            && ((filter.FromDate == null) || x.InspectionDate >= filter.FromDate)
                                                            && ((filter.ToDate == null) || x.InspectionDate >= filter.ToDate))
                                                            .AsQueryable().OrderBy(x => x.InspectionDate)
                                                            .Where(x => x.InspectionDate < currentTime);
        }

        public OperationResult<Guid> CreateInspection(SaveInspectionDTO inspectionDTO)
        {
            try
            {
                var inspectionEntity = _mapper.Map<Inspection>(inspectionDTO);
                _inspectionRepository.Add(inspectionEntity);
                _inspectionRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.CreateInspection));

                return Success(inspectionEntity.Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }

        public OperationResult<SaveInspectionDTO> UpdateInspection(SaveInspectionDTO inspectionDTO)
        {
            try
            {
                var inspectionEntity = _inspectionRepository.GetById(inspectionDTO.Id.GetValueOrDefault());
                inspectionEntity = _mapper.Map(inspectionDTO, inspectionEntity);

                if (inspectionEntity == null)
                {
                    return Exception<SaveInspectionDTO>(new Exception("Няма намерена инспекция."));
                }

                _inspectionRepository.Update(inspectionEntity);
                _inspectionRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.UpdateInspection));

                return Success(_mapper.Map<SaveInspectionDTO>(inspectionEntity));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SaveInspectionDTO>(ex);
            }
        }

        public SampleReportDownloadModel GenerateSampleReportForInspection(Guid inspectionId)
        {
            var templateDocument = _templateDocumentRepository.Get(x => x.Type == TemplateDocumentType.Inspection.ToString(),
                                                                                                    source => source.Include(x => x.DocumentCollection)
                                                                                                                       .ThenInclude(x => x.DocumentContents)
                                                                                                                       .ThenInclude(x => x.Blob))
                                                                                                                           .FirstOrDefault();
            var inspection = _inspectionRepository.GetById(inspectionId, include: source => source.Include(x => x.Syndic)
                                                                                                  .Include(x => x.InspectionType));

            string syndicName = String.Join(" ", inspection.Syndic.FirstName, inspection.Syndic.SecondName, inspection.Syndic.LastName);

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

                    body.InnerXml = body.InnerXml.Replace(SampleReportTemplateModel.InspectionDate, inspection.InspectionDate.ToString("dd-MM-yyyy HH:mm"));
                    body.InnerXml = body.InnerXml.Replace(SampleReportTemplateModel.SyndicName, syndicName);
                    body.InnerXml = body.InnerXml.Replace(SampleReportTemplateModel.InspectionType, inspection.InspectionType.Description);
                    body.InnerXml = body.InnerXml.Replace(SampleReportTemplateModel.InspectorName, inspection.InspectorName);
                    body.InnerXml = body.InnerXml.Replace(SampleReportTemplateModel.InspectionOrderDate, inspection.InspectionOrderDate?.ToString("dd-MM-yyyy HH:mm") ?? string.Empty);
                    body.InnerXml = body.InnerXml.Replace(SampleReportTemplateModel.InspectionOrderNumber, inspection.InspectionOrderNumber);
                    body.InnerXml = body.InnerXml.Replace(SampleReportTemplateModel.IsCompleted, inspection.IsCompleted ? "ДА" : "НЕ");
                    body.InnerXml = body.InnerXml.Replace(SampleReportTemplateModel.FinishDate, inspection.CompletionTime?.ToString("dd-MM-yyyy HH:mm") ?? string.Empty);
                }

                return new SampleReportDownloadModel()
                {
                    FileName = "Доклад за инспекция - " + DateTime.Now.ToShortDateString() + ".docx",
                    MimeType = templateDocument.DocumentCollection.DocumentContents.FirstOrDefault().ContentMimeType,
                    BlobContent = mem.ToArray()
                };
            }


        }
    }
}
