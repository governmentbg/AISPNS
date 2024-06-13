using AISTN.Common.Helper;
using AISTN.InternalAppAPI.Models;
using AISTN.InternalAppAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AISTN.InternalAppAPI.Controllers
{
    [Authorize(Roles = $"{RoleNames.Administrator},{RoleNames.Employee},{RoleNames.Inspector}")]
    [Route("api/InternalApp/[controller]/[action]")]
    [ApiController]
    public class SettingsController : Controller
    {
        private readonly SettingsService _settingsService;

        public SettingsController(SettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        [HttpPost]
        public IActionResult GetSettingForm()
        {
            return Ok(_settingsService.GetSettingForm());
        }

        [HttpPost]
        public IActionResult CreateSettingForm([FromBody] SettingDTO settingDTO)
        {
            return Ok(_settingsService.CreateSetting(settingDTO));
        }

        [HttpPost]
        public IActionResult UpdateSettingForm([FromBody] SettingDTO settingDTO)
        {
            return Ok(_settingsService.UpdateSetting(settingDTO));
        }
    }
}
