using AISTN.Common.Helper;
using AISTN.Common.Services;
using AISTN.Data.DataModel;
using AISTN.InternalAppAPI.Models.Save;
using AISTN.Repository;
using AISTN.Repository.Attributes;
using AISTN.Repository.Repository;
using AutoMapper;

namespace AISTN.InternalAppAPI.Services
{
    [Injectable]
    public class AppealService : ServiceBase
    {
        private readonly IGenericRepository<Appeal> _appealRepository;

        public AppealService(IMapper mapper, ExceptionLogger logger,
                             ExcelGenerator excelGenerator,
                             IGenericRepository<Appeal> appealRepository,
                             UserService userService,
                             IHttpContextAccessor contextAccessor) 
            : base(mapper, logger, excelGenerator)
        {
            SetCurrentUser(userService.GetCurrentUser(contextAccessor.HttpContext!).ResultData);
            _appealRepository = appealRepository;
        }

        public OperationResult<SaveAppealDTO> GetAppealById(Guid id)
        {
            try
            {
                var appeal = _appealRepository.GetById(id);

                if (appeal == null)
                {
                    return Exception<SaveAppealDTO>(new Exception("Няма намерено обжалване."));
                }

                return Success(_mapper.Map<SaveAppealDTO>(appeal));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SaveAppealDTO>(ex);
            }
        }

        public OperationResult<Guid> CreateAppeal(SaveAppealDTO appealDTO)
        {
            try
            {
                var appeal = _mapper.Map<Appeal>(appealDTO);

                _appealRepository.Add(appeal);
                _appealRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.CreateAppeal));

                return Success(appeal.Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }

        public OperationResult<SaveAppealDTO> UpdateAppeal(SaveAppealDTO appealDTO)
        {
            try
            {
                var appeal = _appealRepository.GetById(appealDTO.Id.Value);

                if (appeal == null)
                {
                    return Exception<SaveAppealDTO>(new Exception("Няма намерено обжалване."));
                }

                var mappedAppeal = _mapper.Map(appealDTO, appeal);

                _appealRepository.Update(appeal);
                _appealRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.UpdateAppeal));

                return Success(_mapper.Map<SaveAppealDTO>(mappedAppeal));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SaveAppealDTO>(ex);
            }
        }

        public OperationResult<bool> DeleteAppeal(Guid id)
        {
            try
            {
                var appeal = _appealRepository.GetById(id);

                if (appeal == null)
                {
                    return Exception<bool>(new Exception("Няма намерено обжалване."));
                }

                _appealRepository.Remove(appeal.Id);
                _appealRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.DeleteAppeal));

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
