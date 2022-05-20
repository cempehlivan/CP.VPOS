# CP.VPOS
[![Nuget v2](https://img.shields.io/nuget/v/CP.VPOS)](https://www.nuget.org/packages/CP.VPOS) [![CP.VPOS on fuget.org](https://www.fuget.org/packages/CP.VPOS/badge.svg)](https://www.fuget.org/packages/CP.VPOS) ![build](https://img.shields.io/github/workflow/status/cempehlivan/CP.VPOS/.NET) [![GitHub license](https://img.shields.io/github/license/cempehlivan/CP.VPOS)](https://github.com/cempehlivan/CP.VPOS/blob/master/LICENSE)

![.net version](https://img.shields.io/badge/.net%20framework-4.0-purple) ![.net version](https://img.shields.io/badge/.net%20framework-4.5-purple) ![.net version](https://img.shields.io/badge/.net%20core-3.1-purple) ![.net version](https://img.shields.io/badge/.net-5.0-purple) ![.net version](https://img.shields.io/badge/.net-6.0-purple)


Bu projenin amacı, tüm sanal posları tek bir codebase ile kullanmak.

## Kullanılabilir Sanal POS'lar

| Sanal POS | Satış | Satış 3D | İptal | İade  |
| --------- | :---: | :------: | :---: | :---: |
| Akbank | ✔️ | ✔️ | ✔️ | ✔️ |
| Alternatif Bank | ✔️ | ✔️ | ✔️ | ✔️ |
| Anadolubank | ✔️ | ✔️ | ✔️ | ✔️ |
| QNB Finansbank | ✔️ | ✔️ | ✔️ | ✔️ |
| Garanti BBVA | ✔️ | ✔️ | ❌ | ❌ |
| Halkbank | ✔️ | ✔️ | ✔️ | ✔️ |
| ING Bank | ✔️ | ✔️ | ✔️ | ✔️ |
| İş Bankası | ✔️ | ✔️ | ✔️ | ✔️ |
| Şekerbank | ✔️ | ✔️ | ✔️ | ✔️ |
| Türk Ekonomi Bankası | ✔️ | ✔️ | ✔️ | ✔️ |
| Türkiye Finans | ✔️ | ✔️ | ✔️ | ✔️ |
| Vakıfbank | ✔️ | ✔️ | ❌ | ❌ |
| Yapı Kredı Bankası | ✔️ | ✔️ | ❌ | ❌ |
| Ziraat Bankası | ✔️ | ✔️ | ✔️ | ✔️ |
| Cardplus | ✔️ | ✔️ | ✔️ | ✔️ |
| Paratika | ✔️ | ✔️ | ✔️ | ✔️ |
| Payten - MSU | ✔️ | ✔️ | ✔️ | ✔️ |


## NuGet
[https://www.nuget.org/packages/CP.VPOS](https://www.nuget.org/packages/CP.VPOS)
veya 
Package Manager:

> Install-Package CP.VPOS

## Satış İşlemi
```csharp
VirtualPOSAuth nestpayAkbank = new VirtualPOSAuth
{
	bankCode = CP.VPOS.Services.BankService.Akbank,
	merchantID = "100100000",
	merchantUser = "AKTESTAPI",
	merchantPassword = "AKBANK01",
	merchantStorekey = "123456",
	testPlatform = true
};

CustomerInfo customerInfo = new CustomerInfo
{
	taxNumber = "1111111111",
	emailAddress = "test@test.com",
	name = "cem",
	surname = "pehlivan",
	phoneNumber = "1111111111",
	addressDesc = "adres",
	cityName = "istanbul",
	country = CP.VPOS.Enums.Country.TUR,
	postCode = "34000",
	taxOffice = "maltepe",
	townName = "maltepe"
};

SaleRequest saleRequest = new SaleRequest
{
	invoiceInfo = customerInfo,
	shippingInfo = customerInfo,
	saleInfo = new SaleInfo
	{
		cardNameSurname = "cem test",
		cardNumber = "4355084355084358",
		cardExpiryDateMonth = 12,
		cardExpiryDateYear = 2030,
		amount = (decimal)100.50,
		cardCVV = "000",
		currency = CP.VPOS.Enums.Currency.TRY,
		installment = 1,
	},
	payment3D = new Payment3D
	{
		confirm = false
	},
	customerIPAddress = "1.1.1.1",
	orderNumber = Convert.ToInt32((DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds).ToString("X")
};


var resp = VPOSClient.Sale(saleRequest, nestpayAkbank);
```
