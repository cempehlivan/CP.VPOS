using System;

namespace CP.VPOS.Infrastructures.Iyzico.Model
{
    internal class PaymentCard : RequestStringConvertible
    {
        public String CardHolderName { get; set; }
        public String CardNumber { get; set; }
        public String ExpireYear { get; set; }
        public String ExpireMonth { get; set; }
        public String Cvc { get; set; }
        public int? RegisterCard { get; set; }
        public String CardAlias { get; set; }
        public String CardToken { get; set; }
        public String CardUserKey { get; set; }
        public String ToPKIRequestString()
        {
            return ToStringRequestBuilder.NewInstance()
                .Append("cardHolderName", CardHolderName)
                .Append("cardNumber", CardNumber)
                .Append("expireYear", ExpireYear)
                .Append("expireMonth", ExpireMonth)
                .Append("cvc", Cvc)
                .Append("registerCard", RegisterCard)
                .Append("cardAlias", CardAlias)
                .Append("cardToken", CardToken)
                .Append("cardUserKey", CardUserKey)
                .GetRequestString();
        }
    }
}
