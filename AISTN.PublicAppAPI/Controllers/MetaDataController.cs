using AISTN.Common.Services;
using Microsoft.AspNetCore.Mvc;

namespace AISTN.PublicAppAPI.Controllers
{
    //[Authorize]
    [Route("api/PublicApp/[controller]/[action]")]
    [ApiController]
    public class MetaDataController : Controller
    {
        private readonly MetaDataService _metaDataService;

        public MetaDataController(MetaDataService metaDataService)
        {
            _metaDataService = metaDataService;
        }

        #region Case Info Nomenclatres

        [HttpPost]
        public IActionResult GetActions()
        {
            return Ok(_metaDataService.GetActions());
        }

        [HttpPost]
        public IActionResult GetActKinds()
        {
            return Ok(_metaDataService.GetActKinds());
        }

        [HttpPost]
        public IActionResult GetActReasons()
        {
            return Ok(_metaDataService.GetActReasons());
        }

        [HttpPost]
        public IActionResult GetCaseCodes()
        {
            return Ok(_metaDataService.GetCaseCodes());
        }

        [HttpPost]
        public IActionResult GetCaseKinds()
        {
            return Ok(_metaDataService.GetCaseKinds());
        }

        [HttpPost]
        public IActionResult GetCourtsList()
        {
            return Ok(_metaDataService.GetCourtsList());
        }

        [HttpPost]
        public IActionResult GetIncomingDocumentKinds()
        {
            return Ok(_metaDataService.GetIncomingDocumentKinds());
        }

        [HttpPost]
        public IActionResult GetSendToKinds()
        {
            return Ok(_metaDataService.GetSendToKinds());
        }

        [HttpPost]
        public IActionResult GetSessionKinds()
        {
            return Ok(_metaDataService.GetSessionKinds());
        }

        [HttpPost]
        public IActionResult GetSessionResults()
        {
            return Ok(_metaDataService.GetSessionResults());
        }

        [HttpPost]
        public IActionResult GetReportSources()
        {
            return Ok(_metaDataService.GetReportSources());
        }

        [HttpPost]
        public IActionResult GetReportTypes()
        {
            return Ok(_metaDataService.GetReportTypes());
        }

        #endregion


        [HttpGet]
        public IActionResult GetObjectKind()
        {
            return Ok(_metaDataService.GetObjectKind());
        }
    }
}
