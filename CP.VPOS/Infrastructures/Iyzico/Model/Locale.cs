using System;

namespace CP.VPOS.Infrastructures.Iyzico.Model
{
    internal sealed class Locale
    {
        private readonly String value;

        public static readonly Locale EN = new Locale("en");
        public static readonly Locale TR = new Locale("tr");

        private Locale(String value)
        {
            this.value = value;
        }

        public override String ToString()
        {
            return value;
        }
    }
}
