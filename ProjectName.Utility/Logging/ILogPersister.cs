using System.Collections.Generic;

namespace Utility.Logging
{
    public interface ILogPersister
    {
        void PersistLog(Log log);
        void PersisLogs(List<Log> logs);
    }
}
