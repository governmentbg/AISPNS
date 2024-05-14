using System.Text;
using System.Xml.Linq;




namespace RegixSOAPService
{
    public class RegixSoapRequestResponseSniffingMiddleware
    {
        private readonly RequestDelegate _next;

        public RegixSoapRequestResponseSniffingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                if (context.Request.Method == "POST")
                {
                    // Clone the request body
                    var originalRequestBody = context.Request.Body;
                    var requestBodyStream = new MemoryStream();
                    await context.Request.Body.CopyToAsync(requestBodyStream);
                    requestBodyStream.Seek(0, SeekOrigin.Begin);

                    // Read and modify the request body
                    string requestBodyText = new StreamReader(requestBodyStream).ReadToEnd();
                    string modifiedRequestBody = ModifyRequestBody(requestBodyText); // Implement this method based on your logic

                    // Replace the request body with the modified version
                    var modifiedRequestBodyStream = new MemoryStream(Encoding.UTF8.GetBytes(modifiedRequestBody));
                    context.Request.Body = modifiedRequestBodyStream;

                    // Setup for modifying the response
                    var originalResponseBody = context.Response.Body;
                    using (var responseBody = new MemoryStream())
                    {
                        context.Response.Body = responseBody;

                        // Invoke the next middleware in the pipeline
                        await _next.Invoke(context);

                        // Reset the request body stream for the next middleware
                        modifiedRequestBodyStream.Seek(0, SeekOrigin.Begin);
                        context.Request.Body = originalRequestBody;

                        // Your existing code to handle the response
                        string response = await GetResponse(context, responseBody, originalResponseBody);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<string> GetRequest(HttpContext context)
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

            return requestContent.ToString();
        }

        private async Task<string> GetResponse(HttpContext context, MemoryStream responseBody, Stream originalResponseBody)
        {
            var b = context.Response.StatusCode;

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


            return responseContent.ToString();
        }

        private string ModifyRequestBody(string sourceString)
        {
            var testlist = new List<string> { "QUERY", "PLUS", "LOGIN" };

            foreach (string name in testlist)
            {
                if (sourceString.Contains(":" + name)) return SetNamespacePrefixToAllChildren(sourceString, name);
            }

            return sourceString;
        }


        private static string SetNamespacePrefixToAllChildren(string xmlContent, string elementName)
        {
            try
            {
                var doc = XDocument.Parse(xmlContent);
                var ns = doc.Root.GetDefaultNamespace();

                var queryElements = doc.Descendants().Where(x => x.Name.LocalName == elementName);

                foreach (var element in queryElements)
                {
                    XNamespace currentNs = element.Name.Namespace;

                    foreach (var child in element.Elements())
                    {
                        XName newName = currentNs + child.Name.LocalName;
                        child.Name = newName;
                    }
                }

                return doc.ToString();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while processing the XML", ex);
            }
        }
    }

}

