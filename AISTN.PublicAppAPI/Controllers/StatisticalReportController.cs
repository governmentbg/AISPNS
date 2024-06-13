using AISTN.PublicAppAPI.Models.Filters;
using AISTN.PublicAppAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AISTN.PublicAppAPI.Controllers
{
    [Route("api/PublicApp/[controller]/[action]")]
    [ApiController]
    public class StatisticalReportController : Controller
    {
        private readonly StatisticalReportService _statisticalReportService;

        public StatisticalReportController(StatisticalReportService statisticalReportService)
        {
            _statisticalReportService = statisticalReportService;
        }

        [HttpPost]
        public IActionResult SearchStatisticalReport(int pageNumber, int pageSize,[FromBody] StatisticalReportSearchFilter filter)
        {
            return Ok(_statisticalReportService.SearchStatisticalReport(pageNumber, pageSize, filter));
        }

        [HttpGet]
        public IActionResult GetAllStatisticalReports()
        {
            return Ok(_statisticalReportService.GetAllStatisticalReports());
        }

        [HttpGet]
        public IActionResult GetStatisticalReportById(Guid id)
        {
            return Ok(_statisticalReportService.GetStatisticalReportById(id));
        }
    }
}
