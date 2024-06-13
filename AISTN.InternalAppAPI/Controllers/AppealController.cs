using AISTN.Common.Helper;
using AISTN.InternalAppAPI.Models.Save;
using AISTN.InternalAppAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AISTN.InternalAppAPI.Controllers
{
    [Authorize(Roles = $"{RoleNames.Administrator},{RoleNames.Employee},{RoleNames.Inspector}")]
    [Route("api/InternalApp/[controller]/[action]")]
    [ApiController]
    public class AppealController : Controller
    {
        private readonly AppealService _appealService;

        public AppealController(AppealService appealService)
        {
            _appealService = appealService;
        }

        [HttpGet]
        public IActionResult GetAppealById(Guid id)
        {
            return Ok(_appealService.GetAppealById(id));
        }

        [HttpPost]
        public IActionResult CreateAppeal(SaveAppealDTO appealDTO)
        {
            return Ok(_appealService.CreateAppeal(appealDTO));
        }

        [HttpPost]
        public IActionResult UpdateAppeal(SaveAppealDTO appealDTO)
        {
            return Ok(_appealService.UpdateAppeal(appealDTO));
        }

        [HttpPost]
        public IActionResult DeleteAppeal(Guid id)
        {
            return Ok(_appealService.DeleteAppeal(id));
        }
    }
}
