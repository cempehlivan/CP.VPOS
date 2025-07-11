using CP.VPOS.Models;
using CP.VPOS.Banks.Akbank;
using CP.VPOS.Banks.AlternatifBank;
using CP.VPOS.Banks.Anadolubank;
using CP.VPOS.Banks.Cardplus;
using CP.VPOS.Banks.Denizbank;
using CP.VPOS.Banks.GarantiBBVA;
using CP.VPOS.Banks.Halkbank;
using CP.VPOS.Banks.INGBank;
using CP.VPOS.Banks.IsBankasi;
using CP.VPOS.Banks.Paratika;
using CP.VPOS.Banks.Payten;
using CP.VPOS.Banks.QNBFinansbank;
using CP.VPOS.Banks.Sekerbank;
using CP.VPOS.Banks.TurkEkonomiBankasi;
using CP.VPOS.Banks.TurkiyeFinans;
using CP.VPOS.Banks.Vakifbank;
using CP.VPOS.Banks.YapiKrediBankasi;
using CP.VPOS.Banks.ZiraatBankasi;
using CP.VPOS.Banks.Iyzico;
using CP.VPOS.Banks.Sipay;
using CP.VPOS.Banks.QNBpay;
using CP.VPOS.Banks.ParamPos;
using CP.VPOS.Banks.PayBull;
using CP.VPOS.Banks.Parolapara;
using CP.VPOS.Banks.IQmoney;
using CP.VPOS.Banks.Ahlpay;
using CP.VPOS.Banks.Moka;
using CP.VPOS.Banks.Vepara;
using CP.VPOS.Banks.ZiraatPay;
using CP.VPOS.Banks.VakifPayS;
using System.Collections.Generic;

namespace CP.VPOS.Services
{
    public static class BankService
    {
        public static readonly string Akbank = "0046";
        public static readonly string AkbankNestpay = "9046";
        public static readonly string AlbarakaTurk = "0203";
        public static readonly string AlternatifBank = "0124";
        public static readonly string Anadolubank = "0135";
        public static readonly string Denizbank = "0134";
        public static readonly string Fibabanka = "0103";
        public static readonly string QNBFinansbank = "0111";
        public static readonly string FinansbankNestpay = "9111";
        public static readonly string GarantiBBVA = "0062";
        public static readonly string Halkbank = "0012";
        public static readonly string HSBC = "0123";
        public static readonly string INGBank = "0099";
        public static readonly string IsBankasi = "0064";
        public static readonly string KuveytTurk = "0205";
        public static readonly string Odeabank = "0146";
        public static readonly string TurkEkonomiBankasi = "0032";
        public static readonly string TurkiyeFinans = "0206";
        public static readonly string Vakifbank = "0015";
        public static readonly string YapiKrediBankasi = "0067";
        public static readonly string Sekerbank = "0059";
        public static readonly string ZiraatBankasi = "0010";
        public static readonly string AktifYatirimBankasi = "0143";

        public static readonly string VakifPayS = "9981";
        public static readonly string ZiraatPay = "9982";
        public static readonly string Vepara = "9983";
        public static readonly string Moka = "9984";
        public static readonly string Ahlpay = "9985";
        public static readonly string IQmoney = "9986";
        public static readonly string Parolapara = "9987";
        public static readonly string PayBull = "9988";
        public static readonly string ParamPos = "9989";
        public static readonly string QNBpay = "9990";
        public static readonly string Sipay = "9991";
        public static readonly string Hepsipay = "9992";
        public static readonly string Payten = "9993";
        public static readonly string PayTR = "9994";
        public static readonly string IPara = "9995";
        public static readonly string PayU = "9996";
        public static readonly string Iyzico = "9997";
        public static readonly string Cardplus = "9998";
        public static readonly string Paratika = "9999";


        internal static readonly List<Bank> AllBanks = new List<Bank>()
        {
            new Bank{BankCode = "0046", BankName = "Akbank", BankService = typeof(AkbankVirtualPOSService) },
            new Bank{BankCode = "9046", BankName = "Akbank Nestpay", BankService = typeof(AkbankNestpayVirtualPOSService) },
            new Bank{BankCode = "0203", BankName = "Albaraka Türk" },
            new Bank{BankCode = "0124", BankName = "Alternatif Bank", BankService = typeof(AlternatifBankVirtualPOSService)},
            new Bank{BankCode = "0135", BankName = "Anadolubank", BankService = typeof(AnadolubankVirtualPOSService)},
            new Bank{BankCode = "0134", BankName = "Denizbank", BankService = typeof(DenizbankVirtualPOSService)},
            new Bank{BankCode = "0103", BankName = "Fibabanka"},
            new Bank{BankCode = "0111", BankName = "QNB Finansbank", BankService = typeof(QNBFinansbankVirtualPOSService)},
            new Bank{BankCode = "9111", BankName = "Finansbank Nestpay", BankService = typeof(FinansbankNestpayVirtualPOSService)},
            new Bank{BankCode = "0062", BankName = "Garanti BBVA", BankService = typeof(GarantiBBVAVirtualPOSService)},
            new Bank{BankCode = "0012", BankName = "Halkbank", BankService = typeof(HalkbankVirtualPOSService)},
            new Bank{BankCode = "0123", BankName = "HSBC"},
            new Bank{BankCode = "0099", BankName = "ING Bank", BankService = typeof(INGBankVirtualPOSService)},
            new Bank{BankCode = "0064", BankName = "İş Bankası", BankService = typeof(IsBankasiVirtualPOSService)},
            new Bank{BankCode = "0205", BankName = "Kuveyt Türk"},
            new Bank{BankCode = "0146", BankName = "Odeabank"},
            new Bank{BankCode = "0032", BankName = "Türk Ekonomi Bankası", BankService = typeof(TurkEkonomiBankasiVirtualPOSService)},
            new Bank{BankCode = "0206", BankName = "Türkiye Finans", BankService = typeof(TurkiyeFinansVirtualPOSService)},
            new Bank{BankCode = "0015", BankName = "Vakıfbank", BankService = typeof(VakifbankVirtualPOSService)},
            new Bank{BankCode = "0067", BankName = "Yapı Kredı Bankası", BankService = typeof(YapiKrediBankasiVirtualPOSService)},
            new Bank{BankCode = "0059", BankName = "Şekerbank", BankService = typeof(SekerbankVirtualPOSService)},
            new Bank{BankCode = "0010", BankName = "Ziraat Bankası", BankService = typeof(ZiraatBankasiVirtualPOSService)},
            new Bank{BankCode = "0143", BankName = "Aktif Yatırım Bankası"},

            new Bank{BankCode = "9981", BankName = "VakıfPayS", BankService = typeof(VakifPaySVirtualPOSService), CollectiveVPOS = true, InstallmentAPI = true, CommissionAutoAdd = true},
            new Bank{BankCode = "9982", BankName = "ZiraatPay", BankService = typeof(ZiraatPayVirtualPOSService), CollectiveVPOS = true, InstallmentAPI = true, CommissionAutoAdd = true},
            new Bank{BankCode = "9983", BankName = "Vepara", BankService = typeof(VeparaVirtualPOSService), CollectiveVPOS = true, InstallmentAPI = true, CommissionAutoAdd = true},
            new Bank{BankCode = "9984", BankName = "Moka", BankService = typeof(MokaVirtualPOSService), CollectiveVPOS = true, InstallmentAPI = true, CommissionAutoAdd = true},
            new Bank{BankCode = "9985", BankName = "Ahlpay", BankService = typeof(AhlpayVirtualPOSService), CollectiveVPOS = true, InstallmentAPI = true, CommissionAutoAdd = true},
            new Bank{BankCode = "9986", BankName = "IQmoney", BankService = typeof(IQmoneyVirtualPOSService), CollectiveVPOS = true, InstallmentAPI = true, CommissionAutoAdd = true},
            new Bank{BankCode = "9987", BankName = "Parolapara", BankService = typeof(ParolaparaVirtualPOSService), CollectiveVPOS = true, InstallmentAPI = true, CommissionAutoAdd = true},
            new Bank{BankCode = "9988", BankName = "PayBull", BankService = typeof(PayBullVirtualPOSService), CollectiveVPOS = true, InstallmentAPI = true, CommissionAutoAdd = true},
            new Bank{BankCode = "9989", BankName = "ParamPos", BankService = typeof(ParamPosVirtualPOSService), CollectiveVPOS = true, InstallmentAPI = true, CommissionAutoAdd = true},
            new Bank{BankCode = "9990", BankName = "QNBpay", BankService = typeof(QNBpayVirtualPOSService), CollectiveVPOS = true, InstallmentAPI = true, CommissionAutoAdd = true},
            new Bank{BankCode = "9991", BankName = "Sipay", BankService = typeof(SipayVirtualPOSService), CollectiveVPOS = true, InstallmentAPI = true, CommissionAutoAdd = true},
            new Bank{BankCode = "9992", BankName = "Hepsipay", CollectiveVPOS = true, InstallmentAPI = true, CommissionAutoAdd = true},
            new Bank{BankCode = "9993", BankName = "Payten", BankService = typeof(PaytenVirtualPOSService), CollectiveVPOS = true, InstallmentAPI = true, CommissionAutoAdd = true},
            new Bank{BankCode = "9994", BankName = "PayTR", CollectiveVPOS = true, InstallmentAPI = true},
            new Bank{BankCode = "9995", BankName = "IPara", CollectiveVPOS = true, InstallmentAPI = true},
            new Bank{BankCode = "9996", BankName = "PayU", CollectiveVPOS = true, InstallmentAPI = true},
            new Bank{BankCode = "9997", BankName = "Iyzico", BankService = typeof(IyzicoVirtualPOSService), CollectiveVPOS = true, InstallmentAPI = true},
            new Bank{BankCode = "9998", BankName = "Cardplus", BankService = typeof(CardplusVirtualPOSService) },
            new Bank{BankCode = "9999", BankName = "Paratika", BankService = typeof(ParatikaVirtualPOSService), CollectiveVPOS = true, InstallmentAPI = true, CommissionAutoAdd = true},
        };
    }
}
