# CP.VPOS
[![Nuget download](https://img.shields.io/nuget/dt/CP.VPOS)](https://www.nuget.org/packages/CP.VPOS) [![Nuget v2](https://img.shields.io/nuget/v/CP.VPOS)](https://www.nuget.org/packages/CP.VPOS) ![build](https://img.shields.io/github/actions/workflow/status/cempehlivan/cp.vpos/dotnet.yml?branch=master) [![GitHub license](https://img.shields.io/github/license/cempehlivan/CP.VPOS)](https://github.com/cempehlivan/CP.VPOS/blob/master/LICENSE)



## CP.VPOS: Sanal Pos Entegrasyonlarını Basitleştirin

CP.VPOS, Türkiye'deki birçok bankanın sanal pos entegrasyonlarını tek bir kod tabanı ile kullanmayı mümkün kılan .NET kütüphanesidir. Bu sayede geliştiriciler, her banka için ayrı ayrı kod yazmak zorunda kalmadan, tüm sanal pos işlemlerini tek bir kütüphane üzerinden gerçekleştirebilirler.

## Kütüphanenin Özellikleri

+ **Tek Kod Tabanı:** Farklı bankaların sanal pos entegrasyonları için ayrı ayrı kod yazmaya gerek kalmadan, tek bir kod tabanı ile tüm işlemleri gerçekleştirebilirsiniz.
+ **Basitleştirilmiş İşlem Akışı:** Sanal pos işlemleri için gerekli tüm adımlar kütüphane tarafından otomatik olarak halledilir. Bu sayede kod yazma süreci oldukça basitleşir.
+ **3D Güvenli Ödeme Desteği:** 3D Güvenli Ödeme işlemleri için gerekli tüm adımlar kütüphane tarafından desteklenir.
+ **Geniş Banka Kapsamı:** Akbank, Alternatif Bank, Anadolubank, Denizbank, QNB Finansbank, Finansbank Nestpay, Garanti BBVA, Halkbank, ING Bank, İş Bankası, Şekerbank, TEB, Türkiye Finans, Vakıfbank, Yapı Kredi ve Ziraat Bankası gibi birçok banka ile birlikte, Iyzico ve Sipay gibi ödeme kuruluşlarının da sanal pos entegrasyonları kütüphanede yer alır.
+ **.NET Uyumluluğu:** Kütüphane, .NET Framework, .NET Core ve .NET MAUI da dahil olmak üzere tüm .NET sürümleriyle tam uyumludur. Bu sayede farklı projelerde kolayca entegre edilerek kullanılabilir.

## Sürüm Notları
### v2.2.2
 - Kart bin listesi güncellendi.
### v2.2.1
 - İş bankası test ortamı api adresleri güncellendi.
### v2.2.0
 - ZiraatPay ve VakıfPayS sanal pos entegrasyonları eklendi.
### v2.1.1
 - Nestpay bankalarında 3D'siz işlemlere sipariş numarası eklendi.
### v2.1.0
 - Vepara sanal pos entegrasyonu eklendi.
### v2.0.1
 - QNBpay, Paybull, Parolapara, IQmoney sanal poslarında tüm taksit seçenekleri sorgulama `AllInstallmentQuery` bug fix
### v2.0.0
 - Akbank yeni sanal pos altyapısı geliştirmesi yapıldı. Akbank aşamalı olarak sanal pos müşterilerini nestpay üzerinden kendi altyapsını geçiriyor. Halihazırda akbank kullananlar bu versiyona geçmeden önce yeni altyapı için erişim bilgilerini bankadan talep etmeliler veya eski (Nestpay) entegrasyonunu kullanmak isteyenler `CP.VPOS.Services.BankService.AkbankNestpay` veya `9046` kodu ile Nestpay üzerinden kullanabilirler.

## Kütüphaneyi Nasıl Kullanabilirsiniz?
CP.VPOS kütüphanesini NuGet paket yöneticisi aracılığıyla projenize ekleyebilirsiniz. Kütüphanenin kullanımıyla ilgili aşağıda bulunan kod örneklerine göz atabilirsiniz.

[https://www.nuget.org/packages/CP.VPOS](https://www.nuget.org/packages/CP.VPOS)

Package Manager:

> Install-Package CP.VPOS

Dotnet CLI
> dotnet add package CP.VPOS


## Kullanılabilir Sanal POS'lar

![ss](https://raw.githubusercontent.com/cempehlivan/CP.VPOS/master/bankalar.png)

| Sanal POS | Satış | Satış 3D | İptal | İade  |
| --------- | :---: | :------: | :---: | :---: |
| Akbank | ✔️ | ✔️ | ✔️ | ✔️ |
| Akbank Nestpay | ✔️ | ✔️ | ✔️ | ✔️ |
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
| QNBpay | ✔️ | ✔️ | ✔️ | ✔️ |
| ParamPos | ✔️ | ✔️ | ✔️ | ✔️ |
| PayBull | ✔️ | ✔️ | ✔️ | ✔️ |
| Parolapara | ✔️ | ✔️ | ✔️ | ✔️ |
| IQmoney | ✔️ | ✔️ | ✔️ | ✔️ |
| Ahlpay | ✔️ | ✔️ | ✔️ | ✔️ |
| Moka | ✔️ | ✔️ | ✔️ | ✔️ |
| Vepara | ✔️ | ✔️ | ✔️ | ✔️ |
| ZiraatPay | ✔️ | ✔️ | ✔️ | ✔️ |
| VakıfPayS | ✔️ | ✔️ | ✔️ | ✔️ |



# Dokümanlar

Detaylı dokümanlara [https://www.vpos.com.tr/docs](https://www.vpos.com.tr/docs) adresinden ulaşabilirsiniz.

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
| Akbank | CP.VPOS.Services.BankService.Akbank | İş Yeri No | Güvenli İşyeri Numarası (merchantSafeId) | Terminal Safe ID | Güvenlik Anahtarı (Secret Key) |
| Akbank Nestpay | CP.VPOS.Services.BankService.AkbankNestpay | Mağaza Kodu | Api Kullanıcısı Adı | Api Kullanıcısı Şifre | 3D Storekey (Üye İş Yeri Anahtarı) |
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
| QNBpay | CP.VPOS.Services.BankService.QNBpay | Üye İşyeri ID | Uygulama Anahtarı | Uygulama Parolası | Üye İşyeri Anahtarı |
| ParamPos | CP.VPOS.Services.BankService.ParamPos | Terminal No (Client Code) | Kullanıcı Adı | Şifre | Anahtar (Guid) |
| PayBull | CP.VPOS.Services.BankService.PayBull | Üye İşyeri ID | Uygulama Anahtarı | Uygulama Parolası | Üye İşyeri Anahtarı |
| Parolapara | CP.VPOS.Services.BankService.Parolapara | Üye İşyeri ID | Uygulama Anahtarı | Uygulama Parolası | Üye İşyeri Anahtarı |
| IQmoney | CP.VPOS.Services.BankService.IQmoney | Üye İşyeri ID | Uygulama Anahtarı | Uygulama Parolası | Üye İşyeri Anahtarı |
| Ahlpay | CP.VPOS.Services.BankService.Ahlpay | member Id | Api Kullanıcısı Adı | Api Kullanıcısı Şifre | Üye işyeri API Key (hashPassword) |
| Moka | CP.VPOS.Services.BankService.Moka | Bayi Kodu | Api Kullanıcısı Adı | Api Kullanıcısı Şifre | |
| Vepara | CP.VPOS.Services.BankService.Vepara | Üye İşyeri ID | Uygulama Anahtarı | Uygulama Parolası | Üye İşyeri Anahtarı |
| ZiraatPay | CP.VPOS.Services.BankService.ZiraatPay | Firma Kodu | Api Kullanıcısı Adı | Api Kullanıcısı Şifre | |
| VakıfPayS | CP.VPOS.Services.BankService.VakifPayS | Firma Kodu | Api Kullanıcısı Adı | Api Kullanıcısı Şifre | |



## 3D'siz Direkt Satış İşlemi

`payment3D.confirm = false` gönderilmesi halinde 3D'siz çekim işlemi yapılır ve direkt olarak nihai sonucu döner. 


```csharp
VirtualPOSAuth _qnbPayTest = new VirtualPOSAuth
{
    bankCode = CP.VPOS.Services.BankService.QNBpay,
    merchantID = "20158",
    merchantUser = "07fb70f9d8de575f32baa6518e38c5d6",
    merchantPassword = "61d97b2cac247069495be4b16f8604db",
    merchantStorekey = "$2y$10$N9IJkgazXMUwCzpn7NJrZePy3v.dIFOQUyW4yGfT3eWry6m.KxanK",
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
		cardNameSurname = "test kart",
		cardNumber = "4022780520669303",
		cardExpiryDateMonth = 1,
		cardExpiryDateYear = 2050,
		cardCVV = "988",
		amount = (decimal)10,
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


var resp = VPOSClient.Sale(saleRequest, _qnbPayTest);
```

## 3D Secure Satış İşlemi

`payment3D.confirm = true` gönderilmesi halinde 3D li  satış işlemi başlatılır. 3D li işlemlerde `payment3D.returnURL` alanına 3D den gelecek olan cevabın iletilmesi istenen URL girilmelidir. Örneğin: `https://localhost/Payment/VirtualPOS3DResponse`. 

`VPOSClient.Sale` metodundan dönen cevaptaki `statu` enum alanı `RedirectURL` veya `RedirectHTML` döner. statu `RedirectURL` ise `message` alanında client'ı yönlendirmeniz gereken url bulunur. statu `RedirectHTML` ise `message` alanında client'ın sayfasında çalıştırmanız gereken HTML bulunur. 

Bu işlem sonrası client, banka 3D doğrulama sayfasına yönlendirilir. Bu sayfadaki işlem sonucunu banka, `payment3D.returnURL` alanında belirttiğimiz url e client'ın browserını kullanarak form post yöntemi ile döner.

3D den gelen form request body'sini  `Dictionary<string, object>` e çevirip `VPOSClient.Sale3DResponse` methoduna gönderilmesi gerekmektedir. Bu işlem sonrası nihai sonuç döner.

```csharp
VirtualPOSAuth _qnbPayTest = new VirtualPOSAuth
{
    bankCode = CP.VPOS.Services.BankService.QNBpay,
    merchantID = "20158",
    merchantUser = "07fb70f9d8de575f32baa6518e38c5d6",
    merchantPassword = "61d97b2cac247069495be4b16f8604db",
    merchantStorekey = "$2y$10$N9IJkgazXMUwCzpn7NJrZePy3v.dIFOQUyW4yGfT3eWry6m.KxanK",
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
		cardNameSurname = "test kart",
		cardNumber = "4022780520669303",
		cardExpiryDateMonth = 1,
		cardExpiryDateYear = 2050,
		cardCVV = "988",
		amount = (decimal)10,
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


var resp = VPOSClient.Sale(saleRequest, _qnbPayTest);
```


### 3D Secure Satış İşlemi 2. Adım


```csharp

public class PaymentController
{
    public async Task<IActionResult> VirtualPOS3DResponse()
    {
        Dictionary<string, object>? pairs = null;

        if (Request.Method == "GET")
            pairs = Request.Query.Keys.ToDictionary(k => k, v => (object)Request.Query[v]);
        else
            pairs = Request.Form.Keys.ToDictionary(k => k, v => (object)Request.Form[v]);   

        SaleResponse response = VPOSClient.Sale3DResponse(new Sale3DResponseRequest
        {
            responseArray = pairs
        }, _qnbPayTest);
    }
}

```