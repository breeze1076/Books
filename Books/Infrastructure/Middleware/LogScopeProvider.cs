using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace Books.Infrastructure.Middleware
{
    public class LogScopeProvider
    {
        public Dictionary<string, object> GetScope(HttpContext context)
        {
            return new Dictionary<string, object>
            {
                { "RequestCode", context.TraceIdentifier },
                { "ApplicationName", Assembly.GetEntryAssembly()?.GetName().Name },
                { "MachineName", Environment.MachineName },
                { "EnvironmentName", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") },
                { "ThreadId", Thread.CurrentThread.ManagedThreadId }
            };
        }
    }
}
