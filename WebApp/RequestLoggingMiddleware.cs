using NLog;
using System.Text;

namespace WebApp
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ResponseLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<ResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                context.Request.EnableBuffering();
                string requestBody = await ReadPostedBodyFromStream(context.Request.Body);
                var builder = new StringBuilder();
                builder.Append($"{nameof(context.Request.Path)}:{context.Request.Path}");

                if (!string.IsNullOrEmpty(requestBody))
                {
                    builder.AppendLine();
                    builder.Append($"Request body:{requestBody}");
                }
                string r = builder.ToString();
                _logger.LogInformation(r);
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, $"{nameof(RequestLoggingMiddleware)}: Failed to read HttpContext.Request.Body");
            }
            finally
            {
                await _next(context);
            }            
        }

        private async Task<string> ReadPostedBodyFromStream(Stream stream)
        {
            if (!stream.CanSeek)
            {
                _logger.LogDebug($"{nameof(RequestLoggingMiddleware)}: HttpContext.Request.Body stream is non-seekable");
                return string.Empty;
            }

            string responseText = null;

            var originalPosition = stream.Position;

            try
            {
                stream.Position = 0;

                using (var streamReader = new StreamReader(
                           stream,
                           Encoding.UTF8,
                           true,
                           1024,
                           leaveOpen: true))
                {
                    responseText = await streamReader.ReadToEndAsync().ConfigureAwait(false);
                }
            }
            finally
            {
                stream.Position = originalPosition;
            }

            return responseText;
        }
    }
}
