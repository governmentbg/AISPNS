using AISTN.Common.Helper;
using AISTN.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AISTN.ExternalAppAPI.Controllers
{
    [Authorize(Roles = $"{RoleNames.MEIEmployee},{RoleNames.Syndic}")]
    [Route("api/ExternalApp/[controller]/[action]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;
        private readonly UserService _userService;
        private readonly MetaDataService _metaDataService;

        public AccountController(AccountService accountService, UserService userService, MetaDataService metaDataService)
        {
            _accountService = accountService;
            _userService = userService;
            _metaDataService = metaDataService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult FakeLogin(string username, string password)
        {
            return Ok(_accountService.Login(username, password));
        }

        [HttpPost]
        public IActionResult GetCurrentUserInfo()
        {
            return Ok(_userService.GetCurrentUser(HttpContext));
        }

        [Authorize(Roles = RoleNames.Syndic)]
        [HttpGet]
        public IActionResult GetProfileInformation()
        {
            return Ok(_metaDataService.GetUserInfo(HttpContext));
        }
    }
}
