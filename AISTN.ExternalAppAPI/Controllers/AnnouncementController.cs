using Microsoft.AspNetCore.Mvc;
using AISTN.ExternalAppAPI.Models.Filter;
using AISTN.ExternalAppAPI.Services;
using AISTN.ExternalAppAPI.Models.Save;
using Microsoft.AspNetCore.Authorization;
using AISTN.Common.Helper;

namespace AISTN.ExternalAppAPI.Controllers
{
    [Authorize(Roles = $"{RoleNames.MEIEmployee},{RoleNames.Syndic}")]
    [Route("api/ExternalApp/[controller]/[action]")]
    [ApiController]
    public class AnnouncementController : Controller
    {
        private readonly AnnouncementService _announcementService;
        private readonly CaseService _caseService;

        public AnnouncementController(AnnouncementService announcementService, CaseService caseService)
        {
            _announcementService = announcementService;
            _caseService = caseService;
        }

        [HttpGet]
        public IActionResult GetAnnouncementById(Guid id)
        {
            return Ok(_announcementService.GetAnnouncementById(id));
        }

        [HttpGet]
        public IActionResult GetAnnouncementCaseById(Guid id)
        {
            return Ok(_caseService.GetAnnouncementCaseById(id));
        }

        #region MII Announcements

        [Authorize(Roles = RoleNames.MEIEmployee)]
        [HttpPost]
        public IActionResult SearchCurrentAnnouncement(int pageNumber, int pageSize, [FromBody] AnnouncementSearchFilter filter)
        {
            return Ok(_announcementService.SearchCurrentAnnouncement(pageNumber, pageSize, filter));
        }

        [Authorize(Roles = RoleNames.MEIEmployee)]
        [HttpPost]
        public IActionResult SearchFinishedAnnouncement(int pageNumber, int pageSize, [FromBody] AnnouncementSearchFilter filter)
        {
            return Ok(_announcementService.SearchFinishedAnnouncement(pageNumber, pageSize, filter));
        }

        [HttpPost]
        public IActionResult SearchDraftAnnouncement(int pageNumber, int pageSize, [FromBody] AnnouncementSearchFilter filter)
        {
            return Ok(_announcementService.SearchDraftAnnouncement(pageNumber, pageSize, filter));
        }


        #endregion

        #region MII Announcements By Syndics

        [Authorize(Roles = RoleNames.MEIEmployee)]
        [HttpPost]
        public IActionResult SearchRawAnnouncement(int pageNumber, int pageSize, [FromBody] AnnouncementSearchFilter filter)
        {
            return Ok(_announcementService.SearchRawAnnouncement(pageNumber, pageSize, filter));
        }

        [Authorize(Roles = RoleNames.MEIEmployee)]
        [HttpPost]
        public IActionResult SearchAnnouncementOnHold(int pageNumber, int pageSize, [FromBody] AnnouncementSearchFilter filter)
        {
            return Ok(_announcementService.SearchAnnouncementOnHold(pageNumber, pageSize, filter));
        }

        [HttpPost]
        public IActionResult SearchPublishedAnnouncement(int pageNumber, int pageSize, [FromBody] AnnouncementSearchFilter filter)
        {
            return Ok(_announcementService.SearchPublishedAnnouncement(pageNumber, pageSize, filter));
        }

        #endregion

        #region Syndics Announcement

        [Authorize(Roles = RoleNames.Syndic)]
        [HttpPost]
        public IActionResult SearchAnnouncementForCorrection(int pageNumber, int pageSize, [FromBody] AnnouncementSearchFilter filter)
        {
            return Ok(_announcementService.SearchAnnouncementForCorrection(pageNumber, pageSize, filter));
        }

        #endregion


        [HttpPost]
        public IActionResult CreateAnnouncement(SaveAnnouncementDTO announcementDTO)
        {
            return Ok(_announcementService.CreateAnnouncement(announcementDTO));
        }

        [HttpPost]
        public IActionResult UpdateAnnouncement(SaveAnnouncementDTO announcementDTO)
        {
            return Ok(_announcementService.UpdateAnnouncement(announcementDTO));
        }

        [HttpPost]
        public IActionResult CreateObject(SaveObjectDTO objectDTO)
        {
            return Ok(_announcementService.CreateObject(objectDTO));
        }

        [HttpPost]
        public IActionResult UpdateObject(SaveObjectDTO objectDTO)
        {
            return Ok(_announcementService.UpdateObject(objectDTO));
        }

        [Authorize(Roles = RoleNames.Syndic)]
        [HttpPost]
        public IActionResult SendAnnouncementForPublish(Guid id)
        {
            return Ok(_announcementService.SendAnnouncementForPublish(id));
        }

        [Authorize(Roles = RoleNames.MEIEmployee)]
        [HttpPost]
        public IActionResult PublishAnnouncement(Guid id)
        {
            return Ok(_announcementService.PublishAnnouncement(id));
        }

        [Authorize(Roles = RoleNames.MEIEmployee)]
        [HttpPost]
        public IActionResult CancelAnnouncement(Guid id)
        {
            return Ok(_announcementService.CancelAnnouncement(id));
        }

        [Authorize(Roles = RoleNames.MEIEmployee)]
        [HttpPost]
        public IActionResult SendAnnouncementForCorrectionInDraft(Guid id)
        {
            return Ok(_announcementService.SendAnnouncementForCorrectionInDraft(id));
        }


        [Authorize(Roles = RoleNames.MEIEmployee)]
        [HttpPost]
        public IActionResult SendAnnouncementForCorrection([FromBody] CorrectionCommentFilter filter)
        {
            return Ok(_announcementService.SendAnnouncementForCorrection(filter.AnnouncementId, filter.CorrectionComment));
        }

        [HttpGet]
        public IActionResult GetObjectById(Guid id)
        {
            return Ok(_announcementService.GetObjectById(id));
        }

        [HttpPost]
        public IActionResult GetObjectsByAnnouncementId(int pageNumber, int pageSize, Guid announcementId)
        {
            return Ok(_announcementService.GetObjectsByAnnouncementId(pageNumber, pageSize, announcementId));
        }

        [HttpPost]
        public IActionResult DeleteObject(Guid id)
        {
            return Ok(_announcementService.DeleteObjectById(id));
        }
    }
}
