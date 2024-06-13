using AISTN.Repository;
using AutoMapper;

namespace AISTN.Common.Helper
{
    /// <summary>
    /// Service base is used as inherited class for mapper, logger, operation result etc.
    /// </summary>
    public class ServiceBase
    {
        public readonly IMapper _mapper;
        protected readonly ExceptionLogger _logger;
        protected readonly ExcelGenerator _excelGenerator;

        public ServiceBase(IMapper mapper, ExceptionLogger logger, ExcelGenerator excelGenerator)
        {
            _mapper = mapper;
            _logger = logger;
            _excelGenerator = excelGenerator;
        }

        protected CurrentUser? _currentUser { get; set; }

        protected void SetCurrentUser(CurrentUser? currentUser)
        {
            _currentUser = currentUser;
        }

        protected void checkCurrentUserIsPerson()
        {
            if (_currentUser == null || _currentUser.PersonId == null)
                throw new BusinessException("Портебителят не е служител");
        }

        protected UserActivity CreateUserActivity(CurrentUser user, eUserActionType activityType)
        {
            return UserActivity.NewActivity(new CurrentUser()
            {
                UserId = user.UserId,
                PersonId = user.PersonId,
                IsAuthenticated = user != null,
                IpAddress = user!.IpAddress
            }, activityType)!;
        }

        protected OperationResult<T> Success<T>(string? message = null)
        {
            return OperationResult<T>.Success(message);
        }

        protected OperationResult<T> Success<T>(T resultObject)
        {
            return OperationResult<T>.Success(resultObject);
        }

        protected OperationResult<T> Success<T>(T resultObject, string? message = null, List<string>? additionalMessages = null)
        {
            return OperationResult<T>.Success(resultObject, message, additionalMessages);

        }

        protected OperationResult<T> Error<T>(string errorMessage, List<string>? additionalMessages = null)
        {
            return OperationResult<T>.Error(errorMessage, additionalMessages);
        }

        protected OperationResult<T> Error<T>(string errorMessage, List<FieldMessage> additionalFieldMessages)
        {
            return OperationResult<T>.Error(errorMessage, additionalFieldMessages);
        }

        protected OperationResult<T> Exception<T>(Exception ex)
        {
            return OperationResult<T>.Exception(ex);
        }
    }
}
