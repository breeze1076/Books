namespace Books.Infrastructure.Configuration
{
    public class LoggingMiddlewareOptions
    {
        public const string LoggingMiddleware = "Logging:LoggingMiddleware";
        public bool LogRequests { get; set; }
    }
}
