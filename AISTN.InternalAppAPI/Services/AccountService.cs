using AISTN.Common.Helper;
using AISTN.Common.Models;
using AISTN.Common.Models.PageResult;
using AISTN.Common.Services;
using AISTN.Data.DataModel;
using AISTN.InternalAppAPI.Models.Filter;
using AISTN.Repository;
using AISTN.Repository.Attributes;
using AISTN.Repository.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AISTN.InternalAppAPI.Services
{
    [Injectable]
    public class AccountService : ServiceBase
    {
        private readonly IGenericRepository<User> _userRepository;

        private readonly AistnContextLoggable _db;

        public AccountService(IMapper mapper,
                              ExceptionLogger logger,
                              IGenericRepository<User> userRepository,
                              IHttpContextAccessor contextAccessor,
                              UserService userService,
                              ExcelGenerator excelGenerator,
                              AistnContextLoggable db)
            : base(mapper, logger, excelGenerator)
        {
            SetCurrentUser(userService.GetCurrentUser(contextAccessor.HttpContext!).ResultData);
            _userRepository = userRepository;
            _db = db;
        }

        public OperationResult<PagedList<AccountUserIndexDTO>> GetUsers(int pageNumber, int pageSize, UserFilterDTO filters)
        {
            try
            {
                var result = _userRepository.Get(filter: x => (filters.FirstName == null || x.FirstName.Contains(filters.FirstName))
                                                              && (filters.MiddleName == null || x.SecondName.Contains(filters.MiddleName))
                                                              && (filters.LastName == null || x.LastName.Contains(filters.LastName))
                                                              && (filters.LastName == null || x.LastName.Contains(filters.LastName))
                                                              && (filters.Username == null || x.UserName == filters.Username)
                                                              && (filters.Egn == null || x.Egn == filters.Egn)
                                                              && (filters.Email == null || x.Email == filters.Email)
                                                              && (filters.RoleId == null || x.Roles.Any(x => x.Id == filters.RoleId))
                                                              && (filters.IsActive == null || x.IsActive == filters.IsActive)


                    , x => x.Include(x => x.Roles)).AsQueryable();

                return Success(PagedList<AccountUserIndexDTO>.ToPagedList(result.ProjectTo<AccountUserIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<AccountUserIndexDTO>>(ex);
            }
        }

        public OperationResult<SaveAccountUserDTO> GetUserById(Guid id)
        {
            try
            {
                var result = _userRepository.GetById(id, src => src.Include(x => x.Roles));

                return Success(_mapper.Map<SaveAccountUserDTO>(result));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SaveAccountUserDTO>(ex);
            }
        }

        public OperationResult<SaveAccountUserDTO> GetUserByUsername(string username)
        {
            try
            {
                var user = _userRepository.Get(x => x.UserName == username, src => src.Include(x => x.Roles)).FirstOrDefault();

                return Success(_mapper.Map<SaveAccountUserDTO>(user));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SaveAccountUserDTO>(ex);
            }
        }

        public OperationResult<string> LogOut()
        {
            var ex = new Exception("User has been logged out");

            _logger.LogException(ex);
            return Exception<string>(ex);
        }

        public OperationResult<Guid> CreateUser(SaveAccountUserDTO user)
        {
            try
            {
                var mappedUser = _mapper.Map<User>(user);
                var roles = _db.Roles.Where(x => user.RoleIds.Contains(x.Id));

                foreach (var item in roles)
                {
                    mappedUser.Roles.Add(item);
                }

                _db.Add(mappedUser);
                _db.SaveChanges(CreateUserActivity(_currentUser!, eUserActionType.CreateUser));

                return Success(mappedUser!.Id);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<Guid>(ex);
            }
        }

        public OperationResult<SaveAccountUserDTO> UpdateUser(SaveAccountUserDTO user)
        {
            try
            {
                var dbUser = _db.Users.Include(x => x.Roles).FirstOrDefault(x => x.Id == user.Id.Value);
                if (dbUser == null) return Error<SaveAccountUserDTO>("Несъществуващ потребител");

                _mapper.Map(user, dbUser);

                var roleToAdd = _db.Roles.Where(x => user.RoleIds.Contains(x.Id)).ToList();
                var roleToRemove = dbUser.Roles.Where(x => !user.RoleIds.Contains(x.Id)).ToList();

                foreach (var item in roleToRemove)
                {
                    dbUser.Roles.Remove(item);
                }

                foreach (var item in roleToAdd)
                {
                    dbUser.Roles.Add(item);
                }

                _db.SaveChanges(CreateUserActivity(_currentUser!, eUserActionType.UpdateUser));

                return Success(_mapper.Map<SaveAccountUserDTO>(user));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SaveAccountUserDTO>(ex);
            }
        }

        public OperationResult<bool> DeleteUser(Guid id)
        {
            try
            {
                var dbUser = _userRepository.GetById(id);
                if (dbUser == null) return Error<bool>("Несъществуващ потребител");


                _userRepository.Remove(id);
                _userRepository.Save(CreateUserActivity(_currentUser!, eUserActionType.DeleteUser));

                return Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<bool>(ex);
            }
        }
    }
}
