namespace Utility.Logging
{
    public interface ILogger
    {
        void Info(string message, string category);
        void Log(Log log);
    }
}
