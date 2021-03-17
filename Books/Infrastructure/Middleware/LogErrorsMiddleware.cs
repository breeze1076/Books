using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Books.Infrastructure.Middleware
{
    public class LogErrorsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogErrorsMiddleware> _logger;

        public LogErrorsMiddleware(RequestDelegate next, ILogger<LogErrorsMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context, LogScopeProvider logScopeProvider)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                var scope = logScopeProvider.GetScope(context);
                using (_logger.BeginScope(scope))
                {
                    _logger.LogError("{errorMessage}{stackTrace}", e.Message, e.StackTrace);
                }
                throw;
            }

        }
    }

    public static class LogErrorsMiddlewareExtension
    {
        public static IApplicationBuilder UseLogErrorsMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LogErrorsMiddleware>();
        }
    }
}
