
namespace CP.VPOS.Banks.Anadolubank
{
    internal class AnadolubankVirtualPOSService : NestpayVirtualPOSService
    {
        private static readonly string _urlAPILive = "https://anadolusanalpos.est.com.tr/fim/api";
        private static readonly string _url3DLive = "https://anadolusanalpos.est.com.tr/fim/est3Dgate";

        //test panel: https://entegrasyon.asseco-see.com.tr/anadolu/report/user.login

        public AnadolubankVirtualPOSService() : base(_urlAPILive, _url3DLive)
        {
        }
    }
}
