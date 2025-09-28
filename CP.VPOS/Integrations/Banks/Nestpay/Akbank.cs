namespace CP.VPOS.Banks.Akbank
{
    internal class AkbankNestpayVirtualPOSService : NestpayVirtualPOSService
    {
        private static readonly string _urlAPILive = "https://www.sanalakpos.com/fim/api";
        private static readonly string _url3DLive = "https://www.sanalakpos.com/fim/est3Dgate";

        //test panel: https://entegrasyon.asseco-see.com.tr/akbank/report/user.login

        public AkbankNestpayVirtualPOSService() : base(_urlAPILive, _url3DLive)
        {

        }
    }
}
