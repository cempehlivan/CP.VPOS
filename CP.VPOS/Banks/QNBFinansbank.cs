
namespace CP.VPOS.Banks
{
    internal class QNBFinansbankVirtualPOSService : NestpayVirtualPOSService
    {
        private static readonly string _urlAPILive = "https://www.fbwebpos.com/fim/api";
        private static readonly string _url3DLive = "https://www.fbwebpos.com/fim/est3Dgate";

        //test panel: https://entegrasyon.asseco-see.com.tr/finans/report/user.login

        public QNBFinansbankVirtualPOSService() : base(_urlAPILive, _url3DLive)
        {
        }
    }
}
