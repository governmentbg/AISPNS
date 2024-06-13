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
    public class ReportController: Controller
    {
        private readonly ReportService _reporteService;

        public ReportController(ReportService reportService)
        {
            _reporteService = reportService;
        }

        [HttpPost]
        public IActionResult SearchReport(int pageNumber, int pageSize, ReportSearchFilter filter)
        {
            return Ok(_reporteService.SearchReports(pageNumber, pageSize, filter));
        }

        [HttpGet]
        public IActionResult GetReportById(Guid id)
        {
            return Ok(_reporteService.GetReportById(id));
        }

        [HttpPost]
        public IActionResult CreateReport(SaveReportDTO reportDTO)
        {
            return Ok(_reporteService.CreateReport(reportDTO));
        }

        [HttpPost]
        public IActionResult UpdateReport(SaveReportDTO reportDTO)
        {
            return Ok(_reporteService.UpdateReport(reportDTO));
        }

        [HttpPost]
        public FileResult GenerateReportTemplate(Guid reportTypeId)
        {
            TemplateDownloadModel file = _reporteService.GenerateReportTemplate(reportTypeId);

            return File(file.BlobContent, file.MimeType, file.FileName);
        }
    }
}
