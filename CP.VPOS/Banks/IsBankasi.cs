
namespace CP.VPOS.Banks
{
    internal class IsBankasiVirtualPOSService : NestpayVirtualPOSService
    {
        private static readonly string _urlAPILive = "https://sanalpos.isbank.com.tr/fim/api";
        private static readonly string _url3DLive = "https://sanalpos.isbank.com.tr/fim/est3Dgate";

        //test panel: https://entegrasyon.asseco-see.com.tr/isbank/report/user.login

        public IsBankasiVirtualPOSService() : base(_urlAPILive, _url3DLive)
        {
        }
    }
}
