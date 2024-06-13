using AISTN.Common.Helper;
using AISTN.Common.Models;
using AISTN.Common.Models.PageResult;
using AISTN.Common.Models.Save;
using AISTN.Common.Services;
using AISTN.Data.DataModel;
using AISTN.ExternalAppAPI.Models.Index;
using AISTN.ExternalAppAPI.Models.Save;
using AISTN.InternalAppAPI.Models;
using AISTN.Repository;
using AISTN.Repository.Attributes;
using AISTN.Repository.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.EntityFrameworkCore;

namespace AISTN.ExternalAppAPI.Services;

[Injectable]
public class CourseService : ServiceBase
{
    private readonly IGenericRepository<Course> _courseRepository;
    private readonly IGenericRepository<CourseApplication> _courseApplicationRepository;
    private readonly IGenericRepository<CourseParticipation> _courseParticipationRepository;
    private readonly IGenericRepository<CourseResult> _courseResultRepository;
    private readonly IGenericRepository<Syndic> _syndicRepository;
    private readonly Guid? _userId;
    private readonly Exception _invalidUserException = new Exception("Невалиден потребител.");

    public CourseService(IMapper mapper, ExceptionLogger logger,
                         IGenericRepository<Course> courseRepository,
                         IGenericRepository<CourseApplication> courseApplicationRepository,
                         IGenericRepository<CourseParticipation> courseParticipationRepository,
                         UserService userService,
                         IHttpContextAccessor contextAccessor,
                         ExcelGenerator excelGenerator,
                         IGenericRepository<CourseResult> courseResultRepository,
                         IGenericRepository<Syndic> syndicRepository)
        : base(mapper, logger, excelGenerator)
    {
        SetCurrentUser(userService.GetCurrentUser(contextAccessor.HttpContext!).ResultData);
        _courseRepository = courseRepository;
        _courseApplicationRepository = courseApplicationRepository;
        _courseParticipationRepository = courseParticipationRepository;
        _courseResultRepository = courseResultRepository;
        _syndicRepository = syndicRepository;
        _userId = _currentUser.UserId;
    }

    public OperationResult<SaveCourseDTO> GetCourseById(Guid id)
    {
        try
        {
            var course = _courseRepository.GetById(id, src => src.Include(x => x.Address1Navigation)
                                                                 .Include(x => x.Address2Navigation!));

            if (course == null)
            {
                return Exception<SaveCourseDTO>(new Exception("Няма намерено обучение."));
            }

            return Success(_mapper.Map<SaveCourseDTO>(course));
        }
        catch (Exception ex)
        {
            _logger.LogException(ex);
            return Exception<SaveCourseDTO>(ex);
        }
    }

    public OperationResult<CourseApplicationIndexDTO> GetApplicationById(Guid courseApplicationId)
    {
        try
        {
            var courseApplicationEntity = _courseApplicationRepository.GetById(courseApplicationId, src => src.Include(x => x.Syndic)
                                                                                                            .Include(x => x.Course)
                                                                                                            .ThenInclude(x => x.Program)
                                                                                                            .Include(x => x.Course)
                                                                                                            .ThenInclude(x => x.CourseKind)
                                                                                                            //.Include(x => x.AlternateCourse)
                                                                                                            /*.ThenInclude(x => x.CourseKind)*/);

            return Success(_mapper.Map<CourseApplicationIndexDTO>(courseApplicationEntity));
        }
        catch (Exception ex)
        {
            _logger.LogException(ex);
            return Exception<CourseApplicationIndexDTO>(ex);
        }
    }

    public OperationResult<PagedList<CourseApplicationIndexDTO>> GetCourseApplications(Guid courseId, int pageNumber, int pageSize)
    {
        try
        {
            if (IsInvalidUser())
            {
                return Exception<PagedList<CourseApplicationIndexDTO>>(_invalidUserException);
            }
            var query = GetCourseApplicationsQuery(courseId);
            return Success(PagedList<CourseApplicationIndexDTO>.ToPagedList(query.ProjectTo<CourseApplicationIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
        }
        catch (Exception ex)
        {
            _logger.LogException(ex);
            return Exception<PagedList<CourseApplicationIndexDTO>>(ex);
        }
    }

    public OperationResult<Guid> CreateCourseApplication(SaveCourseApplicationDTO saveCourseApplicationDTO)
    {
        try
        {
            var syndic = _syndicRepository.Get(x => x.UserId == _userId).FirstOrDefault();

            if (syndic?.UserId != _userId || IsInvalidUser())
            {
                return Exception<Guid>(_invalidUserException);
            }

            saveCourseApplicationDTO.SyndicId = syndic.Id;

            var courseApplicationEntity = _mapper.Map<CourseApplication>(saveCourseApplicationDTO);
            _courseApplicationRepository.Add(courseApplicationEntity);
            _courseApplicationRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.CreateCourseApplication));

            return Success(courseApplicationEntity.Id);
        }
        catch (Exception ex)
        {
            _logger.LogException(ex);
            return Exception<Guid>(ex);
        }
    }

    public OperationResult<PagedList<CourseResultIndexDTO>> GetCourseResults(int pageNumber, int pageSize)
    {
        try
        {
            if (IsInvalidUser())
            {
                return Exception<PagedList<CourseResultIndexDTO>>(_invalidUserException);
            }

            var courseResults = _courseResultRepository.Get(x => x.Syndic.UserId == _userId, source => source.Include(x => x.Syndic).Include(x => x.Course)).AsQueryable();
            return Success(PagedList<CourseResultIndexDTO>.ToPagedList(courseResults.ProjectTo<CourseResultIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
        }
        catch (Exception ex)
        {
            _logger.LogException(ex);
            return Exception<PagedList<CourseResultIndexDTO>>(ex);
        }
    }

    public OperationResult<PagedList<CourseParticipationIndexDTO>> GetCourseParticipationsOfUser(int pageNumber, int pageSize)
    {
        try
        {
            if (IsInvalidUser())
            {
                return Exception<PagedList<CourseParticipationIndexDTO>>(_invalidUserException);
            }



            var courseParticipations = _courseParticipationRepository
                .Get(x => x.Syndic.UserId == _userId, source => source.Include(x => x.Syndic)).AsQueryable();

            return Success(PagedList<CourseParticipationIndexDTO>.ToPagedList(
                courseParticipations.ProjectTo<CourseParticipationIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
        }
        catch (Exception ex)
        {
            _logger.LogException(ex);
            return Exception<PagedList<CourseParticipationIndexDTO>>(ex);
        }
    }

    public OperationResult<PagedList<CourseParticipationDTO>> GetSyndicCourseResults(int pageNumber, int pageSize)
    {
        try
        {
            if (IsInvalidUser())
            {
                return Exception<PagedList<CourseParticipationDTO>>(_invalidUserException);
            }

            var courseApplications = _courseParticipationRepository.Get(x => x.Syndic.UserId == _userId, src => src.Include(x => x.Syndic)
                                                                                                            .Include(x => x.CourseApplication).ThenInclude(x => x.CourseParticipations)
                                                                                                            .Include(x => x.CourseApplication).ThenInclude(x => x.Course).ThenInclude(x => x.CourseKind)).AsQueryable();
            return Success(PagedList<CourseParticipationDTO>.ToPagedList(courseApplications.ProjectTo<CourseParticipationDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));

        }
        catch (Exception ex)
        {
            _logger.LogException(ex);
            return Exception<PagedList<CourseParticipationDTO>>(ex);
        }
    }

    public OperationResult<PagedList<CourseApplicationIndexDTO>> GetSyndicApplications(int pageNumber, int pageSize)
    {
        try
        {
            var query = _courseApplicationRepository.Get(x => x.Syndic.UserId == _userId, src => src.Include(x => x.Course)
                                                                                                   //.Include(x => x.AlternateCourse)
                                                                                                   .Include(x => x.Syndic)
                                                                                                   .Include(x => x.Course.Program))
                                                                                                   .AsQueryable();
            return Success(PagedList<CourseApplicationIndexDTO>.ToPagedList(query.ProjectTo<CourseApplicationIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
        }
        catch (Exception ex)
        {
            _logger.LogException(ex);
            return Exception<PagedList<CourseApplicationIndexDTO>>(ex);
        }
    }

    public OperationResult<PagedList<CourseIndexDTO>> GetUserCourseParticipations(int pageNumber, int pageSize)
    {
        try
        {
            if (IsInvalidUser())
            {
                return Exception<PagedList<CourseIndexDTO>>(_invalidUserException);
            }

            var courses = _courseRepository.Get(x => x.CourseApplicationCourses.Any(x => x.Syndic.UserId == _userId && x.CourseParticipations.Any(x => x.PassedTheCourse == null)),
                source => source.Include(x => x.CourseApplicationCourses).ThenInclude(x => x.Syndic)).AsQueryable();

            return Success(PagedList<CourseIndexDTO>.ToPagedList(
                courses.ProjectTo<CourseIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));

        }
        catch (Exception ex)
        {
            _logger.LogException(ex);
            return Exception<PagedList<CourseIndexDTO>>(ex);
        }



    }

    public OperationResult<PagedList<CourseIndexDTO>> GetCoursesUserHasAppliedTo(int pageNumber, int pageSize)
    {
        try
        {
            if (IsInvalidUser())
            {
                return Exception<PagedList<CourseIndexDTO>>(_invalidUserException);
            }

            var courses = _courseRepository.Get(x => x.CourseApplicationCourses.Any(x => x.Syndic.UserId == _userId),
                source => source.Include(x => x.CourseApplicationCourses).ThenInclude(x => x.Syndic)).AsQueryable();

            return Success(PagedList<CourseIndexDTO>.ToPagedList(
                courses.ProjectTo<CourseIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));

        }
        catch (Exception ex)
        {
            _logger.LogException(ex);
            return Exception<PagedList<CourseIndexDTO>>(ex);
        }



    }


    private IQueryable<CourseApplication> GetCourseApplicationsQuery(Guid courseId)
    {
        return _courseApplicationRepository.Get(x => x.CourseId == courseId && x.Syndic.UserId == _userId, src => src.Include(x => x.Course)
                                                                                       //.Include(x => x.AlternateCourse)
                                                                                       .Include(x => x.Syndic))
                                                                                       .AsQueryable();
    }

    private bool IsInvalidUser()
    {
        return _userId is null || _userId.Value == Guid.Empty;
    }
}