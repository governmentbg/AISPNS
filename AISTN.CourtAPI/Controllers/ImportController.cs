using AISTN.Common.Helper.ApiRequestIntercepting;
using AISTN.Common.Models;
using AISTN.CourtAPI.Models.Responses;
using AISTN.CourtAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace AISTN.CourtAPI.Controllers
{
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly ExtendedCaseImportService _importService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ApiRequestLogger _apiRequestlogger;

        public ImportController(ExtendedCaseImportService importService, IHttpContextAccessor contextAccessor, ApiRequestLogger apiRequestlogger)
        {
            _importService = importService;
            _contextAccessor = contextAccessor;
            _apiRequestlogger = apiRequestlogger;
        }

        /// <summary>
        /// Receives a request to 
        /// </summary>
        [Route("ws2/")]
        [HttpPost]
        [Consumes("application/xml", "text/xml")]
        [ProducesResponseType(typeof(ImportResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [AllowInterceptingAttribute(LimitRate = true)]
        public async Task<IActionResult> ImportTransfer([FromBody] XElement xml)
        {
            await _importService.ImportTransfer(xml);

            return Ok(new ImportResponse() { Message = "Import Succesful" });
        }
    }
}
