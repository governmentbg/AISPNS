using AISTN.Common.Models;
using AISTN.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using System.Net;

namespace AISTN.Common.Helper
{
    /// <summary>
    /// Global exception handling
    /// </summary>
    public class HttpResponseExceptionFilter : IExceptionFilter
    {
        private readonly IHostEnvironment _hostEnvironment;
        private readonly ExceptionLogger _logger;

        public HttpResponseExceptionFilter(
            IHostEnvironment hostEnvironment,
            ExceptionLogger logger)
        {
            _hostEnvironment = hostEnvironment;
            _logger = logger;
        }

        /// <inheritdoc/>
        public void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case ValidationErrorsException:
                    var valEx = (ValidationErrorsException)context.Exception;
                    context.Result = new ObjectResult(new ErrorResponse(valEx.Message, valEx.Errors))
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };
                    break;

                case BusinessException:
                    context.Result = new ObjectResult(new ErrorResponse(context.Exception.Message))
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };
                    break;
                default:
                    context.Result = new ObjectResult(new ErrorResponse("Internal error"))
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError
                    };
                    break;
            }

            context.ExceptionHandled = true;

            long logId = _logger.LogException(context.Exception);

            context.HttpContext.Items["ExceptionLogId"] = logId;
        }
    }
}
