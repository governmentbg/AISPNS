using AISTN.Common.Helper;
using AISTN.Common.Models.PageResult;
using AISTN.Common.Services;
using AISTN.Data.DataModel;
using AISTN.PublicAppAPI.Models.Details;
using AISTN.PublicAppAPI.Models.Filters;
using AISTN.PublicAppAPI.Models.Index;
using AISTN.Repository;
using AISTN.Repository.Attributes;
using AISTN.Repository.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NPOI.HSSF.Record.AutoFilter;
using Object = AISTN.Data.DataModel.Object;


namespace AISTN.PublicAppAPI.Services
{
    [Injectable]
    public class AnnouncementService : ServiceBase
    {
        private readonly IGenericRepository<Announcement> _announcementRepository;
        private readonly IGenericRepository<Object> _objectRepository;
        public AnnouncementService(IMapper mapper,
                             ExceptionLogger logger,
                              UserService userService,
                              IHttpContextAccessor contextAccessor,
                             IGenericRepository<Announcement> announcementRepository,
                             IGenericRepository<Object> objectRepository,
                             ExcelGenerator excelGenerator)
            : base(mapper, logger, excelGenerator)
        {
            SetCurrentUser(userService.GetCurrentUser(contextAccessor.HttpContext!).ResultData);
            _announcementRepository = announcementRepository;
            _objectRepository = objectRepository;
        }

        public OperationResult<DetailsAnnouncementDTO> GetAnnouncementById(Guid id)
        {
            try
            {
                var announcement = _announcementRepository.GetById(id, include: source => source.Include(x => x.Court)
                                                                                               .Include(x => x.Syndic).ThenInclude(x => x.SpecialtyNavigation)
                                                                                               .Include(x => x.Syndic).ThenInclude(x => x.Syndic2Addresses).ThenInclude(x => x.Address).ThenInclude(x => x.Municipality)
                                                                                               .Include(x => x.Syndic).ThenInclude(x => x.Syndic2Addresses).ThenInclude(x => x.Address).ThenInclude(x => x.Region)
                                                                                               .Include(x => x.Syndic).ThenInclude(x => x.Syndic2Addresses).ThenInclude(x => x.Address).ThenInclude(x => x.Settlement)
                                                                                               .Include(x => x.SecondSyndic).ThenInclude(x => x.SpecialtyNavigation)
                                                                                               .Include(x => x.SecondSyndic).ThenInclude(x => x.Syndic2Addresses).ThenInclude(x => x.Address).ThenInclude(x => x.Municipality)
                                                                                               .Include(x => x.SecondSyndic).ThenInclude(x => x.Syndic2Addresses).ThenInclude(x => x.Address).ThenInclude(x => x.Region)
                                                                                               .Include(x => x.SecondSyndic).ThenInclude(x => x.Syndic2Addresses).ThenInclude(x => x.Address).ThenInclude(x => x.Settlement)
                                                                                               .Include(x => x.OfferingKind)
                                                                                               .Include(x => x.OfferingProcedure)
                                                                                               .Include(x => x.LocationType)
                                                                                               .Include(x => x.StatusCodeNavigation)
                                                                                               .Include(x => x.Objects).ThenInclude(x => x.Address).ThenInclude(x => x.Municipality)
                                                                                               .Include(x => x.Objects).ThenInclude(x => x.Address).ThenInclude(x => x.Region)
                                                                                               .Include(x => x.Objects).ThenInclude(x => x.Address).ThenInclude(x => x.Settlement)
                                                                                               .Include(x => x.Objects).ThenInclude(x => x.ObjectType)
                                                                                               .Include(x => x.Objects).ThenInclude(x => x.ObjectKind)
                                                                                               .Include(x => x.DocumentCollection).ThenInclude(x => x.DocumentContents).ThenInclude(x => x.AttachedDocumentKind)
                                                                                               .Include(x => x.DocumentCollection).ThenInclude(x => x.DocumentContents).ThenInclude(x => x.Blob)
                                                                                               .Include(x => x.Address)
                                                                                                   .ThenInclude(x => x.Region)
                                                                                                .Include(x => x.Address)
                                                                                                   .ThenInclude(x => x.Settlement)
                                                                                                .Include(x => x.Address)
                                                                                                   .ThenInclude(x => x.Municipality)
                                                                                                .Include(x => x.PapersForSale)
                                                                                                .Include(x => x.SalesProcedure));

                return Success(_mapper.Map<DetailsAnnouncementDTO>(announcement));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<DetailsAnnouncementDTO>(ex);
            }
        }


        #region Search
        public OperationResult<PagedList<AnnouncementIndexDTO>> SearchCurrentAnnouncement(int pageNumber, int pageSize, AnnouncementSearchFilter filter)
        {
            try
            {
                DateTime currentTime = DateTime.Now;
                var query = SearchCurrentAnnouncementQuery(filter, currentTime);
                return Success(PagedList<AnnouncementIndexDTO>.ToPagedList(query.ProjectTo<AnnouncementIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<AnnouncementIndexDTO>>(ex);
            }
        }

        public IQueryable<Announcement> SearchCurrentAnnouncementQuery(AnnouncementSearchFilter filter, DateTime currentTime)
        {
            return _announcementRepository.Get(filter: x => ((filter.CaseNumber == null) || x.CaseNumber.Contains(filter.CaseNumber))
                                                     && ((filter.CaseYear == null) || x.CaseYear == filter.CaseYear)
                                                     && ((filter.CourtNumber == null) || x.Court.Number == filter.CourtNumber)
                                                     && (filter.DebtorName == null || x.DebtorName.Contains(filter.DebtorName))
                                                     && (filter.DebtorIdentifier == null || x.DebtorEik.Contains(filter.DebtorIdentifier))
                                                     && ((filter.SyndicFirstName == null) || x.Syndic.FirstName.Contains(filter.SyndicFirstName))
                                                     && ((filter.SyndicLastName == null) || x.Syndic.LastName.Contains(filter.SyndicLastName))
                                                     && ((filter.FromDate == null) || x.OfferingDate >= filter.FromDate)
                                                     && ((filter.ToDate == null) || x.OfferingDate <= filter.ToDate)
                                                     && ((filter.City == null) || x.Address.Settlement.Name.Contains(filter.City))
                                                     && ((filter.ObjectKindId == null) || x.Objects.Any(x => x.ObjectKindId == filter.ObjectKindId))
                                                     && ((filter.Text == null) || x.Text.Contains(filter.Text))
                                                     && x.OfferingDate >= currentTime
                                                     && (x.StatusCode == 2 || x.StatusCode == 6),
                              include: source => source.Include(x => x.Court)
                                                       .Include(x => x.Syndic)
                                                       .Include(x => x.StatusCodeNavigation)
                                                       .Include(x => x.Address)
                                                           .ThenInclude(x => x.Region)
                                                       .Include(x => x.Address)
                                                           .ThenInclude(x => x.Settlement)
                                                       .Include(x => x.Address)
                                                           .ThenInclude(x => x.Municipality)).AsQueryable().OrderBy(x => x.OfferingDate);
        }

        public OperationResult<PagedList<AnnouncementIndexDTO>> SearchFinishedAnnouncement(int pageNumber, int pageSize, AnnouncementSearchFilter filter)
        {
            try
            {
                DateTime currentTime = DateTime.Now;
                var query = SearchFinishedAnnouncementQuery(filter, currentTime);
                return Success(PagedList<AnnouncementIndexDTO>.ToPagedList(query.ProjectTo<AnnouncementIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<AnnouncementIndexDTO>>(ex);
            }
        }

        public IQueryable<Announcement> SearchFinishedAnnouncementQuery(AnnouncementSearchFilter filter, DateTime currentTime)
        {
            return _announcementRepository.Get(filter: x => ((filter.CaseNumber == null) || x.CaseNumber.Contains(filter.CaseNumber))
                                                      && ((filter.CaseYear == null) || x.CaseYear == filter.CaseYear)
                                                      && ((filter.CourtNumber == null) || x.Court.Number == filter.CourtNumber)
                                                      && (filter.DebtorName == null || x.DebtorName.Contains(filter.DebtorName))
                                                      && (filter.DebtorIdentifier == null || x.DebtorEik.Contains(filter.DebtorIdentifier))
                                                      && ((filter.SyndicFirstName == null) || x.Syndic.FirstName.Contains(filter.SyndicFirstName))
                                                      && ((filter.SyndicLastName == null) || x.Syndic.LastName.Contains(filter.SyndicLastName))
                                                      && ((filter.FromDate == null) || x.OfferingDate >= filter.FromDate)
                                                      && ((filter.ToDate == null) || x.OfferingDate <= filter.ToDate)
                                                      && ((filter.City == null) || x.Address.Settlement.Name.Contains(filter.City))
                                                      && ((filter.ObjectKindId == null) || x.Objects.Any(x => x.ObjectKindId == filter.ObjectKindId))
                                                      && ((filter.Text == null) || x.Text.Contains(filter.Text))
                                                      && x.OfferingDate < currentTime
                                                      && (x.StatusCode == 2 || x.StatusCode == 6),
                               include: source => source.Include(x => x.Court)
                                                        .Include(x => x.Syndic)
                                                        .Include(x => x.StatusCodeNavigation)
                                                        .Include(x => x.Address)
                                                            .ThenInclude(x => x.Region)
                                                        .Include(x => x.Address)
                                                            .ThenInclude(x => x.Settlement)
                                                        .Include(x => x.Address)
                                                            .ThenInclude(x => x.Municipality))
                                                      .AsQueryable().OrderBy(x => x.OfferingDate);
        }


        #endregion

        #region Object

        public OperationResult<DetailsObjectDTO> GetObjectById(Guid id)
        {
            try
            {
                var objectEntity = _objectRepository.GetById(id, include: source => source.Include(x => x.ObjectKind)
                                                                                                .Include(x => x.ObjectType)
                                                                                                .Include(x => x.DocumentCollection).ThenInclude(x => x.DocumentContents).ThenInclude(x => x.AttachedDocumentKind)
                                                                                                .Include(x => x.DocumentCollection).ThenInclude(x => x.DocumentContents).ThenInclude(x => x.Blob)
                                                                                                .Include(x => x.Address)
                                                                                                    .ThenInclude(x => x.Region)
                                                                                                .Include(x => x.Address)
                                                                                                    .ThenInclude(x => x.Settlement)
                                                                                                .Include(x => x.Address)
                                                                                                    .ThenInclude(x => x.Municipality));
                return Success(_mapper.Map<DetailsObjectDTO>(objectEntity));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<DetailsObjectDTO>(ex);
            }
        }


        #endregion
    }
}
