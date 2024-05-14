using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;


namespace AISTN.Common.Helper.ApiRequestIntercepting
{
    public class InterceptingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpContextAccessor _contextAccessor;

        public InterceptingMiddleware(RequestDelegate next,
                                 IHttpContextAccessor contextAccessor)
        {
            _next = next;
            _contextAccessor = contextAccessor;
        }

        public async Task InvokeAsync(HttpContext context, ApiRequestLogger _apiRequestlogger)
        {
            // check for the attribute and skip everything else if it exists
            var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
            var attribute = endpoint?.Metadata.GetMetadata<AllowInterceptingAttribute>();
            if (attribute == null)
            {
                await _next(context);
                return;
            }

            long logId = await LogRequest(_apiRequestlogger,
                                          context,
                                          endpoint.DisplayName);

            var originalResponseBody = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                //Process rate limitting
                if (attribute.LimitRate)
                {
                    await _apiRequestlogger.OkToStartProcessing(logId);
                }

                await _next.Invoke(context);

                var exLogId = context.Items["ExceptionLogId"] as long?;

                await LogResponse(_apiRequestlogger,
                                  context,
                                  responseBody,
                                  originalResponseBody,
                                  logId,
                                  exLogId);
            }
        }

        private async Task<long> LogRequest(ApiRequestLogger _apiRequestlogger, HttpContext context, string endpoint)
        {
            var requestContent = new StringBuilder();

            requestContent.AppendLine("=== Request Info ===");
            requestContent.AppendLine($"method = {context.Request.Method.ToUpper()}");
            requestContent.AppendLine($"path = {context.Request.Path}");

            requestContent.AppendLine("-- headers");
            foreach (var (headerKey, headerValue) in context.Request.Headers)
            {
                requestContent.AppendLine($"header = {headerKey}    value = {headerValue}");
            }

            requestContent.AppendLine("-- body");
            context.Request.EnableBuffering();
            var requestReader = new StreamReader(context.Request.Body);
            var content = await requestReader.ReadToEndAsync();
            requestContent.AppendLine($"body = {content}");

            context.Request.Body.Position = 0;

            string ipAdress = _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

            long logId = await _apiRequestlogger.LogRequest(requestContent.ToString(), endpoint, ipAdress);

            return logId;
        }

        private async Task LogResponse(ApiRequestLogger _apiRequestlogger, HttpContext context, MemoryStream responseBody, Stream originalResponseBody, long requestLogId, long? exceptionLogId = null)
        {
            var responseContent = new StringBuilder();
            responseContent.AppendLine("=== Response Info ===");

            responseContent.AppendLine("-- headers");
            foreach (var (headerKey, headerValue) in context.Response.Headers)
            {
                responseContent.AppendLine($"header = {headerKey}    value = {headerValue}");
            }

            responseContent.AppendLine("-- body");
            responseBody.Position = 0;
            var content = await new StreamReader(responseBody).ReadToEndAsync();
            responseContent.AppendLine($"body = {content}");
            responseBody.Position = 0;
            await responseBody.CopyToAsync(originalResponseBody);
            context.Response.Body = originalResponseBody;


            await _apiRequestlogger.LogResponse(responseContent.ToString(), context.Response.StatusCode, requestLogId, exceptionLogId);

            return;
        }
    }
}
