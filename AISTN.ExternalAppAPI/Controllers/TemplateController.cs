using AISTN.Common.Helper;
using AISTN.ExternalAppAPI.Helper;
using AISTN.ExternalAppAPI.Models.Save;
using AISTN.ExternalAppAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AISTN.ExternalAppAPI.Controllers
{

    [Authorize(Roles = $"{RoleNames.MEIEmployee},{RoleNames.Syndic}")]
    [Route("api/ExternalApp/[controller]/[action]")]
    [ApiController]
    public class TemplateController : Controller
    {
        private readonly TemplateService _templateService;

        public TemplateController(TemplateService templateService)
        {
            _templateService = templateService;
        }

        [HttpGet]
        public IActionResult GetTemplates(int pageNumber, int pageSize)
        {
            return Ok(_templateService.GetAllTemplates(pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult GetAdminTemplates(int pageNumber, int pageSize)
        {
            return Ok(_templateService.GetAllAdminTemplates(pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult GetTemplateById(Guid id)
        {
            return Ok(_templateService.GetTemplateById(id));
        }

        [HttpPost]
        public IActionResult CreateTemplate(SaveTemplateDTO templateDTO)
        {
            return Ok(_templateService.CreateTemplate(templateDTO));
        }

        [HttpPost]
        public IActionResult UpdateTemplate(SaveTemplateDTO templateDTO)
        {
            return Ok(_templateService.UpdateTemplate(templateDTO));
        }

        [HttpPost]
        public FileResult GenerateAdminTemplate(Guid templateId)
        {
            TemplateDownloadModel file = _templateService.GenerateAdminTemplate(templateId);

            return File(file.BlobContent, file.MimeType, file.FileName);
        }
    }
}
