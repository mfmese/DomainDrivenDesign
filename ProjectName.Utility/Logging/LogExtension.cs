using Domain.BusinessBase;
using System.Linq;

namespace Utility.Logging
{
    public static class LogExtension
    {
        public static Log Write(this Log log)
        {
            Logger.Current.Log(log);
            return log;
        }

        public static void WriteAs(this BusinessResponse response, LogTypes logType, string functionName = null)
        {
            new Log().SetCategory("BusinessHandler").SetOutput(response)
                .SetServiceName(response.GetType().Name.Replace("Response", "") + "Handler")
                .SetFunctionName(functionName)
                .SetMessage(response.Result.Messages?.FirstOrDefault()?.MessageText)
                .SetMessageCode(response.Result?.Messages?.FirstOrDefault()?.MessageCode)
                .SetException(response.Result.GetException())
                .SetLogType(logType)
                .Write();       
        }        

        public static Log WriteAs(this Log log, bool isSuccess)
        {
            log.SetLogType(isSuccess ? LogTypes.Info : LogTypes.Error);
            Logger.Current.Log(log);
            return log;
        }

        public static Log WriteAsInfo(this Log log)
        {
            log.SetLogType(LogTypes.Info);
            Logger.Current.Log(log);
            return log;
        }

        public static Log WriteAsError(this Log log)
        {
            log.SetLogType(LogTypes.Error);
            Logger.Current.Log(log);
            return log;
        }

        public static Log WriteAsWarning(this Log log)
        {
            log.SetLogType(LogTypes.Warning);
            Logger.Current.Log(log);
            return log;
        }

        public static Log WriteAsCritical(this Log log)
        {
            log.SetLogType(LogTypes.Critical);
            Logger.Current.Log(log);
            return log;
        }
    }
}
