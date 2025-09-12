
namespace CP.VPOS.Banks.TurkiyeFinans
{
    internal class TurkiyeFinansVirtualPOSService : NestpayVirtualPOSService
    {
        private static readonly string _urlAPILive = "https://sanalpos.turkiyefinans.com.tr/fim/api";
        private static readonly string _url3DLive = "https://sanalpos.turkiyefinans.com.tr/fim/est3Dgate";

        //test panel: https://entegrasyon.asseco-see.com.tr/ziraat/report/user.login
        //canlı panel: https://sanalpos.turkiyefinans.com.tr/tfkb/report/user.login

        public TurkiyeFinansVirtualPOSService() : base(_urlAPILive, _url3DLive)
        {
        }
    }
}
