using AISTN.Common.Helper;
using AISTN.ExternalAppAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AISTN.ExternalAppAPI.Controllers
{
    [Authorize(Roles = $"{RoleNames.MEIEmployee},{RoleNames.Syndic}")]
    [Route("api/ExternalApp/[controller]/[action]")]
    [ApiController]
    public class ProgramController : Controller
    {
        private readonly ProgramService _programService;

        public ProgramController(ProgramService programService)
        {
            _programService = programService;
        }

        [HttpPost]
        public IActionResult GetProgramById(Guid id)
        {
            return Ok(_programService.GetProgramById(id));
        }

        [HttpPost]
        public IActionResult GetAllPrograms(int pageNumber, int pageSize)
        {
            return Ok(_programService.GetAllPrograms(pageNumber, pageSize));
        }

        [HttpPost]
        public IActionResult GetProgramCourseApplications(Guid id, int pageNumber, int pageSize)
        {
            return Ok(_programService.GetProgramCourseApplications(id, pageNumber, pageSize));
        }

        [HttpPost]
        public IActionResult GetProgramCourses(Guid id, int pageNumber, int pageSize)
        {
            return Ok(_programService.GetProgramCourses(id, pageNumber, pageSize));
        }
    }
}
