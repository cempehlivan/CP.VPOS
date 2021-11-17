using CP.VPOS.Banks;
using CP.VPOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CP.VPOS.Services
{
    public static class BankService
    {
        public static readonly string Akbank = "0046";
        public static readonly string AlbarakaTurk = "0203";
        public static readonly string AlternatifBank = "0124";
        public static readonly string Anadolubank = "0135";

        public static readonly string Halkbank = "0012";

        public static readonly List<Bank> allBanks = new List<Bank>()
        {
            new Bank{BankCode = "0046", BankName = "Akbank", BankService = typeof(AkbankVirtualPOSService) },
            new Bank{BankCode = "0203", BankName = "Albaraka Türk" },
            new Bank{BankCode = "0124", BankName = "Alternatif Bank", BankService = typeof(AlternatifBankVirtualPOSService)},
            new Bank{BankCode = "0135", BankName = "Anadolubank", BankService = typeof(AnadolubankVirtualPOSService)},
            new Bank{BankCode = "0134", BankName = "Denizbank"},
            new Bank{BankCode = "0103", BankName = "Fibabanka"},
            new Bank{BankCode = "0111", BankName = "QNB Finansbank"},
            new Bank{BankCode = "0062", BankName = "Garanti BBVA"},
            new Bank{BankCode = "0012", BankName = "Halkbank", BankService = typeof(HalkbankVirtualPOSService)},
            new Bank{BankCode = "0123", BankName = "HSBC"},
            new Bank{BankCode = "0099", BankName = "ING Bank"},
            new Bank{BankCode = "0064", BankName = "İş Bankası"},
            new Bank{BankCode = "0205", BankName = "Kuveyt Türk"},
            new Bank{BankCode = "0146", BankName = "Odeabank"},
            new Bank{BankCode = "0032", BankName = "Türk Ekonomi Bankası"},
            new Bank{BankCode = "0206", BankName = "Türkiye Finans"},
            new Bank{BankCode = "0015", BankName = "Vakıfbank"},
            new Bank{BankCode = "0067", BankName = "Yapı Kredı Bankası"},
            new Bank{BankCode = "0059", BankName = "Şekerbank"},
            new Bank{BankCode = "0010", BankName = "Ziraat Bankası"},
            new Bank{BankCode = "0143", BankName = "Aktif Yatırım Bankası"},

            new Bank{BankCode = "9992", BankName = "Hepsipay", CollectiveVPOS = true, InstallmentAPI = true, CommissionAutoAdd = true},
            new Bank{BankCode = "9993", BankName = "Payten", CollectiveVPOS = true, InstallmentAPI = true, CommissionAutoAdd = true},
            new Bank{BankCode = "9994", BankName = "PayTR", CollectiveVPOS = true, InstallmentAPI = true},
            new Bank{BankCode = "9995", BankName = "IPara", CollectiveVPOS = true, InstallmentAPI = true},
            new Bank{BankCode = "9996", BankName = "PayU", CollectiveVPOS = true, InstallmentAPI = true},
            new Bank{BankCode = "9997", BankName = "Iyzico", CollectiveVPOS = true, InstallmentAPI = true},
            new Bank{BankCode = "9998", BankName = "Cardplus" },
            new Bank{BankCode = "9999", BankName = "Paratika", CollectiveVPOS = true, InstallmentAPI = true, CommissionAutoAdd = true},
        };
    }
}
