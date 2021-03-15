using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Books.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace Books.Infrastructure.Middleware
{
    public class LogRequestsMiddleware
    {
        private readonly RequestDelegate _next; 
        private readonly ILogger<LogRequestsMiddleware> _logger;

        public LogRequestsMiddleware(RequestDelegate next, ILogger<LogRequestsMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context, IOptionsSnapshot<LoggingMiddlewareOptions> options, ExecutionContext executionContext)
        {
            if (options.Value.LogRequests)
            {
                var scope = new Dictionary<string, string> { { "RequestCode", context.TraceIdentifier } };
                using (_logger.BeginScope(scope))
                {
                    var path = context.Request.Path;
                    var method = context.Request.Method;
                    var queryString = context.Request.QueryString;
                    var body = await GetBodyFromRequestAsync(context.Request);
                    var headers = context.Request.Headers;
                    _logger.LogInformation("{path}{@executionContext}", path, executionContext);
                    _logger.LogInformation("{method}{@executionContext}", method, executionContext);
                    _logger.LogInformation("{queryString}{@executionContext}", queryString, executionContext);
                    _logger.LogInformation("{body}{@executionContext}", body, executionContext);
                }
            }
            await _next(context);
        }

        private static async Task<string> GetBodyFromRequestAsync(HttpRequest request)
        {
            request.EnableBuffering();
            var result = await new StreamReader(request.Body).ReadToEndAsync();
            request.Body.Position = 0;
            return result;
        }
    }

    public static class LogRequestsMiddlewareExtension
    {
        public static IApplicationBuilder UseLogRequestsMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LogRequestsMiddleware>();
        }
    }
}
