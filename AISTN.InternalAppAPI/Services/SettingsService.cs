using AISTN.Common.Helper;
using AISTN.Common.Models;
using AISTN.Data.DataModel;
using AISTN.InternalAppAPI.Models;
using AISTN.Repository;
using AISTN.Repository.Attributes;
using AISTN.Repository.Repository;
using AutoMapper;

namespace AISTN.InternalAppAPI.Services
{
    [Injectable]
    public class SettingsService : ServiceBase
    {
        private readonly IGenericRepository<Setting> _settingsRepository;

        public SettingsService(IMapper mapper, ExceptionLogger logger, ExcelGenerator excelGenerator,
                               IGenericRepository<Setting> settingsRepository) 
                : base(mapper, logger, excelGenerator)
        {
            _settingsRepository = settingsRepository;
        }

        public OperationResult<SettingDTO> GetSettingForm()
        {
            try
            {
                var setting = _settingsRepository.GetAll().FirstOrDefault();

                if (setting == null)
                {
                    return Exception<SettingDTO>(new Exception("Няма създадена форма за настройки."));
                }

                return Success(_mapper.Map<SettingDTO>(setting));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SettingDTO>(ex);
            }
        }

        public OperationResult<SettingDTO> CreateSetting(SettingDTO settingDTO)
        {
            try
            {
                var settings = _settingsRepository.GetAll();

                if (settings.Count() >= 1)
                {
                    return Exception<SettingDTO>(new Exception("Вече съществува форма за настройки."));
                }

                var settingEntity = _mapper.Map<Setting>(settingDTO);

                _settingsRepository.Add(settingEntity);
                _settingsRepository.Save();

                return Success(_mapper.Map<SettingDTO>(settingEntity));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SettingDTO>(ex);
            }
        }

        public OperationResult<SettingDTO> UpdateSetting(SettingDTO settingDTO)
        {
            try
            {
                var setting = _settingsRepository.GetAll().FirstOrDefault();

                if (setting == null)
                {
                    return Exception<SettingDTO>(new Exception("Няма създадена форма за настройки."));
                }

                _mapper.Map(settingDTO, setting);

                _settingsRepository.Update(setting);
                _settingsRepository.Save();

                return Success(_mapper.Map<SettingDTO>(setting));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SettingDTO>(ex);
            }
        }
    }
}
