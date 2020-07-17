using System;

namespace Utility
{
    public static class IdGenerator
    {
        public static string GenrateProcessId()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss:") + Guid.NewGuid().ToString();
        }
    }
}
