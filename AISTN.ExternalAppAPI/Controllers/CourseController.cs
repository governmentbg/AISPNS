using AISTN.Common.Helper;
using AISTN.ExternalAppAPI.Models.Save;
using AISTN.ExternalAppAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AISTN.ExternalAppAPI.Controllers
{

    [Authorize(Roles = $"{RoleNames.MEIEmployee},{RoleNames.Syndic}")]
    [Route("api/ExternalApp/[controller]/[action]")]
    [ApiController]
    public class CourseController : Controller
    {
        private readonly CourseService _courseService;

        public CourseController(CourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost]
        public IActionResult GetCourseById(Guid id)
        {
            return Ok(_courseService.GetCourseById(id));
        }

        [HttpPost]
        public IActionResult GetCourseApplicationById(Guid id)
        {
            return Ok(_courseService.GetApplicationById(id));
        }

        [HttpPost]
        public IActionResult GetCourseApplications(Guid courseId, int pageNumber, int pageSize)
        {
            return Ok(_courseService.GetCourseApplications(courseId, pageNumber, pageSize));
        }

        [HttpPost]
        public IActionResult CreateCourseApplication(SaveCourseApplicationDTO saveCourseApplicationDTO)
        {
            return Ok(_courseService.CreateCourseApplication(saveCourseApplicationDTO));
        }

        [HttpGet]
        public IActionResult GetCourseResults(int pageNumber, int pageSize)
        {
            return Ok(_courseService.GetCourseResults(pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult GetCourseParticipationsOfUser(int pageNumber, int pageSize)
        {
            return Ok(_courseService.GetCourseParticipationsOfUser(pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult GetUserCourseParticipations(int pageNumber, int pageSize)
        {
            return Ok(_courseService.GetUserCourseParticipations(pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult GetCoursesUserHasAppliedTo(int pageNumber, int pageSize)
        {
            return Ok(_courseService.GetCoursesUserHasAppliedTo(pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult GetSyndicCourseResults(int pageNumber, int pageSize)
        {
            return Ok(_courseService.GetSyndicCourseResults(pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult GetSyndicCourseApplications(int pageNumber, int pageSize)
        {
            return Ok(_courseService.GetSyndicApplications(pageNumber, pageSize));
        }
    }
}
