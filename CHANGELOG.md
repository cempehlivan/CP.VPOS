## Sürüm Notları
### v3.5.2
 - HalkOde, IQmoney, Parolapara, PayBull, QNBPay, Sipay, Vepara poslarında cancel, refund bug fix.
### v3.5.1
 - HalkOde, IQmoney, Parolapara, PayBull, QNBPay, Sipay, Vepara poslarında AllInstallmentQuery bug fix.
### v3.5.0
 - Iyzico sanal posunda 3d private response detayı eklendi.
 - HalkOde, IQmoney, Parolapara, PayBull, QNBPay, Sipay, Vepara poslarında taksit komisyon politikası ayarı eklendi. `VirtualPOSAuth` class'ında `installmentCommissionPolicy` ayarı ile komisyonu müşteri öder/satıcı öder ayarını yönetebilirsiniz.
 - HalkOde, IQmoney, Parolapara, PayBull, QNBPay, Sipay, Vepara poslarında 3dpay modelinden 3d modeline geçildi.
### v3.4.0
 - Paynet sanal pos entegrasyonu eklendi.
 - Kart bin listesi güncellendi.
 - ZiraatPay test ortamı api adresi güncellendi.
 - Vakıf katılım 3D response bug fix.
### v3.3.1
 - Moka sanal pos taksit sorgulama bug fix.
 - .Net framework 4.0 desteği kaldırıldı. Destek 4.5 ve üzeri olacak şekilde devam edecek.
### v3.3.0
 - PayNKolay sanal pos entegrasyonu eklendi.
 - Moka sanal pos tüm taksit listesi eklendi.
 - Ziraat bankası test ortamı adresleri güncellendi.
 - Vakıfbank iptal/iade geliştirmesi eklendi.
### v3.2.0
 - Kuveyt Türk ile Vakıf Katılım bankaları Non3D ve 3D ödeme sanal pos entegrasyonları eklendi.
### v3.1.1
 - Nestpay bankalarındaki v3.0.0 da oluşan taksitli çekim problemi düzeltildi.
### v3.1.0
 - HalkÖde sanal pos entegrasyonu eklendi.
### v3.0.0
 - **Önemli güncelleme:** Nestpay altyapısındaki banka poslarında (Alternatif Bank, Anadolu Bank, Halkbankası, ING Bank, İş Bankası, Şekerbank, TEB, Türkiye Finans, Ziraat Bankası) 3D güvenli ödemede `storetype` olarak `3d_pay` den `3d` modeline geçiş yapılmıştır. Bu banka poslarına sahip firmalarda üye işyerinde `storetype` olarak `3d` aktif değil ise `3d` modelinin aktif edilmesi gerekmetedir. Aksi halde 3D ile başarılı ödeme alınamayacaktır.
### v2.3.0
 - Tami sanal pos entegrasyonu eklendi.
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