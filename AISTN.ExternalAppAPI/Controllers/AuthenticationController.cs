using AISTN.Common.Helper;
using AISTN.ExternalAppAPI.Helper;
using AISTN.ExternalAppAPI.Models.Authentication;
using AISTN.ExternalAppAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace AISTN.ExternalAppAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/ExternalApp/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly AuthenticationService _authenticationService;
        private readonly IConfiguration _configuration;

        public AuthenticationController(AuthenticationService authenticationService, IConfiguration configuration)
        {
            _authenticationService = authenticationService;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var res = _authenticationService.SendAuthenticationRequest();
            var urlencodedRequest = HttpUtility.UrlEncode(res.SAMLRequest);
            return Ok($"{_configuration["eAuthentication:EgovURL"]}/SingleSignOnService?SAMLRequest=" + urlencodedRequest);
        }

        [HttpPost]
        public void ExtLogin([FromForm] string SAMLResponse)
        {
            var response = _authenticationService.HandleResponse(SAMLResponse);

            if (response.Type != ResultType.Success)
            {
                var message = System.Net.WebUtility.UrlEncode("Невалиден потребител");
                Response.Redirect($"{_configuration["eAuthentication:ExternalAPIFrontendURL"]}/login?notify=" + message);
            }
            else
            {
                Response.Cookies.Append("Auth_Token", response.ResultData, new CookieOptions()
                {
                    Secure = true,
                    HttpOnly = false,
                    IsEssential = true,
                });
                Response.Redirect(_configuration["eAuthentication:ExternalAPIFrontendURL"]);
            }
        }

        [HttpGet]
        public IActionResult Metadata()
        {
            return Ok(_authenticationService.GenerateMetadata(_configuration));
            
        }
      
    }
}

