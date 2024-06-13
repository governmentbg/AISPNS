using AISTN.Common.Helper;
using AISTN.InternalAppAPI.Models.Filter;
using AISTN.InternalAppAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AISTN.InternalAppAPI.Controllers
{
    [Authorize(Roles = $"{RoleNames.Administrator},{RoleNames.Employee},{RoleNames.Inspector}")]
    [Route("api/InternalApp/[controller]/[action]")]
    [ApiController]
    public class CaseController : Controller
    {
        private readonly CaseService _caseService;

        public CaseController(CaseService caseService)
        {
            _caseService = caseService;
        }

        [HttpPost]
        public IActionResult SearchCase(int pageNumber, int pageSize, [FromBody] CaseSearchFilter filter)
        {
            return Ok(_caseService.SearchCase(pageNumber, pageSize, filter));
        }

        [HttpPost]
        public IActionResult SearchCaseByPerson(int pageNumber, int pageSize, [FromBody] CasePersonSearchFilter filter)
        {
            return Ok(_caseService.SearchCaseByPerson(pageNumber, pageSize, filter));
        }

        [HttpPost]
        public IActionResult SearchCaseByEntity(int pageNumber, int pageSize, [FromBody] CaseEntitySearchFilter filter)
        {
            return Ok(_caseService.SearchCaseByEntity(pageNumber, pageSize, filter));
        }

        [HttpPost]
        public IActionResult GetImportedDeedsByCase(Guid caseId)
        {
            return Ok(_caseService.GetImportedDeedsByCaseId(caseId));
        }

        [HttpGet]
        public IActionResult GetAllCases()
        {
            return Ok(_caseService.GetAllCases());
        }

        [HttpGet]
        public IActionResult GetCaseById(Guid id)
        {
            return Ok(_caseService.GetCaseById(id));
        }

        [HttpGet]

        public IActionResult GetCaseBook(Guid id)
        {
            return Ok(_caseService.GetCaseBook(id));
        }

        [HttpPost]
        public IActionResult GetEntityStatisticalDataByEntityId(Guid entityId)
        {
            return Ok(_caseService.GetEntityStatisticalDataByEntityId(entityId));
        }

        [HttpPost]
        public IActionResult GetCaseReports(Guid id, [FromBody] SyndicReportFilter filter, int pageNumber, int pageSize)
        {
            return Ok(_caseService.GetCaseReports(id, filter, pageNumber, pageSize));
        }
    }
}
