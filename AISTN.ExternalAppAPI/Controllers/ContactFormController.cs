using AISTN.Common.Helper;
using AISTN.Common.Models;
using AISTN.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AISTN.ExternalAppAPI.Controllers
{
    [Authorize(Roles = $"{RoleNames.MEIEmployee}")]
    [Route("api/ExternalApp/[controller]/[action]")]
    [ApiController]
    public class ContactFormController : Controller
    {
        private readonly ContactFormService _contactFormService;

        public ContactFormController(ContactFormService contactFormService)
        {
            _contactFormService = contactFormService;
        }

        [HttpGet]
        public IActionResult GetContactForm()
        {
            return Ok(_contactFormService.GetContactForm());
        }

        [HttpPost]
        public IActionResult CreateContactForm([FromBody] ContactFormDTO formDTO)
        {
            return Ok(_contactFormService.CreateContactForm(formDTO));
        }

        [HttpPost]
        public IActionResult UpdateContactForm([FromBody] ContactFormDTO formDTO)
        {
            return Ok(_contactFormService.UpdateContactForm(formDTO));
        }
    }
}
