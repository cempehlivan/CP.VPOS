using System;

namespace CP.VPOS.Infrastructures.Iyzico.Request
{

    internal class CreateAmountBasedRefundRequest : BaseRequest
    {
        public String PaymentId { get; set; }
        public String Price { get; set; }
        public String Ip { get; set; }

        public override String ToPKIRequestString()
        {
            return ToStringRequestBuilder.NewInstance()
                .AppendSuper(base.ToPKIRequestString())
                .Append("paymentId", PaymentId)
                .AppendPrice("price", Price)
                .Append("ip", Ip)
                .GetRequestString();
        }
    }
}