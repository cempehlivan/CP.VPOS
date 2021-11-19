using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;


namespace CP.VPOS.Helpers
{
    internal class JsonConverter<T> : CustomCreationConverter<T> where T : new()
    {
        public override T Create(Type objectType)
        {
            return new T();
        }
    }

    internal static class JsonConvertHelper
    {
        internal static T Convert<T>(string json) where T : new()
        {
            T t = JsonConvert.DeserializeObject<T>(json, new JsonConverter<T>());
            return t;
        }

        internal static string Json<T>(T model)
        {
            string json =
            JsonConvert.SerializeObject(
            model,
            Formatting.Indented,
            new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }
            );
            return json;
        }

        internal static object GetValue(this object jObject, string key)
        {
            return ((JObject)jObject)[key];
        }

        internal static bool ValidateJSON(this string s)
        {
            try
            {
                JToken.Parse(s);
                return true;
            }
            catch //(JsonReaderException ex)
            {
                return false;
            }
        }

        internal static string SerializeObjectToPrettyJson<T>(T obj)
        {
            StringBuilder sb = new StringBuilder(256);
            StringWriter sw = new StringWriter(sb, System.Globalization.CultureInfo.InvariantCulture);

            var jsonSerializer = JsonSerializer.CreateDefault();
            jsonSerializer.NullValueHandling = NullValueHandling.Ignore;
            jsonSerializer.DefaultValueHandling = DefaultValueHandling.Ignore;
            jsonSerializer.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonSerializer.Formatting = Formatting.Indented;

            using (JsonTextWriter jsonWriter = new JsonTextWriter(sw))
            {
                jsonWriter.Formatting = Formatting.Indented;
                jsonWriter.IndentChar = '\t';
                jsonWriter.Indentation = 1;

                jsonSerializer.Serialize(jsonWriter, obj, typeof(T));
            }

            string json = sw.ToString();
            return json.Replace("\r", "");
        }
    }
}
