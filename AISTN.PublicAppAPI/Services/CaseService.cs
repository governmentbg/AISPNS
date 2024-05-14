using AISTN.Common.Helper;
using AISTN.Common.Models.PageResult;
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

namespace AISTN.PublicAppAPI.Services
{
    [Injectable]
    public class CaseService : ServiceBase
    {
        private readonly IGenericRepository<Case> _caseRepository;
        private readonly IGenericRepository<ImportedDeed> _importedDeedRepository;
        private readonly IGenericRepository<Session> _sessionRepository;
        private readonly IGenericRepository<SurroundDocument> _surroundRepository;

        public CaseService(IMapper mapper,
                           ExceptionLogger logger,
                           IGenericRepository<Case> caseRepository,
                           IGenericRepository<ImportedDeed> importedDeedRepository,
                           IGenericRepository<Session> sessionRepository,
                           IGenericRepository<SurroundDocument> surroundRepository,
                           ExcelGenerator excelGenerator)
            : base(mapper, logger, excelGenerator)
        {
            _caseRepository = caseRepository;
            _importedDeedRepository = importedDeedRepository;
            _sessionRepository = sessionRepository;
            _surroundRepository = surroundRepository;
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

        public OperationResult<PagedList<CaseIndexDTO>> SearchCaseByPerson(int pageNumber, int pageSize, CasePersonSearchFilter filter)
        {
            try
            {
                var query = GetCaseByPersonQuery(filter);
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

        public IQueryable<Case> GetCaseQuery(CaseSearchFilter filter)
        {
            return _caseRepository.Get(filter: x => (filter.CourtNumber == null || x.Court.Number == filter.CourtNumber)
                                                 && (filter.CaseYear == null || x.Year == filter.CaseYear)
                                                 && (filter.CaseNumber == null || x.Number == filter.CaseNumber)
                                                 && (filter.IsStabilization == null || x.IsStabilization == filter.IsStabilization),
                                                     source => source.Include(x => x.Sides)
                                                                        .ThenInclude(x => x.Entity)
                                                                     .Include(x => x.Sides)
                                                                         .ThenInclude(x => x.Person)
                                                                     .Include(x => x.Sessions)
                                                                     .Include(x => x.Court)
                                                                     .Include(x => x.CaseKind)
                                                                     .Include(x => x.Judges))
                                                                  .AsQueryable()
                                                                  .OrderBy(x => x.Id);
        }

        public IQueryable<Case> GetCaseByPersonQuery(CasePersonSearchFilter filter)
        {
            return _caseRepository.Get(filter: x => (filter.EGN == null || x.Sides.Any(x => x.Person.Egn.Contains(filter.EGN))) &&
                                                    (filter.FirstName == null || x.Sides.Any(x => x.Person.FirstName.Contains(filter.FirstName))) &&
                                                    (filter.MiddleName == null || x.Sides.Any(x => x.Person.MiddleName.Contains(filter.MiddleName))) &&
                                                    (filter.LastName == null || x.Sides.Any(x => x.Person.LastName.Contains(filter.LastName))) &&
                                                    (filter.IsStabilization == null || x.IsStabilization == filter.IsStabilization),
                                                     source => source.Include(x => x.Sides)
                                                                         .ThenInclude(x => x.Person)
                                                                     .Include(x => x.Sessions)
                                                                     .Include(x => x.Judges))
                                                                 .AsQueryable()
                                                                 .OrderBy(x => x.Id);
        }

        public IQueryable<Case> GetCaseByEntityQuery(CaseEntitySearchFilter filter)
        {
            return _caseRepository.Get(filter: x => (filter.EntityName == null || x.Sides.Any(x => x.Entity.Name.Contains(filter.EntityName)))
                                                 && (filter.Bulstat == null || x.Sides.Any(x => x.Entity.Bulstat == filter.Bulstat))
                                                 && (filter.IsStabilization == null || x.IsStabilization == filter.IsStabilization),
                                                     source => source.Include(x => x.Sides)
                                                                        .ThenInclude(x => x.Entity)
                                                                     .Include(x => x.Sessions)
                                                                     .Include(x => x.Judges))
                                                                   .AsQueryable()
                                                                   .OrderBy(x => x.Id);
        }

        public OperationResult<IEnumerable<CaseIndexDTO>> GetAllCases()
        {
            var cases = _caseRepository.GetAll(x => x.Include(x => x.Court).Include(x => x.CaseKind));
            return Success(_mapper.Map<IEnumerable<CaseIndexDTO>>(cases));
        }

        public OperationResult<DetailsCaseDTO> GetCaseById(Guid id)
        {
            try
            {
                Case _case = _caseRepository.GetById(id, source => source.Include(x => x.Sides).ThenInclude(x => x.Entity)
                                                                         .Include(x => x.Sides).ThenInclude(x => x.Person)
                                                                         .Include(x => x.Sides).ThenInclude(x => x.InvolvementKind)
                                                                         .Include(x => x.Court)
                                                                         .Include(x => x.CaseKind));

                var result = _mapper.Map<DetailsCaseDTO>(_case);

                return Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<DetailsCaseDTO>(ex);
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

        public OperationResult<IEnumerable<ImportedDeedIndexDTO>> GetImportedDeedsByCaseId(Guid caseId)
        {
            try
            {
                var deeds = _importedDeedRepository.Get(x => x.CaseId == caseId, source => source.Include(x => x.Trustees)
                                                                                                 .Include(x => x.Case.Court)
                                                                                                 .Include(x => x.CourtofAppeal));

                return Success(_mapper.Map<IEnumerable<ImportedDeedIndexDTO>>(deeds));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<IEnumerable<ImportedDeedIndexDTO>>(ex);
            }
        }

        public OperationResult<IEnumerable<CaseBookIndexDTO>> GetCaseBook(Guid caseId)
        {
            try
            {
                var caseBookList = new List<CaseBookIndexDTO>();
                var sessionResult = _sessionRepository.Get(x => x.CaseId == caseId, include: source => source.Include(x => x.SessionKind).Include(x => x.Acts));
                var surroundResult = _surroundRepository.Get(x => x.CaseId == caseId, include: source => source.Include(x => x.Sides).ThenInclude(x => x.Entity)
                                                                                                               .Include(x => x.Sides).ThenInclude(x => x.Person)!);

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
    }
}
