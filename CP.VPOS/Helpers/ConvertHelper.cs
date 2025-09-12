using System;
using System.Text.RegularExpressions;


namespace CP.VPOS.Helpers
{
    internal static class ConvertHelper
    {
        internal static Exception lastError = null;

        internal static string getMaxLength(this string text, int maxLength)
        {
            if (text?.Length <= maxLength)
                return text;

            return text.Substring(0, maxLength);
        }

        internal static string clearString(this string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                text = text.Trim();
                text = Regex.Replace(text, @"\s+", " ");
            }

            return text;
        }

        #region ConvertTo
        /// <summary>
        /// Sayı değeri taşıyan her hangi bir metni sayısal değere çevirir (sql=float).
        /// </summary>
        /// <param name="pText"></param>
        /// <returns></returns>
        internal static double cpToDouble(this string pText)
        {
            lastError = null;
            double vVal = 0;
            try
            {
                vVal = Convert.ToDouble(pText);
            }
            catch (Exception e)
            {
                lastError = e;
            }
            return vVal;
        }

        /// <summary>
        /// Sayı değeri taşıyan her hangi bir metni sayısal değere çevirir (sql=real).
        /// </summary>
        /// <param name="pText"></param>
        /// <returns></returns>
        internal static Single cpToSingle(this string pText)
        {
            lastError = null;
            Single vVal = 0;
            try
            {
                vVal = Convert.ToSingle(pText);
            }
            catch (Exception e)
            {
                lastError = e;
            }
            return vVal;
        }

        /// <summary>
        /// Sayı değeri taşıyan her hangi bir metni sayısal değere çevirir (sql=bigint).
        /// </summary>
        /// <param name="pText"></param>
        /// <returns></returns>
        internal static long cpToLong(this string pText)
        {
            lastError = null;
            long vVal = 0;
            Int64.TryParse(pText, out vVal);
            //try
            //{
            //    vVal = Convert.ToInt64(pText);
            //}
            //catch (Exception e)
            //{
            //    lastError = e;
            //}
            return vVal;
        }

        /// <summary>
        /// Sayı değeri taşıyan her hangi bir metni sayısal değere çevirir (sql=int).
        /// </summary>
        /// <param name="pText"></param>
        /// <returns></returns>
        internal static int cpToInt(this string pText)
        {
            lastError = null;
            int vVal = 0;
            try
            {
                vVal = Convert.ToInt32(pText);
            }
            catch (Exception e)
            {
                lastError = e;
            }
            return vVal;
        }

        /// <summary>
        /// Sayı değeri taşıyan her hangi bir metni sayısal değere çevirir (sql=smallint).
        /// </summary>
        /// <param name="pText"></param>
        /// <returns></returns>
        internal static short cpToShort(this string pText)
        {
            lastError = null;
            short vVal = 0;
            try
            {
                vVal = Convert.ToInt16(pText);
            }
            catch (Exception e)
            {
                lastError = e;
            }
            return vVal;
        }

        /// <summary>
        /// Bool değeri taşıyan her hangi bir objeyi bool değere çevirir (sql=bit).
        /// </summary>
        /// <param name="pText"></param>
        /// <returns></returns>
        internal static bool cpToBool(this string pText)
        {
            lastError = null;
            bool vVal = false;
            try
            {
                switch (pText)
                {
                    case "1":
                        vVal = true;
                        break;
                    case "0":
                        vVal = false;
                        break;
                    case "":
                        break;
                    default:
                        vVal = Convert.ToBoolean(pText);
                        break;
                }
            }
            catch (Exception e)
            {
                lastError = e;
            }
            return vVal;
        }

        /// <summary>
        /// Sayı değeri taşıyan her hangi bir metni sayısal değere çevirir (sql=tinyint).
        /// </summary>
        /// <param name="pText"></param>
        /// <returns></returns>
        internal static byte cpToByte(this string pText)
        {
            lastError = null;
            byte vVal = 0;
            try
            {
                vVal = Convert.ToByte(pText);
            }
            catch (Exception e)
            {
                lastError = e;
            }
            return vVal;
        }

        /// <summary>
        /// Sayı değeri taşıyan her hangi bir metni sayısal değere çevirir (sql=decimal).
        /// </summary>
        /// <param name="pText"></param>
        /// <returns></returns>
        internal static decimal cpToDecimal(this string pText)
        {
            lastError = null;
            decimal vVal = 0;
            try
            {
                vVal = Convert.ToDecimal(pText);
            }
            catch (Exception e)
            {
                lastError = e;
            }
            return vVal;
        }

        /// <summary>
        /// Gelen tarih bilgisini sadece tarih içerecek şekilde metin ifadeye dönüştürür (sql=date).
        /// </summary>
        /// <param name="pText"></param>
        /// <returns></returns>
        internal static string cpToDBDate(this DateTime pText)
        {
            lastError = null;
            string vVal = "'0'";
            try
            {
                vVal = "'" + pText.ToString("yyyyMMdd") + "'";
            }
            catch (Exception e)
            {
                lastError = e;
            }
            return vVal;
        }

        /// <summary>
        /// Gelen tarih bilgisini sadece saat (HH:mm:ss) içerecek şekilde metin ifadeye dönüştürür (sql=time).
        /// </summary>
        /// <param name="pText"></param>
        /// <param name="pWithSecond">Saniye bilgisi içersin mi?</param>
        /// <returns></returns>
        internal static string cpToDBTime(this DateTime pText, bool pWithSecond = true)
        {
            lastError = null;
            string vVal = "'0'";
            try
            {
                vVal = "'" + pText.ToString("HH:mm" + (pWithSecond ? ":ss" : "")) + "'";
            }
            catch (Exception e)
            {
                lastError = e;
            }
            return vVal;
        }

        /// <summary>
        /// Gelen tarih bilgisini tarih ve saat (HH:mm:ss) içerecek şekilde metin ifadeye dönüştürür (sql=datetime).
        /// </summary>
        /// <param name="pText"></param>
        /// <param name="pWithSecond">Saniye bilgisi içersin mi?</param>
        /// <returns></returns>
        internal static string cpToDBDateTime(this DateTime pText, bool pWithSecond = true)
        {
            lastError = null;
            string vVal = "0";
            try
            {
                vVal = "'" + pText.ToString("yyyyMMdd HH:mm" + (pWithSecond ? ":ss" : "")) + "'";
            }
            catch (Exception e)
            {
                lastError = e;
            }
            return vVal;
        }

        /// <summary>
        /// Gelen tarih bilgisini crystal report için metin ifadeye dönüştürür (başına ve sonuna çift tırnak ekler).
        /// </summary>
        /// <param name="pText"></param>
        /// <param name="pWithSecond">Saniye bilgisi içersin mi?</param>
        /// <returns></returns>
        internal static string cpToCryDateTime(this DateTime pText, bool pWithTime = false, bool pWithSecond = false)
        {
            lastError = null;
            string vVal = "0";
            try
            {
                vVal = "\"" + pText.ToString("yyyy-MM-dd" + (pWithTime ? " HH:mm" + (pWithSecond ? ":ss" : "") : "")) + "\"";
            }
            catch (Exception e)
            {
                lastError = e;
            }
            return vVal;
        }

        /// <summary>
        /// Sayı değeri taşıyan her hangi bir objeyi sayısal değere çevirir (sql=float).
        /// </summary>
        /// <param name="pText"></param>
        /// <returns></returns>
        internal static double cpToDouble(this object pText)
        {
            lastError = null;
            double vVal = 0;
            try
            {
                if (!(pText is DBNull))
                    vVal = Convert.ToDouble(pText);
            }
            catch (Exception e)
            {
                lastError = e;
            }
            return vVal;
        }

        /// <summary>
        /// Sayı değeri taşıyan her hangi bir objeyi sayısal değere çevirir (sql=real).
        /// </summary>
        /// <param name="pText"></param>
        /// <returns></returns>
        internal static Single cpToSingle(this object pText)
        {
            lastError = null;
            Single vVal = 0;
            try
            {
                if (!(pText is DBNull))
                    vVal = Convert.ToSingle(pText);
            }
            catch (Exception e)
            {
                lastError = e;
            }
            return vVal;
        }

        /// <summary>
        /// Sayı değeri taşıyan her hangi bir objeyi sayısal değere çevirir (sql=long).
        /// </summary>
        /// <param name="pText"></param>
        /// <returns></returns>
        internal static long cpToLong(this object pText)
        {
            lastError = null;
            long vVal = 0;
            try
            {
                if (!(pText is DBNull))
                    vVal = Convert.ToInt64(pText);
            }
            catch (Exception e)
            {
                lastError = e;
            }
            return vVal;
        }

        /// <summary>
        /// Sayı değeri taşıyan her hangi bir objeyi sayısal değere çevirir (sql=int).
        /// </summary>
        /// <param name="pText"></param>
        /// <returns></returns>
        internal static int cpToInt(this object pText)
        {
            lastError = null;
            int vVal = 0;
            try
            {
                if (!(pText is DBNull))
                    vVal = Convert.ToInt32(pText);
            }
            catch (Exception e)
            {
                lastError = e;
            }
            return vVal;
        }

        /// <summary>
        /// Sayı değeri taşıyan her hangi bir metni sayısal değere çevirir (sql=smallint).
        /// </summary>
        /// <param name="pText"></param>
        /// <returns></returns>
        internal static short cpToShort(this object pText)
        {
            lastError = null;
            short vVal = 0;
            try
            {
                if (!(pText is DBNull))
                    vVal = Convert.ToInt16(pText);
            }
            catch (Exception e)
            {
                lastError = e;
            }
            return vVal;
        }

        /// <summary>
        /// Sayı değeri taşıyan her hangi bir metni sayısal değere çevirir (sql=tinyint).
        /// </summary>
        /// <param name="pText"></param>
        /// <returns></returns>
        internal static byte cpToByte(this object pText)
        {
            lastError = null;
            byte vVal = 0;
            try
            {
                if (!(pText is DBNull))
                    vVal = Convert.ToByte(pText);
            }
            catch (Exception e)
            {
                lastError = e;
            }
            return vVal;
        }

        /// <summary>
        /// Bool değeri taşıyan her hangi bir objeyi bool değere çevirir (sql=bit).
        /// </summary>
        /// <param name="pText"></param>
        /// <returns></returns>
        internal static bool cpToBool(this object pText)
        {
            lastError = null;
            bool vVal = false;
            try
            {
                switch ("" + pText)
                {
                    case "1":
                        vVal = true;
                        break;
                    case "0":
                        vVal = false;
                        break;
                    case "":
                        break;
                    default:
                        vVal = Convert.ToBoolean(pText);
                        break;
                }
                //vVal = Convert.ToBoolean(pText);
            }
            catch (Exception e)
            {
                lastError = e;
            }
            return vVal;
        }

        /// <summary>
        /// Sayı değeri taşıyan her hangi bir objeyi sayısal değere çevirir (sql=decimal).
        /// </summary>
        /// <param name="pText"></param>
        /// <returns></returns>
        internal static decimal cpToDecimal(this object pText)
        {
            lastError = null;
            decimal vVal = 0;
            try
            {
                if (!(pText is DBNull))
                    vVal = Convert.ToDecimal(pText);
            }
            catch (Exception e)
            {
                lastError = e;
            }
            return vVal;
        }

        /// <summary>
        /// Object türündeki bir değerden string ifadeye çeviri. Özellikle ToString() ile yaşanan null sorununu çözmek için kullanılabilir (sql=varchar).
        /// </summary>
        /// <param name="pText"></param>
        /// <returns></returns>
        internal static string cpToString(this object pText)
        {
            lastError = null;
            string vVal = "";
            try
            {
                if (!(pText is DBNull))
                    vVal = Convert.ToString(pText);
            }
            catch (Exception e)
            {
                lastError = e;
            }
            return vVal;
        }

        /// <summary>
        /// Object türündeki bir değerden datetime ifadeye çeviri.
        /// </summary>
        /// <param name="pText"></param>
        /// <returns></returns>
        internal static DateTime cpToDateTime(this object pText)
        {
            lastError = null;
            DateTime vVal = Convert.ToDateTime("01.01.1900");
            try
            {
                if (!(pText is DBNull))
                    vVal = Convert.ToDateTime(pText);
            }
            catch (Exception e)
            {
                lastError = e;
            }
            return vVal;
        }

        /// <summary>
        /// string türündeki bir değerden datetime ifadeye çeviri.
        /// </summary>
        /// <param name="pText"></param>
        /// <returns></returns>
        internal static DateTime cpToDateTime(this string pText)
        {
            lastError = null;
            DateTime vVal = Convert.ToDateTime("01.01.1900");
            try
            {
                vVal = Convert.ToDateTime(pText);
            }
            catch (Exception e)
            {
                lastError = e;
            }
            return vVal;
        }

        internal static bool cpIsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// String sayıyı decimal'e çevirir. 
        /// Son "fractionDigits" kadar haneyi kuruş/ondalık hanesi olarak kabul eder.
        /// Geçersiz değerlerde null döner.
        /// </summary>
        /// <param name="value">Örn: "81000"</param>
        /// <param name="fractionDigits">Ondalık basamak sayısı (default: 2)</param>
        /// <returns>Decimal değer veya null</returns>
        internal static decimal? cpToFlatStringParseDecimal(this string value, int fractionDigits = 2)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            if (!long.TryParse(value, out long number))
                return null;

            if (fractionDigits < 0 || number < 0)
                return null;

            decimal divisor = (decimal)Math.Pow(10, fractionDigits);

            return number / divisor;
        }
        #endregion
    }
}
