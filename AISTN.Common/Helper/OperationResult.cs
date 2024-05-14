using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace AISTN.Common.Helper
{
    public interface IOperationResult { }
    public class OperationResult<T> : IOperationResult
    {
        public OperationResult()
        {
        }

        public ResultType Type { get; private set; }
        public string? Message { get; private set; }
        public List<string>? AdditionalMessages { get; private set; }
        public List<FieldMessage>? AdditionalFieldMessages { get; private set; }

        public T? ResultData { get; private set; }

        /// <summary>
        /// Used by the DataSourceResult -- mandatory for Kendo grid
        /// </summary>
        public int TotalResultData { get; set; }

        [JsonIgnore]
        public Exception? exception { get; set; }


        public static OperationResult<T> Success(string? message = null, List<string>? additionalMessages = null)
        {
            return new OperationResult<T>()
            {
                Type = ResultType.Success,
                Message = message,
                AdditionalMessages = additionalMessages,
                ResultData = default
            };
        }

        public static OperationResult<T> Success(T resultObject, string? message = null, List<string>? additionalMessages = null)
        {
            return new OperationResult<T>()
            {
                Type = ResultType.Success,
                Message = message,
                AdditionalMessages = additionalMessages,
                ResultData = resultObject
            };
        }

        public static OperationResult<T> Success(T resultObject)
        {
            return new OperationResult<T>()
            {
                Type = ResultType.Success,
                ResultData = resultObject
            };
        }

        public static OperationResult<T> Error(string errorMessage, List<string>? additionalMessages = null)
        {
            return new OperationResult<T>()
            {
                Type = ResultType.Error,
                Message = errorMessage,
                AdditionalMessages = additionalMessages,
                ResultData = default
            };
        }

        public static OperationResult<T> Error(string errorMessage, List<FieldMessage> additionalFieldMessages)
        {
            return new OperationResult<T>()
            {
                Type = ResultType.Error,
                Message = errorMessage,
                AdditionalFieldMessages = additionalFieldMessages,
                ResultData = default
            };
        }

        public static OperationResult<T> Exception(Exception ex)
        {
            if (ex.InnerException is SqlException sqlException)
            {
                var operationResult = new OperationResult<T>()
                {
                    Type = ResultType.Error,
                    ResultData = default,
                };

                switch (sqlException.Number)
                {
                    case 547:
                        operationResult.Message = "Записът не може да бъде изтрит";
                        break;
                    default:
                        break;
                }

                return operationResult;

            }

            return new OperationResult<T>()
            {
                Type = ResultType.Exception,
#if DEBUG
                Message = ex.Message,
#else
                /// When on production, return exception only if it's a Business one. 
                /// Avoid sending system exceptions to frontend.
                Message = (ex.GetType() == typeof(BusinessException) || ex.GetType().IsSubclassOf(typeof(BusinessException))) ? ex.Message : null, 
#endif

                ResultData = default,

                exception = ex
            };
        }

        /// <summary>
        /// Transforms Operation result from one (T) to another. Preserving all properties apart from result data
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static OperationResult<T> TransformFrom<S>(OperationResult<S> original)
        {
            OperationResult<T> newOne = new OperationResult<T>();
            newOne.Type = original.Type;
            newOne.Message = original.Message;
            newOne.AdditionalMessages = original.AdditionalMessages;
            newOne.exception = original.exception;

            return newOne;
        }

        public void SetData(T data)
        {
            ResultData = data;
        }

        public void SetMessage(string message)
        {
            Message = message;
        }

        public void AddAdditionalMessage(string message)
        {
            if (AdditionalMessages == null) AdditionalMessages = new List<string>();

            AdditionalMessages.Add(message);
        }

        public string[] MessagesToArray()
        {
            List<string> msgs = new List<string>();

            msgs.Add(Message);

            if (AdditionalMessages is not null)
                msgs.AddRange(AdditionalMessages);

            return msgs.ToArray();
        }
    }

    /// <summary>
    /// enum type for Result 
    /// </summary>
    public enum ResultType
    {
        Success = 1,
        Error = 0,
        Exception = -1
    }
}
