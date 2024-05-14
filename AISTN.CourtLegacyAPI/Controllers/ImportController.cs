using AISTN.Common.Helper;
using AISTN.Common.Helper.ApiRequestIntercepting;
using AISTN.Common.Models;
using AISTN.CourtLegacyAPI.Models.Responses;
using AISTN.CourtLegacyAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Xml.Linq;

namespace AISTN.CourtLegacyAPI.Controllers
{
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly LegacyCaseImportService _importService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ApiRequestLogger _apiRequestlogger;

        public ImportController(LegacyCaseImportService importService, IHttpContextAccessor contextAccessor, ApiRequestLogger apiRequestlogger)
        {
            _importService = importService;
            _contextAccessor = contextAccessor;
            _apiRequestlogger = apiRequestlogger;
        }

        /// <summary>
        /// Receives a request to 
        /// </summary>
        [Route("ws/")]
        [HttpPost]
        [Consumes("application/xml", "text/xml")]
        [Produces("text/xml")]
        [ProducesResponseType(typeof(ImportResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [AllowInterceptingAttribute(LimitRate = true)]
        public async Task<IActionResult> ImportTransfer([FromBody] XElement xml)
        {
            try
            {
                await _importService.ImportTransfer(xml);

                return new ContentResult
                {
                    ContentType = "text/xml",
                    Content = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<response>\r\n<success>Validation successful</success>\r\n</response>",
                    StatusCode = 200
                };
            }
            catch (ValidationErrorsException vex)
            {
                return new ContentResult
                {
                    ContentType = "text/xml",
                    Content = $"<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<response>\r\n<error>{vex.GetWholeMessage}</error>\r\n</response>",
                    StatusCode = 200
                };
            }
            catch (BusinessException bex)
            {
                return new ContentResult
                {
                    ContentType = "text/xml",
                    Content = $"<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<response>\r\n<error>{bex.Message}</error>\r\n</response>",
                    StatusCode = 200
                };
            }
        }
    }
}
