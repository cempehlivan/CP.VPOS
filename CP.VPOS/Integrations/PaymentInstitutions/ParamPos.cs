using CP.VPOS.Enums;
using CP.VPOS.Helpers;
using CP.VPOS.Interfaces;
using CP.VPOS.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Xml;

namespace CP.VPOS.Banks.ParamPos
{
    internal class ParamPosVirtualPOSService : IVirtualPOSService
    {
        private readonly string _urlAPITest = "https://testposws.param.com.tr/turkpos.ws/service_turkpos_prod.asmx";
        private readonly string _urlAPILive = "https://posws.param.com.tr/turkpos.ws/service_turkpos_prod.asmx";


        public SaleResponse Sale(SaleRequest request, VirtualPOSAuth auth)
        {
            SaleResponse response = new SaleResponse
            {
                statu = SaleResponseStatu.Error,
                message = "İşlem sırasında bilinmeyen bir hata oluştu.",
                orderNumber = request.orderNumber,
            };

            int _installment = request.saleInfo.installment > 1 ? request.saleInfo.installment : 1;

            var installmentList = BINInstallmentQuery(new BINInstallmentQueryRequest { BIN = request.saleInfo.cardNumber.Substring(0,8) }, auth);

            decimal _decAmountTotal = request.saleInfo.amount;

            if(installmentList?.confirm == true)
            {
                var installmentDetail = installmentList.installmentList?.FirstOrDefault(s => s.count == _installment);

                if(installmentDetail?.customerCostCommissionRate > 0)
                    _decAmountTotal = request.saleInfo.amount + ((request.saleInfo.amount * installmentDetail.customerCostCommissionRate.cpToDecimal()) / 100);
            }


            string _amount = request.saleInfo.amount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "");
            string _amountTotal = _decAmountTotal.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "");

            string hashParam = auth.merchantID + auth.merchantStorekey + _installment.ToString() + _amount + _amountTotal + request.orderNumber;

            string _hash = GetHashPrivate(hashParam); //GetHash(hashParam, (auth.testPlatform ? _urlAPITest : _urlAPILive));

            string xml = $@"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soap:Body>
        <TP_WMD_UCD xmlns=""https://turkpos.com.tr/"">
            <G>
                <CLIENT_CODE>{auth.merchantID}</CLIENT_CODE>
                <CLIENT_USERNAME>{auth.merchantUser}</CLIENT_USERNAME>
                <CLIENT_PASSWORD>{auth.merchantPassword}</CLIENT_PASSWORD>
            </G>
            <GUID>{auth.merchantStorekey}</GUID>
            <KK_Sahibi>{request.saleInfo.cardNameSurname}</KK_Sahibi>
            <KK_No>{request.saleInfo.cardNumber}</KK_No>
            <KK_SK_Ay>{request.saleInfo.cardExpiryDateMonth.ToString("00")}</KK_SK_Ay>
            <KK_SK_Yil>{request.saleInfo.cardExpiryDateYear}</KK_SK_Yil>
            <KK_CVC>{request.saleInfo.cardCVV}</KK_CVC>
            <Hata_URL>{request.payment3D.returnURL}</Hata_URL>
            <Basarili_URL>{request.payment3D.returnURL}</Basarili_URL>
            <Siparis_ID>{request.orderNumber}</Siparis_ID>
            <Taksit>{_installment}</Taksit>
            <Islem_Tutar>{_amount}</Islem_Tutar>
            <Toplam_Tutar>{_amountTotal}</Toplam_Tutar>
            <Islem_Hash>{_hash}</Islem_Hash>
            <Islem_Guvenlik_Tip>{(request.payment3D.confirm ? "3D" : "NS")}</Islem_Guvenlik_Tip>
            <IPAdr>{request.customerIPAddress}</IPAdr>
        </TP_WMD_UCD>
    </soap:Body>
</soap:Envelope>";

            string resp = this.xmlRequest(xml, (auth.testPlatform ? _urlAPITest : _urlAPILive));

            Dictionary<string, object> respDic = FoundationHelper.XmltoDictionary(resp, "/*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='TP_WMD_UCDResponse']/*[local-name()='TP_WMD_UCDResult']");


            long resp_Islem_ID = respDic?.ContainsKey("Islem_ID") == true ? respDic["Islem_ID"].cpToLong() : 0;
            string resp_UCD_HTML = respDic?.ContainsKey("UCD_HTML") == true ? respDic["UCD_HTML"].cpToString() : "";
            int resp_Sonuc = respDic?.ContainsKey("Sonuc") == true ? respDic["Sonuc"].cpToInt() : -1;
            string resp_Sonuc_Str = respDic?.ContainsKey("Sonuc_Str") == true ? respDic["Sonuc_Str"].cpToString() : "";

            response.privateResponse = respDic;
            response.transactionId = resp_Islem_ID.cpToString();

            if (resp_Sonuc > 0 && resp_UCD_HTML == "NONSECURE" && resp_Islem_ID > 0)
            {
                response.statu = SaleResponseStatu.Success;
                response.message = "İşlem başarılı";
            }
            else if (resp_Sonuc > 0 && !string.IsNullOrWhiteSpace(resp_UCD_HTML) && resp_UCD_HTML != "NONSECURE")
            {
                response.statu = SaleResponseStatu.RedirectHTML;
                response.message = resp_UCD_HTML;
            }
            else
            {
                response.statu = SaleResponseStatu.Error;
                response.message = !string.IsNullOrWhiteSpace(resp_Sonuc_Str) ? resp_Sonuc_Str : "İşlem sırasında bilinmeyen bir hata oluştu.";
            }

            return response;
        }

        public SaleResponse Sale3DResponse(Sale3DResponseRequest request, VirtualPOSAuth auth)
        {
            SaleResponse response = new SaleResponse
            {
                statu = SaleResponseStatu.Error,
                message = "İşlem sırasında bilinmeyen bir hata oluştu.",
                privateResponse = new Dictionary<string, object>
                {
                    {"step1response", request.responseArray }
                },
                orderNumber = request?.responseArray?.ContainsKey("orderId") == true ? request.responseArray["orderId"].cpToString() : "",
            };

            int mdStatus = request?.responseArray?.ContainsKey("mdStatus") == true ? request.responseArray["mdStatus"].cpToInt() : 0;

            if (mdStatus == 1)
            {
                string req_UCD_MD = request?.responseArray?.ContainsKey("md") == true ? request.responseArray["md"].cpToString() : "";
                string req_Islem_GUID = request?.responseArray?.ContainsKey("islemGUID") == true ? request.responseArray["islemGUID"].cpToString() : "";

                string xml = $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tur=""https://turkpos.com.tr/"">
   <soapenv:Header/>
   <soapenv:Body>
      <tur:TP_WMD_Pay>
         <tur:G>
            <tur:CLIENT_CODE>{auth.merchantID}</tur:CLIENT_CODE>
            <tur:CLIENT_USERNAME>{auth.merchantUser}</tur:CLIENT_USERNAME>
            <tur:CLIENT_PASSWORD>{auth.merchantPassword}</tur:CLIENT_PASSWORD>
         </tur:G>
         <tur:GUID>{auth.merchantStorekey}</tur:GUID>
         <tur:UCD_MD>{req_UCD_MD}</tur:UCD_MD>
         <tur:Islem_GUID>{req_Islem_GUID}</tur:Islem_GUID>
         <tur:Siparis_ID>{response.orderNumber}</tur:Siparis_ID>
      </tur:TP_WMD_Pay>
   </soapenv:Body>
</soapenv:Envelope>";

                string resp = this.xmlRequest(xml, (auth.testPlatform ? _urlAPITest : _urlAPILive));

                Dictionary<string, object> respDic = FoundationHelper.XmltoDictionary(resp, "/*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='TP_WMD_PayResponse']/*[local-name()='TP_WMD_PayResult']");

                int resp_Sonuc = respDic?.ContainsKey("Sonuc") == true ? respDic["Sonuc"].cpToInt() : -1;
                long resp_Dekont_ID = respDic?.ContainsKey("Dekont_ID") == true ? respDic["Dekont_ID"].cpToLong() : 0;
                string resp_Sonuc_Ack = respDic?.ContainsKey("Sonuc_Ack") == true ? respDic["Sonuc_Ack"].cpToString() : "";

                response.transactionId = resp_Dekont_ID.cpToString();

                response.privateResponse.Add("step2response", respDic);

                if (resp_Sonuc > 0 && resp_Dekont_ID > 0)
                {
                    response.statu = SaleResponseStatu.Success;
                    response.message = "İşlem başarılı";
                }
                else
                {
                    response.statu = SaleResponseStatu.Error;
                    response.message = !string.IsNullOrWhiteSpace(resp_Sonuc_Ack) ? resp_Sonuc_Ack : "İşlem sırasında bilinmeyen bir hata oluştu.";
                }
            }

            return response;
        }

        public CancelResponse Cancel(CancelRequest request, VirtualPOSAuth auth)
        {
            CancelResponse response = new CancelResponse
            {
                statu = ResponseStatu.Error,
                message = "İşlem sırasında bilinmeyen bir hata oluştu."
            };

            string xml = $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tur=""https://turkpos.com.tr/"">
   <soapenv:Header/>
   <soapenv:Body>
      <tur:TP_Islem_Iptal_Iade_Kismi2>
         <tur:G>
            <tur:CLIENT_CODE>{auth.merchantID}</tur:CLIENT_CODE>
            <tur:CLIENT_USERNAME>{auth.merchantUser}</tur:CLIENT_USERNAME>
            <tur:CLIENT_PASSWORD>{auth.merchantPassword}</tur:CLIENT_PASSWORD>
         </tur:G>
         <tur:GUID>{auth.merchantStorekey}</tur:GUID>
         <tur:Durum>IPTAL</tur:Durum>
         <tur:Siparis_ID>{request.orderNumber}</tur:Siparis_ID>
         <tur:Tutar>0.00</tur:Tutar>
      </tur:TP_Islem_Iptal_Iade_Kismi2>
   </soapenv:Body>
</soapenv:Envelope>";

            string resp = this.xmlRequest(xml, (auth.testPlatform ? _urlAPITest : _urlAPILive));

            Dictionary<string, object> respDic = FoundationHelper.XmltoDictionary(resp, "/*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='TP_Islem_Iptal_Iade_Kismi2Response']/*[local-name()='TP_Islem_Iptal_Iade_Kismi2Result']");

            int resp_Sonuc = respDic?.ContainsKey("Sonuc") == true ? respDic["Sonuc"].cpToInt() : -1;
            string resp_Sonuc_Str = respDic?.ContainsKey("Sonuc_Str") == true ? respDic["Sonuc_Str"].cpToString() : "";


            response.statu = resp_Sonuc > 0 ? ResponseStatu.Success : ResponseStatu.Error;
            response.message = resp_Sonuc_Str;
            response.privateResponse = respDic;

            return response;
        }

        public RefundResponse Refund(RefundRequest request, VirtualPOSAuth auth)
        {
            RefundResponse response = new RefundResponse
            {
                statu = ResponseStatu.Error,
                message = "İşlem sırasında bilinmeyen bir hata oluştu."
            };

            string amount = request.refundAmount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", ".");

            string xml = $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tur=""https://turkpos.com.tr/"">
   <soapenv:Header/>
   <soapenv:Body>
      <tur:TP_Islem_Iptal_Iade_Kismi2>
         <tur:G>
            <tur:CLIENT_CODE>{auth.merchantID}</tur:CLIENT_CODE>
            <tur:CLIENT_USERNAME>{auth.merchantUser}</tur:CLIENT_USERNAME>
            <tur:CLIENT_PASSWORD>{auth.merchantPassword}</tur:CLIENT_PASSWORD>
         </tur:G>
         <tur:GUID>{auth.merchantStorekey}</tur:GUID>
         <tur:Durum>IADE</tur:Durum>
         <tur:Siparis_ID>{request.orderNumber}</tur:Siparis_ID>
         <tur:Tutar>{amount}</tur:Tutar>
      </tur:TP_Islem_Iptal_Iade_Kismi2>
   </soapenv:Body>
</soapenv:Envelope>";

            string resp = this.xmlRequest(xml, (auth.testPlatform ? _urlAPITest : _urlAPILive));

            Dictionary<string, object> respDic = FoundationHelper.XmltoDictionary(resp, "/*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='TP_Islem_Iptal_Iade_Kismi2Response']/*[local-name()='TP_Islem_Iptal_Iade_Kismi2Result']");

            int resp_Sonuc = respDic?.ContainsKey("Sonuc") == true ? respDic["Sonuc"].cpToInt() : -1;
            string resp_Sonuc_Str = respDic?.ContainsKey("Sonuc_Str") == true ? respDic["Sonuc_Str"].cpToString() : "";


            response.statu = resp_Sonuc > 0 ? ResponseStatu.Success : ResponseStatu.Error;
            response.message = resp_Sonuc_Str;
            response.privateResponse = respDic;
            response.refundAmount = request.refundAmount;

            return response;
        }


        public BINInstallmentQueryResponse BINInstallmentQuery(BINInstallmentQueryRequest request, VirtualPOSAuth auth)
        {
            List<installment> installments = null;

            try
            {
                string sanalpos_ID = GetBINSanalPOS_ID(request.BIN, auth);

                installments = GetInstallmentTableBySanalPOS_ID(sanalpos_ID, auth);
            }
            catch
            {
                installments = null;
            }

            return new BINInstallmentQueryResponse
            {
                confirm = installments?.Count > 0,
                installmentList = installments?.Count > 0 ? installments : null
            };
        }

        public AdditionalInstallmentQueryResponse AdditionalInstallmentQuery(AdditionalInstallmentQueryRequest request, VirtualPOSAuth auth)
        {
            return null;
        }

        public AllInstallmentQueryResponse AllInstallmentQuery(AllInstallmentQueryRequest request, VirtualPOSAuth auth)
        {
            return null;
        }


        private string GetBINSanalPOS_ID(string binNumber, VirtualPOSAuth auth)
        {
            string sanalpos_ID = "";

            string xml = $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tur=""https://turkpos.com.tr/"">
   <soapenv:Header/>
   <soapenv:Body>
      <tur:BIN_SanalPos>
         <tur:G>
            <tur:CLIENT_CODE>{auth.merchantID}</tur:CLIENT_CODE>
            <tur:CLIENT_USERNAME>{auth.merchantUser}</tur:CLIENT_USERNAME>
            <tur:CLIENT_PASSWORD>{auth.merchantPassword}</tur:CLIENT_PASSWORD>
         </tur:G>
         <tur:BIN>{binNumber}</tur:BIN>
      </tur:BIN_SanalPos>
   </soapenv:Body>
</soapenv:Envelope>";

            string resp = this.xmlRequest(xml, (auth.testPlatform ? _urlAPITest : _urlAPILive));
            Dictionary<string, object> respDic = FoundationHelper.XmltoDictionary(resp, "/*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='BIN_SanalPosResponse']/*[local-name()='BIN_SanalPosResult']/*[local-name()='DT_Bilgi']/*[local-name()='diffgram']/*[local-name()='NewDataSet']/*[local-name()='Temp']");

            if (respDic?.ContainsKey("SanalPOS_ID") == true)
                sanalpos_ID = respDic["SanalPOS_ID"].cpToString();

            return sanalpos_ID;
        }


        private List<installment> GetInstallmentTableBySanalPOS_ID(string sanalpos_ID, VirtualPOSAuth auth)
        {
            List<installment> installments = new List<installment>();

            string xml = $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tur=""https://turkpos.com.tr/"">
   <soapenv:Header/>
   <soapenv:Body>
      <tur:TP_Ozel_Oran_SK_Liste>
        <tur:G>
            <tur:CLIENT_CODE>{auth.merchantID}</tur:CLIENT_CODE>
            <tur:CLIENT_USERNAME>{auth.merchantUser}</tur:CLIENT_USERNAME>
            <tur:CLIENT_PASSWORD>{auth.merchantPassword}</tur:CLIENT_PASSWORD>
         </tur:G>
         <tur:GUID>{auth.merchantStorekey}</tur:GUID>
      </tur:TP_Ozel_Oran_SK_Liste>
   </soapenv:Body>
</soapenv:Envelope>";

            string resp = this.xmlRequest(xml, (auth.testPlatform ? _urlAPITest : _urlAPILive));

            Dictionary<string, object> respDic = FoundationHelper.XmltoDictionary(resp, "/*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='TP_Ozel_Oran_SK_ListeResponse']/*[local-name()='TP_Ozel_Oran_SK_ListeResult']/*[local-name()='DT_Bilgi']/*[local-name()='diffgram']/*[local-name()='NewDataSet']");

            if (respDic?.Count > 0)
            {
                foreach (KeyValuePair<string, object> item in respDic)
                {
                    if (item.Value is Dictionary<string, object>)
                    {
                        Dictionary<string, object> pos = item.Value as Dictionary<string, object>;

                        if (pos?.ContainsKey("SanalPOS_ID") != true || pos["SanalPOS_ID"].cpToString() != sanalpos_ID)
                            continue;

                        for (int i = 1; i <= 12; i++)
                        {
                            string installment_key = $"MO_{i.ToString("00")}";

                            if (pos?.ContainsKey(installment_key) == true && float.TryParse(pos[installment_key].cpToString(), NumberStyles.Float, CultureInfo.GetCultureInfo("en-US"), out float comissionRate))
                            {
                                if (comissionRate >= 0)
                                {
                                    installments.Add(new installment
                                    {
                                        count = i,
                                        customerCostCommissionRate = comissionRate
                                    });
                                }

                            }
                        }
                    }
                }
            }


            return installments;
        }

        private string GetHash(string str, string link)
        {
            string xml = $@"<?xml version=""1.0"" encoding=""utf-8""?> <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/""> <soap:Body>
<SHA2B64 xmlns=""https://turkpos.com.tr/"">
<Data>{str}</Data>
</SHA2B64>
</soap:Body>
</soap:Envelope>";


            string response = xmlRequest(xml, link);

            var dic = FoundationHelper.XmltoDictionary(response, "/*[local-name()='Envelope']/*[local-name()='Body']/*[local-name()='SHA2B64Response']");

            return dic["SHA2B64Result"].cpToString();
        }

        private string GetHashPrivate(string originalString)
        {
            using (System.Security.Cryptography.SHA1 sha1Hash = System.Security.Cryptography.SHA1.Create())
            {
                byte[] bytes = sha1Hash.ComputeHash(Encoding.UTF8.GetBytes(originalString));
                return Convert.ToBase64String(bytes);
            }
        }

        private string xmlRequest(string xml, string link)
        {
            ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            System.Net.ServicePointManager.Expect100Continue = false;
#pragma warning disable SYSLIB0014
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(link);
#pragma warning restore SYSLIB0014
            byte[] postdatabytes = System.Text.Encoding.UTF8.GetBytes(xml);
            request.Method = "POST";
            request.ContentType = "text/xml";
            request.ContentLength = postdatabytes.Length;
            System.IO.Stream requeststream = request.GetRequestStream();
            requeststream.Write(postdatabytes, 0, postdatabytes.Length);
            requeststream.Close();

            System.Net.HttpWebResponse resp = (System.Net.HttpWebResponse)request.GetResponse();
            System.IO.StreamReader responsereader = new System.IO.StreamReader(resp.GetResponseStream(), System.Text.Encoding.UTF8);
            string gelenXml = responsereader.ReadToEnd();

            return gelenXml;
        }

        public SaleQueryResponse SaleQuery(SaleQueryRequest request, VirtualPOSAuth auth)
        {
            return new SaleQueryResponse { statu = SaleQueryResponseStatu.Error, message = "Bu sanal pos için satış sorgulama işlemi şuan desteklenmiyor" };
        }
    }
}
