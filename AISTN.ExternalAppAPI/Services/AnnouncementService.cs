using AISTN.Common.Helper;
using AISTN.Common.Models.PageResult;
using AISTN.Common.Models.Export;
using AISTN.Common.Services;
using AISTN.Data.DataModel;
using AISTN.ExternalAppAPI.Models.Filter;
using AISTN.ExternalAppAPI.Models.Index;
using AISTN.ExternalAppAPI.Models.Save;
using AISTN.Repository;
using AISTN.Repository.Attributes;
using AISTN.Repository.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using Object = AISTN.Data.DataModel.Object;
using AISTN.ExternalAppAPI.Models.Export;
using AISTN.ExternalAppAPI.Models.Authentication;

namespace AISTN.ExternalAppAPI.Services
{
    [Injectable]
    public class AnnouncementService : ServiceBase
    {
        private readonly IGenericRepository<Announcement> _announcementRepository;
        private readonly IGenericRepository<Object> _objectRepository;
        private readonly IConfiguration _configuration;
        public AnnouncementService(IMapper mapper,
                             ExceptionLogger logger,
                              UserService userService,
                              IHttpContextAccessor contextAccessor,
                             IGenericRepository<Announcement> announcementRepository,
                             IGenericRepository<Object> objectReporsitory,
                             ExcelGenerator excelGenerator,
                             IConfiguration configuration)
            : base(mapper, logger, excelGenerator)
        {
            SetCurrentUser(userService.GetCurrentUser(contextAccessor.HttpContext!).ResultData);
            _announcementRepository = announcementRepository;
            _objectRepository = objectReporsitory;
            _configuration = configuration;
        }

        public OperationResult<SaveAnnouncementDTO> GetAnnouncementById(Guid id)
        {
            try
            {
                var announcement = _announcementRepository.GetById(id, include: source => source.Include(x => x.Case)
                                                                                                   .ThenInclude(x => x.Court)
                                                                                                .Include(x => x.Case)
                                                                                                   .ThenInclude(x => x.Sides.Where(x => x.IsDebtor == true))
                                                                                                        .ThenInclude(x => x.Entity)
                                                                                                .Include(x => x.Court)
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
                return Success(_mapper.Map<SaveAnnouncementDTO>(announcement));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SaveAnnouncementDTO>(ex);
            }
        }

        #region MII Announcements 

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
                                                        .Include(x => x.Syndic).ThenInclude(x => x.Syndic2Addresses).ThenInclude(x => x.Address)
                                                        .Include(x => x.OfferingKind)
                                                        .Include(x => x.StatusCodeNavigation)
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
                                                        .Include(x => x.Syndic).ThenInclude(x => x.Syndic2Addresses).ThenInclude(x => x.Address)
                                                        .Include(x => x.OfferingKind)
                                                        .Include(x => x.StatusCodeNavigation)
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


        public OperationResult<PagedList<AnnouncementIndexDTO>> SearchDraftAnnouncement(int pageNumber, int pageSize, AnnouncementSearchFilter filter)
        {
            try
            {
                DateTime currentTime = DateTime.Now;
                var query = SearchDraftAnnouncementQuery(filter, currentTime);
                return Success(PagedList<AnnouncementIndexDTO>.ToPagedList(query.ProjectTo<AnnouncementIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<AnnouncementIndexDTO>>(ex);
            }
        }

        public IQueryable<Announcement> SearchDraftAnnouncementQuery(AnnouncementSearchFilter filter, DateTime currentTime)
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
                                                      && x.StatusCode == 1,
                               include: source => source.Include(x => x.Court)
                                                        .Include(x => x.Syndic).ThenInclude(x => x.Syndic2Addresses).ThenInclude(x => x.Address)
                                                        .Include(x => x.OfferingKind)
                                                        .Include(x => x.StatusCodeNavigation)
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

        #endregion

        #region MII Announcements By Syndics

        public OperationResult<PagedList<AnnouncementIndexDTO>> SearchRawAnnouncement(int pageNumber, int pageSize, AnnouncementSearchFilter filter)
        {
            try
            {
                var query = SearchRawAnnouncementQuery(filter);
                return Success(PagedList<AnnouncementIndexDTO>.ToPagedList(query.ProjectTo<AnnouncementIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<AnnouncementIndexDTO>>(ex);
            }
        }

        public IQueryable<Announcement> SearchRawAnnouncementQuery(AnnouncementSearchFilter filter)
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
                                                      && x.StatusCode == 3,
                               include: source => source.Include(x => x.Case)
                                                            .ThenInclude(x => x.Court)
                                                        .Include(x => x.Case)
                                                            .ThenInclude(x => x.Sides.Where(x => x.IsDebtor == true))
                                                                .ThenInclude(x => x.Entity)
                                                        .Include(x => x.Syndic)
                                                        .Include(x => x.OfferingKind)
                                                        .Include(x => x.StatusCodeNavigation)
                                                        .Include(x => x.OfferingProcedure)
                                                        .Include(x => x.LocationType)
                                                        .Include(x => x.Address)
                                                            .ThenInclude(x => x.Region)
                                                        .Include(x => x.Address)
                                                            .ThenInclude(x => x.Settlement)
                                                        .Include(x => x.Address)
                                                            .ThenInclude(x => x.Municipality))
                                                      .AsQueryable().OrderByDescending(x => x.OfferingDate);
        }

        public OperationResult<PagedList<AnnouncementIndexDTO>> SearchAnnouncementOnHold(int pageNumber, int pageSize, AnnouncementSearchFilter filter)
        {
            try
            {
                var query = SearchAnnouncementOnHoldQuery(filter);
                return Success(PagedList<AnnouncementIndexDTO>.ToPagedList(query.ProjectTo<AnnouncementIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<AnnouncementIndexDTO>>(ex);
            }
        }

        public IQueryable<Announcement> SearchAnnouncementOnHoldQuery(AnnouncementSearchFilter filter)
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
                                                      && (x.StatusCode == 4 || x.StatusCode == 5),
                               include: source => source.Include(x => x.Case)
                                                            .ThenInclude(x => x.Court)
                                                        .Include(x => x.Case)
                                                            .ThenInclude(x => x.Sides.Where(x => x.IsDebtor == true))
                                                                .ThenInclude(x => x.Entity)
                                                        .Include(x => x.Syndic)
                                                        .Include(x => x.OfferingKind)
                                                        .Include(x => x.StatusCodeNavigation)
                                                        .Include(x => x.OfferingProcedure)
                                                        .Include(x => x.LocationType)
                                                        .Include(x => x.Address)
                                                            .ThenInclude(x => x.Region)
                                                        .Include(x => x.Address)
                                                            .ThenInclude(x => x.Settlement)
                                                        .Include(x => x.Address)
                                                            .ThenInclude(x => x.Municipality))
                                                      .AsQueryable().OrderByDescending(x => x.OfferingDate);
        }

        public OperationResult<PagedList<AnnouncementIndexDTO>> SearchPublishedAnnouncement(int pageNumber, int pageSize, AnnouncementSearchFilter filter)
        {
            try
            {
                var query = SearchPublishedAnnouncement(filter);
                return Success(PagedList<AnnouncementIndexDTO>.ToPagedList(query.ProjectTo<AnnouncementIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<AnnouncementIndexDTO>>(ex);
            }
        }

        public IQueryable<Announcement> SearchPublishedAnnouncement(AnnouncementSearchFilter filter)
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
                                                      && (x.StatusCode == 2 || x.StatusCode == 6)
                                                      && x.Syndic.UserId == _currentUser.UserId,
                               include: source => source.Include(x => x.Case)
                                                            .ThenInclude(x => x.Court)
                                                        .Include(x => x.Case)
                                                            .ThenInclude(x => x.Sides.Where(x => x.IsDebtor == true))
                                                                .ThenInclude(x => x.Entity)
                                                        .Include(x => x.Syndic)
                                                        .Include(x => x.OfferingKind)
                                                        .Include(x => x.StatusCodeNavigation)
                                                        .Include(x => x.OfferingProcedure)
                                                        .Include(x => x.LocationType)
                                                        .Include(x => x.Address)
                                                            .ThenInclude(x => x.Region)
                                                        .Include(x => x.Address)
                                                            .ThenInclude(x => x.Settlement)
                                                        .Include(x => x.Address)
                                                            .ThenInclude(x => x.Municipality))
                                                      .AsQueryable().OrderByDescending(x => x.OfferingDate);
        }

        #endregion

        public OperationResult<PagedList<AnnouncementIndexDTO>> SearchAnnouncementForCorrection(int pageNumber, int pageSize, AnnouncementSearchFilter filter)
        {
            try
            {
                var query = SearchAnnouncementForCorrection(filter);
                return Success(PagedList<AnnouncementIndexDTO>.ToPagedList(query.ProjectTo<AnnouncementIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<AnnouncementIndexDTO>>(ex);
            }
        }

        public IQueryable<Announcement> SearchAnnouncementForCorrection(AnnouncementSearchFilter filter)
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
                                                      && (x.StatusCode == 5 || x.StatusCode == 3),
                               include: source => source.Include(x => x.Case)
                                                            .ThenInclude(x => x.Court)
                                                        .Include(x => x.Case)
                                                            .ThenInclude(x => x.Sides.Where(x => x.IsDebtor == true))
                                                                .ThenInclude(x => x.Entity)
                                                        .Include(x => x.Court)
                                                        .Include(x => x.Syndic)
                                                        .Include(x => x.OfferingKind)
                                                        .Include(x => x.StatusCodeNavigation)
                                                        .Include(x => x.OfferingProcedure)
                                                        .Include(x => x.LocationType)
                                                        .Include(x => x.Address)
                                                            .ThenInclude(x => x.Region)
                                                        .Include(x => x.Address)
                                                            .ThenInclude(x => x.Settlement)
                                                        .Include(x => x.Address)
                                                            .ThenInclude(x => x.Municipality))
                                                      .AsQueryable().OrderByDescending(x => x.OfferingDate);
        }

        #region Object

        public OperationResult<PagedList<ObjectIndexDTO>> GetObjectsByAnnouncementId(int pageNumber, int pageSize, Guid announcementId)
        {
            try
            {
                var query = GetObjectsByAnnouncementIdQuery(announcementId);
                return Success(PagedList<ObjectIndexDTO>.ToPagedList(query.ProjectTo<ObjectIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<ObjectIndexDTO>>(ex);
            }
        }

        public IQueryable<Object> GetObjectsByAnnouncementIdQuery(Guid announcementId)
        {
            return _objectRepository.Get(filter: x => (x.AnnouncementId == announcementId),
                                                    include: source => source.Include(x => x.ObjectKind)
                                                                             .Include(x => x.ObjectType)
                                                                             .Include(x => x.Address)
                                                                                 .ThenInclude(x => x.Region)
                                                                             .Include(x => x.Address)
                                                                                 .ThenInclude(x => x.Settlement)
                                                                             .Include(x => x.Address)
                                                                                 .ThenInclude(x => x.Municipality))
                                                                             .AsQueryable();
        }


        public OperationResult<SaveObjectDTO> GetObjectById(Guid id)
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
                return Success(_mapper.Map<SaveObjectDTO>(objectEntity));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SaveObjectDTO>(ex);
            }
        }

        public OperationResult<bool> DeleteObjectById(Guid id)
        {
            try
            {
                var objectEntity = _objectRepository.GetById(id);
                if (objectEntity is null)
                {
                    return Exception<bool>(new Exception("Няма намерено имущество към обява за продажба."));

                }

                _objectRepository.Remove(objectEntity!.Id);
                _objectRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.DeleteObject));
                return Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<bool>(ex);
            }
        }

        #endregion

        #region Documents




        #endregion

        public OperationResult<Guid> CreateAnnouncement(SaveAnnouncementDTO saveAnnouncementDTO)
        {
            try
            {
                var announcement = _mapper.Map<Announcement>(saveAnnouncementDTO);

                _announcementRepository.Add(announcement);
                _announcementRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.CreateAnnouncement));

                return Success(announcement.Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }

        public OperationResult<SaveAnnouncementDTO> UpdateAnnouncement(SaveAnnouncementDTO saveAnnouncementDTO)
        {
            try
            {
                var announcementEntity = _announcementRepository.GetById((Guid)saveAnnouncementDTO.Id!);

                if (announcementEntity == null)
                {
                    return Exception<SaveAnnouncementDTO>(new Exception("Няма намерена обява."));
                }

                _mapper.Map(saveAnnouncementDTO, announcementEntity);
                _announcementRepository.Update(announcementEntity);
                _announcementRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.UpdateAnnouncement));

                return Success(_mapper.Map<SaveAnnouncementDTO>(announcementEntity));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SaveAnnouncementDTO>(ex);
            }
        }

        public OperationResult<Guid> CreateObject(SaveObjectDTO saveObjectDTO)
        {
            try
            {
                var objectEntity = _mapper.Map<Object>(saveObjectDTO);

                _objectRepository.Add(objectEntity);
                _objectRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.CreateObject));

                return Success(objectEntity.Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }

        public OperationResult<SaveObjectDTO> UpdateObject(SaveObjectDTO saveObjectDTO)
        {
            try
            {
                var objectEntity = _objectRepository.GetById((Guid)saveObjectDTO.Id!);

                if (objectEntity == null)
                {
                    return Exception<SaveObjectDTO>(new Exception("Имущество не е намерено."));
                }

                _mapper.Map(saveObjectDTO, objectEntity);

                _objectRepository.Update(objectEntity);
                _objectRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.UpdateObject));

                return Success(_mapper.Map<SaveObjectDTO>(objectEntity));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SaveObjectDTO>(ex);
            }
        }

        public OperationResult<bool> SendAnnouncementForPublish(Guid id)
        {
            try
            {
                var announcement = _announcementRepository.GetById(id);

                announcement.StatusCode = 3;
                _announcementRepository.Update(announcement);
                _announcementRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.SendAnnouncementForPublish));

                return Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<bool>(ex);
            }
        }

        public OperationResult<bool> PublishAnnouncement(Guid id)
        {
            try
            {
                var announcement = _announcementRepository.GetById(id);

                announcement.StatusCode = 2;
                announcement.PublishDate = DateTime.Now;
                announcement.PublishedBy = _currentUser.UserId;
                _announcementRepository.Update(announcement);
                _announcementRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.PublishAnnouncement));

                SendAnnouncementsDataToOpenData();
                return Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<bool>(ex);
            }
        }

        public OperationResult<bool> SendAnnouncementForCorrection(Guid announcementId, string correctionComment)
        {
            try
            {
                var announcement = _announcementRepository.GetById(announcementId);

                announcement.StatusCode = 5;
                announcement.CorrectionComment = correctionComment;
                announcement.SendForCorrectionBy = _currentUser.UserId;
                _announcementRepository.Update(announcement);
                _announcementRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.SendAnnouncementForCorrection));

                return Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<bool>(ex);
            }
        }
        public OperationResult<bool> CancelAnnouncement(Guid announcementId)
        {
            try
            {
                var announcement = _announcementRepository.GetById(announcementId);

                announcement.StatusCode = 6;
                _announcementRepository.Update(announcement);
                _announcementRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.CancelAnnouncement));

                return Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<bool>(ex);
            }
        }

        public OperationResult<bool> SendAnnouncementForCorrectionInDraft(Guid announcementId)
        {
            try
            {
                var announcement = _announcementRepository.GetById(announcementId);

                announcement.StatusCode = 1;
                _announcementRepository.Update(announcement);
                _announcementRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.SendAnnouncementForCorrectionInDraft));

                return Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<bool>(ex);
            }
        }

        #region OPENDATA
        public void SendAnnouncementsDataToOpenData()
        {

            var announcements = _announcementRepository.Get(filter: x => (x.StatusCode == 2 || x.StatusCode == 6),
                               include: source => source.Include(x => x.Case)
                                                            .ThenInclude(x => x.Court)
                                                        .Include(x => x.Case)
                                                            .ThenInclude(x => x.Sides.Where(x => x.IsDebtor == true))
                                                                .ThenInclude(x => x.Entity)
                                                        .Include(x => x.Court)
                                                        .Include(x => x.Syndic)
                                                        .Include(x => x.OfferingKind)
                                                        .Include(x => x.StatusCodeNavigation)
                                                        .Include(x => x.OfferingProcedure)
                                                        .Include(x => x.LocationType)
                                                        .Include(x => x.Address)
                                                            .ThenInclude(x => x.Region)
                                                        .Include(x => x.Address)
                                                            .ThenInclude(x => x.Settlement)
                                                        .Include(x => x.Address)
                                                            .ThenInclude(x => x.Municipality)
                                                        .Include(x => x.Objects)
                                                            .ThenInclude(x => x.ObjectType))
                                                      .AsQueryable().OrderByDescending(x => x.OfferingDate);
            var announcementsDtos = announcements.ProjectTo<OpenDataRawAnnouncementDTO>(_mapper.ConfigurationProvider).ToList();


            List<OpenDataAnnouncementDTO> AnnouncementsList = new List<OpenDataAnnouncementDTO>();

            for (int i = 0; i < announcementsDtos.Count; i++)
            {
                OpenDataAnnouncementDTO AnnouncementRecord = new OpenDataAnnouncementDTO();
                AnnouncementRecord.CaseNumber = announcementsDtos[i].CaseNumber;
                AnnouncementRecord.CaseYear = announcementsDtos[i].CaseYear.ToString();
                AnnouncementRecord.CourtName = announcementsDtos[i].CourtName;
                AnnouncementRecord.OfferingDate = announcementsDtos[i].OfferingDate.ToString("dd.MM.yyyy");
                AnnouncementRecord.Address = (announcementsDtos[i].LocationType != null ? announcementsDtos[i].LocationType.Description+"- " : "") +  announcementsDtos[i].FullAddress;
                AnnouncementRecord.SyndicName = GetConcatenatedSyndicName(announcementsDtos[i].Syndic);
                AnnouncementRecord.DebtorName = announcementsDtos[i].DebtorName;
                AnnouncementRecord.OfferingKind = announcementsDtos[i].OfferingKindName;
                
                if (announcementsDtos[i].Objects.Count() > 0)
                {
                    List<string> ObjectTypes = new List<string>();
                    var objectType = "";
                    foreach(var obj in announcementsDtos[i].Objects)
                    {
                        if (obj.ObjectType != null)
                        {
                            ObjectTypes.Add(obj.ObjectType);
                        }
                    }

                    if(ObjectTypes.Count > 0)
                    {
                        if(ObjectTypes.Contains("Движимо") && ObjectTypes.Contains("Недвижимо"))
                        {
                            objectType = "Движимо и недвижимо имущество";
                        }
                        else if(ObjectTypes.Contains("Движимо"))
                        {
                            objectType = "Движимо имущество";
                        } else if (ObjectTypes.Contains("Недвижимо"))
                        {
                            objectType = "Недвижимо имущество";
                        } else
                        {
                            objectType = "Имущество";
                        }
                    }
                    AnnouncementRecord.ObjectType = objectType;
                }
                else
                {
                    AnnouncementRecord.ObjectType = "Имущество";
                }

                AnnouncementRecord.URL = "https://aistn.mjs.bg/sell-announcements/" + announcementsDtos[i].Id.ToString();

                AnnouncementsList.Add(AnnouncementRecord);
            }


            using (HttpClient client = new HttpClient())
            {
                AddResourceEGOVModel requestData = new AddResourceEGOVModel();

                requestData.api_key = _configuration.GetValue<string>("OpenData:ApiKey");
                requestData.resource_uri = _configuration.GetValue<string>("OpenData:AnnouncementsResourceId");
                requestData.extension_format = "json";
                requestData.data = AnnouncementsList;

                string data = JsonConvert.SerializeObject(requestData);


                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, _configuration.GetValue<string>("OpenData:EndPoint"));
                requestMessage.Content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.SendAsync(requestMessage).GetAwaiter().GetResult();
            }
        }
        #endregion

        private static string GetConcatenatedSyndicName(SyndicIndexDTO source)
        {
            if (source == null) return null;
            List<string> syndicNames = new List<string>();

            if (!String.IsNullOrEmpty(source.FirstName))
            {
                syndicNames.Add(source.FirstName);
            }
            if (!String.IsNullOrEmpty(source.SecondName))
            {
                syndicNames.Add(source.SecondName);
            }
            if (!String.IsNullOrEmpty(source.LastName))
            {
                syndicNames.Add(source.LastName);
            }


            return string.Join(" ", syndicNames);
        }
    }
}
