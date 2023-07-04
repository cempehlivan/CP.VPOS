# CP.VPOS
[![Nuget download](https://img.shields.io/nuget/dt/CP.VPOS)](https://www.nuget.org/packages/CP.VPOS) [![Nuget v2](https://img.shields.io/nuget/v/CP.VPOS)](https://www.nuget.org/packages/CP.VPOS) ![build](https://img.shields.io/github/actions/workflow/status/cempehlivan/cp.vpos/dotnet.yml?branch=master) [![GitHub license](https://img.shields.io/github/license/cempehlivan/CP.VPOS)](https://github.com/cempehlivan/CP.VPOS/blob/master/LICENSE)

![.net version](https://img.shields.io/badge/.net%20framework-4.0-purple) ![.net version](https://img.shields.io/badge/.net%20framework-4.5-purple) ![.net version](https://img.shields.io/badge/.net%20core-3.1-purple) ![.net version](https://img.shields.io/badge/.net-5.0-purple) ![.net version](https://img.shields.io/badge/.net-6.0-purple) ![.net version](https://img.shields.io/badge/.net-7.0-purple) ![.net maui](https://img.shields.io/badge/.net-MAUI-purple)


Bu projenin amacı, tüm sanal posları tek bir codebase ile kullanmak.

## Kullanılabilir Sanal POS'lar

| Sanal POS | Satış | Satış 3D | İptal | İade  |
| --------- | :---: | :------: | :---: | :---: |
| Akbank | ✔️ | ✔️ | ✔️ | ✔️ |
| Alternatif Bank | ✔️ | ✔️ | ✔️ | ✔️ |
| Anadolubank | ✔️ | ✔️ | ✔️ | ✔️ |
| Denizbank | ✔️ | ✔️ | ✔️ | ✔️ |
| QNB Finansbank | ✔️ | ✔️ | ✔️ | ✔️ |
| Finansbank Nestpay | ✔️ | ✔️ | ✔️ | ✔️ |
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
| Iyzico | ✔️ | ✔️ | ✔️ | ✔️ |
| Sipay | ✔️ | ✔️ | ✔️ | ✔️ |


## NuGet
[https://www.nuget.org/packages/CP.VPOS](https://www.nuget.org/packages/CP.VPOS)

Package Manager:

> Install-Package CP.VPOS

Dotnet CLI
> dotnet add package CP.VPOS


# Dökümanlar

## API Bilgilerinin ayarlanması - `VirtualPOSAuth` Class'ı

Alan açıklamaları:

| Alan | Tür | Açıklama |
| ---- | --- | -------- |
| `bankCode` | `string` | Hangi banka entegrasyonunun kullanılacağının belirlendiği alandır. Banka kodlarının belirlenmesinde, bankaların global EFT kodları kullanılmıştır. 4 haneli olarak girilmelidir. Örneğin; Akbank global EFT kodu `46` dır. Akbank Sanal POS entegrasyonunu kullanmak için `0046` girilmelidir. Veya [CP.VPOS.Services.BankService](./CP.VPOS/Services/BankService.cs) Enum Class'ı kullanılabilir. Örneğin: `CP.VPOS.Services.BankService.Akbank` |
| `merchantID` | `string` | Firma kodu |
| `merchantUser` | `string` | API kullanıcı adı |
| `merchantPassword` | `string` | API kullanıcı şifre |
| `merchantStorekey` | `string` | Bazı bankalar için 3D store key |
| `testPlatform` | `boolean` | Ortam bilgisi. Sanal POS Test ortamı için `true` gönderilmelidir. |


Sanal POS bazlı alan açıklamaları:

| Sanal POS | bankCode | merchantID | merchantUser | merchantPassword | merchantStorekey |
| --------- | -------- | ---------- | ------------ | ---------------- | ---------------- |
| Akbank | CP.VPOS.Services.BankService.Akbank | Mağaza Kodu | Api Kullanıcısı Adı | Api Kullanıcısı Şifre | 3D Storekey (Üye İş Yeri Anahtarı) |
| Alternatif Bank | CP.VPOS.Services.BankService.AlternatifBank | Mağaza Kodu | Api Kullanıcısı Adı | Api Kullanıcısı Şifre | 3D Storekey (Üye İş Yeri Anahtarı) |
| Anadolubank | CP.VPOS.Services.BankService.Anadolubank | Mağaza Kodu | Api Kullanıcısı Adı | Api Kullanıcısı Şifre | 3D Storekey (Üye İş Yeri Anahtarı) |
| Denizbank | CP.VPOS.Services.BankService.Denizbank | Mağaza Kodu | Api Kullanıcısı Adı | Api Kullanıcısı Şifre | 3D Storekey (Üye İş Yeri Anahtarı) |
| QNB Finansbank | CP.VPOS.Services.BankService.QNBFinansbank | Mağaza Kodu | Api Kullanıcısı Adı | Api Kullanıcısı Şifre | 3D Storekey (Üye İş Yeri Anahtarı) |
| Finansbank Nestpay | CP.VPOS.Services.BankService.FinansbankNestpay | Mağaza Kodu | Api Kullanıcısı Adı | Api Kullanıcısı Şifre | 3D Storekey (Üye İş Yeri Anahtarı) |
| Garanti BBVA | CP.VPOS.Services.BankService.GarantiBBVA | Firma Kodu | Terminal No | `PROVAUT` kullanıcısı şifresi | 3D secure anahtarı |
| Halkbank | CP.VPOS.Services.BankService.Halkbank | Mağaza Kodu | Api Kullanıcısı Adı | Api Kullanıcısı Şifre | 3D Storekey (Üye İş Yeri Anahtarı) |
| ING Bank | CP.VPOS.Services.BankService.INGBank | Mağaza Kodu | Api Kullanıcısı Adı | Api Kullanıcısı Şifre | 3D Storekey (Üye İş Yeri Anahtarı) |
| İş Bankası | CP.VPOS.Services.BankService.IsBankasi | Mağaza Kodu | Api Kullanıcısı Adı | Api Kullanıcısı Şifre | 3D Storekey (Üye İş Yeri Anahtarı) |
| Şekerbank | CP.VPOS.Services.BankService.Sekerbank | Mağaza Kodu | Api Kullanıcısı Adı | Api Kullanıcısı Şifre | 3D Storekey (Üye İş Yeri Anahtarı) |
| Türk Ekonomi Bankası | CP.VPOS.Services.BankService.TurkEkonomiBankasi | Mağaza Kodu | Api Kullanıcısı Adı | Api Kullanıcısı Şifre | 3D Storekey (Üye İş Yeri Anahtarı) |
| Türkiye Finans | CP.VPOS.Services.BankService.TurkiyeFinans | Mağaza Kodu | Api Kullanıcısı Adı | Api Kullanıcısı Şifre | 3D Storekey (Üye İş Yeri Anahtarı) |
| Vakıfbank | CP.VPOS.Services.BankService.Vakifbank | Üye İşyeri Numarası | POS No | Api Şifresi | |
| Yapı Kredı Bankası | CP.VPOS.Services.BankService.YapiKrediBankasi | Firma Kodu | Terminal No | Pos Net ID | ENCKEY |
| Ziraat Bankası | CP.VPOS.Services.BankService.ZiraatBankasi | Mağaza Kodu | Api Kullanıcısı Adı | Api Kullanıcısı Şifre | 3D Storekey (Üye İş Yeri Anahtarı) |
| Cardplus | CP.VPOS.Services.BankService.Cardplus | Mağaza Kodu | Api Kullanıcısı Adı | Api Kullanıcısı Şifre | 3D Storekey (Üye İş Yeri Anahtarı) |
| Paratika | CP.VPOS.Services.BankService.Paratika | Firma Kodu | Api Kullanıcısı Adı | Api Kullanıcısı Şifre | |
| Payten - MSU | CP.VPOS.Services.BankService.Payten | Firma Kodu | Api Kullanıcısı Adı | Api Kullanıcısı Şifre | |
| Iyzico | CP.VPOS.Services.BankService.Iyzico | Üye İşyeri Numarası | API Anahtarı | Güvenlik Anahtarı | |
| Sipay | CP.VPOS.Services.BankService.Sipay | Üye İşyeri ID | Uygulama Anahtarı | Uygulama Parolası | Üye İşyeri Anahtarı |

## 3D'siz Direkt Satış İşlemi

`payment3D.confirm = false` gönderilmesi halinde 3D'siz çekim işlemi yapılır ve direkt olarak nihai sonucu döner. 


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

## 3D Secure Satış İşlemi

`payment3D.confirm = true` gönderilmesi halinde 3D li  satış işlemi başlatılır. 3D li işlemlerde `payment3D.returnURL` alanına 3D den gelecek olan cevabın iletilmesi istenen URL girilmelidir. Örneğin: `https://localhost/Payment/VirtualPOS3DResponse`. 

`VPOSClient.Sale` metodundan dönen cevaptaki `statu` enum alanı `RedirectURL` veya `RedirectHTML` döner. statu `RedirectURL` ise `message` alanında client'ı yönlendirmeniz gereken url bulunur. statu `RedirectHTML` ise `message` alanında client'ın sayfasında çalıştırmanız gereken HTML bulunur. 

Bu işlem sonrası client, banka 3D doğrulama sayfasına yönlendirilir. Bu sayfadaki işlem sonucunu banka, `payment3D.returnURL` alanında belirttiğimiz url e client'ın browserını kullanarak form post yöntemi ile döner.

3D den gelen form request body'sini  `Dictionary<string, object>` e çevirip `VPOSClient.Sale3DResponse` methoduna gönderilmesi gerekmektedir. Bu işlem sonrası nihai sonuç döner.

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
		confirm = true,
		returnURL = "https://localhost/Payment/VirtualPOS3DResponse"
	},
	customerIPAddress = "1.1.1.1",
	orderNumber = Convert.ToInt32((DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds).ToString("X")
};


var resp = VPOSClient.Sale(saleRequest, nestpayAkbank);
```


### 3D Secure Satış İşlemi 2. Adım


```csharp

public class PaymentController
{
    public async Task<IActionResult> VirtualPOS3DResponse()
    {
        Dictionary<string, object> pairs = Request.Form.Keys.ToDictionary(k => k, v => (object)Request.Form[v]);    

        SaleResponse response = VPOSClient.Sale3DResponse(new Sale3DResponseRequest
        {
            responseArray = pairs
        }, nestpayAkbank);
    }
}

```