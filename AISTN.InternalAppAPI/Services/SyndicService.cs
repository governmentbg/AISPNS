using AISTN.Common.Helper;
using AISTN.Common.Models;
using AISTN.Common.Models.PageResult;
using AISTN.Common.Models.Export;
using AISTN.Common.Services;
using AISTN.Data.DataModel;
using AISTN.InternalAppAPI.Models.Details;
using AISTN.InternalAppAPI.Models.Export;
using AISTN.InternalAppAPI.Models.Filter;
using AISTN.InternalAppAPI.Models.Index;
using AISTN.InternalAppAPI.Models.Save;
using AISTN.InternalAppAPI.Models.Export;
using AISTN.Repository;
using AISTN.Repository.Attributes;
using AISTN.Repository.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Newtonsoft.Json;
using System.Text;
using CourseIndexDTO = AISTN.InternalAppAPI.Models.Index.CourseIndexDTO;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;

namespace AISTN.InternalAppAPI.Services
{
    [Injectable]
    public class SyndicService : ServiceBase
    {
        private readonly IGenericRepository<Syndic> _syndicRepository;
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<Course> _courseRepository;
        private readonly IGenericRepository<CourseApplication> _courseApplicationRepository;
        private readonly IGenericRepository<Installment> _installmentRepository;
        private readonly IGenericRepository<Case> _caseRepository;
        private readonly IGenericRepository<Template> _templateRepository;
        private readonly IGenericRepository<Signal> _signalRepository;
        private readonly IGenericRepository<Report> _reportRepository;
        private readonly IGenericRepository<Plea> _pleaRepository;
        private readonly IGenericRepository<Appeal> _appealRepository;
        private readonly IGenericRepository<CourseResult> _courseResultRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<NomSyndicStatus> _syndicStatusRepo;
        private readonly EmailService _emailService;
        private readonly IConfiguration _configuration;

        public SyndicService(IMapper mapper,
                             ExceptionLogger logger,
                             ExcelGenerator excelGenerator,
                             IGenericRepository<Syndic> syndicRepository,
                             IHttpContextAccessor contextAccessor,
                             IGenericRepository<Order> orderRepository,
                             IGenericRepository<Installment> installmentRepository,
                             IGenericRepository<Course> courseRepository,
                             IGenericRepository<CourseApplication> courseApplicationRepository,
                             IGenericRepository<Case> caseRepository,
                             IGenericRepository<Plea> pleaRepository,
                             IGenericRepository<Template> templateRepository,
                             IGenericRepository<Report> reportRepository,
                             IGenericRepository<Signal> signalRepository,
                             IGenericRepository<Appeal> appealRepository,
                             IGenericRepository<CourseResult> courseResultRepository,
                             UserService userService,
                             EmailService emailService,
                             IGenericRepository<User> userRepository,
                             IGenericRepository<NomSyndicStatus> syndicStatusRepo,
                             IConfiguration configuration)
            : base(mapper, logger, excelGenerator)
        {
            SetCurrentUser(userService.GetCurrentUser(contextAccessor.HttpContext!).ResultData);
            _syndicRepository = syndicRepository;
            _orderRepository = orderRepository;
            _installmentRepository = installmentRepository;
            _courseRepository = courseRepository;
            _courseApplicationRepository = courseApplicationRepository;
            _caseRepository = caseRepository;
            _templateRepository = templateRepository;
            _pleaRepository = pleaRepository;
            _signalRepository = signalRepository;
            _appealRepository = appealRepository;
            _reportRepository = reportRepository;
            _courseResultRepository = courseResultRepository;
            _emailService = emailService;
            _userRepository = userRepository;
            _syndicStatusRepo = syndicStatusRepo;
            _configuration = configuration;
        }

        public OperationResult<PagedList<SyndicIndexDTO>> SearchSyndic(int pageNumber, int pageSize, SyndicSearchFilter filter)
        {
            try
            {
                var query = GetSyndicQuery(filter);
                return Success(PagedList<SyndicIndexDTO>.ToPagedList(query.ProjectTo<SyndicIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<SyndicIndexDTO>>(ex);
            }
        }

        private IQueryable<Syndic> GetSyndicQuery(SyndicSearchFilter filter)
        {
            return _syndicRepository.Get(filter: x => ((filter.FirstName == null) || x.FirstName.Contains(filter.FirstName))
                                                      && ((filter.SecondName == null) || x.SecondName.Contains(filter.SecondName))
                                                      && ((filter.LastName == null) || x.LastName.Contains(filter.LastName))
                                                      && ((filter.Egn == null) || x.Egn.Contains(filter.Egn))
                                                      && ((filter.City == null) || x.Syndic2Addresses.Any(x => x.Address.Settlement.Name.Contains(filter.City)))
                                                      && ((filter.SyndicStatusID == null) || x.SyndicStatusId == filter.SyndicStatusID)
                                                      && ((filter.IsCustodian == null) || x.IsCustodian == filter.IsCustodian)
                                                      && ((filter.Active == null) || x.Active == filter.Active),
                                        include: source => source.Include(x => x.SpecialtyNavigation)
                                                                 .Include(x => x.Installments)
                                                                 .Include(x => x.Orders)
                                                                 .Include(x => x.SyndicStatus)
                                                                 .Include(x => x.Syndic2Addresses)
                                                                 .ThenInclude(x => x.Address!)
                                                                 .ThenInclude(x => x.Region)
                                                                 .Include(x => x.Syndic2Addresses)
                                                                 .ThenInclude(x => x.Address!)
                                                                 .ThenInclude(x => x.Settlement)
                                                                 .Include(x => x.Syndic2Addresses)
                                                                 .ThenInclude(x => x.Address!)
                                                                 .ThenInclude(x => x.Municipality!))
                                                                 .AsQueryable()
                                                                 .OrderBy(x => x.FirstName)
                                                                    .ThenBy(x => x.Number);
        }

        public OperationResult<SaveSyndicDTO> GetSyndicById(Guid id)
        {
            try
            {
                var syndic = _syndicRepository.GetById(id, include: source => source.Include(x => x.SpecialtyNavigation)
                                                                                    .Include(x => x.Installments)
                                                                                    .Include(x => x.Orders)
                                                                                    .Include(x => x.SyndicStatus)
                                                                                    .Include(x => x.Syndic2Addresses)
                                                                                        .ThenInclude(x => x.Address!)
                                                                                        .ThenInclude(x => x.Region)
                                                                                     .Include(x => x.Syndic2Addresses)
                                                                                        .ThenInclude(x => x.Address!)
                                                                                        .ThenInclude(x => x.Settlement)
                                                                                     .Include(x => x.Syndic2Addresses)
                                                                                        .ThenInclude(x => x.Address!)
                                                                                        .ThenInclude(x => x.Municipality!));
                return Success(_mapper.Map<SaveSyndicDTO>(syndic));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SaveSyndicDTO>(ex);
            }
        }

        public OperationResult<string> SendExpirationDateEmail(Guid id)
        {
            try
            {
                var syndic = _syndicRepository.GetById(id, src => src.Include(x => x.Installments)
                                                                        .ThenInclude(x => x.InstallmentYear));
                var installment = syndic.Installments.OrderByDescending(x => x.DateCreated).FirstOrDefault(x => x.TerminationDeadline != null);

                if (installment == null)
                {
                    return Exception<string>(new Exception("Не е намерена вноска."));
                }

                _emailService.SendEmail(syndic, installment);

                return Success("Успешно изпратен имейл.");
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<string>(new Exception("Неуспешно изпратен имейл"));
            }
        }

        public byte[] ExportActiveSyndicsToExcel()
        {
            try
            {
                var syndics = GetActiveSyndicsQuery();
                var syndicsDtos = syndics.ProjectTo<ExportSyndicDTO>(_mapper.ConfigurationProvider).ToList();

                int number = 1;

                foreach (var syndic in syndicsDtos)
                {
                    syndic.Number = number++.ToString();
                }

                return _excelGenerator.ExportGridToExcelXlsxFile(syndicsDtos, new List<string> { "Id", "Orders" });
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                throw;
            }
        }

        public byte[] ExportActiveSyndicsWithoutPaidInstallmentToExcel()
        {
            try
            {
                var syndics = GetActiveSyndicsWithoutPaidInstallmentQuery();
                var syndicsDtos = syndics.ProjectTo<ExportSyndicDTO>(_mapper.ConfigurationProvider).ToList();

                int number = 1;

                foreach (var syndic in syndicsDtos)
                {

                    syndic.Number = number++.ToString();
                }

                return _excelGenerator.ExportGridToExcelXlsxFile(syndicsDtos, new List<string> { "Id", "Orders" });
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                throw;
            }
        }

        public OperationResult<PagedList<OrderIndexDTO>> GetSyndicOrders(Guid syndicId, int pageNumber, int pageSize)
        {
            try
            {
                var orders = GetSyndicOrdersQuery(syndicId);
                return Success(PagedList<OrderIndexDTO>.ToPagedList(orders.ProjectTo<OrderIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<OrderIndexDTO>>(ex);
            }
        }

        public OperationResult<PagedList<SignalIndexDTO>> GetSyndicSignals(Guid syndicId, int pageNumber, int pageSize)
        {
            try
            {
                var signals = GetSyndicSignalsQuery(syndicId);

                return Success(PagedList<SignalIndexDTO>.ToPagedList(signals.ProjectTo<SignalIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<SignalIndexDTO>>(ex);
            }
        }

        public OperationResult<PagedList<AppealIndexDTO>> GetSyndicAppeals(Guid syndicId, int pageNumber, int pageSize)
        {
            try
            {
                var appeals = GetSyndicAppealsQuery(syndicId);

                return Success(PagedList<AppealIndexDTO>.ToPagedList(appeals.ProjectTo<AppealIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<AppealIndexDTO>>(ex);
            }
        }

        public byte[] ExportSyndicOrdersToExcel(Guid syndicId)
        {
            try
            {
                IQueryable<Order> orders = GetSyndicOrdersQuery(syndicId);
                return _excelGenerator.ExportGridToExcelXlsxFile(_mapper.Map<List<OrderIndexDTO>>(orders));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                throw;
            }
        }

        public OperationResult<Guid> LockSyndic(Guid id)
        {
            try
            {
                var syndic = _syndicRepository.GetById(id);
                syndic.Locked = true;

                _syndicRepository.Update(syndic);
                _syndicRepository.Save();

                return Success(syndic.Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }

        public OperationResult<Guid> UnlockSyndic(Guid id)
        {
            try
            {
                var syndic = _syndicRepository.GetById(id);
                syndic.Locked = false;

                _syndicRepository.Update(syndic);
                _syndicRepository.Save();

                return Success(syndic.Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }

        public OperationResult<PagedList<CourseIndexDTO>> GetSyndicCourses(Guid syndicId, int pageNumber, int pageSize)
        {
            try
            {
                var courses = GetSyndicCoursesQuery(syndicId);
                return Success(PagedList<CourseIndexDTO>.ToPagedList(courses.ProjectTo<CourseIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<CourseIndexDTO>>(ex);
            }
        }

        public byte[] ExportSyndicCoursesToExcel(Guid syndicId)
        {
            try
            {
                var courses = GetSyndicCoursesQuery(syndicId);
                return _excelGenerator.ExportGridToExcelXlsxFile(_mapper.Map<List<CourseIndexDTO>>(courses));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                throw;
            }
        }

        public OperationResult<PagedList<InstallmentIndexDTO>> GetSyndicInstallments(Guid syndicId, int pageNumber, int pageSize)
        {
            try
            {
                var installments = GetSyndicInstallmentsQuery(syndicId);

                return Success(PagedList<InstallmentIndexDTO>.ToPagedList(installments.OrderByDescending(x => x.PaymentDate)
                              .ProjectTo<InstallmentIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<InstallmentIndexDTO>>(ex);
            }
        }

        public byte[] ExportSyndicInstallmentsToExcel(Guid syndicId)
        {
            try
            {
                var installments = GetSyndicInstallmentsQuery(syndicId);
                return _excelGenerator.ExportGridToExcelXlsxFile(_mapper.Map<List<InstallmentIndexDTO>>(installments.OrderByDescending(x => x.PaymentDate)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                throw;
            }
        }

        public OperationResult<PagedList<CaseIndexDTO>> GetSyndicCases(Guid syndicId, CaseSearchFilter filter, int pageNumber, int pageSize)
        {
            try
            {
                var syndicCases = _caseRepository.Get(filter: x => (x.CaseAssignments.Any(x => x.SyndicId == syndicId))
                                                                && (filter.CourtNumber == null || x.Court.Number == filter.CourtNumber)
                                                                && (filter.CaseYear == null || x.Year == filter.CaseYear)
                                                                && (filter.CaseNumber == null || x.Number.Contains(filter.CaseNumber))
                                                                && (filter.EntityName == null || x.Sides.Any(x => x.Entity.Name.Contains(filter.EntityName)))
                                                                && (filter.Bulstat == null || x.Sides.Any(x => x.Entity.Bulstat == filter.Bulstat))
                                                                && (filter.IsStabilization == null || x.IsStabilization == filter.IsStabilization),
                                                                source => source.Include(x => x.Sides)
                                                                                    .ThenInclude(x => x.Entity)
                                                                                .Include(x => x.Sessions)
                                                                                .Include(x => x.Court)
                                                                                .Include(x => x.CaseKind)
                                                                                .Include(x => x.Judges))
                                                                                .OrderByDescending(x => x.FormationDate)
                                                                                .AsQueryable();

                return Success(PagedList<CaseIndexDTO>.ToPagedList(syndicCases.ProjectTo<CaseIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<CaseIndexDTO>>(ex);
            }
        }

        public OperationResult<PagedList<TemplateIndexDTO>> GetSyndicTemplates(Guid syndicId, SyndicTemplateFilter filter, int pageNumber, int pageSize)
        {
            try
            {
                var syndicTemplate = _templateRepository.Get(x => (x.SyndicId == syndicId)
                                                                    && (filter.TemplateKindId == null || x.TemplateKindId == filter.TemplateKindId)
                                                                    && (filter.FromDate == null || x.Date >= filter.FromDate)
                                                                    && (filter.ToDate == null || x.Date <= filter.ToDate))
                                                        .AsQueryable();

                return Success(PagedList<TemplateIndexDTO>.ToPagedList(syndicTemplate.OrderByDescending(x => x.DateCreated).ProjectTo<TemplateIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<TemplateIndexDTO>>(ex);
            }
        }

        public OperationResult<TemplateIndexDTO> GetSyndicTemplateById(Guid id)
        {
            try
            {
                var template = _templateRepository.GetById(id, source => source.Include(x => x.TemplateKind));

                return Success(_mapper.Map<TemplateIndexDTO>(template));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<TemplateIndexDTO>(ex);
            }
        }

        public OperationResult<Guid> CreateSyndic(SaveSyndicDTO syndicDto)
        {
            try
            {
                var syndicEntity = _mapper.Map<Syndic>(syndicDto);
                syndicEntity.Syndic2Addresses.Add(new Syndic2Address { Address = _mapper.Map<Address>(syndicDto.Address) });

                UpdateSyndicStatus(syndicEntity);

                syndicEntity.User = new User
                {
                    UserName = syndicEntity.Egn,
                    FirstName = syndicEntity.FirstName,
                    SecondName = syndicEntity.SecondName,
                    LastName = syndicEntity.LastName,
                    Egn = syndicEntity.Egn,
                    Email = syndicEntity.Email,
                    IsActive = syndicEntity.Active!.Value,
                    Roles = new List<Role> { new Role { Name = RoleNames.Syndic } }
                };

                _syndicRepository.Add(syndicEntity);
                _syndicRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.CreateSyndic));

                return Success(syndicEntity.Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }

        public OperationResult<SaveSyndicDTO> UpdateSyndic(SaveSyndicDTO syndicDto)
        {
            try
            {
                var syndicEntity = _syndicRepository.GetById((Guid)syndicDto.Id!, source => source.Include(x => x.Syndic2Addresses)
                                                                                                    .ThenInclude(x => x.Address)
                                                                                                  .Include(x => x.User)
                                                                                                  .Include(x => x.SyndicStatus));
                if (syndicEntity == null)
                {
                    return Exception<SaveSyndicDTO>(new Exception("Няма намерен синдик."));
                }

                syndicEntity = _mapper.Map(syndicDto, syndicEntity);
                syndicEntity.Syndic2Addresses.FirstOrDefault().Address = _mapper.Map<Address>(syndicDto.Address);

                UpdateSyndicStatus(syndicEntity);

                syndicEntity.User = _mapper.Map(syndicEntity, syndicEntity.User);

                _syndicRepository.Update(syndicEntity);
                _syndicRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.UpdateSyndic));

                SendSyndicsDataToOpenData();

                return Success(_mapper.Map<SaveSyndicDTO>(syndicEntity));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SaveSyndicDTO>(ex);
            }
        }

        public OperationResult<bool> DeleteSyndic(Guid id)
        {
            try
            {
                var syndicEntity = _syndicRepository.GetById(id);

                if (syndicEntity == null)
                {
                    return Exception<bool>(new Exception("Няма намерен синдик."));
                }

                _syndicRepository.Remove(id);
                _syndicRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.DeleteSyndic));

                SendSyndicsDataToOpenData();

                return Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<bool>(ex);
            }
        }

        public OperationResult<Guid> CreateCustodian(Guid syndicId)
        {
            try
            {
                var syndic = _syndicRepository.GetById(syndicId);

                syndic.IsCustodian = true;

                _syndicRepository.Update(syndic);
                _syndicRepository.Save();

                SendSyndicsDataToOpenData();

                return Success(syndic.Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }

        public OperationResult<PagedList<CourseResultIndexDTO>> GetSyndicCourseResults(Guid syndicId, int pageNumber, int pageSize)
        {
            try
            {
                var courseResults = GetSyndicCourseResultsQuery(syndicId);
                return Success(PagedList<CourseResultIndexDTO>.ToPagedList(courseResults.ProjectTo<CourseResultIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));

            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<CourseResultIndexDTO>>(ex);
            }
        }

        public byte[] ExportSyndicCourseResultsToExcel(Guid syndicId)
        {
            try
            {
                IQueryable<CourseResult> courseResults = GetSyndicCourseResultsQuery(syndicId);
                return _excelGenerator.ExportGridToExcelXlsxFile(_mapper.Map<List<CourseResultIndexDTO>>(courseResults));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                throw;
            }
        }

        public OperationResult<PagedList<CourseApplicationIndexDTO>> GetSyndicCourseApplications(Guid id, int pageNumber, int pageSize)
        {
            try
            {
                var courseApplications = GetSyndicCourseApplicationsQuery(id);
                return Success(PagedList<CourseApplicationIndexDTO>.ToPagedList(courseApplications.ProjectTo<CourseApplicationIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));

            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<CourseApplicationIndexDTO>>(ex);
            }
        }

        public OperationResult<SaveCourseApplicationDTO> UpdateSyndicCourseApplication(SaveCourseApplicationDTO courseApplictionDTO)
        {
            try
            {
                var courseApplicationEntity = _courseApplicationRepository.GetById(courseApplictionDTO.Id.GetValueOrDefault());
                courseApplicationEntity = _mapper.Map(courseApplictionDTO, courseApplicationEntity);

                if (courseApplicationEntity is null)
                {
                    return Exception<SaveCourseApplicationDTO>(new Exception("Няма намерена заявка за курс."));
                }

                _courseApplicationRepository.Update(courseApplicationEntity);
                _courseApplicationRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.UpdateSyndic));
                return Success(_mapper.Map<SaveCourseApplicationDTO>(courseApplicationEntity));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SaveCourseApplicationDTO>(ex);
            }
        }

        public OperationResult<CourseApplicationIndexDTO> GetSyndicApplicationById(Guid courseApplicationId)
        {
            try
            {
                var courseApplicationEntity = _courseApplicationRepository.GetById(courseApplicationId, CourseApplicationIncludeAll);

                return Success(_mapper.Map<CourseApplicationIndexDTO>(courseApplicationEntity));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<CourseApplicationIndexDTO>(ex);
            }
        }

        private IQueryable<Order> GetSyndicOrdersQuery(Guid syndicId)
        {
            return _orderRepository.Get(x => x.SyndicId == syndicId).AsQueryable();
        }

        private IQueryable<Course> GetSyndicCoursesQuery(Guid syndicId)
        {
            return _courseRepository.Get(x => x.CourseApplicationCourses.Any(x => x.SyndicId == syndicId)).AsQueryable();
        }

        private IQueryable<Signal> GetSyndicSignalsQuery(Guid syndicId)
        {
            return _signalRepository.Get(x => x.SyndicId == syndicId).AsQueryable();
        }

        private IQueryable<Appeal> GetSyndicAppealsQuery(Guid syndicId)
        {
            return _appealRepository.Get(x => x.SyndicId == syndicId).AsQueryable();
        }

        private IQueryable<Installment> GetSyndicInstallmentsQuery(Guid syndicId)
        {
            return _installmentRepository.Get(x => x.SyndicId == syndicId, source => source.Include(x => x.Syndic)
                                                                                           .Include(x => x.InstallmentKind)
                                                                                           .Include(x => x.VerifiedByNavigation)
                                                                                           .Include(x => x.InstallmentYear!))
                                                                                           .AsQueryable();
        }

        private IQueryable<Syndic> GetActiveSyndicsQuery()
        {
            return _syndicRepository.Get(filter: x => x.Active == true,
                                             include: source => source.Include(x => x.SpecialtyNavigation)
                                                                      .Include(x => x.Installments)
                                                                      .Include(x => x.Orders)
                                                                      .Include(x => x.SyndicStatus)
                                                                      .Include(x => x.Syndic2Addresses)
                                                                      .ThenInclude(x => x.Address!)
                                                                      .ThenInclude(x => x.Region)
                                                                      .Include(x => x.Syndic2Addresses)
                                                                      .ThenInclude(x => x.Address!)
                                                                      .ThenInclude(x => x.Settlement)
                                                                      .Include(x => x.Syndic2Addresses)
                                                                      .ThenInclude(x => x.Address!)
                                                                      .ThenInclude(x => x.Municipality!))
                                                                      .AsQueryable();
        }

        private IQueryable<Syndic> GetActiveSyndicsWithoutPaidInstallmentQuery()
        {
            return _syndicRepository.Get(filter: x => x.Active == true
                                                 && (!x.Installments.Any() || x.Installments.Max(installment => installment.PaymentDate.Value.Year) < DateTime.Now.Year),
                                             include: source => source.Include(x => x.SpecialtyNavigation)
                                                                      .Include(x => x.Installments)
                                                                      .Include(x => x.Orders)
                                                                      .Include(x => x.SyndicStatus)
                                                                      .Include(x => x.Syndic2Addresses)
                                                                      .ThenInclude(x => x.Address!)
                                                                      .ThenInclude(x => x.Region)
                                                                      .Include(x => x.Syndic2Addresses)
                                                                      .ThenInclude(x => x.Address!)
                                                                      .ThenInclude(x => x.Settlement)
                                                                      .Include(x => x.Syndic2Addresses)
                                                                      .ThenInclude(x => x.Address!)
                                                                      .ThenInclude(x => x.Municipality!))
                                                                      .AsQueryable();
        }

        private IQueryable<CourseResult> GetSyndicCourseResultsQuery(Guid syndicId)
        {
            return _courseResultRepository.Get(x => x.SyndicId == syndicId).AsQueryable();
        }

        private IQueryable<CourseApplication> GetSyndicCourseApplicationsQuery(Guid syndicId)
        {
            return _courseApplicationRepository.Get(x => x.SyndicId == syndicId, CourseApplicationIncludeAll).AsQueryable();
        }


        private static IIncludableQueryable<CourseApplication, object> CourseApplicationIncludeAll<T>(IQueryable<T> query) where T : CourseApplication
        {
            return query.Include(x => x.Syndic)
                .Include(x => x.Course)
                .ThenInclude(x => x.Program)
                .Include(x => x.Course)
                .ThenInclude(x => x.CourseKind);
        }

        public OperationResult<Guid> CreateSyndicCourseApplication(SaveCourseApplicationDTO courseApplicationDTO)
        {
            try
            {
                var courseApplicationEntity = _mapper.Map<CourseApplication>(courseApplicationDTO);

                _courseApplicationRepository.Add(courseApplicationEntity);
                _courseApplicationRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.CreateSyndic));

                return Success(courseApplicationEntity.Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }

        private void UpdateUser(Syndic syndic, User user)
        {
            if (syndic.FirstName != user.FirstName)
            {
                user.FirstName = syndic.FirstName;
            }

            if (syndic.SecondName != user.SecondName)
            {
                user.SecondName = syndic.SecondName;
            }

            if (syndic.LastName != user.LastName)
            {
                user.LastName = syndic.LastName;
            }

            if (syndic.Egn != user.Egn)
            {
                user.Egn = syndic.Egn;
            }

            if (syndic.Email != user.Email)
            {
                user.Email = syndic.Email;
            }

            user.IsActive = syndic.Active!.Value;
            user.UserName = syndic.Egn;
        }

        private void UpdateSyndicStatus(Syndic syndic)
        {
            if (syndic.SyndicStatusId == SyndicStatus.TemporarySuspended ||
                syndic.SyndicStatusId == SyndicStatus.Unlisted ||
                syndic.SyndicStatusId == SyndicStatus.SelfUnlisted ||
                syndic.SyndicStatusId == SyndicStatus.Inactive)
            {
                syndic.Active = false;
            }
            else
            {
                syndic.Active = true;
            }

            if (syndic.SyndicStatusId != null)
            {
                syndic.SyndicStatus = _syndicStatusRepo.GetById(syndic.SyndicStatusId!.Value);
            }
        }

        #region Reports

        public OperationResult<PagedList<ReportIndexDTO>> GetSyndicReports(Guid syndicId, SyndicReportFilter filter, int pageNumber, int pageSize)
        {
            try
            {
                var reports = _reportRepository.Get(x => (x.SyndicId == syndicId)
                                                    && (filter.CaseNumber == null || x.Case.Number == filter.CaseNumber)
                                                    && (filter.CaseYear == null || x.Case.Year == filter.CaseYear)
                                                    && (filter.CourtNumber == null || x.Case.Court.Number == filter.CourtNumber)
                                                    && (filter.ReportTypeId == null || x.ReportTypeId == filter.ReportTypeId)
                                                    && (filter.FromDate == null || x.ReportDate >= filter.FromDate)
                                                    && (filter.ToDate == null || x.ReportDate <= filter.ToDate),
                                            source => source
                                            .Include(x => x.ReportType)
                                            .Include(x => x.Syndic))
                                            .AsQueryable();

                return Success(PagedList<ReportIndexDTO>.ToPagedList(reports.OrderByDescending(x => x.ReportDate).ProjectTo<ReportIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<ReportIndexDTO>>(ex);
            }
        }

        public OperationResult<DetailsSyndicReportDTO> GetSyndicReportById(Guid reportId)
        {
            try
            {
                var template = _reportRepository.GetById(reportId, source => source.Include(x => x.Case)
                                                                                        .ThenInclude(x => x.Sides)
                                                                                        .ThenInclude(x => x.Entity)
                                                                                    .Include(x => x.Case)
                                                                                        .ThenInclude(x => x.Court)
                                                                                    .Include(x => x.Syndic)
                                                                                    .Include(x => x.ReportType));

                return Success(_mapper.Map<DetailsSyndicReportDTO>(template));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<DetailsSyndicReportDTO>(ex);
            }
        }

        #endregion


        #region Plea

        public OperationResult<SavePleaDTO> GetSyndicPleaById(Guid id)
        {
            try
            {
                var plea = _pleaRepository.GetById(id, include: source => source.Include(x => x.Syndic));

                return Success(_mapper.Map<SavePleaDTO>(plea));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SavePleaDTO>(ex);
            }
        }


        public OperationResult<PagedList<PleaIndexDTO>> GetSyndicPleas(Guid syndicId, int pageNumber, int pageSize)
        {
            try
            {
                var plea = GetSyndicPleasQuery(syndicId);

                return Success(PagedList<PleaIndexDTO>.ToPagedList(plea.ProjectTo<PleaIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<PleaIndexDTO>>(ex);
            }
        }

        private IQueryable<Plea> GetSyndicPleasQuery(Guid syndicId)
        {
            return _pleaRepository.Get(x => x.SyndicId == syndicId).AsQueryable();
        }

        public OperationResult<Guid> CreatePlea(SavePleaDTO pleaDTO)
        {
            try
            {
                var pleaEntity = _mapper.Map<Plea>(pleaDTO);

                _pleaRepository.Add(pleaEntity);
                _pleaRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.CreatePlea));

                return Success(pleaEntity.Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }

        public OperationResult<SavePleaDTO> UpdatePlea(SavePleaDTO pleaDTO)
        {
            try
            {
                var pleaEntity = _pleaRepository.GetById((Guid)pleaDTO.Id!);

                if (pleaEntity == null)
                {
                    return Exception<SavePleaDTO>(new Exception("Жалба не е намерена."));
                }

                _mapper.Map(pleaDTO, pleaEntity);

                _pleaRepository.Update(pleaEntity);
                _pleaRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.UpdatePlea));

                return Success(_mapper.Map<SavePleaDTO>(pleaEntity));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SavePleaDTO>(ex);
            }
        }

        #endregion


        #region OPENDATA
        public void SendSyndicsDataToOpenData()
        {

            var syndics = GetActiveSyndicsQuery();
            var syndicsDtos = syndics.ProjectTo<SyndicIndexDTO>(_mapper.ConfigurationProvider).ToList();

            int number = 1;

            foreach (var syndic in syndicsDtos)
            {
                syndic.Number = number++.ToString();
            }



            List<OpenDataSyndicDTO> SyndicsList = new List<OpenDataSyndicDTO>();

            for (int i = 0; i < syndicsDtos.Count; i++)
            {
                OpenDataSyndicDTO SyndicRecord = new OpenDataSyndicDTO();
                SyndicRecord.Name = GetConcatenatedSyndicName(syndicsDtos[i]);
                SyndicRecord.Address = syndicsDtos[i].FullAddress;
                SyndicRecord.Specialty = syndicsDtos[i].SyndicSpecialty;
                SyndicRecord.Email = syndicsDtos[i].Email;
                SyndicRecord.PhoneNumber = syndicsDtos[i].Phone;


                SyndicsList.Add(SyndicRecord);
            }


            using (HttpClient client = new HttpClient())
            {
                AddResourceEGOVModel requestData = new AddResourceEGOVModel();

                requestData.api_key = _configuration.GetValue<string>("OpenData:ApiKey");
                requestData.resource_uri = _configuration.GetValue<string>("OpenData:SyndicsResourceId");
                requestData.extension_format = "json";
                requestData.data = SyndicsList;

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
            if (!String.IsNullOrEmpty(source.SecondLastName))
            {
                syndicNames.Add(source.SecondLastName);
            }


            return string.Join(" ", syndicNames);
        }
    }
}
