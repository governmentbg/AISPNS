using AISTN.Common.Helper;
using AISTN.Common.Models.PageResult;
using AISTN.Common.Services;
using AISTN.Data.DataModel;
using AISTN.InternalAppAPI.Models;
using AISTN.InternalAppAPI.Models.Index;
using AISTN.InternalAppAPI.Models.Save;
using AISTN.Repository;
using AISTN.Repository.Attributes;
using AISTN.Repository.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AISTN.InternalAppAPI.Services
{
    [Injectable]
    public class CourseService : ServiceBase
    {
        private readonly IGenericRepository<Course> _courseRepository;
        private readonly IGenericRepository<CourseApplication> _courseApplicationRepository;
        private readonly IGenericRepository<CourseParticipation> _courseParticipationRepository;

        public CourseService(IMapper mapper, ExceptionLogger logger,
                             IGenericRepository<Course> courseRepository,
                             IGenericRepository<CourseApplication> courseApplicationRepository,
                             IGenericRepository<CourseParticipation> courseParticipationRepository,
                             UserService userService,
                             IHttpContextAccessor contextAccessor,
                             ExcelGenerator excelGenerator)
            : base(mapper, logger, excelGenerator)
        {
            SetCurrentUser(userService.GetCurrentUser(contextAccessor.HttpContext!).ResultData);
            _courseRepository = courseRepository;
            _courseApplicationRepository = courseApplicationRepository;
            _courseParticipationRepository = courseParticipationRepository;
        }

        public OperationResult<SaveCourseDTO> GetCourseById(Guid id)
        {
            try
            {
                var course = _courseRepository.GetById(id, src => src.Include(x => x.Address1Navigation)
                                                                     .Include(x => x.Address2Navigation!)
                                                                     .Include(x => x.DocumentCollection));

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

        public OperationResult<ProgramIndexDTO> GetCourseProgram(Guid courseId)
        {
            try
            {
                var course = _courseRepository.GetById(courseId, src => src.Include(x => x.Program));

                if (course == null)
                {
                    return Exception<ProgramIndexDTO>(new Exception("Няма намерено обучение."));
                }

                return Success(_mapper.Map<ProgramIndexDTO>(course.Program));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<ProgramIndexDTO>(ex);
            }
        }

        public OperationResult<PagedList<CourseApplicationIndexDTO>> GetCourseApplications(Guid courseId, int pageNumber, int pageSize)
        {
            try
            {
                var query = GetCourseApplicationsQuery(courseId);
                return Success(PagedList<CourseApplicationIndexDTO>.ToPagedList(query.ProjectTo<CourseApplicationIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<CourseApplicationIndexDTO>>(ex);
            }
        }

        private IQueryable<CourseApplication> GetCourseApplicationsQuery(Guid courseId)
        {
            var courseApplications = _courseApplicationRepository.Get(x => x.CourseId == courseId, src => src.Include(x => x.Course)
                                                                                                                                            .Include(x => x.AlternateCourse)
                                                                                                                                            .Include(x => x.Syndic))
                                                                                                                                            .AsQueryable();
            return courseApplications;
        }

        public OperationResult<PagedList<CourseApplicationIndexDTO>> GetEnrolledDate1(Guid courseId, int pageNumber, int pageSize)
        {
            try
            {
                var query = GetCourseApplicationsDate1Query(courseId);
                return Success(PagedList<CourseApplicationIndexDTO>.ToPagedList(query.ProjectTo<CourseApplicationIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<CourseApplicationIndexDTO>>(ex);
            }
        }

        private IQueryable<CourseApplication> GetCourseApplicationsDate1Query(Guid courseId)
        {
            return _courseApplicationRepository.Get(x => x.CourseId == courseId && x.CourseParticipations.Any(x => x.EnrolledDate1 == true),
                                                                                 src => src.Include(x => x.Course)
                                                                                           .Include(x => x.AlternateCourse)
                                                                                           .Include(x => x.Syndic))
                                                                                           .AsQueryable();
        }

        public OperationResult<PagedList<CourseApplicationIndexDTO>> GetEnrolledDate2(Guid courseId, int pageNumber, int pageSize)
        {
            try
            {
                var query = GetCourseApplicationsDate2Query(courseId);
                return Success(PagedList<CourseApplicationIndexDTO>.ToPagedList(query.ProjectTo<CourseApplicationIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<CourseApplicationIndexDTO>>(ex);
            }
        }

        private IQueryable<CourseApplication> GetCourseApplicationsDate2Query(Guid courseId)
        {
            return _courseApplicationRepository.Get(x => x.CourseId == courseId && x.CourseParticipations.Any(x => x.EnrolledDate2 == true),
                                                                                 src => src.Include(x => x.Course)
                                                                                           .Include(x => x.AlternateCourse)
                                                                                           .Include(x => x.Syndic))
                                                                                           .AsQueryable();
        }


        public OperationResult<PagedList<CourseParticipationDTO>> GetEnrolledParticipants(Guid couurseId, int pageNumber, int pageSize)
        {
            try
            {
                var courseApplications = _courseParticipationRepository.Get(x => x.CourseApplication.CourseId == couurseId && x.CourseApplication.CourseParticipations.Any(), src => src.Include(x => x.Syndic)
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

        private IQueryable<CourseApplication> GetEnrolledCourseApplicationsQuery(Guid courseId)
        {
            return _courseApplicationRepository.Get(x => x.CourseId == courseId && x.CourseParticipations.Any(), src => src.Include(x => x.Course)
                                                                                           .Include(x => x.AlternateCourse)
                                                                                           .Include(x => x.Syndic))
                                                                                           .AsQueryable();
        }

        public OperationResult<bool> RemoveCourseApplicaion(Guid courseApplicationId)
        {
            try
            {
                var courseApplicationEntity = _courseApplicationRepository.GetById(courseApplicationId);

                if (courseApplicationEntity == null)
                {
                    return Exception<bool>(new Exception("Заявката не беше намерена."));
                }

                _courseApplicationRepository.Remove(courseApplicationId);
                _courseApplicationRepository.Save();

                return Success(true);

            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<bool>(ex);
            }
        }

        public OperationResult<List<CourseParticipationDTO>> GetCourseParticipants(Guid courseId)
        {
            try
            {
                var participants = _courseParticipationRepository.Get(x => x.CourseApplication.CourseId == courseId);

                if (participants.Count() <= 0)
                {
                    return Exception<List<CourseParticipationDTO>>(new Exception("Няма намерени участници."));
                }

                return Success(_mapper.Map<List<CourseParticipationDTO>>(participants));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<CourseParticipationDTO>>(ex);
            }
        }

        public OperationResult<Guid> CreateCourse(SaveCourseDTO courseDTO)
        {
            try
            {
                var courseEntity = _mapper.Map<Course>(courseDTO);

                _courseRepository.Add(courseEntity);
                _courseRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.CreateCourse));

                return Success(courseEntity.Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }

        public OperationResult<SaveCourseDTO> UpdateCourse(SaveCourseDTO courseDTO)
        {
            try
            {
                var courseEntity = _courseRepository.GetById(courseDTO.Id.Value);

                if (courseEntity == null)
                {
                    return Exception<SaveCourseDTO>(new Exception("Няма намерено обучение."));
                }

                _mapper.Map(courseDTO, courseEntity);

                _courseRepository.Update(courseEntity);
                _courseRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.UpdateCourse));

                return Success(_mapper.Map<SaveCourseDTO>(courseEntity));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SaveCourseDTO>(ex);
            }
        }

        public OperationResult<bool> DeleteCourse(Guid courseId)
        {
            try
            {
                var courseEntity = _courseRepository.GetById(courseId);

                if (courseEntity == null)
                {
                    return Exception<bool>(new Exception("Няма намерено обучение."));
                }

                _courseRepository.Remove(courseId);
                _courseRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.DeleteCourse));

                return Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<bool>(ex);
            }
        }

        public OperationResult<bool> MarkCourseParticipantCourseAsPassed(Guid courseParticipantId)
        {
            try
            {
                var courseParticipant = _courseParticipationRepository.GetById(courseParticipantId);

                courseParticipant.PassedTheCourse = true;
                _courseParticipationRepository.Update(courseParticipant);
                _courseParticipationRepository.Save();

                return Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<bool>(ex);
            }
        }

        public OperationResult<bool> MarkCourseParticipantCourseAsNotPassed(Guid courseParticipantId)
        {
            try
            {
                var courseParticipant = _courseParticipationRepository.GetById(courseParticipantId);

                courseParticipant.PassedTheCourse = false;
                _courseParticipationRepository.Update(courseParticipant);
                _courseParticipationRepository.Save();

                return Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<bool>(ex);
            }
        }
    }
}
