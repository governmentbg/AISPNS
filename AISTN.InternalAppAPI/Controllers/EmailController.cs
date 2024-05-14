using AISTN.Common.Helper;
using AISTN.Common.Models;
using AISTN.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AISTN.InternalAppAPI.Controllers
{
    [Authorize(Roles = $"{RoleNames.Administrator},{RoleNames.Employee},{RoleNames.Inspector}")]
    [Route("api/InternalApp/[controller]/[action]")]
    [ApiController]
    public class EmailController : Controller
    {
        private readonly EmailService _emailService;

        public EmailController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public IActionResult SendCustomEmail([FromBody] SendEmailDTO emailDTO)
        {
            return Ok(_emailService.SendCustomEmail(emailDTO));
        }
    }
}
