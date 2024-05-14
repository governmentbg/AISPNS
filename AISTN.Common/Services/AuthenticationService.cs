using AISTN.Common.Helper;
using AISTN.Repository;
using AISTN.Repository.Attributes;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Novell.Directory.Ldap;
using System.Security.Claims;

namespace AISTN.Common.Services
{
    [Injectable]
    public class AuthenticationService : ServiceBase
    {
        private readonly AistnContextLoggable _db;
        private readonly LdapConfig _config;

        public AuthenticationService(AistnContextLoggable db, IMapper mapper,
            ExceptionLogger logger,
            IOptions<LdapConfig> config,
            ExcelGenerator excelGenerator) : base(mapper, logger, excelGenerator)
        {
            _db = db;
            _config = config.Value;
        }

        public OperationResult<bool> CheckUserAndPassword(string userName, string password)
        {
            try
            {

                var noDomainUserName = userName.Split("\\")[1].ToLower();
                var user = _db.Users.FirstOrDefault(x => x.UserName.ToLower() == noDomainUserName.ToLower());
                if (user == null) return Error<bool>("Несъществуващ потребител");

                using (var ldapConnection = new LdapConnection { SecureSocketLayer = _config.UseSSL })
                {
                    ldapConnection.Connect(_config.ServerName, _config.ServerPort);

                    try
                    {
                        ldapConnection.Bind(userName, password);

                        return Success(true);
                    }
                    catch (Exception)
                    {
                        return Error<bool>("Грешна парола");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<bool>(ex);
            }
        }
        public IEnumerable<Claim> AssignUserRoles(string userName)
        {
            try
            {
                var noDomainUserName = userName.Split("\\")[1].ToLower();
                var user = _db.Users.Include(x => x.Roles).FirstOrDefault(x => x.UserName.ToLower() == noDomainUserName.ToLower());

                if (user == null)
                {
                    throw new Exception("Потребителя не беше намерен");
                }

                return user.Roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role.Name!));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                throw ex;
            }
        }
    }
}
