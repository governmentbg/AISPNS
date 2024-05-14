using AISTN.PublicAppAPI.Models.Filters;
using AISTN.PublicAppAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AISTN.PublicAppAPI.Controllers
{
    //[Authorize]
    [Route("api/PublicApp/[controller]/[action]")]
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
    }
}
