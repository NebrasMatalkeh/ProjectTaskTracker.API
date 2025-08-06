using ProjectTaskTracker.API.Interfase;
using System.Diagnostics;

namespace ProjectTaskTracker.API.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, ILoggingService _loggingService)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            
            var traceId = context.TraceIdentifier;
            _loggingService.LogRequest($"Incoming Request: [{context.Request.Method}] {context.Request.Path} | TraceId: {traceId}");

            
            await _next(context);

            stopwatch.Stop();

           
            _loggingService.LogResponse($"Outgoing Response: [{context.Request.Method}] {context.Request.Path} responded [{context.Response.StatusCode}] in {stopwatch.ElapsedMilliseconds}ms | TraceId: {traceId}");

            

           
        }
     
        }
}

