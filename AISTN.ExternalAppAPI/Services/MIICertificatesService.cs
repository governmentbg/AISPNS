using AISTN.Common.Helper;
using AISTN.Common.Models.PageResult;
using AISTN.Data.DataModel;
using AISTN.ExternalAppAPI.Models;
using AISTN.ExternalAppAPI.Models.Index;
using AISTN.Repository;
using AISTN.Repository.Attributes;
using AISTN.Repository.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using NPOI.XWPF.UserModel;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using AISTN.ExternalAppAPI.Helper;
using AISTN.ExternalAppAPI.Models.Save;

namespace AISTN.ExternalAppAPI.Services
{
    [Injectable]
    public class MIICertificatesService : ServiceBase
    {
        private readonly IGenericRepository<Announcement> _announcementRepository;
        private readonly IGenericRepository<TemplateDocument> _templateDocumentRepository;
        private readonly IGenericRepository<Blob> _blobRepository;

        public MIICertificatesService(IMapper mapper,
                                      ExceptionLogger logger,
                                      IGenericRepository<Announcement> announcementRepository,
                                      IGenericRepository<TemplateDocument> templateDocumentRepository,
                                      IGenericRepository<Blob> blobRepository,
                                      ExcelGenerator excelGenerator)
                        : base(mapper, logger, excelGenerator)
        {
            _announcementRepository = announcementRepository;
            _templateDocumentRepository = templateDocumentRepository;
            _blobRepository = blobRepository;
        }

        public OperationResult<PagedList<CertificateIndexDTO>> GetAnnouncementCertificates(int pageNumber, int pageSize)
        {
            try
            {
                DateTime currentTime = DateTime.Now;
                var query = GetAnnouncementCertificatesQuery(currentTime);
                return Success(PagedList<CertificateIndexDTO>.ToPagedList(query.ProjectTo<CertificateIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<CertificateIndexDTO>>(ex);
            }
        }


        public IQueryable<Announcement> GetAnnouncementCertificatesQuery(DateTime currentTime)
        {
            return _announcementRepository.Get(filter: x => x.OfferingDate >= currentTime
                                                             && x.StatusCode == 2,
                               include: source => source.Include(x => x.Case)
                                                            .ThenInclude(x => x.Court)
                                                        .Include(x => x.Case)
                                                            .ThenInclude(x => x.Sides.Where(x => x.IsDebtor == true))
                                                                .ThenInclude(x => x.Entity)
                                                        .Include(x => x.Syndic))
                                                        .AsQueryable().OrderBy(x => x.OfferingDate);
        }

        public OperationResult<PagedList<CertificateIndexDTO>> GetFinishedAnnouncementCertificates(int pageNumber, int pageSize)
        {
            try
            {
                DateTime currentTime = DateTime.Now;
                var query = GetFinishedAnnouncementCertificatesQuery(currentTime);
                return Success(PagedList<CertificateIndexDTO>.ToPagedList(query.ProjectTo<CertificateIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<CertificateIndexDTO>>(ex);
            }
        }


        public IQueryable<Announcement> GetFinishedAnnouncementCertificatesQuery(DateTime currentTime)
        {
            return _announcementRepository.Get(filter: x => x.OfferingDate < currentTime
                                                             && x.StatusCode == 2,
                               include: source => source.Include(x => x.Case)
                                                            .ThenInclude(x => x.Court)
                                                        .Include(x => x.Case)
                                                            .ThenInclude(x => x.Sides.Where(x => x.IsDebtor == true))
                                                                .ThenInclude(x => x.Entity)
                                                        .Include(x => x.Syndic))
                                                        .AsQueryable().OrderBy(x => x.OfferingDate);
        }

        public TemplateDownloadModel GenerateCertificateForAnnouncement(Guid announcementId)
        {
            var templateDocument = _templateDocumentRepository.Get(x => x.Type == TemplateDocumentType.Certificate.ToString(), 
                                                                                                        src => src.Include(x => x.DocumentCollection)
                                                                                                                    .ThenInclude(x => x.DocumentContents)
                                                                                                                    .ThenInclude(x => x.Blob))
                                                                                                                       .FirstOrDefault();
            OperationResult<DetailsCertificateDTO> result = GetAnnouncementById(announcementId);

            DetailsCertificateDTO announcement = result.ResultData;

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

                    body.InnerXml = body.InnerXml.Replace(CertificateTemplateModel.DateCreated, announcement.DateCreated);
                    body.InnerXml = body.InnerXml.Replace(CertificateTemplateModel.TimeCreated, announcement.TimeCreated);
                    body.InnerXml = body.InnerXml.Replace(CertificateTemplateModel.AnnouncementNumber, announcement.AnnouncementNumber);
                    body.InnerXml = body.InnerXml.Replace(CertificateTemplateModel.OfferingDate, announcement.OfferingDate);
                    body.InnerXml = body.InnerXml.Replace(CertificateTemplateModel.Address, announcement.Address);
                    body.InnerXml = body.InnerXml.Replace(CertificateTemplateModel.SyndicName, announcement.SyndicName);
                    body.InnerXml = body.InnerXml.Replace(CertificateTemplateModel.CaseNumber, announcement.CaseNumber);
                    body.InnerXml = body.InnerXml.Replace(CertificateTemplateModel.CaseYear, announcement.CaseYear);
                    body.InnerXml = body.InnerXml.Replace(CertificateTemplateModel.CourtName, announcement.CourtName);
                    body.InnerXml = body.InnerXml.Replace(CertificateTemplateModel.DebtorName, announcement.DebtorName);
                    body.InnerXml = body.InnerXml.Replace(CertificateTemplateModel.SalesProcedure, announcement.SalesProcedure);
                }

                return new TemplateDownloadModel()
                {
                    FileName = "Удостоверение за продажба - " + DateTime.Now.ToShortDateString() + ".docx",
                    MimeType = templateDocument.DocumentCollection.DocumentContents.FirstOrDefault().ContentMimeType,
                    BlobContent = mem.ToArray()
                };
            }
        }

        public OperationResult<DetailsCertificateDTO> GetAnnouncementById(Guid id)
        {
            try
            {
                var announcement = _announcementRepository.GetById(id, include: source => source.Include(x => x.Case)
                                                                                                   .ThenInclude(x => x.Court)
                                                                                                .Include(x => x.Case)
                                                                                                   .ThenInclude(x => x.Sides.Where(x => x.IsDebtor == true))
                                                                                                        .ThenInclude(x => x.Entity)
                                                                                                .Include(x => x.Syndic)
                                                                                                    .ThenInclude(x => x.Syndic2Addresses)
                                                                                                    .ThenInclude(x => x.Address)
                                                                                                .Include(x => x.OfferingKind)
                                                                                                .Include(x => x.StatusCodeNavigation)
                                                                                                .Include(x => x.OfferingProcedure)
                                                                                                .Include(x => x.LocationType)
                                                                                                .Include(x => x.DocumentCollection).ThenInclude(x => x.DocumentContents).ThenInclude(x => x.AttachedDocumentKind)
                                                                                                .Include(x => x.DocumentCollection).ThenInclude(x => x.DocumentContents).ThenInclude(x => x.Blob)
                                                                                                .Include(x => x.Address)
                                                                                                    .ThenInclude(x => x.Region)
                                                                                                 .Include(x => x.Address)
                                                                                                    .ThenInclude(x => x.Settlement)
                                                                                                 .Include(x => x.Address)
                                                                                                    .ThenInclude(x => x.Municipality));
                return OperationResult<DetailsCertificateDTO>.Success(_mapper.Map<DetailsCertificateDTO>(announcement));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return OperationResult<DetailsCertificateDTO>.Exception(ex);
            }
        }

    }
}
