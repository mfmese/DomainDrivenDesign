using System;
using System.Collections.Generic;

namespace Domain.BusinessBase
{
    public class BusinessResponse
    {
        public BusinessResult Result { get; set; }

        public BusinessResponse()
        {
            Result = new BusinessResult();
        }

        public bool IsSuccess => Result.IsSuccess;

        public bool IsFailed => !Result.IsSuccess;

        public string GetStatus => Result.IsSuccess ? "Success" : "Failed";

        public BusinessResponse Clear()
        {
            Result.IsSuccess = true;
            Result.Messages.Clear();
            return this;
        }

        public void SetSuccess(string message, string messageCode = "OK")
        {
            Result.IsSuccess = true;

            if (!string.IsNullOrEmpty(message))
            {
                InsertMessage(message, messageCode);
            }
        }

        public void SetFailed()
        {
            Result.IsSuccess = false;
        }

        public void SetFailed(string message, string messageCode, string relatedItem = "", bool isEndUserMessage = true)
        {
            SetFailed();

            if (!string.IsNullOrEmpty(message))
            {
                InsertMessage(message, messageCode, relatedItem, isEndUserMessage);
            }
        }

        public void SetFailed(string message, string messageCode, Exception ex)
        {
            SetFailed();

            Result.SetException(ex);

            if(ex != null)
            {
                InsertMessage(ex.Message + ". " + ex.InnerException?.Message, "EXCEPTION_DETAIL", isEndUserMessage: false);
            }

            InsertMessage(message, messageCode, isEndUserMessage: true);
        }

        public void SetFailed(List<BusinessMessage> messages)
        {
            SetFailed();
            messages.ForEach (message => Result.Messages.Add(message) );
        }


        private void InsertMessage(string message, string messageCode, string relatedItem = "", bool isEndUserMessage = true)
        {
            throw new NotImplementedException();
        }
    }
}
