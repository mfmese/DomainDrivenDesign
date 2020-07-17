using System;
using System.Collections.Generic;
using System.Timers;

namespace Utility.Logging
{
    public class Logger : ILogger
    {
        [ThreadStatic] //Multi thread işlemlerde contextId değişkenin threadler arası paylaşılması için kullanılır.
        static string contextId;

        public static string ContextId
        {
            get
            {
                if(contextId == null)
                {
                    contextId = IdGenerator.GenrateProcessId();
                }

                return contextId;
            }
        }

        static ILogPersister LogPersister => new LogPersister();

        static List<Log> LogQueue = new List<Log>();        

        static ILogger logger = new Logger();

        static bool BufferEnabled = false;

        static Logger()
        {
            SetTimer();
        }

        private Logger() { }

        public static ILogger Current { get { return logger; } }

        private static void SetTimer()
        {
            System.Timers.Timer timer = new System.Timers.Timer(2000);
            timer.Interval = 5000;
            timer.Enabled = BufferEnabled;
            timer.Elapsed += Timer_Elapsed;
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Flush();
        }

        private static void Flush()
        {
            if(LogQueue.Count > 0)
            {
                lock (LogQueue)
                {
                    LogPersister.PersisLogs(LogQueue);
                    LogQueue.Clear();
                }
            }
        }

        public void Info(string message, string category)
        {
            Log(new Log().SetMessage(message).SetLogType(LogTypes.Info).SetCategory(category));
        }

        public void Log(Log log)
        {
            var internalLog = log.InternalLog;
            internalLog.ContextId = ContextId;           
            internalLog.LogDate = DateTime.Now;   
            internalLog.ServerName = internalLog.ServerName ?? Environment.MachineName;
            internalLog.LogType = internalLog.LogType ?? LogTypes.Info.ToString();
            internalLog.Message = internalLog.Message ?? "";
            internalLog.MessageCode = internalLog.MessageCode ?? "";
            internalLog.ServiceName = internalLog.ServiceName ?? "";
            internalLog.FunctionName = internalLog.FunctionName ?? "";
            internalLog.ResponseStatus = internalLog.ResponseStatus ?? "";
            internalLog.ClientIp = internalLog.ClientIp ?? "";

            log.IsLogged = true;
            log.DurationInSeconds = DateTime.Now.Subtract(internalLog.RequestDate).TotalSeconds;

            lock (LogQueue)
            {
                if (BufferEnabled)
                {
                    LogQueue.Add(log);
                }
                else
                {
                    LogPersister.PersistLog(log);
                }
            }

#if DEBUG
            Console.WriteLine("ContextId: {0}, LogDate: {1}, ServerName: {2}, LogType: {3}, Message: {4}, MessageCode: {5}, " +
                "ServiceName: {6}, FunctionName: {7}, ResponseStatus: {8}, ClientIp: {9}, IsLogged: {10}, DurationInSeconds: {11}",
                internalLog.ContextId, internalLog.LogDate, internalLog.ServerName, internalLog.LogType, internalLog.Message,
                internalLog.MessageCode, internalLog.ServiceName, internalLog.FunctionName, internalLog.ResponseStatus,
                internalLog.ClientIp, log.IsLogged, log.DurationInSeconds);
            System.Threading.Thread.Sleep(1000);
#endif
        }
    }
}
