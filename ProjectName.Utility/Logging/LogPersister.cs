using Integration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Utility.Logging
{
    public class LogPersister : ILogPersister
    {
        public void PersisLogs(List<Log> logs)
        {
            PersistLogs(null, logs);
        }

        public void PersistLog(Log log)
        {
            PersistLogs(log, null);
        }

        private void PersistLogs(Log log, List<Log> logs)
        {
            try
            {
                using(var dc = DataContextFactory.Create())
                {
                    if(log != null)
                    {
                        dc.ApplicationLogs.Add(log.InternalLog);
                    }
                    else
                    {
                        if(logs != null)
                        {
                            logs.ForEach(x => dc.ApplicationLogs.Add(x.InternalLog));
                        }
                    }
                    dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                WriteFileLog("ApplicaitonLogger.LoggingError " + GetNowString(), ex.Message);
                Fallback(log, logs);
            }
        }

        private string GetNowString()
        {
            return DateTime.Now.ToString("yyyy-dd-MM");
        }

        private void Fallback(Log log, List<Log> logs)
        {
            try
            {
                string json;
                if(log != null)
                {
                    json = Newtonsoft.Json.JsonConvert.SerializeObject(log.InternalLog);
                }
                else
                {
                    json = Newtonsoft.Json.JsonConvert.SerializeObject(logs.Select(x => x.InternalLog));
                }
                WriteFileLog("ApplicationLogger.Fallback.Logs." + GetNowString(), json);
            }
            catch (Exception ex)
            {
                WriteFileLog("ApplicationLogger.Fallback.Error" + GetNowString(), ex.Message);
            }
        }

        private void WriteFileLog(string fileName, string content)
        {
            string applicationDirectory = AppDomain.CurrentDomain.BaseDirectory;

            string directoryName = Path.Combine(applicationDirectory, "Logs");

            string filePath = null;

            if (Directory.Exists(directoryName))
            {
                filePath = Path.Combine(directoryName, fileName) + ".appLog";
            }
            else
            {
                filePath = Path.Combine(applicationDirectory, fileName) + ".appLog";
            }

            System.IO.File.AppendAllText(filePath, content);
        }
    }
}
