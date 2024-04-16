using System;
using System.Collections.Generic;

namespace CP.VPOS.Helpers
{
    internal static class FoundationHelper
    {
        internal static long time()
        {
            return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        internal static Dictionary<string, object> getFormParams(string formhtml)
        {
            Dictionary<string, object> keyValues = new Dictionary<string, object>();

            if (formhtml.cpIsNullOrEmpty())
                return keyValues;

            System.Text.RegularExpressions.MatchCollection match = System.Text.RegularExpressions.Regex.Matches(formhtml, "<input.*name=\"(.*?)\".*value=\"(.*?)\".*>");

            if (match != null && match.Count > 0)
            {
                foreach (System.Text.RegularExpressions.Match item in match)
                {
                    if (!keyValues.ContainsKey(item.Groups[1].Value))
                        keyValues.Add(item.Groups[1].Value, item.Groups[2].Value);
                }
            }

            return keyValues;
        }

        internal static string toXml(this Dictionary<string, object> valuePairs, string rootTag = "CC5Request", string charset = "ISO-8859-9")
        {
            string resp = "";

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();

            System.Xml.XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", charset, "yes");

            doc.AppendChild(dec);

            System.Xml.XmlElement cc5Request = doc.CreateElement(rootTag);
            doc.AppendChild(cc5Request);


            foreach (KeyValuePair<string, object> item in valuePairs)
            {
                //System.Xml.XmlElement payerauthenticationcode = doc.CreateElement(item.Key);
                //payerauthenticationcode.AppendChild(doc.CreateTextNode(item.Value.cpToString()));
                //cc5Request.AppendChild(payerauthenticationcode);

                cc5Request.AppendChild(GetSubXmlElement(ref doc, item));
            }

            resp = doc.OuterXml;


            return resp;
        }

        private static System.Xml.XmlElement GetSubXmlElement(ref System.Xml.XmlDocument doc, KeyValuePair<string, object> item)
        {
            System.Xml.XmlElement node = doc.CreateElement(item.Key);

            if (item.Value is Dictionary<string, object>)
            {
                Dictionary<string, object> subNodes = item.Value as Dictionary<string, object>;

                foreach (KeyValuePair<string, object> subNode in subNodes)
                {
                    node.AppendChild(GetSubXmlElement(ref doc, subNode));
                }
            }
            else
            {
                node.AppendChild(doc.CreateTextNode(item.Value.cpToString()));
            }

            return node;
        }

        internal static Dictionary<string, object> XmltoDictionary(string xml, string xpath = "CC5Response")
        {
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();

            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.LoadXml(xml);

            var nodes = xmlDoc.SelectSingleNode(xpath);

            keyValuePairs = nodes.getChildDictionary();

            return keyValuePairs;
        }

        private static Dictionary<string, object> getChildDictionary(this System.Xml.XmlNode xmlNode)
        {
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();

            foreach (System.Xml.XmlNode sub in xmlNode.ChildNodes)
            {
                string keyName = sub.LocalName;

                if (keyValuePairs.ContainsKey(keyName))
                    keyName = $"{keyName}||{Guid.NewGuid()}";

                if (sub.ChildNodes.Count > 1 || sub.FirstChild?.HasChildNodes == true)
                    keyValuePairs.Add(keyName, sub.getChildDictionary());
                else
                    keyValuePairs.Add(keyName, sub.InnerText.cpToString());
            }

            return keyValuePairs;
        }

        internal static string ToHtmlForm(this Dictionary<string, string> keyValuePairs, string link)
        {
            string input = "";

            foreach (var item in keyValuePairs)
            {
                input += $@"<input type=""hidden"" name=""{item.Key}"" value=""{item.Value}"" />" + Environment.NewLine;
            }

            string htmlForm = $@"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"">
<head><title>Sanal Pos</title></head>
<body>
    <form action=""{link}"" name=""3DFormPost"" id=""3DFormPost"" method=""post"">
    {input}
    </form>
    <script type=""text/javascript"">
        document.getElementById('3DFormPost').submit();
    </script>
</body>
</html>
";

            return htmlForm;
        }

        internal static bool IsCardNumberValid(string cardNumber)
        {
            int i, checkSum = 0;

            for (i = cardNumber.Length - 1; i >= 0; i -= 2)
                checkSum += (cardNumber[i] - '0');

            for (i = cardNumber.Length - 2; i >= 0; i -= 2)
            {
                int val = ((cardNumber[i] - '0') * 2);
                while (val > 0)
                {
                    checkSum += (val % 10);
                    val /= 10;
                }
            }

            return ((checkSum % 10) == 0);
        }
    }
}
