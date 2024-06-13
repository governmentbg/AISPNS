using AISTN.Common.Helper;
using AISTN.Common.Models;
using AISTN.Common.Services;
using AISTN.InternalAppAPI.Models;
using AISTN.InternalAppAPI.Models.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AccountService = AISTN.InternalAppAPI.Services.AccountService;

namespace AISTN.InternalAppAPI.Controllers
{
    [Authorize(Roles = $"{RoleNames.Administrator},{RoleNames.Employee},{RoleNames.Inspector}")]
    [Route("api/InternalApp/[controller]/[action]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly AuthenticationService _authenticationService;
        private readonly AccountService _accountService;
        private readonly UserService _userService;
        private readonly IConfiguration _configuration;

        public AccountController(AuthenticationService authenticationService, AccountService accountService, UserService userservice, IConfiguration configuration)
        {
            _authenticationService = authenticationService;
            _accountService = accountService;
            _configuration = configuration;
            _userService = userservice;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginDTO model)
        {
            if (model.Username.ToLower().Contains(_configuration["LdapSettings:DomainControllerPrefix"].ToLower()) == false)
            {
                model.Username = _configuration["LdapSettings:DomainControllerPrefix"] + model.Username;
            }
            else if (model.Username.Contains(_configuration["LdapSettings:DomainControllerPrefix"]) == false)
            {
                model.Username = _configuration["LdapSettings:DomainControllerPrefix"] + model.Username.Substring(_configuration["LdapSettings:DomainControllerPrefix"].Length);
            }

            var user = _authenticationService.CheckUserAndPassword(model.Username, model.Password);

            if (user.Type != ResultType.Success)
                return Ok(user);

            var authClaims = new List<Claim>
            {
                new (ClaimTypes.Name, model.Username),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };


            authClaims.AddRange(_authenticationService.AssignUserRoles(model.Username));

            var token = GetToken(authClaims);

            return Ok(OperationResult<object>.Success(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,
                userName = model.Username
            }));
        }

        [HttpPost]
        public IActionResult GetCurrentUserInfo()
        {
            return Ok(_userService.GetCurrentUser(HttpContext));
        }

        [HttpPost]
        public IActionResult Logout()
        {
            return Ok(_accountService.LogOut());
        }

        [HttpPost]
        public IActionResult GetUsers(int pageNumber, int pageSize, [FromBody] UserFilterDTO filters)
        {
            return Ok(_accountService.GetUsers(pageNumber, pageSize, filters));
        }

        [HttpPost]
        public IActionResult GetUserById(Guid id)
        {
            return Ok(_accountService.GetUserById(id));
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] SaveAccountUserDTO user)
        {
            return Ok(_accountService.CreateUser(user));
        }

        [HttpPost]
        public IActionResult UpdateUser([FromBody] SaveAccountUserDTO user)
        {
            return Ok(_accountService.UpdateUser(user));
        }

        [HttpPost]
        public IActionResult DeleteUser(Guid id)
        {
            return Ok(_accountService.DeleteUser(id));
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }
    }
}
