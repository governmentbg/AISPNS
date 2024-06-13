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
    public class StatisticalReportController : Controller
    {
        private readonly StatisticalReportService _statisticalReportService;

        public StatisticalReportController(StatisticalReportService statisticalReportService)
        {
            _statisticalReportService = statisticalReportService;
        }

        [HttpPost]
        public IActionResult SearchStatisticalReport(int pageNumber, int pageSize, [FromBody] StatisticalReportSearchFilter filter)
        {
            return Ok(_statisticalReportService.SearchStatisticalReport(pageNumber, pageSize, filter));
        }

        [HttpPost]
        public IActionResult GetAllStatisticalReports()
        {
            return Ok(_statisticalReportService.GetAllStatisticalReports());
        }

        [HttpPost]
        public IActionResult GetStatisticalReportById(Guid id)
        {
            return Ok(_statisticalReportService.GetStatisticalReportById(id));
        }

        [HttpPost]
        public IActionResult CreateStatisticalReport(SaveStatisticalReportDTO statisticalReportDTO)
        {
            return Ok(_statisticalReportService.CreateStatisticalReport(statisticalReportDTO));
        }

        [HttpPost]
        public IActionResult UpdateStatisticalReport(SaveStatisticalReportDTO statisticalReportDTO)
        {
            return Ok(_statisticalReportService.UpdateStatisticalReport(statisticalReportDTO));
        }

        [HttpPost]
        public IActionResult DeleteStatisticalReport(Guid id)
        {
            return Ok(_statisticalReportService.DeleteStatisticalReport(id));
        }

        [HttpPost]
        public IActionResult PublishStatisticalReport(Guid id)
        {
            return Ok(_statisticalReportService.PublishStatisticalReport(id));
        }

        [HttpPost]
        public IActionResult UnpublishStatisticalReport(Guid id)
        {
            return Ok(_statisticalReportService.UnpublishStatisticalReport(id));
        }
    }
}
