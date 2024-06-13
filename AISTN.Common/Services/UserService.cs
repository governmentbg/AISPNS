using AISTN.Common.Helper;
using AISTN.Common.Models;
using AISTN.Data.DataModel;
using AISTN.Repository;
using AISTN.Repository.Attributes;
using AISTN.Repository.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AISTN.Common.Services
{
    [Injectable]
    public class UserService : ServiceBase
    {
        private readonly IGenericRepository<User> _userRepository;

        public UserService(IMapper _mapper, ExceptionLogger _logger,
                           IGenericRepository<User> userRepository,
                           ExcelGenerator excelGenerator)
                      : base(_mapper, _logger, excelGenerator)
        {
            _userRepository = userRepository;
        }

        public OperationResult<CurrentUser> GetCurrentUser(HttpContext context)
        {
            try
            {
                var username = context.User.Identity?.Name;
                var ip = context.Request.Headers["X-Real-IP"].ToString();

                if(ip.IsNullOrEmpty())
                {
                    ip = context.Connection.RemoteIpAddress.ToString();
                }

                if (username != null && ip != null)
                {
                    if (username.Split("\\").Length > 1)
                    {
                        username = username.Split("\\")[1].ToLower();
                    }

                    var user = _userRepository.Get(x => x.UserName.ToLower() == username.ToLower(), src => src.Include(x => x.Roles)).FirstOrDefault();

                    if (user == null)
                    {
                        throw new Exception("User was not found");
                    }

                    var currentUser = _mapper.Map<CurrentUser>(user);
                    currentUser.IpAddress = ip;

                    return Success(currentUser);
                }
                else
                {
                    var dummyUser = new CurrentUser
                    {
                        UserName = "Dummy User",
                        UserId = Guid.Parse("B488DA43-C8B7-4CBE-A5B7-09079D298933"),
                        IsAuthenticated = true,
                        IpAddress = "127.0.0.1",
                        PersonId = 37,
                    };

                    return Success(dummyUser);
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<CurrentUser>(ex);
            }
        }
    }
}
