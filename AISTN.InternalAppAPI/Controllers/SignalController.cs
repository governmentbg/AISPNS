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
    public class SignalController : Controller
    {
        private readonly SignalService _signalService;

        public SignalController(SignalService signalService)
        {
            _signalService = signalService;
        }

        [HttpPost]
        public IActionResult GetSignalById(Guid id)
        {
            return Ok(_signalService.GetSignalById(id));
        }

        [HttpPost]
        public IActionResult CreateSignal(SaveSignalDTO signalDTO)
        {
            return Ok(_signalService.CreateSignal(signalDTO));
        }

        [HttpPost]
        public IActionResult UpdateSignal(SaveSignalDTO signalDTO)
        {
            return Ok(_signalService.UpdateSignal(signalDTO));
        }

        [HttpPost]
        public IActionResult DeleteSignal(Guid id)
        {
            return Ok(_signalService.DeleteSignal(id));
        }
    }
}
