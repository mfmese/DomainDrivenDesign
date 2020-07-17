using System;
using System.ComponentModel.DataAnnotations;

namespace Integration.LogDbIntegration
{
    public class ApplicationLog
    {
        [Key]
        public long LogId { get; set; }
        public string ContextId { get; set; }
        public DateTime RequestDate { get; set; }
        public string LogType { get; set; }
        public string Category { get; set; }
        public string ServiceName { get; set; }
        public string FunctionName { get; set; }
        public string ResponseStatus { get; set; }
        public string Message { get; set; }
        public string MessageCode { get; set; }
        public Nullable<double> DurationInSeconds { get; set; }
        public string Exception { get; set; }
        public string ServiceUrl { get; set; }
        public string ServerName { get; set; }
        public string ClientUsername { get; set; }
        public string ClientIp { get; set; }
        public DateTime LogDate { get; set; }
        public string ResponseId { get; set; }
        public string ProcessId { get; set; }
        public string Input { get; set; }
    }
}
