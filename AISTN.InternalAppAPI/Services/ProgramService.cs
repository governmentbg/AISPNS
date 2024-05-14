using AISTN.Common.Helper;
using AISTN.Common.Models;
using AISTN.Common.Models.PageResult;
using AISTN.Common.Services;
using AISTN.Data.DataModel;
using AISTN.InternalAppAPI.Models.Index;
using AISTN.InternalAppAPI.Models.Save;
using AISTN.Repository;
using AISTN.Repository.Attributes;
using AISTN.Repository.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DocumentFormat.OpenXml.Office.CustomUI;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MimeKit.Text;
using System.Text;
using CourseIndexDTO = AISTN.InternalAppAPI.Models.Index.CourseIndexDTO;
using ProgramIndexDTO = AISTN.InternalAppAPI.Models.Index.ProgramIndexDTO;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace AISTN.InternalAppAPI.Services
{
    [Injectable]
    public class ProgramService : ServiceBase
    {
        private readonly IGenericRepository<AISTN.Data.DataModel.Program> _programRepository;
        private readonly IGenericRepository<CourseApplication> _courseApplication;
        private readonly IGenericRepository<Course> _course;
        private readonly IGenericRepository<CourseParticipation> _courseParticipationRepository;
        private readonly IGenericRepository<Syndic> _syndicRepository;
        private readonly IGenericRepository<Sender> _senderRepository;
        private readonly MessageService _messageService;
        private readonly EmailService _emailService;

        public ProgramService(IMapper mapper,
                              ExceptionLogger logger,
                              IGenericRepository<AISTN.Data.DataModel.Program> programRepository,
                              UserService userService,
                              IHttpContextAccessor contextAccessor,
                              ExcelGenerator excelGenerator,
                              IGenericRepository<CourseApplication> courseApplication,
                              IGenericRepository<Course> course,
                              IGenericRepository<CourseParticipation> courseParticipationRepository,
                              IGenericRepository<Syndic> syndicRepository,
                              MessageService messageService,
                              IGenericRepository<Sender> senderRepository,
                              EmailService emailService)
                : base(mapper, logger, excelGenerator)
        {
            SetCurrentUser(userService.GetCurrentUser(contextAccessor.HttpContext!).ResultData);
            _programRepository = programRepository;
            _courseApplication = courseApplication;
            _course = course;
            _courseParticipationRepository = courseParticipationRepository;
            _syndicRepository = syndicRepository;
            _messageService = messageService;
            _senderRepository = senderRepository;
            _emailService = emailService;
        }

        public OperationResult<SaveProgramDTO> GetProgramById(Guid id)
        {
            try
            {
                var program = _programRepository.GetById(id, src => src.Include(x => x.Courses));

                if (program == null)
                {
                    return Exception<SaveProgramDTO>(new Exception("Няма намерена програма."));
                }

                return Success(_mapper.Map<SaveProgramDTO>(program));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SaveProgramDTO>(ex);
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

        public OperationResult<Guid> CreateProgram(SaveProgramDTO program)
        {
            try
            {
                var programEntity = _mapper.Map<AISTN.Data.DataModel.Program>(program);

                _programRepository.Add(programEntity);
                _programRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.CreateProgram));

                return Success(programEntity.Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }

        public OperationResult<SaveProgramDTO> UpdateProgram(SaveProgramDTO programDTO)
        {
            try
            {
                if (programDTO == null)
                {
                    return Exception<SaveProgramDTO>(new Exception("Няма намерена програма."));
                }

                var mappedProgram = _mapper.Map<AISTN.Data.DataModel.Program>(programDTO);

                _programRepository.Update(mappedProgram);
                _programRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.UpdateProgram));

                return Success(_mapper.Map<SaveProgramDTO>(mappedProgram));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SaveProgramDTO>(ex);
            }
        }

        public OperationResult<bool> DeleteProgram(Guid programId)
        {
            try
            {
                var program = _programRepository.GetById(programId);

                if (program == null)
                {
                    return Exception<bool>(new Exception("Няма намерена програма."));
                }

                _programRepository.Remove(program.Id);
                _programRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.DeleteProgram));

                return Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<bool>(ex);
            }
        }

        public OperationResult<bool> PublishProgram(Guid programId)
        {
            try
            {
                var program = _programRepository.GetById(programId);

                if (program == null)
                {
                    return Exception<bool>(new Exception("Няма намерена програма."));
                }

                program.Published = true;
                _programRepository.Update(program);
                _programRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.UpdateProgram));

                return Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<bool>(ex);
            }
        }

        public OperationResult<PagedList<CourseApplicationIndexDTO>> GetProgramCourseApplications(Guid programId, int pageNumber, int pageSize)
        {
            try
            {
                var courseApplications = _courseApplication.Get(x => x.Course.ProgramId == programId, src => src.Include(x => x.Syndic)
                                                                                                                .Include(x => x.Course)
                                                                                                                .ThenInclude(x => x.CourseKind!)).AsQueryable();
                return Success(PagedList<CourseApplicationIndexDTO>.ToPagedList(courseApplications.ProjectTo<CourseApplicationIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));

            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<CourseApplicationIndexDTO>>(ex);
            }
        }

        public OperationResult<PagedList<CourseApplicationIndexDTO>> GetProgramEnrolledParticipants(Guid programId, int pageNumber, int pageSize)
        {
            try
            {
                var courseApplications = _courseApplication.Get(x => x.Course.ProgramId == programId && x.CourseParticipations.Any(), src => src.Include(x => x.Syndic)
                                                                                                                                                .Include(x => x.Course)
                                                                                                                                                .Include(x => x.CourseParticipations)
                                                                                                                                                .Include(x => x.Course)
                                                                                                                                                .ThenInclude(x => x.CourseKind!)).AsQueryable();
                return Success(PagedList<CourseApplicationIndexDTO>.ToPagedList(courseApplications.ProjectTo<CourseApplicationIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));

            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<CourseApplicationIndexDTO>>(ex);
            }
        }

        public OperationResult<PagedList<CourseApplicationIndexDTO>> GetProgramDiscardedParticipants(Guid programId, int pageNumber, int pageSize)
        {
            try
            {
                var courseApplications = _courseApplication.Get(x => x.Course.ProgramId == programId && !x.CourseParticipations.Any(), src => src.Include(x => x.Syndic)
                                                                                                                                                 .Include(x => x.CourseParticipations)
                                                                                                                                                 .Include(x => x.Course)
                                                                                                                                                 .Include(x => x.Course)
                                                                                                                                                 .ThenInclude(x => x.CourseKind)).AsQueryable();
                return Success(PagedList<CourseApplicationIndexDTO>.ToPagedList(courseApplications.ProjectTo<CourseApplicationIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));

            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<CourseApplicationIndexDTO>>(ex);
            }
        }

        public OperationResult<bool> EnrollCourseParticipants(Guid programId)
        {
            try
            {
                var courses = _course.Get(x => x.ProgramId == programId, src => src.Include(x => x.Address1Navigation)
                                                                           .Include(x => x.Address2Navigation)
                                                                           .Include(x => x.CourseApplicationCourses).ThenInclude(x => x.CourseParticipations)).ToList();

                if (courses == null) { return OperationResult<bool>.Error("Няма намерена програма"); }

                foreach (var course in courses)
                {
                    var applicationsToEnroll = course.CourseApplicationCourses.OrderBy(x => x.DateCreated).Take(course.MaximumParticipants.Value).ToList();

                    foreach (var application in applicationsToEnroll)
                    {

                        application.CourseParticipations.Add(new CourseParticipation()
                        {
                            SyndicId = application.SyndicId,
                            EnrolledDate1 = application.CourseDate1 == true ? true : false,
                            EnrolledDate2 = application.CourseDate1 == false ? true : false
                        });

                        // try enroll for alternative course
                        if (application.AlternateCourseId.HasValue && application.CourseId != application.AlternateCourseId)
                        {
                            application.CourseParticipations.Add(new CourseParticipation()
                            {
                                SyndicId = application.SyndicId,
                                EnrolledDate1 = application.AlternateCourseDate2 == true ? true : false,
                                EnrolledDate2 = application.AlternateCourseDate2 == false ? true : false
                            });
                        }
                    }

                    _course.Update(course);
                    _course.Save();
                }

                var program = _programRepository.GetById(programId);
                program.IsEnrolled = true;

                _programRepository.Update(program);
                _programRepository.Save();

                return Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<bool>(ex);
            }
        }

        public OperationResult<bool> DiscardCourseParticipants(Guid programId)
        {
            try
            {
                var program = _programRepository.GetById(programId, source => source.Include(x => x.Courses).ThenInclude(x => x.CourseApplicationCourses).ThenInclude(x => x.CourseParticipations));
                program.IsEnrolled = false;


                foreach (var course in program.Courses)
                {
                    foreach (var item in course.CourseApplicationCourses)
                    {
                        _courseParticipationRepository.RemoveRange(item.CourseParticipations);
                        _courseParticipationRepository.Save();
                    }
                }

                _programRepository.Update(program);
                _programRepository.Save();
                return Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<bool>(ex);
            }
        }

        public OperationResult<bool> SendEnrolledSyndicsEmail(Guid programId)
        {
            try
            {
                SendEnrolledSyndicEmails(programId);

                SendUnenrolledSyndicEmails(programId);

                return Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<bool>(ex);
            }
        }

        public OperationResult<bool> SendPublishedProgramEmail(Guid programId)
        {
            try
            {
                var syndics = _syndicRepository.GetAll();
                var program = _programRepository.GetById(programId);

                foreach (var syndic in syndics)
                {
                    var sender = _senderRepository.Get(x => x.UserId == syndic.UserId).FirstOrDefault();
                    var message = new SaveMessageDTO()
                    {
                        MessageReceiverIDs = new List<Guid>()
                        {
                            syndic.UserId.Value
                        },
                        SendToAll = false,
                        Subject = $"Публикувана е нова програма.",
                        Body = $"Публикувана е учебна програма за {program.MojorderDate}. Можете да я видите във Вашият профил."
                    };

                    _messageService.SendMessage(message);

                    _emailService.SendEmail(syndic, EmailReason.PublishedProgram, message.Body);
                }

                return Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<bool>(ex);
            }
        }

        #region PrivateSendEmailMethods

        private void SendEnrolledSyndicEmails(Guid programId)
        {
            try
            {
                var enrolledCourseParticipations = _courseParticipationRepository.Get(x => x.CourseApplication.Course.ProgramId == programId
                                                                                       && (x.EnrolledDate1 == true || x.EnrolledDate2 == true),
                                                                                       src => src.Include(x => x.Syndic)
                                                                                                 .Include(x => x.CourseApplication)
                                                                                                 .ThenInclude(x => x.Course));
                var syndics = enrolledCourseParticipations.Select(x => x.Syndic);
                var program = _programRepository.GetById(programId);

                foreach (var syndic in syndics)
                {
                    var body = new StringBuilder();
                    body.Append($"Вие сте класиран по учебна програма за година {program.MojorderDate} за следните курсове:");
                    var courses = enrolledCourseParticipations.Where(x => x.SyndicId == syndic.Id).Select(x => x.CourseApplication.Course);

                    foreach (var course in courses)
                    {
                        body.Append($"{course.Topic}, от {course.FromDate1}/{course.FromDate2} {DateTime.Now}");
                    }

                    var sender = _senderRepository.Get(x => x.UserId == syndic.UserId).FirstOrDefault();
                    var message = new SaveMessageDTO()
                    {
                        MessageReceiverIDs = new List<Guid>()
                        {
                            syndic.UserId.Value
                        },
                        SendToAll = false,
                        Subject = "Успешно класиране на обучение.",
                        Body = body.ToString()
                    };

                    _messageService.SendMessage(message);

                    _emailService.SendEmail(syndic, EmailReason.EnrolledSyndic, message.Body);
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
            }
        }

        private void SendUnenrolledSyndicEmails(Guid programId)
        {
            try
            {
                var unenrolledCourseParticipations = _courseParticipationRepository.Get(x => x.CourseApplication.Course.ProgramId == programId
                                                                                       && (x.EnrolledDate1 == false || x.EnrolledDate1 == null)
                                                                                       && (x.EnrolledDate2 == false || x.EnrolledDate2 == null),
                                                                                       src => src.Include(x => x.Syndic)
                                                                                       .Include(x => x.CourseApplication)
                                                                                       .ThenInclude(x => x.Course)
                                                                                       .ThenInclude(x => x.CourseKind));

                var syndics = unenrolledCourseParticipations.Select(x => x.Syndic);
                var program = _programRepository.GetById(programId);

                foreach (var syndic in syndics)
                {
                    var courseApplication = _courseApplication.Get(x => x.SyndicId == syndic.Id).FirstOrDefault();

                    var sender = _senderRepository.Get(x => x.UserId == syndic.UserId).FirstOrDefault();
                    var message = new SaveMessageDTO()
                    {
                        MessageReceiverIDs = new List<Guid>()
                        {
                            syndic.UserId.Value
                        },
                        SendToAll = false,
                        Subject = "Успешно класиране на обучение.",
                        Body = $"Информираме Ви, че по Ваша заявка от {courseApplication.DateCreated}" +
                               $"за учебна програма за {program.MojorderDate} година не сте класиран."
                    };

                    _messageService.SendMessage(message);

                    _emailService.SendEmail(syndic, EmailReason.UnenrolledSyndic, message.Body);
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
            }
        }
        #endregion
    }
}
