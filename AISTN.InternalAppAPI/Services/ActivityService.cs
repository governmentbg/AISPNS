using AISTN.Common.Helper;
using AISTN.Common.Models.PageResult;
using AISTN.Common.Services;
using AISTN.Data.DataModel;
using AISTN.InternalAppAPI.Models.Filter;
using AISTN.InternalAppAPI.Models.Index;
using AISTN.Repository;
using AISTN.Repository.Attributes;
using AISTN.Repository.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AISTN.InternalAppAPI.Services;

[Injectable]
public class ActivityService : ServiceBase
{
    private readonly IGenericRepository<Activity> _activityRepository;
    private readonly IConfiguration _configuration;
    private readonly Guid? _userId;
    private readonly IGenericRepository<Syndic> _syndicRepository;
    private readonly Exception _invalidUserException = new Exception("Невалиден потребител.");

    public ActivityService(IMapper mapper, ExceptionLogger logger, ExcelGenerator excelGenerator,
        IGenericRepository<Activity> activityRepository,
        IConfiguration configuration,
        IGenericRepository<Syndic> syndicRepository,
        UserService userService,
        IHttpContextAccessor contextAccessor)
        : base(mapper, logger, excelGenerator)
    {
        SetCurrentUser(userService.GetCurrentUser(contextAccessor.HttpContext!).ResultData);
        _activityRepository = activityRepository;
        _configuration = configuration;
        _syndicRepository = syndicRepository;
        _userId = _currentUser.UserId;
    }

    public OperationResult<PagedList<ActivityIndexDTO>> SearchActivities(int pageNumber, int pageSize, ActivitySearchFilter filter)
    {
        try
        {
            var query = GetActivitiesFiltered(filter);
            return Success(PagedList<ActivityIndexDTO>.ToPagedList(
                query.ProjectTo<ActivityIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
        }
        catch (Exception ex)
        {
            _logger.LogException(ex);
            return Exception<PagedList<ActivityIndexDTO>>(ex);
        }
    }

    public OperationResult<ActivityIndexDTO> GetActivityById(Guid activityId)
    {
        try
        {
            var activity = _activityRepository.GetById(activityId, source => source.Include(x => x.Syndic));
            var activityDTO = _mapper.Map<ActivityIndexDTO>(activity);

            return Success(activityDTO);
        }
        catch (Exception ex)
        {
            _logger.LogException(ex);
            return Exception<ActivityIndexDTO>(ex);
        }
    }

    private IQueryable<Activity> GetActivitiesFiltered(ActivitySearchFilter filter)
    {
        return _activityRepository.Get(filter: x => (filter.CaseId == null || x.CaseId == filter.CaseId),
                                              source => source.Include(x => x.Case)
                                                        .Include(x => x.Syndic)).AsQueryable()
                                                        .OrderBy(x => x.Id);

        //return _activityRepository.Get(filter: x => (filter.CaseId == null || x.CaseId == filter.CaseId)
        //                                            && x.Syndic.UserId == filter.SyndicId,
        //                                      source => source.Include(x => x.Case)
        //                                                .Include(x => x.Syndic)).AsQueryable()
        //                                                .OrderBy(x => x.Id);
    }
}