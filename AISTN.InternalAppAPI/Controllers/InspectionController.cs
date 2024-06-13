using AISTN.Common.Helper;
using AISTN.InternalAppAPI.Models;
using AISTN.InternalAppAPI.Models.Filter;
using AISTN.InternalAppAPI.Models.Save;
using AISTN.InternalAppAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AISTN.InternalAppAPI.Controllers
{
    [Authorize(Roles = $"{RoleNames.Inspector}")]
    [Route("api/InternalApp/[controller]/[action]")]
    [ApiController]

    public class InspectionController : Controller
    {
        private readonly InspectionService _inspectionService;

        public InspectionController(InspectionService inspectionService)
        {
            _inspectionService = inspectionService;
        }

        [HttpGet]
        public IActionResult GetInspectionById(Guid id)
        {
            return Ok(_inspectionService.GetInspectionById(id));
        }

        [HttpPost]
        public IActionResult SearchCurrentInspection(int pageNumber, int pageSize, [FromBody] InspectionSearchFilter filter)
        {
            return Ok(_inspectionService.SearchCurrentInspection(pageNumber, pageSize, filter));
        }

        [HttpPost]
        public IActionResult SearchFinishedInspection(int pageNumber, int pageSize, [FromBody] InspectionSearchFilter filter)
        {
            return Ok(_inspectionService.SearchFinishedInspection(pageNumber, pageSize, filter));
        }

        [HttpPost]
        public IActionResult CreateInspection(SaveInspectionDTO inspectionDTO)
        {
            return Ok(_inspectionService.CreateInspection(inspectionDTO));
        }

        [HttpPost]
        public IActionResult UpdateInspection(SaveInspectionDTO inspectionDto)
        {
            return Ok(_inspectionService.UpdateInspection(inspectionDto));
        }

        [HttpPost]
        public FileResult GenerateSampleReportForInspection(Guid inspectionId)
        {
            SampleReportDownloadModel file = _inspectionService.GenerateSampleReportForInspection(inspectionId);

            return File(file.BlobContent, file.MimeType, file.FileName);
        }
    }
}
