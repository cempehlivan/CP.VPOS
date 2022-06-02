using System;
using System.Collections.Generic;

namespace CP.VPOS.Infrastructures.Iyzico
{
    internal class ToStringRequestBuilder
    {
        private String _requestString;

        private ToStringRequestBuilder(String requestString)
        {
            this._requestString = requestString;
        }

        public static ToStringRequestBuilder NewInstance()
        {
            return new ToStringRequestBuilder("");
        }

        public static ToStringRequestBuilder NewInstance(String requestString)
        {
            return new ToStringRequestBuilder(requestString);
        }

        public ToStringRequestBuilder AppendSuper(String superRequestString)
        {
            if (superRequestString != null)
            {
                superRequestString = superRequestString.Substring(1);
                superRequestString = superRequestString.Substring(0, superRequestString.Length - 1);

                if (superRequestString.Length > 0)
                {
                    this._requestString = this._requestString + superRequestString + ",";
                }
            }
            return this;
        }

        public ToStringRequestBuilder Append(String key, Object value = null)
        {
            if (value != null)
            {
                if (value is RequestStringConvertible)
                {
                    AppendKeyValue(key, ((RequestStringConvertible)value).ToPKIRequestString());
                }
                else
                {
                    AppendKeyValue(key, value.ToString());
                }
            }
            return this;
        }

        public ToStringRequestBuilder AppendPrice(String key, String value)
        {
            if (value != null)
            {
                AppendKeyValue(key, RequestFormatter.FormatPrice(value));
            }
            return this;
        }

        public ToStringRequestBuilder AppendList<T>(String key, List<T> list = null) where T : RequestStringConvertible
        {
            if (list != null)
            {
                String appendedValue = "";
                foreach (RequestStringConvertible value in list)
                {
                    appendedValue = appendedValue + value.ToPKIRequestString() + ", ";
                }
                AppendKeyValueArray(key, appendedValue);
            }
            return this;
        }

        public ToStringRequestBuilder AppendList (String key, List<int> list = null)
        {
            if (list != null)
            {
                String appendedValue = "";
                foreach (int value in list)
                {
                    appendedValue = appendedValue + value + ", ";
                }
                AppendKeyValueArray(key, appendedValue);
            }
            return this;
        }

        private ToStringRequestBuilder AppendKeyValue(String key, String value)
        {
            if (value != null)
            {
                this._requestString = this._requestString + key + "=" + value + ",";
            }
            return this;
        }

        private ToStringRequestBuilder AppendKeyValueArray(String key, String value)
        {
            if (value != null)
            {
                value = value.Substring(0, value.Length - 2);
                this._requestString = this._requestString + key + "=[" + value + "],";
            }
            return this;
        }

        private ToStringRequestBuilder AppendPrefix()
        {
            this._requestString = "[" + this._requestString + "]";
            return this;
        }

        private ToStringRequestBuilder RemoveTrailingComma()
        {
            if (!string.IsNullOrEmpty(this._requestString))
            {
                this._requestString = this._requestString.Substring(0, this._requestString.Length - 1);
            }
            return this;
        }

        public String GetRequestString()
        {
            RemoveTrailingComma();
            AppendPrefix();
            return _requestString;
        }
    }
}
