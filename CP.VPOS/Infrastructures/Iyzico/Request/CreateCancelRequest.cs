using System;

namespace CP.VPOS.Infrastructures.Iyzico.Request
{
    internal class CreateCancelRequest : BaseRequest
    {
        public String PaymentId { get; set; }
        public String Ip { get; set; }
        public String Reason { get; set; }
        public String Description { get; set; }

        public override String ToPKIRequestString()
        {
            return ToStringRequestBuilder.NewInstance()
                .AppendSuper(base.ToPKIRequestString())
                .Append("paymentId", PaymentId)
                .Append("ip", Ip)
                .Append("reason", Reason)
                .Append("description", Description)
                .GetRequestString();
        }
    }
}
