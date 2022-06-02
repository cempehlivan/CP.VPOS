using System;

namespace CP.VPOS.Infrastructures.Iyzico.Request
{
    internal class CreateThreedsPaymentRequest : BaseRequest
    {
        public String PaymentId { get; set; }
        public String ConversationData { get; set; }

        public override String ToPKIRequestString()
        {
            return ToStringRequestBuilder.NewInstance()
                .AppendSuper(base.ToPKIRequestString())
                .Append("paymentId", PaymentId)
                .Append("conversationData", ConversationData)
                .GetRequestString();
        }
    }
}
