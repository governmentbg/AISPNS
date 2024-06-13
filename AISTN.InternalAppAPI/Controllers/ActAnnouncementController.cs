using AISTN.Common.Helper;
using AISTN.InternalAppAPI.Models.Filter;
using AISTN.InternalAppAPI.Models.Save;
using AISTN.InternalAppAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AISTN.InternalAppAPI.Controllers
{
    [Authorize(Roles = $"{RoleNames.Administrator},{RoleNames.Employee},{RoleNames.Inspector}")]
    [Route("api/InternalApp/[controller]/[action]")]
    [ApiController]
    public class ActAnnouncementController : Controller
    {
        private readonly ActAnnouncementService _actAnnouncementService;

        public ActAnnouncementController(ActAnnouncementService actAnnouncementService)
        {
            _actAnnouncementService = actAnnouncementService;
        }

        #region Act

        [HttpPost]
        public IActionResult SearchActAnnouncement(int pageNumber, int pageSize, [FromBody] ActAnnouncementSearchFilter filter)
        {
            return Ok(_actAnnouncementService.SearchActAnnouncements(pageNumber, pageSize, filter));
        }

        [HttpGet]
        public IActionResult GetActById(Guid id)
        {
            return Ok(_actAnnouncementService.GetActById(id));
        }

        #endregion

        #region ActAnnouncements

        [HttpGet]
        public IActionResult GetActAnnouncementById(Guid id)
        {
            return Ok(_actAnnouncementService.GetActAnnouncementById(id));
        }

        [HttpPost]
        public IActionResult AnnounceAct(SaveActAnnouncementDTO actAnnouncementDTO)
        {
            return Ok(_actAnnouncementService.AnnounceAct(actAnnouncementDTO));
        }

        [HttpPost]
        public IActionResult NoSubjectToAnnouncement(SaveActAnnouncementDTO actAnnouncementDTO)
        {
            return Ok(_actAnnouncementService.NoSubjectToAnnouncement(actAnnouncementDTO));
        }

        [HttpPost]
        public IActionResult GetActAnnouncementsByCaseId(Guid caseId)
        {
            return Ok(_actAnnouncementService.GetActAnnouncementsByCaseId(caseId));
        }

        #endregion

        #region ActRegistrations

        [HttpGet]
        public IActionResult GetAllRegisterEntries(Guid actId, int pageNumber, int pageSize)
        {
            return Ok(_actAnnouncementService.GetAllRegisterEntries(actId, pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult GetRegisterEntryById(Guid id)
        {
            return Ok(_actAnnouncementService.GetRegisterEntryById(id));
        }

        [HttpPost]
        public IActionResult CreateRegisterEntry(SaveRegisterEntryDTO actRegisterEntryDTO)
        {
            return Ok(_actAnnouncementService.CreateRegisterEntry(actRegisterEntryDTO));
        }

        [HttpPost]
        public IActionResult UpdateRegisterEntry(SaveRegisterEntryDTO actRegisterEntryDTO)
        {
            return Ok(_actAnnouncementService.UpdateRegisterEntry(actRegisterEntryDTO));
        }

        [HttpPost]
        public IActionResult DeleteRegisterEntry(Guid id)
        {
            return Ok(_actAnnouncementService.DeleteRegisterEntry(id));
        }

        [HttpPost]
        public IActionResult RegisterAct(SaveActAnnouncementDTO actAnnouncementDTO)
        {
            return Ok(_actAnnouncementService.RegisterAct(actAnnouncementDTO));
        }

        [HttpPost]
        public IActionResult NoSubjectToRegistration(SaveActAnnouncementDTO actAnnouncementDTO)
        {
            return Ok(_actAnnouncementService.NoSubjectToRegistration(actAnnouncementDTO));
        }

        [HttpPost]
        public IActionResult GetRegisterEntriesByCaseId(Guid caseId)
        {
            return Ok(_actAnnouncementService.GetRegisterEntriesByCaseId(caseId));
        }

        [HttpPost]
        public IActionResult GetRegisterEntryHistory(Guid registerEntryId)
        {
            return Ok(_actAnnouncementService.GetRegisterEntryHistory(registerEntryId));
        }

        #endregion

        [HttpPost]
        public FileResult DownloadActImage(Guid actId)
        {
            var result = _actAnnouncementService.GetActById(actId).ResultData;

            return File(result.Image, "application/json", "Акт");
        }

        [HttpPost]
        public FileResult DownloadActLetterImage(Guid actId)
        {
            var result = _actAnnouncementService.GetActById(actId).ResultData;

            return File(result.OriginalLetterImage, "application/json", "Писмо");
        }

        [HttpPost]
        public FileResult DownloadRedactedActLetterImage(Guid actId)
        {
            var result = _actAnnouncementService.GetActById(actId).ResultData;

            return File(result.RedactedLetterImage, "application/json", "Писмо");
        }
    }
}
