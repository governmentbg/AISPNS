using AISTN.PublicAppAPI.Models.Filters;
using AISTN.PublicAppAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AISTN.PublicAppAPI.Controllers
{
    //[Authorize]
    [Route("api/PublicApp/[controller]/[action]")]
    [ApiController]
    public class SyndicController : Controller
    {
        private readonly SyndicService _syndicService;

        public SyndicController(SyndicService syndicService)
        {
            _syndicService = syndicService;
        }

        [HttpPost]
        public IActionResult SearchSyndic(int pageNumber, int pageSize, [FromBody] SyndicSearchFilter filter)
        {
            return Ok(_syndicService.SearchSyndic(pageNumber, pageSize, filter));
        }

        [HttpGet]
        public IActionResult GetSyndicById(Guid id)
        {
            return Ok(_syndicService.GetSyndicById(id));
        }

        [HttpPost]
        public IActionResult SearchSyndicTemplate(int pageNumber, int pageSize)
        {
            return Ok(_syndicService.GetSyndicTemplates(pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult GetSyndicTemplateById(Guid id)
        {
            return Ok(_syndicService.GetSyndicTemplateById(id));
        }

        [HttpGet]
        public IActionResult GetAllDocumentLegalBasis(int pageNumber, int pageSize)
        {
            return Ok(_syndicService.GetAllDocumentLegalBases(pageNumber, pageSize));
        }
    }
}
