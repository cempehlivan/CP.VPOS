using Newtonsoft.Json;
using System;

namespace CP.VPOS.Infrastructures.Iyzico.Model
{
    internal class Address : RequestStringConvertible
    {
        [JsonProperty(PropertyName = "Address")]
        public String Description { get; set; }
        public String ZipCode { get; set; }
        public String ContactName { get; set; }
        public String City { get; set; }
        public String Country { get; set; }

        public String ToPKIRequestString()
        {
            return ToStringRequestBuilder.NewInstance()
                .Append("address", Description)
                .Append("zipCode", ZipCode)
                .Append("contactName", ContactName)
                .Append("city", City)
                .Append("country", Country)
                .GetRequestString();
        }
    }
}
