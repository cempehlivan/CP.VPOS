using System.Collections.Generic;
using CP.VPOS.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CP.VPOS.Test
{
    /// <summary>
    /// Ağ erişimi gerektirmeyen çekirdek yardımcı metot testleri.
    /// </summary>
    [TestClass]
    public class FoundationHelperTests
    {
        #region IsCardNumberValid

        [TestMethod]
        [DataRow("5289394722895016")]
        [DataRow("4022780198283155")]
        [DataRow("4546711234567894")]
        [DataRow("5406675406675403")]
        [DataRow("5890040000000016")]
        [DataRow("4508034508034509")]
        public void IsCardNumberValid_AcceptsValidCardNumbers(string cardNumber)
        {
            Assert.IsTrue(FoundationHelper.IsCardNumberValid(cardNumber));
        }

        [TestMethod]
        [DataRow(null, "null")]
        [DataRow("", "bos string")]
        [DataRow("   ", "bosluk")]
        [DataRow("1234567812345678", "luhn gecersiz")]
        [DataRow("424242424", "9 hane - cok kisa")]
        [DataRow("42424242424242424242", "20 hane - cok uzun")]
        [DataRow("4242424242424242abc", "rakam olmayan karakter")]
        [DataRow("4242 4242 4242 4242", "bosluk iceren")]
        [DataRow("4242-4242-4242-4242", "tire iceren")]
        public void IsCardNumberValid_RejectsInvalidValues(string cardNumber, string scenario)
        {
            Assert.IsFalse(FoundationHelper.IsCardNumberValid(cardNumber), scenario);
        }

        #endregion

        #region ToHtmlForm

        [TestMethod]
        public void ToHtmlForm_WritesFieldsAsHiddenInputs()
        {
            Dictionary<string, string> param = new Dictionary<string, string>
            {
                { "orderid", "ORD-123" },
                { "amount", "100" }
            };

            string html = param.ToHtmlForm("https://banka.example/3d");

            StringAssert.Contains(html, @"<input type=""hidden"" name=""orderid"" value=""ORD-123"" />");
            StringAssert.Contains(html, @"<input type=""hidden"" name=""amount"" value=""100"" />");
            StringAssert.Contains(html, @"<form action=""https://banka.example/3d""");
            StringAssert.Contains(html, "document.getElementById('3DFormPost').submit();");
        }

        [TestMethod]
        public void ToHtmlForm_HtmlEncodesValues()
        {
            Dictionary<string, string> param = new Dictionary<string, string>
            {
                { "kotu", "\" /><script>alert('x')</script><input a=\"" }
            };

            string html = param.ToHtmlForm("https://banka.example/3d");

            Assert.IsFalse(html.Contains("<script>alert"), "script etiketi encode edilmeden forma yazılmış");
            StringAssert.Contains(html, "&quot;");
            StringAssert.Contains(html, "&lt;script&gt;");
        }

        [TestMethod]
        public void ToHtmlForm_EncodesAmpersandInFormAction()
        {
            Dictionary<string, string> param = new Dictionary<string, string> { { "a", "1" } };

            string html = param.ToHtmlForm("https://banka.example/3d?x=1&y=2");

            StringAssert.Contains(html, @"action=""https://banka.example/3d?x=1&amp;y=2""");
        }

        #endregion

        #region toXml / XmltoDictionary

        [TestMethod]
        public void ToXml_ConvertsDictionaryToXml()
        {
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "Name", "Cem" },
                { "Amount", "100" }
            };

            string xml = param.toXml("CC5Request", "UTF-8");

            StringAssert.Contains(xml, "<CC5Request>");
            StringAssert.Contains(xml, "<Name>Cem</Name>");
            StringAssert.Contains(xml, "<Amount>100</Amount>");
            StringAssert.Contains(xml, @"encoding=""UTF-8""");
        }

        [TestMethod]
        public void ToXml_WritesNestedDictionaryAsChildElements()
        {
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "Extra", new Dictionary<string, object> { { "a", "1" }, { "b", "2" } } }
            };

            string xml = param.toXml("CC5Request", "UTF-8");

            StringAssert.Contains(xml, "<Extra><a>1</a><b>2</b></Extra>");
        }

        [TestMethod]
        public void ToXml_EscapesSpecialCharacters()
        {
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "Desc", "a & b < c" }
            };

            string xml = param.toXml("CC5Request", "UTF-8");

            StringAssert.Contains(xml, "a &amp; b &lt; c");
        }

        [TestMethod]
        public void XmltoDictionary_RoundTripPreservesValues()
        {
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                { "Name", "Cem" },
                { "Desc", "a & b" }
            };

            string xml = param.toXml("CC5Request", "UTF-8");

            Dictionary<string, object> back = FoundationHelper.XmltoDictionary(xml, "CC5Request");

            Assert.AreEqual(2, back.Count);
            Assert.AreEqual("Cem", back["Name"]);
            Assert.AreEqual("a & b", back["Desc"]);
        }

        [TestMethod]
        public void XmltoDictionary_ReadsNestedElementsAsSubDictionary()
        {
            string xml = "<CC5Response><Code>00</Code><Extra><a>1</a><b>2</b></Extra></CC5Response>";

            Dictionary<string, object> result = FoundationHelper.XmltoDictionary(xml);

            Assert.AreEqual("00", result["Code"]);

            Dictionary<string, object>? extra = result["Extra"] as Dictionary<string, object>;

            Assert.IsNotNull(extra);
            Assert.AreEqual("1", extra["a"]);
            Assert.AreEqual("2", extra["b"]);
        }

        #endregion

        #region getFormParams

        [TestMethod]
        public void GetFormParams_ReadsHiddenInputs()
        {
            string html = @"<form action=""https://banka.example"" method=""post"">
<input type=""hidden"" name=""orderid"" value=""ORD-1"" />
<input type=""hidden"" name=""amount"" value=""100"" />
</form>";

            Dictionary<string, object> result = FoundationHelper.getFormParams(html);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("ORD-1", result["orderid"]);
            Assert.AreEqual("100", result["amount"]);
        }

        [TestMethod]
        public void GetFormParams_ReturnsEmptyDictionaryForEmptyInput()
        {
            Assert.AreEqual(0, FoundationHelper.getFormParams("").Count);
            Assert.AreEqual(0, FoundationHelper.getFormParams((string?)null).Count);
        }

        #endregion

        #region CalculateCustomerCommissionRate

        [TestMethod]
        [DataRow(0f)]
        [DataRow(-5f)]
        public void CalculateCustomerCommissionRate_ReturnsZeroForZeroOrNegativeRate(float sellerRate)
        {
            Assert.AreEqual(0f, FoundationHelper.CalculateCustomerCommissionRate(sellerRate));
        }

        [TestMethod]
        public void CalculateCustomerCommissionRate_CalculatesRatePassedToCustomer()
        {
            // Satıcıdan %10 kesiliyorsa, tutarın net kalması için müşteriye %11,11 eklenmeli
            Assert.AreEqual(11.1111f, FoundationHelper.CalculateCustomerCommissionRate(10f), 0.001f);

            // %50 kesinti -> %100 ekleme
            Assert.AreEqual(100f, FoundationHelper.CalculateCustomerCommissionRate(50f), 0.001f);
        }

        #endregion
    }
}
