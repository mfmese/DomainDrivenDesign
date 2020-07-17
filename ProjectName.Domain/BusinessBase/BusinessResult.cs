using System;
using System.Collections.Generic;

namespace Domain.BusinessBase
{
    public class BusinessResult
    {
        public bool IsSuccess { get; set; }

        public string ResponseId { get; set; }

        public List<BusinessMessage> Messages { get; set; }

        private Exception _exception;

        public BusinessResult()
        {
            Messages = new List<BusinessMessage>();
        }

        public void SetException(Exception exception)
        {
            _exception = exception;
        }

        public Exception GetException()
        {
            return _exception;
        }
    }
}
