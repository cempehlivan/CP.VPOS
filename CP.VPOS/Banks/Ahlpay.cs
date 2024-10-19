using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using CP.VPOS.Enums;
using CP.VPOS.Helpers;
using CP.VPOS.Interfaces;
using CP.VPOS.Models;

namespace CP.VPOS.Banks.Ahlpay
{
	internal class AhlpayVirtualPOSService : IVirtualPOSService
	{
		private readonly string _urlAPITest = "https://testahlsanalpos.ahlpay.com.tr";
		private readonly string _urlAPILive = "https://ahlsanalpos.ahlpay.com.tr";


		public SaleResponse Sale(SaleRequest request, VirtualPOSAuth auth)
		{
			SaleResponse response = new SaleResponse();

			request.saleInfo.currency = request.saleInfo.currency ?? Currency.TRY;
			request.saleInfo.installment = request.saleInfo.installment > 1 ? request.saleInfo.installment : (sbyte)1;

			if (request?.payment3D?.confirm == true)
				return Sale3D(request, auth);

			response.orderNumber = request.orderNumber;

			AhlpayTokenModel _token = null;

			try
			{
				_token = GetTokenModel(auth);
			}
			catch (Exception ex)
			{
				response.statu = SaleResponseStatu.Error;
				response.message = ex.Message;

				return response;
			}

			string totalStr = request.saleInfo.amount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", ".");
			string installmentStr = request.saleInfo.installment.ToString();

			string name = request.invoiceInfo?.name;
			string surname = request.invoiceInfo?.surname;

			if (string.IsNullOrWhiteSpace(name))
				name = "[boş]";

			if (string.IsNullOrWhiteSpace(surname))
				surname = "[boş]";

			int _memberId = 1;

			string _rnd = Guid.NewGuid().ToString();

			Dictionary<string, object> req = new Dictionary<string, object> {
				{"cardNumber", request.saleInfo.cardNumber},
				{"expiryDateMonth", request.saleInfo.cardExpiryDateMonth.ToString("00") },
				{"expiryDateYear", request.saleInfo.cardExpiryDateYear.cpToString() },
				{"cvv", request.saleInfo.cardCVV},
				{"cardHolderName", request.saleInfo.cardNameSurname },
				{"merchantId", _token.merchantId },
				{"totalAmount", totalStr },
				{"memberId", _memberId },
				{"userCode", auth.merchantUser },
				{"txnType", "Auth" },
				{"installmentCount", installmentStr },
				{"currency", ((int)request.saleInfo.currency).ToString() },
				{"orderId", request.orderNumber },
				{"webUrl", "" },
				{"description", $"{request.orderNumber} nolu sipariş ödemesi" },
				{"requestIp", request.customerIPAddress },
				{"rnd", _rnd },
				{"hash", "" },
			};

			string hash_key = GenerateHash($"{auth.merchantStorekey}{_rnd}{request.orderNumber}{totalStr}{_token.merchantId}");
			
			req["hash"] = hash_key;

			string link = $"{(auth.testPlatform ? _urlAPITest : _urlAPILive)}/api/Payment/PaymentNon3D";

			string responseStr = Request(req, link, _token);

			Dictionary<string, object> responseDic = JsonConvertHelper.Convert<Dictionary<string, object>>(responseStr);

			response.privateResponse = responseDic;

			if (responseDic?.ContainsKey("isSuccess") == true && responseDic["isSuccess"].cpToBool() == true)
			{
				string transactionId = "";

				try
				{
					if (responseDic.ContainsKey("data"))
					{
						var dataObj = JsonConvertHelper.Convert<Dictionary<string, object>>(JsonConvertHelper.Json(responseDic["data"]));

						if (dataObj?.ContainsKey("authCode") == true)
							transactionId = dataObj["authCode"].cpToString();
					}
				}
				catch { }


				response.statu = SaleResponseStatu.Success;
				response.message = "İşlem başarılı";
				response.transactionId = transactionId;

				return response;
			}
			else
			{
				string errorMsg = "İşlem sırasında bir hata oluştu";

				if (responseDic?.ContainsKey("message") == true && responseDic["message"].cpToString() != "")
					errorMsg = responseDic["message"].cpToString();

				response.statu = SaleResponseStatu.Error;
				response.message = errorMsg;

				return response;
			}
		}

		private SaleResponse Sale3D(SaleRequest request, VirtualPOSAuth auth)
		{
			SaleResponse response = new SaleResponse();
			response.orderNumber = request.orderNumber;

			AhlpayTokenModel _token = null;

			try
			{
				_token = GetTokenModel(auth);
			}
			catch (Exception ex)
			{
				response.statu = SaleResponseStatu.Error;
				response.message = ex.Message;

				return response;
			}

			string totalStr = request.saleInfo.amount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", ".");
			string installmentStr = request.saleInfo.installment.ToString();

			string name = request.invoiceInfo?.name;
			string surname = request.invoiceInfo?.surname;

			if (string.IsNullOrWhiteSpace(name))
				name = "[boş]";

			if (string.IsNullOrWhiteSpace(surname))
				surname = "[boş]";

			int _memberId = 1;

			string _rnd = Guid.NewGuid().ToString();

			Dictionary<string, object> req = new Dictionary<string, object> {
				{"cardNumber", request.saleInfo.cardNumber},
				{"expiryDateMonth", request.saleInfo.cardExpiryDateMonth.ToString("00") },
				{"expiryDateYear", request.saleInfo.cardExpiryDateYear.cpToString() },
				{"cvv", request.saleInfo.cardCVV},
				{"cardHolderName", request.saleInfo.cardNameSurname },
				{"merchantId", _token.merchantId },
				{"totalAmount", totalStr },
				{"memberId", _memberId },
				{"userCode", auth.merchantUser },
				{"txnType", "Auth" },
				{"installmentCount", installmentStr },
				{"currency", ((int)request.saleInfo.currency).ToString() },
				{"orderId", request.orderNumber },
				{"webUrl", "" },
				{"description", $"{request.orderNumber} nolu sipariş ödemesi" },
				{"requestIp", request.customerIPAddress },
				{"rnd", _rnd },
				{"hash", "" },
				{"okUrl", request.payment3D.returnURL },
				{"failUrl", request.payment3D.returnURL },
			};

			string hash_key = GenerateHash($"{auth.merchantStorekey}{_rnd}{request.orderNumber}{totalStr}{_token.merchantId}");

			req["hash"] = hash_key;

			string link = $"{(auth.testPlatform ? _urlAPITest : _urlAPILive)}/api/Payment/Payment3DWithEventRedirect";

			string responseStr = Request(req, link, _token);

			Dictionary<string, object> responseDic = JsonConvertHelper.Convert<Dictionary<string, object>>(responseStr);

			response.privateResponse = responseDic;

			if (responseDic?.ContainsKey("isSuccess") == true && responseDic["isSuccess"].cpToBool() == true)
			{
				response.statu = SaleResponseStatu.RedirectHTML;
				response.message = responseDic["data"].cpToString();

				return response;
			}
			else
			{
				string errorMsg = "İşlem sırasında bir hata oluştu";

				if (responseDic?.ContainsKey("message") == true && responseDic["message"].cpToString() != "")
					errorMsg = responseDic["message"].cpToString();

				response.statu = SaleResponseStatu.Error;
				response.message = errorMsg;

				return response;
			}
		}

		public SaleResponse Sale3DResponse(Sale3DResponseRequest request, VirtualPOSAuth auth)
		{
			SaleResponse response = new SaleResponse();

			response.privateResponse = request?.responseArray;



			return response;
		}

		public CancelResponse Cancel(CancelRequest request, VirtualPOSAuth auth)
		{
			throw new NotImplementedException();
		}

		public RefundResponse Refund(RefundRequest request, VirtualPOSAuth auth)
		{
			throw new NotImplementedException();
		}

		public AllInstallmentQueryResponse AllInstallmentQuery(AllInstallmentQueryRequest request, VirtualPOSAuth auth)
		{
			throw new NotImplementedException();
		}

		public BINInstallmentQueryResponse BINInstallmentQuery(BINInstallmentQueryRequest request, VirtualPOSAuth auth)
		{
			throw new NotImplementedException();
		}

		public AdditionalInstallmentQueryResponse AdditionalInstallmentQuery(AdditionalInstallmentQueryRequest request, VirtualPOSAuth auth)
		{
			throw new NotImplementedException();
		}

		private AhlpayTokenModel GetTokenModel(VirtualPOSAuth auth)
		{
			AhlpayTokenModel token = null;

			Dictionary<string, object> postData = new Dictionary<string, object>
			{
				{ "email", auth.merchantUser },
				{ "password", auth.merchantPassword }
			};

			string link = $"{(auth.testPlatform ? _urlAPITest : _urlAPILive)}/api/Security/AuthenticationMerchant";

			string loginResponse = Request(postData, link);

			Dictionary<string, object> loginDic = JsonConvertHelper.Convert<Dictionary<string, object>>(loginResponse);

			if (loginDic?.ContainsKey("isSuccess") == true && loginDic["isSuccess"].cpToBool() == true && loginDic?.ContainsKey("data") == true)
			{
				token = JsonConvertHelper.Convert<AhlpayTokenModel>(JsonConvertHelper.Json(loginDic["data"]));

				if (!string.IsNullOrWhiteSpace(token?.token))
					return token;
			}

			string errorMsg = "Ahlpay token error";

			if (loginDic?.ContainsKey("message") == true && loginDic["message"].cpToString() != "")
				errorMsg = errorMsg + " - " + loginDic["message"].cpToString();

			throw new Exception(errorMsg);
		}

		private string Request(Dictionary<string, object> param, string link, AhlpayTokenModel token = null)
		{
			string responseString = "";

			try
			{
				ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072;
				ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
				System.Net.ServicePointManager.Expect100Continue = false;

				string jsonContent = JsonConvertHelper.Json(param);

				using (HttpClient client = new HttpClient())
				using (var req = new StringContent(jsonContent, Encoding.UTF8, "application/json"))
				{
					req.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json") { CharSet = Encoding.UTF8.WebName };

					if (token != null)
						client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(token.tokenType, token.token);

					var response = client.PostAsync(link, req).Result;
					byte[] responseByte = response.Content.ReadAsByteArrayAsync().Result;
					responseString = Encoding.UTF8.GetString(responseByte);
				}
			}
			catch { }

			return responseString;
		}

		private string GenerateHash(string hashString)
		{
			string hash = "";

			using (SHA512 s512 = SHA512.Create())
			{
				UnicodeEncoding ByteConverter = new UnicodeEncoding();
				byte[] bytes = s512.ComputeHash(ByteConverter.GetBytes(hashString));
				hash = BitConverter.ToString(bytes).Replace("-", "");
			}

			return hash;
		}
	}

	internal class AhlpayTokenModel
	{
		public string token { get; set; }
		public string tokenType { get; set; }
		public string fullName { get; set; }
		public string sessionId { get; set; }
		public string merchantName { get; set; }
		public long merchantId { get; set; }
	}
}
