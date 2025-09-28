
namespace CP.VPOS.Banks.QNBFinansbank
{
    internal class FinansbankNestpayVirtualPOSService : NestpayVirtualPOSService
    {
        private static readonly string _urlAPILive = "https://www.fbwebpos.com/fim/api";
        private static readonly string _url3DLive = "https://www.fbwebpos.com/fim/est3Dgate";

        //test panel: https://entegrasyon.asseco-see.com.tr/finans/report/user.login

        public FinansbankNestpayVirtualPOSService() : base(_urlAPILive, _url3DLive)
        {
        }
    }
}
