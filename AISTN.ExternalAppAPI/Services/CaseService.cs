using AISTN.Common.Helper;
using AISTN.Common.Models.PageResult;
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

namespace AISTN.ExternalAppAPI.Services
{
    [Injectable]
    public class CaseService : ServiceBase
    {
        private readonly IGenericRepository<Case> _caseRepository;
        private readonly IGenericRepository<EntityStatisticalDatum> _entityStatisticalDataRepository;
        private readonly IGenericRepository<Session> _sessionRepository;
        private readonly IGenericRepository<SurroundDocument> _surroundRepository;
        private readonly IGenericRepository<Syndic> _syndicRepository;
        private readonly Guid? _userId;

        public CaseService(IMapper mapper,
                           ExceptionLogger logger,
                           IGenericRepository<Case> caseRepository,
                           IGenericRepository<EntityStatisticalDatum> entityStatisticalData,
                           IGenericRepository<Session> sessionRepository,
                           IGenericRepository<SurroundDocument> surroundRepository,
                           IGenericRepository<Syndic> syndicRepository,
                           UserService userService,
                           IHttpContextAccessor contextAccessor,
                           ExcelGenerator excelGenerator)
            : base(mapper, logger, excelGenerator)
        {
            SetCurrentUser(userService.GetCurrentUser(contextAccessor.HttpContext!).ResultData);
            _caseRepository = caseRepository;
            _entityStatisticalDataRepository = entityStatisticalData;
            _sessionRepository = sessionRepository;
            _surroundRepository = surroundRepository;
            _syndicRepository = syndicRepository;
            _userId = _currentUser.UserId;
        }

        public OperationResult<IEnumerable<CaseIndexDTO>> GetAllCases()
        {
            var cases = _caseRepository.GetAll(x => x.Include(x => x.Court).Include(x => x.CaseKind!));
            return Success(_mapper.Map<IEnumerable<CaseIndexDTO>>(cases));
        }

        public OperationResult<CaseIndexDTO> GetCaseById(Guid id)
        {
            try
            {
                var _case = _caseRepository.GetById(id, source => source.Include(x => x.Sides).ThenInclude(x => x.Entity)
                                                                         .Include(x => x.Sides).ThenInclude(x => x.InvolvementKind)
                                                                         .Include(x => x.Court)
                                                                         .Include(x => x.CaseKind)
                                                                         .Include(x => x.Sessions)
                                                                         .Include(x => x.Judges));

                var result = _mapper.Map<CaseIndexDTO>(_case);

                return Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<CaseIndexDTO>(ex);
            }
        }

        public OperationResult<AnnouncementCaseIndexDTO> GetAnnouncementCaseById(Guid id)
        {
            try
            {
                Case _case = _caseRepository.GetById(id, source => source.Include(x => x.Sides).ThenInclude(x => x.Entity)
                                                                         .Include(x => x.Sides).ThenInclude(x => x.InvolvementKind)
                                                                         .Include(x => x.Court)
                                                                         .Include(x => x.CaseKind)
                                                                         .Include(x => x.Sessions)
                                                                         .Include(x => x.Judges));

                var result = _mapper.Map<AnnouncementCaseIndexDTO>(_case);

                return Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<AnnouncementCaseIndexDTO>(ex);
            }
        }

        public OperationResult<IEnumerable<CaseBookIndexDTO>> GetCaseBook(Guid caseId)
        {
            try
            {
                var caseBookList = new List<CaseBookIndexDTO>();
                var sessionResult = _sessionRepository.Get(x => x.CaseId == caseId, include: source => source.Include(x => x.SessionKind).Include(x => x.Acts));
                var surroundResult = _surroundRepository.Get(x => x.CaseId == caseId, include: source => source.Include(x => x.Sides).ThenInclude(x => x.Entity));

                // map sessions
                var mappedSessionResult = _mapper.Map<IEnumerable<CaseBookIndexDTO>>(sessionResult);

                // map surrouds
                var mappedSurroundResult = _mapper.Map<IEnumerable<CaseBookIndexDTO>>(surroundResult);

                caseBookList.AddRange(mappedSessionResult);
                caseBookList.AddRange(mappedSurroundResult);

                caseBookList = caseBookList.OrderBy(x => x.Date).Select((row, index) => new CaseBookIndexDTO
                {
                    RowNumber = index + 1,
                    ParticipantName = row.ParticipantName,
                    Date = row.Date != null ? row.Date : null,
                    Actvity = row.Actvity,
                    SessionResult = row.SessionResult,
                }).ToList();

                return Success<IEnumerable<CaseBookIndexDTO>>(caseBookList);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);

                return Exception<IEnumerable<CaseBookIndexDTO>>(ex);
            }


        }

        public OperationResult<PagedList<CaseIndexDTO>> SearchCase(int pageNumber, int pageSize, CaseSearchFilter filter)
        {
            try
            {
                var query = GetCaseQuery(filter);
                return Success(PagedList<CaseIndexDTO>.ToPagedList(query.ProjectTo<CaseIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<CaseIndexDTO>>(ex);
            }
        }

        public OperationResult<PagedList<CaseIndexDTO>> SearchCaseByEntity(int pageNumber, int pageSize, CaseEntitySearchFilter filter)
        {
            try
            {
                var query = GetCaseByEntityQuery(filter);
                return Success(PagedList<CaseIndexDTO>.ToPagedList(query.ProjectTo<CaseIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<CaseIndexDTO>>(ex);
            }
        }

        public OperationResult<PagedList<CaseIndexDTO>> SearchCaseBySyndic(int pageNumber, int pageSize, CaseSyndicSearchFilter filter)
        {
            try
            {
                var syndic = _syndicRepository.Get(x => x.UserId == _userId).FirstOrDefault();
                if (syndic == null)
                {
                    return Exception<PagedList<CaseIndexDTO>>(new Exception("Няма намерен синдик."));
                }
                filter.SyndicId = syndic.Id;

                var query = GetSyndicCaseQuery(filter);
                return Success(PagedList<CaseIndexDTO>.ToPagedList(query.ProjectTo<CaseIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<CaseIndexDTO>>(ex);
            }
        }

        public IQueryable<Case> GetCaseQuery(CaseSearchFilter filter)
        {
            return _caseRepository.Get(filter: x => (filter.CourtNumber == null || x.Court.Number == filter.CourtNumber)
                                                    && (filter.CaseYear == null || x.Year == filter.CaseYear)
                                                    && (filter.CaseNumber == null || x.Number == filter.CaseNumber)
                                                    && x.IsStabilization == filter.IsStabilization
                                                    , source => source.Include(x => x.Sides)
                                                                        .ThenInclude(x => x.Entity)
                                                                     .Include(x => x.Sessions)
                                                                     .Include(x => x.Court)
                                                                     .Include(x => x.CaseKind)
                                                                     .Include(x => x.Judges)).AsQueryable().OrderByDescending(x => x.FormationDate);
        }

        public IQueryable<Case> GetSyndicCaseQuery(CaseSyndicSearchFilter filter)
        {
            return _caseRepository.Get(filter: x => (filter.SyndicId == null || x.CaseAssignments.Any(x => x.SyndicId == filter.SyndicId))
                                                      && (filter.EntityName == null || x.Sides.Any(x => x.Entity.Name.Contains(filter.EntityName)))
                                                      && (filter.Bulstat == null || x.Sides.Any(x => x.Entity.Bulstat == filter.Bulstat))
                                                      && (filter.EGN == null || x.Sides.Any(x => x.Person.Egn.Contains(filter.EGN)))
                                                      && (filter.FirstName == null || x.Sides.Any(x => x.Person.FirstName.Contains(filter.FirstName)))
                                                      && (filter.LastName == null || x.Sides.Any(x => x.Person.LastName.Contains(filter.LastName)))
                                                      && (filter.CourtNumber == null || x.Court.Number == filter.CourtNumber)
                                                      && (filter.CaseYear == null || x.Year == filter.CaseYear)
                                                      && (filter.CaseNumber == null || x.Number == filter.CaseNumber),
                                                      source => source.Include(x => x.Sides)
                                                                        .ThenInclude(x => x.Entity)
                                                                      .Include(x => x.Sessions)
                                                                      .Include(x => x.Court)
                                                                      .Include(x => x.CaseKind)
                                                                      .Include(x => x.Judges))
                                                    .AsQueryable().OrderByDescending(x => x.FormationDate);
        }

        public IQueryable<Case> GetCaseByEntityQuery(CaseEntitySearchFilter filter)
        {
            return _caseRepository.Get(filter: x => (filter.EntityName == null || x.Sides.Any(x => x.Entity.Name.Contains(filter.EntityName)))
                                                    && (filter.Bulstat == null || x.Sides.Any(x => x.Entity.Bulstat == filter.Bulstat)
                                                    && x.IsStabilization == filter.IsStabilization)
                                                    , source => source.Include(x => x.Sides)
                                                                     .ThenInclude(x => x.Entity)
                                                                     .Include(x => x.Sessions)
                                                                     .Include(x => x.Judges)).AsQueryable().OrderBy(x => x.Id);
        }

        public OperationResult<SaveEntityStatisticalDataDTO> GetEntityStatisticalDataByEntityId(Guid entityId)
        {
            try
            {
                var entityStatisticalData = _entityStatisticalDataRepository.Get(x => x.EntityId == entityId).FirstOrDefault();

                var result = _mapper.Map<SaveEntityStatisticalDataDTO>(entityStatisticalData);

                return Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SaveEntityStatisticalDataDTO>(ex);
            }
        }


        public OperationResult<Guid> SaveEntityStatisticalData(SaveEntityStatisticalDataDTO saveEntityStatisticalDataDTO)
        {
            try
            {
                var dbEntityStatisticalData = _entityStatisticalDataRepository.Get(x => x.EntityId == saveEntityStatisticalDataDTO.EntityId).FirstOrDefault();
                if (dbEntityStatisticalData != null)
                {
                    saveEntityStatisticalDataDTO.Id = dbEntityStatisticalData.Id;
                    _mapper.Map(saveEntityStatisticalDataDTO, dbEntityStatisticalData);
                    _entityStatisticalDataRepository.Update(dbEntityStatisticalData);
                    _entityStatisticalDataRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.UpdateEntityStatisticalData));
                    return Success(saveEntityStatisticalDataDTO.Id.Value);
                }
                else
                {
                    var data = _mapper.Map<EntityStatisticalDatum>(saveEntityStatisticalDataDTO);

                    _entityStatisticalDataRepository.Add(data);
                    _entityStatisticalDataRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.CreateEntityStatisticalData));

                    return Success(data.Id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }
    }
}
