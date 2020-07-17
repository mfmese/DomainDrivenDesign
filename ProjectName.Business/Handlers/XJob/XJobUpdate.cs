using Business.BusinessBase;
using Domain.BusinessBase;
using System;

namespace Business
{
    public class XJobRequest: BusinessRequest
    {

    }
    public class XJobResponse : BusinessResponse
    {

    }

    public class XJobUpdate : BaseHandler<XJobRequest, XJobResponse>
    {
        protected override XJobResponse HandleRequest(XJobRequest request)
        {
            Console.WriteLine("XJobHandler working here...");

            return new XJobResponse();
        }
    }
}
