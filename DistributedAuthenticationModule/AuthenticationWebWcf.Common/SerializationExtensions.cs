using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace AuthenticationWebWcf.Common
{
    public static class SerializationExtensions
    {
        public static string ToJson(this object instance)
        {
            try
            {
                return JsonConvert.SerializeObject(instance);
            }
            catch (Exception ex)
            {
                throw new SerializationException("Error en la serializacion de Json", ex);
            }
        }

        public static IDictionary<string, object> ToDictionaryTuple(this object instance)
        {
            var json = instance.ToJson();
            return json.FromJson<Dictionary<string, object>>();
        }

        public static T FromJson<T>(this string data)
        {
            try
            {
                return string.IsNullOrEmpty(data) ? default(T) : JsonConvert.DeserializeObject<T>(data);
            }
            catch (Exception ex)
            {
                throw new SerializationException("Error en la serializacion de Json", ex);
            }
        }

        public static T FromDictionary<T>(this IDictionary dic)
        {
            var json = dic.ToJson();
            return json.FromJson<T>();
        }
    }
}