using System;

namespace CP.VPOS.Infrastructures.Iyzico.Request
{
    internal class RetrieveInstallmentInfoRequest : BaseRequest
    {
        public String BinNumber { get; set; }
        public String Price { get; set; }

        public override String ToPKIRequestString()
        {
            return ToStringRequestBuilder.NewInstance()
                .AppendSuper(base.ToPKIRequestString())
                .Append("binNumber", BinNumber)
                .AppendPrice("price", Price)
                .GetRequestString();
        }
    }
}
