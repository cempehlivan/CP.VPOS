
namespace CP.VPOS.Banks.Sekerbank
{
    internal class SekerbankVirtualPOSService : NestpayVirtualPOSService
    {
        private static readonly string _urlAPILive = "https://sanalpos.sekerbank.com.tr/fim/api";
        private static readonly string _url3DLive = "https://sanalpos.sekerbank.com.tr/fim/est3Dgate";

        //test panel: https://entegrasyon.asseco-see.com.tr/ziraat/report/user.login
        //canlı panel: https://sanalpos.sekerbank.com.tr/sekerbank/report/user.login

        public SekerbankVirtualPOSService() : base(_urlAPILive, _url3DLive)
        {
        }
    }
}
