using Integration.LogDbIntegration;
using System;

namespace Utility.Logging
{
    public sealed class Log : IDisposable
    {
        public ApplicationLog InternalLog { get; set; }

        public Log()
        {
            InternalLog = new ApplicationLog { LogDate = DateTime.Now };
            InternalLog.LogType = LogTypes.Info.ToString();
        }

        internal double? DurationInSeconds { get { return InternalLog.DurationInSeconds; } set { InternalLog.DurationInSeconds = value; } }

        public bool IsLogged = false;

        public Log SetLogType(LogTypes logTypes)
        {
            InternalLog.LogType = logTypes.ToString();
            return this;
        }

        public Log SetCategory(string category)
        {
            InternalLog.Category = category;
            return this;
        }

        public Log SetMessage(string message)
        {
            InternalLog.Message = message;
            return this;
        }

        public Log SetMessageCode(string messageCode)
        {
            InternalLog.MessageCode = messageCode;
            return this;
        }

        public Log AppendMessage(string message)
        {
            InternalLog.Message += (string.IsNullOrEmpty(InternalLog.Message) ? "": " | ") + message;
            return this;
        }

        public Log PrePendMessage(string message)
        {
            InternalLog.Message = message + (string.IsNullOrEmpty(InternalLog.Message) ? "" : " | ") + InternalLog.Message;
            return this;
        }

        public Log ResetDuration()
        {
            InternalLog.RequestDate = DateTime.Now;
            return this;
        }

        public Log SetServiceName(string serviceName)
        {
            InternalLog.ServiceName = serviceName;
            return this;
        }

        public Log SetFunctionName(string functionName)
        {
            InternalLog.FunctionName = functionName;
            return this;
        }

        public Log SetStatus(string status)
        {
            InternalLog.ResponseStatus = status;
            return this;
        }

        public Log SetException(Exception ex)
        {
            InternalLog.Exception = (ex == null) ? null : ex.Message;
            return this;
        }

        public Log SetResponseId(string  responseId)
        {
            InternalLog.ResponseId = responseId;
            return this;
        }

        public Log SetProcessId(string processId)
        {
            InternalLog.ProcessId = processId;
            return this;
        }

        public Log SetServiceUrl(string serviceUrl)
        {
            InternalLog.ServiceUrl = serviceUrl;
            return this;
        }

        public Log SetClientUserName(string clientUserName)
        {
            InternalLog.ServiceName = clientUserName;
            return this;
        }

        public Log SetClientIp(string clientIp)
        {
            InternalLog.ClientIp = clientIp;
            return this;
        }

        public Log SetInput(object input)
        {
            if(input != null)
            {
                try
                {
                    if(input is String || input is int || input is long || input is byte)
                    {
                        InternalLog.Input = (string)input;
                    }
                    else
                    {
                        InternalLog.Input = Newtonsoft.Json.JsonConvert.SerializeObject(input);
                    }

                    HandleSensitiveData(input);
                }
                catch (Exception ex)
                {
                    InternalLog.Input = "Error while json serialization, input type " + input.GetType().FullName + " Error was " + ex.Message;
                }
            }
            return this;
        }

        public Log SetOutput(object output)
        {
            if (output != null)
            {
                try
                {
                    if (output is String || output is int || output is long || output is byte)
                    {
                        InternalLog.Input = (string)output;
                    }
                    else
                    {
                        InternalLog.Input = Newtonsoft.Json.JsonConvert.SerializeObject(output);
                    }
                }
                catch (Exception ex)
                {
                    InternalLog.Input = "Error while json serialization, input type " + output.GetType().FullName + " Error was " + ex.Message;
                }
            }
            return this;
        }

        private void HandleSensitiveData(object input)
        {
            if (!string.IsNullOrEmpty(InternalLog.Input))
            {
                Type inputBasetype = input.GetType().BaseType;

                if(inputBasetype != null && inputBasetype.Name == "ServiceRequest")
                {
                    InternalLog.Input = InternalLog.Input.Replace("password","*****");
                }
            }
        }

        public void Dispose()
        {
            if (!IsLogged)
            {
                Logger.Current.Log(this);
            }
        }
    }
}
