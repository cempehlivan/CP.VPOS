using CP.VPOS.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CP.VPOS.Test
{
    /// <summary>
    /// Banka isteklerinde alan uzunluğu ve biçim düzeltmesi yapan dönüştürücü testleri.
    /// </summary>
    [TestClass]
    public class ConvertHelperTests
    {
        [TestMethod]
        public void GetMaxLength_LeavesTextShorterThanLimitUnchanged()
        {
            Assert.AreEqual("istanbul", "istanbul".getMaxLength(25));
            Assert.AreEqual("", "".getMaxLength(10));
        }

        [TestMethod]
        public void GetMaxLength_TruncatesTextExceedingLimit()
        {
            Assert.AreEqual("istan", "istanbul".getMaxLength(5));
            Assert.AreEqual(25, new string('a', 100).getMaxLength(25).Length);
        }

        [TestMethod]
        public void ClearString_TrimsLeadingAndTrailingWhitespace()
        {
            Assert.AreEqual("cem pehlivan", "  cem pehlivan  ".clearString());
        }

        [TestMethod]
        public void ClearString_CollapsesConsecutiveWhitespace()
        {
            Assert.AreEqual("cem pehlivan", "cem     pehlivan".clearString());
            Assert.AreEqual("a b c", "a\tb\nc".clearString());
        }

        [TestMethod]
        public void ClearString_ReturnsInputForNullOrEmpty()
        {
            Assert.IsNull(((string?)null).clearString());
            Assert.AreEqual("", "".clearString());
        }

        [TestMethod]
        public void ClearNumber_RemovesNonDigitCharacters()
        {
            Assert.AreEqual("5312345678", "(531) 234 56 78".clearNumber());
            Assert.AreEqual("4242424242424242", "4242-4242-4242-4242".clearNumber());
            Assert.AreEqual("", "abc".clearNumber());
        }

        [TestMethod]
        public void CpIsNullOrEmpty_DetectsNullAndEmpty()
        {
            Assert.IsTrue(((string?)null).cpIsNullOrEmpty());
            Assert.IsTrue("".cpIsNullOrEmpty());
            Assert.IsFalse("a".cpIsNullOrEmpty());
        }

        [TestMethod]
        public void CpToString_ReturnsEmptyStringForNull()
        {
            Assert.AreEqual("", ((object?)null).cpToString());
            Assert.AreEqual("abc", ((object)"abc").cpToString());
            Assert.AreEqual("12", ((object)12).cpToString());
        }

        [TestMethod]
        public void CpToFlatStringParseDecimal_ConvertsMinorUnitsToDecimal()
        {
            // Bankalar tutarı kuruş cinsinden düz metin olarak döner: 10050 -> 100,50
            Assert.AreEqual(100.50m, "10050".cpToFlatStringParseDecimal());
            Assert.AreEqual(1.99m, "199".cpToFlatStringParseDecimal());
            Assert.AreEqual(100m, "100".cpToFlatStringParseDecimal(0));
        }

        [TestMethod]
        public void CpToFlatStringParseDecimal_ReturnsNullForInvalidValues()
        {
            Assert.IsNull("".cpToFlatStringParseDecimal());
            Assert.IsNull(((string?)null).cpToFlatStringParseDecimal());
            Assert.IsNull("abc".cpToFlatStringParseDecimal());
            Assert.IsNull("-100".cpToFlatStringParseDecimal());
            Assert.IsNull("100".cpToFlatStringParseDecimal(-1));
        }
    }
}
