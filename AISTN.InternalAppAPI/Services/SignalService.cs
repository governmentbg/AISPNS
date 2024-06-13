using AISTN.Common.Helper;
using AISTN.Common.Services;
using AISTN.Data.DataModel;
using AISTN.InternalAppAPI.Models.Save;
using AISTN.Repository;
using AISTN.Repository.Attributes;
using AISTN.Repository.Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AISTN.InternalAppAPI.Services
{
    [Injectable]
    public class SignalService : ServiceBase
    {
        private readonly IGenericRepository<Signal> _signalRepository;

        public SignalService(IMapper mapper, ExceptionLogger logger, ExcelGenerator excelGenerator,
                             UserService userService,
                             IHttpContextAccessor contextAccessor,
                             IGenericRepository<Signal> signalRepository)
              : base(mapper, logger, excelGenerator)
        {
            SetCurrentUser(userService.GetCurrentUser(contextAccessor.HttpContext!).ResultData);
            _signalRepository = signalRepository;
        }

        public OperationResult<SaveSignalDTO> GetSignalById(Guid id)
        {
            try
            {
                var signal = _signalRepository.GetById(id, src => src.Include(x => x.Case)
                                                                     .Include(x => x.DocumentCollection!)
                                                                     .Include(x => x.Sender)
                                                                        .ThenInclude(x => x.Address)!);

                if (signal == null)
                {
                    return Exception<SaveSignalDTO>(new Exception("Няма намерен сигнал."));
                }

                return Success(_mapper.Map<SaveSignalDTO>(signal));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SaveSignalDTO>(ex);
            }
        }

        public OperationResult<Guid> CreateSignal(SaveSignalDTO signalDTO)
        {
            try
            {
                var signal = _mapper.Map<Signal>(signalDTO);

                _signalRepository.Add(signal);
                _signalRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.CreateSignal));

                return Success(signal.Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }

        public OperationResult<SaveSignalDTO> UpdateSignal(SaveSignalDTO signalDTO)
        {
            try
            {
                var signal = _signalRepository.GetById(signalDTO.Id.Value);

                if (signal == null)
                {
                    return Exception<SaveSignalDTO>(new Exception("Няма намерен сигнал."));
                }

                _mapper.Map(signalDTO, signal);

                _signalRepository.Update(signal);
                _signalRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.UpdateSignal));

                return Success(_mapper.Map<SaveSignalDTO>(signal));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SaveSignalDTO>(ex);
            }
        }

        public OperationResult<bool> DeleteSignal(Guid id)
        {
            try
            {
                var signal = _signalRepository.GetById(id);

                if (signal == null)
                {
                    return Exception<bool>(new Exception("Сигналът не беше намерен."));
                }

                _signalRepository.Remove(signal.Id);
                _signalRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.DeleteSignal));

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
