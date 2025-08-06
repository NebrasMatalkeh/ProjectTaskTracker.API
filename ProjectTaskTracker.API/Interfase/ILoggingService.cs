
namespace ProjectTaskTracker.API.Interfase
{
    public interface ILoggingService
    {
        void LogRequest(string message);
        void LogResponse(string message);
        //void LogRequest(string method, string path, string queryString, string traceId);

        //void LogResponse(string method, string path, int statusCode, long durationMs, string traceId);
    }
}
