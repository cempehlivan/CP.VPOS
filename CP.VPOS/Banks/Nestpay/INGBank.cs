
namespace CP.VPOS.Banks.INGBank
{
    internal class INGBankVirtualPOSService : NestpayVirtualPOSService
    {
        private static readonly string _urlAPILive = "https://sanalpos.ingbank.com.tr/fim/api";
        private static readonly string _url3DLive = "https://sanalpos.ingbank.com.tr/fim/est3Dgate";

        //test panel: https://entegrasyon.asseco-see.com.tr/ingbank/report/user.login

        public INGBankVirtualPOSService() : base(_urlAPILive, _url3DLive)
        {
        }
    }
}
