
namespace CP.VPOS.Banks.ZiraatBankasi
{
    internal class ZiraatBankasiVirtualPOSService : NestpayVirtualPOSService
    {
        private static readonly string _urlAPILive = "https://sanalpos2.ziraatbank.com.tr/fim/api";
        private static readonly string _url3DLive = "https://sanalpos2.ziraatbank.com.tr/fim/est3Dgate";

        private static readonly string _urlAPITest = "https://torus-stage-ziraat.asseco-see.com.tr/fim/api";
        private static readonly string _url3DTest = "https://torus-stage-ziraat.asseco-see.com.tr/fim/est3Dgate";

        //test panel: https://torus-stage-ziraat.asseco-see.com.tr/ziraat2/#/auth/login

        public ZiraatBankasiVirtualPOSService() : base(_urlAPILive, _url3DLive, _urlAPITest, _url3DTest)
        {
        }
    }
}
