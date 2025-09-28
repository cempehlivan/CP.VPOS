
namespace CP.VPOS.Banks.ZiraatBankasi
{
    internal class ZiraatBankasiVirtualPOSService : NestpayVirtualPOSService
    {
        private static readonly string _urlAPILive = "https://sanalpos2.ziraatbank.com.tr/fim/api";
        private static readonly string _url3DLive = "https://sanalpos2.ziraatbank.com.tr/fim/est3Dgate";

        //test panel: https://entegrasyon.asseco-see.com.tr/ziraat/report/user.login

        public ZiraatBankasiVirtualPOSService() : base(_urlAPILive, _url3DLive)
        {
        }
    }
}
