﻿using CP.VPOS.Banks;
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
        public static readonly string Denizbank = "0134";
        public static readonly string Fibabanka = "0103";
        public static readonly string QNBFinansbank = "0111";
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

        public static readonly string Hepsipay = "9992";
        public static readonly string Payten = "9993";
        public static readonly string PayTR = "9994";
        public static readonly string IPara = "9995";
        public static readonly string PayU = "9996";
        public static readonly string Iyzico = "9997";
        public static readonly string Cardplus = "9998";
        public static readonly string Paratika = "9999";


        public static readonly List<Bank> allBanks = new List<Bank>()
        {
            new Bank{BankCode = "0046", BankName = "Akbank", BankService = typeof(AkbankVirtualPOSService) },
            new Bank{BankCode = "0203", BankName = "Albaraka Türk" },
            new Bank{BankCode = "0124", BankName = "Alternatif Bank", BankService = typeof(AlternatifBankVirtualPOSService)},
            new Bank{BankCode = "0135", BankName = "Anadolubank", BankService = typeof(AnadolubankVirtualPOSService)},
            new Bank{BankCode = "0134", BankName = "Denizbank"},
            new Bank{BankCode = "0103", BankName = "Fibabanka"},
            new Bank{BankCode = "0111", BankName = "QNB Finansbank", BankService = typeof(QNBFinansbankVirtualPOSService)},
            new Bank{BankCode = "0062", BankName = "Garanti BBVA"},
            new Bank{BankCode = "0012", BankName = "Halkbank", BankService = typeof(HalkbankVirtualPOSService)},
            new Bank{BankCode = "0123", BankName = "HSBC"},
            new Bank{BankCode = "0099", BankName = "ING Bank", BankService = typeof(INGBankVirtualPOSService)},
            new Bank{BankCode = "0064", BankName = "İş Bankası", BankService = typeof(IsBankasiVirtualPOSService)},
            new Bank{BankCode = "0205", BankName = "Kuveyt Türk"},
            new Bank{BankCode = "0146", BankName = "Odeabank"},
            new Bank{BankCode = "0032", BankName = "Türk Ekonomi Bankası", BankService = typeof(TurkEkonomiBankasiVirtualPOSService)},
            new Bank{BankCode = "0206", BankName = "Türkiye Finans", BankService = typeof(TurkiyeFinansVirtualPOSService)},
            new Bank{BankCode = "0015", BankName = "Vakıfbank"},
            new Bank{BankCode = "0067", BankName = "Yapı Kredı Bankası"},
            new Bank{BankCode = "0059", BankName = "Şekerbank", BankService = typeof(SekerbankVirtualPOSService)},
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
