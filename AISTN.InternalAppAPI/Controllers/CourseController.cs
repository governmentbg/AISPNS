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
    public class CourseController : Controller
    {
        private readonly CourseService _courseService;
        private readonly SyndicService _syndicService;

        public CourseController(CourseService courseService, SyndicService syndicService)
        {
            _courseService = courseService;
            _syndicService = syndicService;
        }

        [HttpPost]
        public IActionResult GetCourseById(Guid id)
        {
            return Ok(_courseService.GetCourseById(id));
        }

        [HttpPost]
        public IActionResult GetCourseProgram(Guid courseId)
        {
            return Ok(_courseService.GetCourseProgram(courseId));
        }

        [HttpPost]
        public IActionResult GetCourseParticipants(Guid courseId)
        {
            return Ok(_courseService.GetCourseParticipants(courseId));
        }

        [HttpPost]
        public IActionResult GetCourseApplications(Guid courseId, int pageNumber, int pageSize)
        {
            return Ok(_courseService.GetCourseApplications(courseId, pageNumber, pageSize));
        }

        [HttpPost]
        public IActionResult GetEnrolledDate1(Guid courseId, int pageNumber, int pageSize)
        {
            return Ok(_courseService.GetEnrolledDate1(courseId, pageNumber, pageSize));
        }

        [HttpPost]
        public IActionResult GetEnrolledDate2(Guid courseId, int pageNumber, int pageSize)
        {
            return Ok(_courseService.GetEnrolledDate2(courseId, pageNumber, pageSize));
        }      
        
        [HttpPost]
        public IActionResult GetEnrolledParticipants(Guid courseId, int pageNumber, int pageSize)
        {
            return Ok(_courseService.GetEnrolledParticipants(courseId, pageNumber, pageSize));
        }

        [HttpPost]
        public IActionResult CreateCourse(SaveCourseDTO courseDTO)
        {
            return Ok(_courseService.CreateCourse(courseDTO));
        }

        [HttpPost]
        public IActionResult UpdateCourse(SaveCourseDTO courseDTO)
        {
            return Ok(_courseService.UpdateCourse(courseDTO));
        }

        [HttpPost]
        public IActionResult DeleteCourse(Guid courseId)
        {
            return Ok(_courseService.DeleteCourse(courseId));
        }

        [HttpPost]
        public IActionResult MarkCourseParticipantCourseAsPassed(Guid courseParticipantId)
        {
            return Ok(_courseService.MarkCourseParticipantCourseAsPassed(courseParticipantId));
        }

        [HttpPost]
        public IActionResult MarkCourseParticipantCourseAsNotPassed(Guid courseParticipantId)
        {
            return Ok(_courseService.MarkCourseParticipantCourseAsNotPassed(courseParticipantId));
        }
    }
}
