using ProjectTaskTracker.API.Interfase;

namespace ProjectTaskTracker.API.service
{
    public class ConsoleLoggingService : ILoggingService

    {
        public void LogRequest(string message)
        {
            Console.WriteLine($"INFO: {message}");
        }

        public void LogResponse(string message)
        {
            Console.WriteLine($"INFO: {message}");
        }
        //{
        //    public void LogRequest(string method, string path, string queryString, string traceId)
        //    {
        //        Console.WriteLine($"INFO: Incoming Request: [{method}] {path}{queryString} | TraceId: {traceId}");
        //    }

        //    public void LogResponse(string method, string path, int statusCode, long durationMs, string traceId)
        //    {
        //        Console.WriteLine($"INFO: Outgoing Response: [{method}] {path} responded [{statusCode}] in {durationMs}ms | TraceId: {traceId}");
        //    }
        //}
    }
}