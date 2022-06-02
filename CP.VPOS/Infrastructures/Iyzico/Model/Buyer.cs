using System;

namespace CP.VPOS.Infrastructures.Iyzico.Model
{
    internal class Buyer : RequestStringConvertible
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public String IdentityNumber { get; set; }
        public String Email { get; set; }
        public String GsmNumber { get; set; }
        public String RegistrationDate { get; set; }
        public String LastLoginDate { get; set; }
        public String RegistrationAddress { get; set; }
        public String City { get; set; }
        public String Country { get; set; }
        public String ZipCode { get; set; }
        public String Ip { get; set; }

        public String ToPKIRequestString()
        {
            return ToStringRequestBuilder.NewInstance()
                .Append("id", Id)
                .Append("name", Name)
                .Append("surname", Surname)
                .Append("identityNumber", IdentityNumber)
                .Append("email", Email)
                .Append("gsmNumber", GsmNumber)
                .Append("registrationDate", RegistrationDate)
                .Append("lastLoginDate", LastLoginDate)
                .Append("registrationAddress", RegistrationAddress)
                .Append("city", City)
                .Append("country", Country)
                .Append("zipCode", ZipCode)
                .Append("ip", Ip)
                .GetRequestString();
        }
    }
}
