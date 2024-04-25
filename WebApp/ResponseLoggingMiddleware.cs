namespace WebApp
{
    public class ResponseLoggingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ResponseLoggingMiddleware> _logger;

        public ResponseLoggingMiddleware(RequestDelegate next, ILogger<ResponseLoggingMiddleware> logger)
        {
            this.next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                var originalBodyStream = context.Response.Body;
                using (var responseBody = new MemoryStream())
                {
                    context.Response.Body = responseBody;

                    await next(context);
                    var response = await FormatResponse(context.Response);
                    if (!string.IsNullOrEmpty(response))
                    {
                        _logger.LogInformation(response);
                    }                    
                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, $"{GetType().Name}: Failed to process HttpContext.Response.Body");
            }          
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return text;
        }
    }
}
