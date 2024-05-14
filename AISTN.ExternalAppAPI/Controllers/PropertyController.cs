using AISTN.Common.Helper;
using AISTN.ExternalAppAPI.Helper;
using AISTN.ExternalAppAPI.Models.Filter;
using AISTN.ExternalAppAPI.Models.Save;
using AISTN.ExternalAppAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AISTN.ExternalAppAPI.Controllers
{
    [Authorize(Roles = $"{RoleNames.MEIEmployee},{RoleNames.Syndic}")]
    [Route("api/ExternalApp/[controller]/[action]")]
    [ApiController]
    public class PropertyController : Controller
    {
        private readonly PropertyService _propertyService;

        public PropertyController(PropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        [HttpPost]
        public IActionResult SearchProperty(int pageNumber, int pageSize, [FromBody] PropertySearchFilter filter)
        {
            return Ok(_propertyService.SearchProperty(pageNumber, pageSize, filter));
        }

        [HttpPost]
        public IActionResult GetPropertiesByClass(int pageNumber, int pageSize, [FromBody] PropertyCaseFilter filter)
        {
            return Ok(_propertyService.GetPropertiesByClassKind(pageNumber, pageSize, filter));
        }

        [HttpGet]
        public IActionResult GetPropertyById(Guid id)
        {
            return Ok(_propertyService.GetPropertyById(id));
        }

        [HttpPost]
        public IActionResult CreateProperty(SavePropertyDTO propertyDTO)
        {
            return Ok(_propertyService.CreateProperty(propertyDTO));
        }

        [HttpPost]
        public IActionResult UpdateProperty(SavePropertyDTO propertyDTO)
        {
            return Ok(_propertyService.UpdateProperty(propertyDTO));
        }

        [HttpPost]
        public IActionResult DeleteProperty(Guid id)
        {
            return Ok(_propertyService.DeleteProperty(id));
        }

        [HttpPost]
        public FileResult GeneratePropertyDocument(Guid caseId)
        {
            TemplateDownloadModel file = _propertyService.GeneratePropertyDocument(caseId);

            return File(file.BlobContent, file.MimeType, file.FileName);
        }
    }
}
