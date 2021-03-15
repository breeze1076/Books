using System;
using System.Reflection;
using System.Threading;

namespace Books.Infrastructure.Middleware
{
    public class ExecutionContext
    {
        public string ApplicationName { get; }
        public string MachineName { get; }
        public string EnvironmentName { get; }
        public int ThreadId => Thread.CurrentThread.ManagedThreadId;

        public ExecutionContext()
        {
            ApplicationName = Assembly.GetEntryAssembly()?.GetName().Name;
            MachineName = Environment.MachineName;
            EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        }
}
}
