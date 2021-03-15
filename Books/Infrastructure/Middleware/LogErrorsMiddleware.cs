using System;
using System.Net;
using System.Threading.Tasks;
using Books.Models;
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

        public async Task Invoke(HttpContext context, ExecutionContext executionContext)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                _logger.LogError("{errorMessage}{stackTrace}{@executionContext}", e.Message, e.StackTrace, executionContext);
                throw;
            }

        }

        /*
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error from the custom middleware."
            }.ToString());
        }
        */
    }

    public static class LogErrorsMiddlewareExtension
    {
        public static IApplicationBuilder UseLogErrorsMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LogErrorsMiddleware>();
        }
    }
}
