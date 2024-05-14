using AISTN.Common.Helper;
using AISTN.InternalAppAPI.Models.Filter;
using AISTN.InternalAppAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AISTN.InternalAppAPI.Controllers
{
    [Authorize(Roles = $"{RoleNames.Administrator},{RoleNames.Employee},{RoleNames.Inspector}")]
    [Route("api/InternalApp/[controller]/[action]")]
    [ApiController]
    public class AnnouncementController : Controller
    {
        private readonly AnnouncementService _announcementService;

        public AnnouncementController(AnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        [HttpGet]
        public IActionResult GetAnnouncementById(Guid id)
        {
            return Ok(_announcementService.GetAnnouncementById(id));
        }

        [HttpPost]
        public IActionResult SearchCurrentAnnouncement(int pageNumber, int pageSize, [FromBody] AnnouncementSearchFilter filter)
        {
            return Ok(_announcementService.SearchCurrentAnnouncement(pageNumber, pageSize, filter));
        }

        [HttpPost]
        public IActionResult SearchFinishedAnnouncement(int pageNumber, int pageSize, [FromBody] AnnouncementSearchFilter filter)
        {
            return Ok(_announcementService.SearchFinishedAnnouncement(pageNumber, pageSize, filter));
        }

        [HttpGet]
        public IActionResult GetObjectById(Guid id)
        {
            return Ok(_announcementService.GetObjectById(id));
        }

    }
}
