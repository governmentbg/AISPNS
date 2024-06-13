using AISTN.Common.Helper;
using AISTN.Common.Models.PageResult;
using AISTN.Common.Services;
using AISTN.Data.DataModel;
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
using Microsoft.EntityFrameworkCore;

namespace AISTN.InternalAppAPI.Services
{
    [Injectable]

    public class ActAnnouncementService : ServiceBase
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Act> _actRepository;
        private readonly IGenericRepository<ActAnnouncement> _actAnnouncementRepository;
        private readonly IGenericRepository<NomActAnnouncementStatus> _actAnnouncementStatusRepository;
        private readonly IGenericRepository<RegisterEntry> _actRegisterEntryRepository;
        private readonly IGenericRepository<Case> _caseRepository;

        private readonly AistnContextLoggable _db;

        public ActAnnouncementService(IMapper mapper,
                              ExceptionLogger logger,
                              IGenericRepository<User> userRepository,
                              IGenericRepository<Act> actRepository,
                              IGenericRepository<ActAnnouncement> actAnnouncementRepository,
                              IHttpContextAccessor contextAccessor,
                              UserService userService,
                              ExcelGenerator excelGenerator,
                              AistnContextLoggable db,
                              IGenericRepository<NomActAnnouncementStatus> actAnnouncementStatusRepository,
                              IGenericRepository<RegisterEntry> actRegisterEntryRepository,
                              IGenericRepository<Case> caseRepository)
            : base(mapper, logger, excelGenerator)
        {
            SetCurrentUser(userService.GetCurrentUser(contextAccessor.HttpContext!).ResultData);
            _userRepository = userRepository;
            _actRepository = actRepository;
            _actAnnouncementRepository = actAnnouncementRepository;
            _db = db;
            _actAnnouncementStatusRepository = actAnnouncementStatusRepository;
            _actRegisterEntryRepository = actRegisterEntryRepository;
            _caseRepository = caseRepository;
        }

        #region Act

        public OperationResult<PagedList<ActAnnouncementIndexDTO>> SearchActAnnouncements(int pageNumber, int pageSize, ActAnnouncementSearchFilter filter)
        {
            try
            {
                var query = SearchActAnnouncementsQuery(filter);
                return Success(PagedList<ActAnnouncementIndexDTO>.ToPagedList(query.ProjectTo<ActAnnouncementIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<ActAnnouncementIndexDTO>>(ex);
            }
        }

        public IQueryable<Act> SearchActAnnouncementsQuery(ActAnnouncementSearchFilter filter)
        {
            return _actRepository.Get(filter: x => (filter.CaseNumber == null || x.Session.Case.Number.Contains(filter.CaseNumber))
                                                     && (filter.CaseYear == null || x.Session.Case.Year == filter.CaseYear)
                                                     && (filter.CourtNumber == null || x.Session.Case.Court.Number == filter.CourtNumber)
                                                     && (filter.ProprietorName == null || x.Session.Case.Sides.Any(x => x.Entity.Name.Contains(filter.ProprietorName)))
                                                     && (filter.ProprietorIdentifier == null || x.Session.Case.Sides.Any(x => x.Entity.Bulstat.Contains(filter.ProprietorIdentifier)))
                                                     && (filter.FromDate == null || x.Session.Case.FormationDate >= filter.FromDate)
                                                     && (filter.ToDate == null || x.Session.Case.FormationDate <= filter.ToDate)
                                                     && (filter.IsStabilization == null || x.Session.Case.IsStabilization == filter.IsStabilization)
                                                     && (filter.AnnouncementStatusId == null || x.ActAnnouncements.FirstOrDefault().AnnouncementStatusId == filter.AnnouncementStatusId)
                                                     && (filter.RegistrationStatusId == null || x.ActAnnouncements.FirstOrDefault().RegistrationStatusId == filter.RegistrationStatusId)
                                                     && (x.Session.Case.IsProprietor == true)
                                                     , source => source.Include(x => x.Session)
                                                                      .ThenInclude(x => x.Case)
                                                                         .ThenInclude(x => x.Court)
                                                                   .Include(x => x.Session)
                                                                      .ThenInclude(x => x.Case)
                                                                         .ThenInclude(x => x.Sides)
                                                                            .ThenInclude(x => x.Entity)
                                                                   .Include(x => x.ActAnnouncements)
                                                                      .ThenInclude(x => x.AnnouncementStatus)
                                                                    .Include(x => x.ActAnnouncements)
                                                                      .ThenInclude(x => x.RegistrationStatus)
                                                                   .Include(x => x.ActAnnouncements)
                                                                      .ThenInclude(x => x.AnnouncedByUser)
                                                                   .Include(x => x.ActAnnouncements)
                                                                      .ThenInclude(x => x.DocumentCollection)
                                                                        .ThenInclude(x => x.DocumentContents))
                                                                   .AsQueryable();
        }

        public OperationResult<DetailsActDTO> GetActById(Guid id)
        {
            try
            {
                var act = _actRepository.GetById(id, src => src.Include(x => x.Session)
                                                                  .ThenInclude(x => x.Case)
                                                                     .ThenInclude(x => x.Court)
                                                               .Include(x => x.Session)
                                                                  .ThenInclude(x => x.Case)
                                                                       .ThenInclude(x => x.Sides)
                                                                           .ThenInclude(x => x.Entity)
                                                               .Include(x => x.ActKind)
                                                               .Include(x => x.ActAnnouncements)
                                                                  .ThenInclude(x => x.AnnouncementStatus)
                                                               .Include(x => x.ActAnnouncements)
                                                                  .ThenInclude(x => x.RegistrationStatus));

                if (act == null)
                {
                    return Exception<DetailsActDTO>(new Exception("Няма намерен запис"));
                }

                return Success(_mapper.Map<DetailsActDTO>(act));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<DetailsActDTO>(ex);
            }
        }

        #endregion

        #region ActAnnouncements

        public OperationResult<ActAnnouncementDTO> GetActAnnouncementById(Guid id)
        {
            try
            {
                var actAnnouncement = _actAnnouncementRepository.GetById(id, src => src.Include(x => x.Act)
                                                                                           .ThenInclude(x => x.Session)
                                                                                              .ThenInclude(x => x.Case)
                                                                                                 .ThenInclude(x => x.Court)
                                                                                       .Include(x => x.Act)
                                                                                           .ThenInclude(x => x.Session)
                                                                                              .ThenInclude(x => x.Case)
                                                                                                   .ThenInclude(x => x.Sides)
                                                                                                       .ThenInclude(x => x.Entity)
                                                                                       .Include(x => x.AnnouncedByUser)
                                                                                       .Include(x => x.DocumentCollection)
                                                                                           .ThenInclude(x => x.DocumentContents));

                if (actAnnouncement == null)
                {
                    return Exception<ActAnnouncementDTO>(new Exception("Няма намерен запис"));
                }

                return Success(_mapper.Map<ActAnnouncementDTO>(actAnnouncement));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<ActAnnouncementDTO>(ex);
            }
        }

        public OperationResult<Guid> AnnounceAct(SaveActAnnouncementDTO actAnnouncementDTO)
        {
            try
            {
                var actAnnouncement = _actAnnouncementRepository.GetById(actAnnouncementDTO.Id.Value, src => src.Include(x => x.Act)
                                                                                                                    .ThenInclude(x => x.RegisterEntries));

                if (actAnnouncement == null)
                {
                    return Exception<Guid>(new Exception("Няма намерен акт."));
                }

                var number = DateTime.Now.ToString("yyyyMMddHHmmss");

                var mappedActAnnouncement = _mapper.Map(actAnnouncementDTO, actAnnouncement);
                mappedActAnnouncement.AnnouncedByUserId = _currentUser.UserId;
                mappedActAnnouncement.AnnouncementStatusId = ActAnnouncementStatus.Processed;
                mappedActAnnouncement.Number = number;

                _actAnnouncementRepository.Update(mappedActAnnouncement);
                _actAnnouncementRepository.Save();

                return Success(mappedActAnnouncement.Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }

        public OperationResult<bool> NoSubjectToAnnouncement(SaveActAnnouncementDTO actAnnouncementDTO)
        {
            try
            {
                var actAnnouncement = _actAnnouncementRepository.GetById(actAnnouncementDTO.Id.Value);

                if (actAnnouncement == null)
                {
                    return Exception<bool>(new Exception("Няма намерен акт."));
                }

                actAnnouncement.AnnouncementStatusId = _actAnnouncementStatusRepository.GetById(ActAnnouncementStatus.NoSubjectToAnnouncement).Id;
                actAnnouncement.AnnouncedByUserId = _currentUser.UserId;

                _actAnnouncementRepository.Update(actAnnouncement);
                _actAnnouncementRepository.Save();

                return Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<bool>(ex);
            }
        }

        public OperationResult<List<GroupedActAnnouncements>> GetActAnnouncementsByCaseId(Guid caseId)
        {
            try
            {
                var actAnnouncements = _actAnnouncementRepository.Get(x => x.Act.Session.CaseId == caseId && x.AnnouncementStatusId == ActAnnouncementStatus.Processed,
                                                                                                              src => src.Include(x => x.Act)
                                                                                                                        .ThenInclude(x => x.Session)
                                                                                                                           .ThenInclude(x => x.Case)
                                                                                                                              .ThenInclude(x => x.Court)
                                                                                                                    .Include(x => x.Act)
                                                                                                                        .ThenInclude(x => x.Session)
                                                                                                                           .ThenInclude(x => x.Case)
                                                                                                                                .ThenInclude(x => x.Sides)
                                                                                                                                    .ThenInclude(x => x.Entity)
                                                                                                                    .Include(x => x.AnnouncedByUser)
                                                                                                                        .ThenInclude(x => x.Roles)
                                                                                                                    .Include(x => x.DocumentCollection)
                                                                                                                       .ThenInclude(x => x.DocumentContents)
                                                                                                                    .Include(x => x.RegistrationStatus)
                                                                                                                    .Include(x => x.AnnouncementStatus)
                                                                                                                    .Include(x => x.ActCategory));
                if (actAnnouncements.Count() <= 0)
                {
                    return Exception<List<GroupedActAnnouncements>>(new Exception());
                }

                var result = actAnnouncements.GroupBy(x => x.ActCategoryId).Select(x => new GroupedActAnnouncements
                {
                    Category = x.Key.Value,
                    ActAnnouncements = _mapper.Map<List<ActAnnouncementDTO>>(x)
                }).ToList();

                return Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<GroupedActAnnouncements>>(ex);
            }
        }

        #endregion

        #region ActRegistrations

        public OperationResult<PagedList<RegisterEntryIndexDTO>> GetAllRegisterEntries(Guid actId, int pageNumber, int pageSize)
        {
            try
            {
                var query = GetAllRegisterEntriesQuery(actId);
                return Success(PagedList<RegisterEntryIndexDTO>.ToPagedList(query.ProjectTo<RegisterEntryIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<RegisterEntryIndexDTO>>(ex);
            }
        }

        public IQueryable<RegisterEntry> GetAllRegisterEntriesQuery(Guid actId)
        {
            return _actRegisterEntryRepository.Get(x => x.ActId == actId, src => src.Include(x => x.Act)
                                                                                       .ThenInclude(x => x.ActKind)
                                                                                    .Include(x => x.Field)
                                                                                    .Include(x => x.LegalBasis)
                                                                                    .Include(x => x.AnnouncedByUser)
                                                                                    .Include(x => x.Address)
                                                                                    .Include(x => x.ReplacedByEntry)
                                                                                    .Include(x => x.PreviousEntry))
                                                                                    .AsQueryable();
        }

        public OperationResult<SaveRegisterEntryDTO> GetRegisterEntryById(Guid id)
        {
            try
            {
                var registerAct = _actRegisterEntryRepository.GetById(id, src => src.Include(x => x.Address)
                                                                                    .Include(x => x.ReplacedByEntry)
                                                                                        .ThenInclude(x => x.Address)
                                                                                    .Include(x => x.PreviousEntry)
                                                                                        .ThenInclude(x => x.Address)
                                                                                    .Include(x => x.AnnouncedByUser));

                if (registerAct == null)
                {
                    return Exception<SaveRegisterEntryDTO>(new Exception("Няма намерено вписване."));
                }

                return Success(_mapper.Map<SaveRegisterEntryDTO>(registerAct));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SaveRegisterEntryDTO>(ex);
            }
        }

        public OperationResult<Guid> CreateRegisterEntry(SaveRegisterEntryDTO actRegisterEntryDTO)
        {
            try
            {
                var act = _actRepository.GetById(actRegisterEntryDTO.ActId, src => src.Include(x => x.ActAnnouncements)
                                                                                      .Include(x => x.Session)
                                                                                        .ThenInclude(x => x.Case)
                                                                                            .ThenInclude(x => x.Sides)
                                                                                                .ThenInclude(x => x.Entity)
                                                                                      .Include(x => x.RegisterEntries));

                if (act == null)
                {
                    return Exception<Guid>(new Exception("Няма намерен акт."));
                }

                var registerEntry = _mapper.Map<RegisterEntry>(actRegisterEntryDTO);
                registerEntry.AnnouncedByUser = _userRepository.GetById(_currentUser.UserId.Value);
                registerEntry.AnnouncedDate = DateTime.Now;
                registerEntry.ProprietorId = act.Session.Case.Sides.FirstOrDefault(x => x.Entity != null).Id;

                if (act.RegisterEntries.Count >= 1 && act.RegisterEntries.Any(x => x.FieldId == registerEntry.FieldId))
                {
                    var newestRegisterEntry = act.RegisterEntries.Where(x => x.FieldId == registerEntry.FieldId).OrderByDescending(x => x.DateCreated).FirstOrDefault();
                    newestRegisterEntry.ReplacedByEntry = registerEntry;
                    registerEntry.PreviousEntry = newestRegisterEntry;
                }

                act.RegisterEntries.Add(registerEntry);

                _actRepository.Update(act);
                _actRepository.Save();

                return Success(act.RegisterEntries.OrderByDescending(x => x.DateCreated).FirstOrDefault().Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }

        public OperationResult<Guid> UpdateRegisterEntry(SaveRegisterEntryDTO actRegisterEntryDTO)
        {
            try
            {
                var registerEntry = _actRegisterEntryRepository.GetById(actRegisterEntryDTO.Id.Value);

                if (registerEntry == null)
                {
                    return Exception<Guid>(new Exception("Няма намерено вписване."));
                }

                var mappedActEntry = _mapper.Map(actRegisterEntryDTO, registerEntry);

                _actRegisterEntryRepository.Update(registerEntry);
                _actRegisterEntryRepository.Save();

                return Success(registerEntry.Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }

        public OperationResult<Guid> DeleteRegisterEntry(Guid id)
        {
            try
            {
                var registerEntry = _actRegisterEntryRepository.GetById(id, src => src.Include(x => x.Act)
                                                                                        .ThenInclude(x => x.ActAnnouncements));

                if (registerEntry == null)
                {
                    return Exception<Guid>(new Exception("Няма намерено вписване."));
                }

                if (registerEntry.Act.ActAnnouncements.FirstOrDefault().AnnouncementStatusId != ActAnnouncementStatus.Unprocessed)
                {
                    return Exception<Guid>(new Exception("Този запис не може да бъде изтрит."));
                }

                _actRegisterEntryRepository.Remove(registerEntry.Id);
                _actRegisterEntryRepository.Save();

                return Success(registerEntry.Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }

        public OperationResult<Guid> RegisterAct(SaveActAnnouncementDTO actAnnouncementDTO)
        {
            try
            {
                var actAnnouncement = _actAnnouncementRepository.GetById(actAnnouncementDTO.Id.Value, src => src.Include(x => x.Act)
                                                                                                                    .ThenInclude(x => x.RegisterEntries));

                if (actAnnouncement == null)
                {
                    return Exception<Guid>(new Exception("Няма намерен акт."));
                }

                var number = DateTime.Now.ToString("yyyyMMddHHmmss");

                foreach (var actRegistry in actAnnouncement.Act.RegisterEntries)
                {
                    actRegistry.Number = number;
                }

                var mappedActAnnouncement = _mapper.Map(actAnnouncementDTO, actAnnouncement);
                mappedActAnnouncement.RegistrationStatusId = ActAnnouncementStatus.Processed;

                _actAnnouncementRepository.Update(mappedActAnnouncement);
                _actAnnouncementRepository.Save();

                return Success(mappedActAnnouncement.Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }

        public OperationResult<bool> NoSubjectToRegistration(SaveActAnnouncementDTO actAnnouncementDTO)
        {
            try
            {
                var actAnnouncement = _actAnnouncementRepository.GetById(actAnnouncementDTO.Id.Value);

                if (actAnnouncement == null)
                {
                    return Exception<bool>(new Exception("Няма намерен акт."));
                }

                actAnnouncement.RegistrationStatusId = _actAnnouncementStatusRepository.GetById(ActAnnouncementStatus.NoSubjectToRegistration).Id;
                actAnnouncement.AnnouncedByUserId = _currentUser.UserId;

                _actAnnouncementRepository.Update(actAnnouncement);
                _actAnnouncementRepository.Save();

                return Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<bool>(ex);
            }
        }



        public OperationResult<List<RegisterEntryDTO>> GetRegisterEntriesByCaseId(Guid caseId)
        {
            try
            {
                var registerEntries = _actRegisterEntryRepository.Get(x => x.Act.Session.CaseId == caseId, src => src.Include(x => x.Act)
                                                                                                                        .ThenInclude(x => x.ActKind)
                                                                                                                      .Include(x => x.Act)
                                                                                                                        .ThenInclude(x => x.ActCategory)
                                                                                                                      .Include(x => x.Act)
                                                                                                                        .ThenInclude(x => x.ActContractor)
                                                                                                                      .Include(x => x.Act)
                                                                                                                        .ThenInclude(x => x.ActReason)
                                                                                                                      .Include(x => x.Act)
                                                                                                                        .ThenInclude(x => x.Session)
                                                                                                                      .Include(x => x.Address)
                                                                                                                        .ThenInclude(x => x.Municipality)
                                                                                                                      .Include(x => x.Address)
                                                                                                                        .ThenInclude(x => x.Region)
                                                                                                                      .Include(x => x.Address)
                                                                                                                        .ThenInclude(x => x.Settlement)
                                                                                                                      .Include(x => x.Field)
                                                                                                                      .Include(x => x.RegisterSyndicKind)
                                                                                                                      .Include(x => x.Syndic)
                                                                                                                      .Include(x => x.LegalBasis)
                                                                                                                      .Include(x => x.DocumentCollection)
                                                                                                                        .ThenInclude(x => x.DocumentContents)
                                                                                                                      .Include(x => x.ReplacedByEntry)
                                                                                                                      .Include(x => x.PreviousEntry))
                                                                                                                      .OrderByDescending(x => x.Field.Description)
                                                                                                                      .DistinctBy(x => x.Field.Description)
                                                                                                                      .ToList();

                if (registerEntries == null)
                {
                    return Exception<List<RegisterEntryDTO>>(new Exception());
                }

                return Success(_mapper.Map<List<RegisterEntryDTO>>(registerEntries));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<RegisterEntryDTO>>(ex);
            }
        }

        public OperationResult<List<RegisterEntryDTO>> GetRegisterEntryHistory(Guid registerEntryId)
        {
            try
            {
                var registerEntry = _actRegisterEntryRepository.GetById(registerEntryId, src => src.Include(x => x.Address)
                                                                                                     .ThenInclude(x => x.Municipality)
                                                                                                   .Include(x => x.Address)
                                                                                                     .ThenInclude(x => x.Region)
                                                                                                   .Include(x => x.Address)
                                                                                                     .ThenInclude(x => x.Settlement)
                                                                                                   .Include(x => x.Field)
                                                                                                   .Include(x => x.RegisterSyndicKind)
                                                                                                   .Include(x => x.Syndic)
                                                                                                   .Include(x => x.LegalBasis)
                                                                                                   .Include(x => x.Act)
                                                                                                     .ThenInclude(x => x.ActKind)
                                                                                                   .Include(x => x.Act)
                                                                                                     .ThenInclude(x => x.ActCategory)
                                                                                                   .Include(x => x.Act)
                                                                                                     .ThenInclude(x => x.ActContractor)
                                                                                                   .Include(x => x.Act)
                                                                                                     .ThenInclude(x => x.ActReason)
                                                                                                   .Include(x => x.Act)
                                                                                                     .ThenInclude(x => x.Session)
                                                                                                   .Include(x => x.DocumentCollection)
                                                                                                     .ThenInclude(x => x.DocumentContents)
                                                                                                   .Include(x => x.ReplacedByEntry)
                                                                                                   .Include(x => x.PreviousEntry));
                var history = new List<RegisterEntry>();

                if (registerEntry != null)
                {
                    GetAllReplacementRegisterEntries(registerEntry, history);
                    //GetAllPreviousRegisterEntries(registerEntry, history);
                }

                return Success(_mapper.Map<List<RegisterEntryDTO>>(history.OrderBy(x => x.AnnouncedDate).ToList()));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<RegisterEntryDTO>>(ex);
            }
        }

        private void GetAllReplacementRegisterEntries(RegisterEntry registerEntry, List<RegisterEntry> history)
        {
            history.Add(registerEntry!);

            if (registerEntry.ReplacedByEntryId != null)
            {
                var replacementEntry = GetReplacementRegisterEntry(registerEntry);

                if (replacementEntry != null)
                {
                    GetAllReplacementRegisterEntries(replacementEntry, history);
                }
            }
        }

        private void GetAllPreviousRegisterEntries(RegisterEntry registerEntry, List<RegisterEntry> history)
        {
            if (registerEntry.PreviousEntryId != null)
            {
                var previousEntry = GetPreviousRegisterEntry(registerEntry);
                history.Add(previousEntry!);
                if (previousEntry.PreviousEntry != null)
                {
                    GetAllReplacementRegisterEntries(previousEntry.PreviousEntry, history);
                }
            }
        }

        private RegisterEntry GetReplacementRegisterEntry(RegisterEntry registerEntry)
        {
            return _actRegisterEntryRepository.Get(x => x.Id == registerEntry.ReplacedByEntryId, src => src.Include(x => x.Address)
                                                                                                             .ThenInclude(x => x.Municipality)
                                                                                                           .Include(x => x.Address)
                                                                                                             .ThenInclude(x => x.Region)
                                                                                                           .Include(x => x.Address)
                                                                                                             .ThenInclude(x => x.Settlement)
                                                                                                           .Include(x => x.Field)
                                                                                                           .Include(x => x.RegisterSyndicKind)
                                                                                                           .Include(x => x.Syndic)
                                                                                                           .Include(x => x.LegalBasis)
                                                                                                           .Include(x => x.Act)
                                                                                                             .ThenInclude(x => x.ActKind)
                                                                                                           .Include(x => x.Act)
                                                                                                             .ThenInclude(x => x.ActCategory)
                                                                                                           .Include(x => x.Act)
                                                                                                             .ThenInclude(x => x.ActContractor)
                                                                                                           .Include(x => x.Act)
                                                                                                             .ThenInclude(x => x.ActReason)
                                                                                                           .Include(x => x.Act)
                                                                                                             .ThenInclude(x => x.Session)
                                                                                                           .Include(x => x.DocumentCollection)
                                                                                                             .ThenInclude(x => x.DocumentContents)
                                                                                                           .Include(x => x.ReplacedByEntry)
                                                                                                           .Include(x => x.PreviousEntry))
                                                                                                           .FirstOrDefault()!;
        }

        private RegisterEntry GetPreviousRegisterEntry(RegisterEntry registerEntry)
        {
            return _actRegisterEntryRepository.Get(x => x.Id == registerEntry.PreviousEntryId, src => src.Include(x => x.Address)
                                                                                                           .ThenInclude(x => x.Municipality)
                                                                                                         .Include(x => x.Address)
                                                                                                           .ThenInclude(x => x.Region)
                                                                                                         .Include(x => x.Address)
                                                                                                           .ThenInclude(x => x.Settlement)
                                                                                                         .Include(x => x.Field)
                                                                                                         .Include(x => x.RegisterSyndicKind)
                                                                                                         .Include(x => x.Syndic)
                                                                                                         .Include(x => x.LegalBasis)
                                                                                                         .Include(x => x.Act)
                                                                                                           .ThenInclude(x => x.ActKind)
                                                                                                         .Include(x => x.Act)
                                                                                                           .ThenInclude(x => x.ActCategory)
                                                                                                         .Include(x => x.Act)
                                                                                                           .ThenInclude(x => x.ActContractor)
                                                                                                         .Include(x => x.Act)
                                                                                                           .ThenInclude(x => x.ActReason)
                                                                                                         .Include(x => x.Act)
                                                                                                           .ThenInclude(x => x.Session)
                                                                                                         .Include(x => x.DocumentCollection)
                                                                                                           .ThenInclude(x => x.DocumentContents)
                                                                                                         .Include(x => x.ReplacedByEntry)
                                                                                                         .Include(x => x.PreviousEntry))
                                                                                                         .FirstOrDefault()!;
        }

        #endregion
    }
}
