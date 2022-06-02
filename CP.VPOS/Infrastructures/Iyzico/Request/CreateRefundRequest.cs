using System;

namespace CP.VPOS.Infrastructures.Iyzico.Request
{
    internal class CreateRefundRequest : BaseRequest
    {
        public String PaymentTransactionId { get; set; }
        public String Price { get; set; }
        public String Ip { get; set; }
        public String Currency { get; set; }
        public String Reason { get; set; }
        public String Description { get; set; }

        public override String ToPKIRequestString()
        {
            return ToStringRequestBuilder.NewInstance()
                .AppendSuper(base.ToPKIRequestString())
                .Append("paymentTransactionId", PaymentTransactionId)
                .AppendPrice("price", Price)
                .Append("ip", Ip)
                .Append("currency", Currency)
                .Append("reason", Reason)
                .Append("description", Description)
                .GetRequestString();
        }
    }
}
