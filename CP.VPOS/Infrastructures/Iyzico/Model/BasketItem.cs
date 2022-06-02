using System;

namespace CP.VPOS.Infrastructures.Iyzico.Model
{
    internal class BasketItem : RequestStringConvertible
    {
        public String Id { get; set; }
        public String Price { get; set; }
        public String Name { get; set; }
        public String Category1 { get; set; }
        public String Category2 { get; set; }
        public String ItemType { get; set; }
        public String SubMerchantKey { get; set; }
        public String SubMerchantPrice { get; set; }

        public String ToPKIRequestString()
        {
            return ToStringRequestBuilder.NewInstance()
                .Append("id", Id)
                .AppendPrice("price", Price)
                .Append("name", Name)
                .Append("category1", Category1)
                .Append("category2", Category2)
                .Append("itemType", ItemType)
                .Append("subMerchantKey", SubMerchantKey)
                .AppendPrice("subMerchantPrice", SubMerchantPrice)
                .GetRequestString();
        }
    }
}
