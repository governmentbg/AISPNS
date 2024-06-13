using AISTN.Common.Helper;
using AISTN.ExternalAppAPI.Helper;
using AISTN.ExternalAppAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AISTN.ExternalAppAPI.Controllers
{
    [Authorize(Roles = $"{RoleNames.MEIEmployee},{RoleNames.Syndic}")]
    [Route("api/ExternalApp/[controller]/[action]")]
    [ApiController]
    public class MIICertificatesController : Controller
    {
        private readonly MIICertificatesService _certificatesService;

        public MIICertificatesController(MIICertificatesService certificatesService)
        {
            _certificatesService = certificatesService;
        }

        [Authorize(Roles = $"{RoleNames.MEIEmployee}")]
        [HttpPost]
        public IActionResult GetAnnouncementCertificates(int pageNumber, int pageSize)
        {
            return Ok(_certificatesService.GetAnnouncementCertificates(pageNumber, pageSize));
        }

        [Authorize(Roles = $"{RoleNames.MEIEmployee}")]
        [HttpPost]
        public IActionResult GetFinishedAnnouncementCertificates(int pageNumber, int pageSize)
        {
            return Ok(_certificatesService.GetFinishedAnnouncementCertificates(pageNumber, pageSize));
        }

        [HttpPost]
        public FileResult GenerateCertificateForAnnouncement(Guid announcementId)
        {
            TemplateDownloadModel file = _certificatesService.GenerateCertificateForAnnouncement(announcementId);

            return File(file.BlobContent, file.MimeType, file.FileName);
        }
    }
}
