
namespace CP.VPOS.Banks.Halkbank
{
    internal class HalkbankVirtualPOSService : NestpayVirtualPOSService
    {
        private static readonly string _urlAPILive = "https://sanalpos.halkbank.com.tr/fim/api";
        private static readonly string _url3DLive = "https://sanalpos.halkbank.com.tr/fim/est3Dgate";

        //test panel: https://entegrasyon.asseco-see.com.tr/halk/report/user.login

        public HalkbankVirtualPOSService() : base(_urlAPILive, _url3DLive)
        {
        }
    }
}
