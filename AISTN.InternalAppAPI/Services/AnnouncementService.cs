using AISTN.Common.Helper;
using AISTN.Common.Models.PageResult;
using AISTN.Data.DataModel;
using AISTN.InternalAppAPI.Models.Details;
using AISTN.InternalAppAPI.Models.Filter;
using AISTN.InternalAppAPI.Models.Index;
using AISTN.Repository;
using AISTN.Repository.Attributes;
using AISTN.Repository.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Object = AISTN.Data.DataModel.Object;

namespace AISTN.InternalAppAPI.Services
{
    [Injectable]
    public class AnnouncementService : ServiceBase
    {
        private readonly IGenericRepository<Announcement> _announcementRepository;
        private readonly IGenericRepository<Object> _objectRepository;
        public AnnouncementService(IMapper mapper,
                             ExceptionLogger logger,
                             IGenericRepository<Announcement> announcementRepository,
                             IGenericRepository<Object> objectReporsitory,
                             ExcelGenerator excelGenerator)
            : base(mapper, logger, excelGenerator)
        {
            _announcementRepository = announcementRepository;
            _objectRepository = objectReporsitory;
        }

        public OperationResult<DetailsAnnouncementDTO> GetAnnouncementById(Guid id)
        {
            try
            {
                var announcement = _announcementRepository.GetById(id, include: source => source.Include(x => x.Case)
                                                                                                  .ThenInclude(x => x.Court)
                                                                                               .Include(x => x.Case)
                                                                                                  .ThenInclude(x => x.Sides.Where(x => x.IsDebtor == true))
                                                                                                       .ThenInclude(x => x.Entity)
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
            return _announcementRepository.Get(filter: x => ((filter.CaseNumber == null) || x.Case.Number.Contains(filter.CaseNumber))
                                                     && ((filter.CaseYear == null) || x.Case.Year == filter.CaseYear)
                                                     && ((filter.CourtNumber == null) || x.Case.Court.Number == filter.CourtNumber)
                                                     && (filter.DebtorName == null || x.Case.Sides.Any(x => x.IsDebtor == true && x.Entity.Name.Contains(filter.DebtorName)))
                                                     && (filter.DebtorIdentifier == null || x.Case.Sides.Any(x => x.IsDebtor == true && x.Entity.Bulstat.Contains(filter.DebtorIdentifier)))
                                                     && ((filter.SyndicFirstName == null) || x.Syndic.FirstName.Contains(filter.SyndicFirstName))
                                                     && ((filter.SyndicLastName == null) || x.Syndic.LastName.Contains(filter.SyndicLastName))
                                                     && ((filter.FromDate == null) || x.OfferingDate >= filter.FromDate)
                                                     && ((filter.ToDate == null) || x.OfferingDate <= filter.ToDate)
                                                     && x.OfferingDate >= currentTime
                                                     && (x.StatusCode == 2 || x.StatusCode == 6),
                              include: source => source.Include(x => x.Case)
                                                           .ThenInclude(x => x.Court)
                                                       .Include(x => x.Case)
                                                           .ThenInclude(x => x.Sides.Where(x => x.IsDebtor == true))
                                                               .ThenInclude(x => x.Entity)
                                                       .Include(x => x.Syndic).ThenInclude(x => x.Syndic2Addresses).ThenInclude(x => x.Address)
                                                       .Include(x => x.OfferingKind)
                                                       .Include(x => x.OfferingProcedure)
                                                       .Include(x => x.LocationType)
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
            return _announcementRepository.Get(filter: x => ((filter.CaseNumber == null) || x.Case.Number.Contains(filter.CaseNumber))
                                                      && ((filter.CaseYear == null) || x.Case.Year == filter.CaseYear)
                                                      && ((filter.CourtNumber == null) || x.Case.Court.Number == filter.CourtNumber)
                                                      && (filter.DebtorName == null || x.Case.Sides.Any(x => x.IsDebtor == true && x.Entity.Name.Contains(filter.DebtorName)))
                                                      && (filter.DebtorIdentifier == null || x.Case.Sides.Any(x => x.IsDebtor == true && x.Entity.Bulstat.Contains(filter.DebtorIdentifier)))
                                                      && ((filter.SyndicFirstName == null) || x.Syndic.FirstName.Contains(filter.SyndicFirstName))
                                                      && ((filter.SyndicLastName == null) || x.Syndic.LastName.Contains(filter.SyndicLastName))
                                                      && ((filter.FromDate == null) || x.OfferingDate >= filter.FromDate)
                                                      && ((filter.ToDate == null) || x.OfferingDate <= filter.ToDate)
                                                      && x.OfferingDate < currentTime
                                                      && (x.StatusCode == 2 || x.StatusCode == 6),
                               include: source => source.Include(x => x.Case)
                                                            .ThenInclude(x => x.Court)
                                                        .Include(x => x.Case)
                                                            .ThenInclude(x => x.Sides.Where(x => x.IsDebtor == true))
                                                                .ThenInclude(x => x.Entity)
                                                        .Include(x => x.Syndic).ThenInclude(x => x.Syndic2Addresses).ThenInclude(x => x.Address)
                                                        .Include(x => x.OfferingKind)
                                                        .Include(x => x.OfferingProcedure)
                                                        .Include(x => x.LocationType)
                                                        .Include(x => x.Address)
                                                            .ThenInclude(x => x.Region)
                                                        .Include(x => x.Address)
                                                            .ThenInclude(x => x.Settlement)
                                                        .Include(x => x.Address)
                                                            .ThenInclude(x => x.Municipality))
                                                      .AsQueryable().OrderBy(x => x.OfferingDate);
        }

        public OperationResult<DetailsObjectDTO> GetObjectById(Guid id)
        {
            try
            {
                var objectEntity = _objectRepository.GetById(id, include: source => source.Include(x => x.ObjectKind)
                                                                                          .Include(x => x.ObjectType)
                                                                                          .Include(x => x.DocumentCollection)
                                                                                          .ThenInclude(x => x.DocumentContents)
                                                                                          .ThenInclude(x => x.AttachedDocumentKind)
                                                                                          .Include(x => x.DocumentCollection)
                                                                                          .ThenInclude(x => x.DocumentContents)
                                                                                          .ThenInclude(x => x.Blob)
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
    }
}
