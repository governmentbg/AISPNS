using AISTN.Common.Services;
using AISTN.PublicAppAPI.Models.Filters;
using AISTN.PublicAppAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AISTN.PublicAppAPI.Controllers
{
    //[Authorize]
    [Route("api/PublicApp/[controller]/[action]")]
    [ApiController]
    public class AnnouncementController : Controller
    {
        private readonly AnnouncementService _announcementService;
        private readonly ContactFormService _contactFormService;
        private readonly CaseService _caseService;

        public AnnouncementController(AnnouncementService announcementService, ContactFormService contactFormService, CaseService caseService)
        {
            _announcementService = announcementService;
            _contactFormService = contactFormService;
            _caseService = caseService;
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

        [HttpPost]
        public IActionResult GetContactForm()
        {
            return Ok(_contactFormService.GetContactForm());
        }
    }
}
