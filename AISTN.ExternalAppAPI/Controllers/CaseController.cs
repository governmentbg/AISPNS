using AISTN.Common.Helper;
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
    public class CaseController : Controller
    {
        private readonly CaseService _caseService;

        public CaseController(CaseService caseService)
        {
            _caseService = caseService;
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
        public IActionResult SearchCase(int pageNumber, int pageSize, [FromBody] CaseSearchFilter filter)
        {
            return Ok(_caseService.SearchCase(pageNumber, pageSize, filter));
        }

        [HttpPost]
        public IActionResult SearchCaseByEntity(int pageNumber, int pageSize, [FromBody] CaseEntitySearchFilter filter)
        {
            return Ok(_caseService.SearchCaseByEntity(pageNumber, pageSize, filter));
        }

        [HttpPost]
        public IActionResult SearchCaseBySyndic(int pageNumber, int pageSize, [FromBody] CaseSyndicSearchFilter filter)
        {
            return Ok(_caseService.SearchCaseBySyndic(pageNumber, pageSize, filter));
        }

        [HttpPost]
        public IActionResult GetEntityStatisticalDataByEntityId(Guid entityId)
        {
            return Ok(_caseService.GetEntityStatisticalDataByEntityId(entityId));
        }

        [HttpPost]
        public IActionResult SaveEntityStatisticalData(SaveEntityStatisticalDataDTO saveEntityStatisticalDataDTO)
        {
            return Ok(_caseService.SaveEntityStatisticalData(saveEntityStatisticalDataDTO));
        }
    }
}
