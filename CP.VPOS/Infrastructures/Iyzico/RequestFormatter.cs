using System;

namespace CP.VPOS.Infrastructures.Iyzico
{
    internal class RequestFormatter
    {
        public static String FormatPrice(String price)
        {
            if (!price.Contains("."))
            {
                return price + ".0";
            }
            int subStrIndex = 0;
            String priceReversed = StringHelper.Reverse(price);
            for (int i = 0; i < priceReversed.Length; i++)
            {
                if (priceReversed[i].Equals('0'))
                {
                    subStrIndex = i + 1;
                }
                else if (priceReversed[i].Equals('.'))
                {
                    priceReversed = "0" + priceReversed;
                    break;
                }
                else
                {
                    break;
                }
            }
            return StringHelper.Reverse(priceReversed.Substring(subStrIndex));
        }
    }
}
