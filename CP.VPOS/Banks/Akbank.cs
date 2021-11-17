
namespace CP.VPOS.Banks
{
    internal class AkbankVirtualPOSService : NestpayVirtualPOSService
    {
        private static readonly string _urlAPILive = "https://www.sanalakpos.com/fim/api";
        private static readonly string _url3DLive = "https://www.sanalakpos.com/fim/est3Dgate";

        //test panel: https://entegrasyon.asseco-see.com.tr/akbank/report/user.login

        public AkbankVirtualPOSService() : base(_urlAPILive, _url3DLive)
        {

        }
    }
}
