using Domain.BusinessBase;
using System;
using Utility.Logging;

namespace Business.BusinessBase
{
    public abstract class BaseHandler<TRequest, TResponse>  where TRequest: BusinessRequest where TResponse: BusinessResponse, new()
    {
        protected TResponse Response;

        private Log _log;

        protected BaseHandler()
        {
            Response = new TResponse();          
        }

        public bool LogEnabled { get; set; } = true;

        public string ProcessId { get; set; } = "";

        protected abstract TResponse HandleRequest(TRequest request);

        protected virtual TResponse HandleRequestMock(TRequest request)
        {
            throw new ApplicationException("Mocking is not enabled for this request");
        }

        public BusinessResponse Excute(TRequest request)
        {
            try
            {
#if DEBUG
                Console.WriteLine("Execution started");
#endif 
                if(request == null)
                {
                    Response.SetFailed("Request is NULL", "REQUEST_IS_NULL");
                    return HandleBusinessResponse(Response);
                }

                if (string.IsNullOrEmpty(ProcessId))
                {
                    ProcessId = request.ProcessId;
                }

                if (LogEnabled)
                {
#if DEBUG
                    Console.WriteLine("logging");
#endif 
                    _log = new Log().SetCategory("BusinessHandler").SetStatus("Executing").SetInput(request).SetProcessId(request.ProcessId).SetServiceName(this.GetType().Name).WriteAsInfo();
                }

                Response = HandleRequest(request);
#if DEBUG
                Console.WriteLine("Execution finished");
#endif 
            }
            catch (Exception ex)
            {
                return HandleBusinessResponse(Response, ex);
            }

            return HandleBusinessResponse(Response); 
        }

        public TResponse ExecuteMock(TRequest request)
        {
            Response = HandleRequestMock(request);
            return Response;
        }

        private BusinessResponse HandleBusinessResponse(TResponse response, Exception ex = null)
        {
           if(ex != null)
            {
                response.SetFailed("Unhandled exception occured in handler", "UNHANDLED_EXCEPTION", ex);
            }

            return response;
        }
    }
}
