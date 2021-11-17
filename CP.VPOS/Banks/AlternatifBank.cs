
namespace CP.VPOS.Banks.AlternatifBank
{
    internal class AlternatifBankVirtualPOSService : NestpayVirtualPOSService
    {
        private static readonly string _urlAPILive = "https://sanalpos.abank.com.tr/fim/api";
        private static readonly string _url3DLive = "https://sanalpos.abank.com.tr/fim/est3Dgate";

        //test panel: https://entegrasyon.asseco-see.com.tr/ziraat/report/user.login
        //canlı panel: https://sanalpos.abank.com.tr/abank/report/user.login

        public AlternatifBankVirtualPOSService() : base(_urlAPILive, _url3DLive)
        {

        }
    }
}
