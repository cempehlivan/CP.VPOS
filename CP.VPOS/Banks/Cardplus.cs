
namespace CP.VPOS.Banks.Cardplus
{
    internal class CardplusVirtualPOSService : NestpayVirtualPOSService
    {
        private static readonly string _urlAPILive = "https://sanalpos.card-plus.net/fim/api";
        private static readonly string _url3DLive = "https://sanalpos.card-plus.net/fim/est3Dgate";

        //test panel: https://entegrasyon.asseco-see.com.tr/cardplus/report/user.login

        public CardplusVirtualPOSService() : base(_urlAPILive, _url3DLive)
        {
        }
    }
}
