
namespace CP.VPOS.Banks.TurkEkonomiBankasi
{
    internal class TurkEkonomiBankasiVirtualPOSService : NestpayVirtualPOSService
    {
        private static readonly string _urlAPILive = "https://sanalpos.teb.com.tr/fim/api";
        private static readonly string _url3DLive = "https://sanalpos.teb.com.tr/fim/est3Dgate";

        //test panel: https://entegrasyon.asseco-see.com.tr/teb/report/user.login

        public TurkEkonomiBankasiVirtualPOSService() : base(_urlAPILive, _url3DLive)
        {
        }
    }
}
