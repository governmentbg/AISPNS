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
        public IActionResult GetProgramCourses(Guid id, int pageNumber, int pageSize)
        {
            return Ok(_programService.GetProgramCourses(id, pageNumber, pageSize));
        }


        [HttpPost]
        public IActionResult GetProgramCourseApplications(Guid id, int pageNumber, int pageSize)
        {
            return Ok(_programService.GetProgramCourseApplications(id, pageNumber, pageSize));
        }

        [HttpPost]
        public IActionResult GetProgramEnrolledParticipants(Guid id, int pageNumber, int pageSize)
        {
            return Ok(_programService.GetProgramEnrolledParticipants(id, pageNumber, pageSize));
        }

        [HttpPost]
        public IActionResult SendEnrolledSyndicsEmail(Guid id)
        {
            return Ok(_programService.SendEnrolledSyndicsEmail(id));
        }

        [HttpPost]
        public IActionResult SendPublishedProgramEmail(Guid id)
        {
            return Ok(_programService.SendPublishedProgramEmail(id));
        }

        [HttpPost]
        public IActionResult GetProgramDiscardedParticipants(Guid id, int pageNumber, int pageSize)
        {
            return Ok(_programService.GetProgramDiscardedParticipants(id, pageNumber, pageSize));
        }

        [HttpPost]
        public IActionResult CreateProgram(SaveProgramDTO programDTO)
        {
            return Ok(_programService.CreateProgram(programDTO));
        }

        [HttpPost]
        public IActionResult UpdateProgram(SaveProgramDTO programDTO)
        {
            return Ok(_programService.UpdateProgram(programDTO));
        }

        [HttpPost]
        public IActionResult DeleteProgram(Guid id)
        {
            return Ok(_programService.DeleteProgram(id));
        }

        [HttpPatch(("{programId}"))]
        public IActionResult PublishProgram([FromRoute] Guid programId)
        {
            return Ok(_programService.PublishProgram(programId));
        }

        [HttpPost]
        public IActionResult EnrollCourseParticipants(Guid programId)
        {
            return Ok(_programService.EnrollCourseParticipants(programId));
        }

        [HttpPost]
        public IActionResult DiscardCourseParticipants(Guid programId)
        {
            return Ok(_programService.DiscardCourseParticipants(programId));
        }

    }
}
