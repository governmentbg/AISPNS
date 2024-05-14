using AISTN.Common.Helper;
using AISTN.Common.Models;
using AISTN.Common.Models.PageResult;
using AISTN.Common.Services;
using AISTN.Data.DataModel;
using AISTN.ExternalAppAPI.Models.Index;
using AISTN.Repository;
using AISTN.Repository.Attributes;
using AISTN.Repository.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AISTN.ExternalAppAPI.Services;

[Injectable]
public class ProgramService : ServiceBase
{
    private readonly IGenericRepository<AISTN.Data.DataModel.Program> _programRepository;
    private readonly IGenericRepository<CourseApplication> _courseApplication;
    private readonly IGenericRepository<Course> _course;
    private readonly IGenericRepository<CourseParticipation> _courseParticipationRepository;
    private readonly IGenericRepository<Syndic> _syndicRepository;
    private readonly Guid? _userId;

    public ProgramService(IMapper mapper,
                          ExceptionLogger logger,
                          IGenericRepository<AISTN.Data.DataModel.Program> programRepository,
                          UserService userService,
                          IHttpContextAccessor contextAccessor,
                          ExcelGenerator excelGenerator,
                          IGenericRepository<CourseApplication> courseApplication,
                          IGenericRepository<Course> course,
                          IGenericRepository<CourseParticipation> courseParticipationRepository,
                          IGenericRepository<Syndic> syndicRepository)
            : base(mapper, logger, excelGenerator)
    {
        SetCurrentUser(userService.GetCurrentUser(contextAccessor.HttpContext!).ResultData);
        _programRepository = programRepository;
        _courseApplication = courseApplication;
        _course = course;
        _courseParticipationRepository = courseParticipationRepository;
        _syndicRepository = syndicRepository;
        _userId = _currentUser.UserId;
    }

    public OperationResult<ProgramIndexDTO> GetProgramById(Guid id)
    {
        try
        {
            var program = _programRepository.GetById(id, src => src.Include(x => x.Courses));

            if (program == null)
            {
                return Exception<ProgramIndexDTO>(new Exception("Няма намерена програма."));
            }

            return Success(_mapper.Map<ProgramIndexDTO>(program));
        }
        catch (Exception ex)
        {
            _logger.LogException(ex);
            return Exception<ProgramIndexDTO>(ex);
        }
    }

    public OperationResult<PagedList<ProgramIndexDTO>> GetAllPrograms(int pageNumber, int pageSize)
    {
        try
        {
            var programs = _programRepository.Get(null, x => x.Include(x => x.Courses)).AsQueryable();

            if (!programs.Any())
            {
                return Exception<PagedList<ProgramIndexDTO>>(new Exception("Няма намерени програми."));
            }

            return Success(PagedList<ProgramIndexDTO>.ToPagedList(programs.ProjectTo<ProgramIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
        }
        catch (Exception ex)
        {
            _logger.LogException(ex);
            return Exception<PagedList<ProgramIndexDTO>>(ex);
        }
    }

    public OperationResult<PagedList<CourseApplicationIndexDTO>> GetProgramCourseApplications(Guid programId, int pageNumber, int pageSize)
    {
        try
        {
            if (_userId is null || _userId.Value == Guid.Empty)
            {
                return Exception<PagedList<CourseApplicationIndexDTO>>(new Exception("Невалиден потребител."));
            }

            var courseApplications = _courseApplication.Get(x => x.Course.ProgramId == programId && x.Syndic.UserId == _userId, src => src.Include(x => x.Syndic)
                .Include(x => x.Course)
                .ThenInclude(x => x.CourseKind!))
                .AsQueryable();
            return Success(PagedList<CourseApplicationIndexDTO>.ToPagedList(courseApplications.ProjectTo<CourseApplicationIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));

        }
        catch (Exception ex)
        {
            _logger.LogException(ex);
            return Exception<PagedList<CourseApplicationIndexDTO>>(ex);
        }
    }

    public OperationResult<PagedList<CourseIndexDTO>> GetProgramCourses(Guid programId, int pageNumber, int pageSize)
    {
        try
        {
            var courese = _course.Get(x => x.ProgramId == programId, source => source.Include(x => x.CourseKind)).AsQueryable();
            return Success(PagedList<CourseIndexDTO>.ToPagedList(courese.ProjectTo<CourseIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));

        }
        catch (Exception ex)
        {
            _logger.LogException(ex);
            return Exception<PagedList<CourseIndexDTO>>(ex);
        }
    }
}