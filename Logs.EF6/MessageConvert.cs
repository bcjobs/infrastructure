using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logs.EF6
{
    static class MessageConvert
    {
        public static string ToJson(this object obj)
        {
            if (obj == null)
                return null;

            return JsonConvert.SerializeObject(obj, JsonSettings);
        }

        public static T ToObject<T>(this string json)
        {
            if (json == null)
                return default(T);

            return JsonConvert.DeserializeObject<T>(json, JsonSettings);
        }

        static JsonSerializerSettings JsonSettings =>
            new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                //ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                //PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            };
    }
}
