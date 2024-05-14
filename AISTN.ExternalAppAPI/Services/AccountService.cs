using AISTN.Common.Helper;
using AISTN.Data.DataModel;
using AISTN.Repository;
using AISTN.Repository.Attributes;
using AISTN.Repository.Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AISTN.ExternalAppAPI.Services
{
    [Injectable]
    public class AccountService : ServiceBase
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IConfiguration _configuration;

        public AccountService(IMapper mapper, ExceptionLogger logger, ExcelGenerator excelGenerator,
                              IGenericRepository<User> userRepository,
                              IConfiguration configuration)
                        : base(mapper, logger, excelGenerator)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public OperationResult<object> Login(string username, string password)
        {
            try
            {
                var user = _userRepository.Get(x => x.UserName == username, src => src.Include(x => x.Roles)).FirstOrDefault();

                if (user == null)
                {
                    return Exception<object>(new Exception("Няма намерен потребител."));
                }

                var authClaims = new List<Claim>
                {
                    new (ClaimTypes.Name, username),
                    new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var roles = user.Roles;

                if (roles.Count > 0)
                {
                    authClaims.AddRange(roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role.Name!)));
                }

                var token = GetToken(authClaims);

                return OperationResult<object>.Success(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    userName = username
                });
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<object>(ex);
            }
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
