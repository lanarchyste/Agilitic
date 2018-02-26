using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agilitic.Utils
{
    internal static class JSON
    {
        public static T Deserialize<T>(string json)
        {
            try
            {
                T deserialized = JsonConvert.DeserializeObject<T>(json);
                return deserialized;
            }
            catch
            {
                return default(T);
            }
        }

        public static string Serialize(Object obj)
        {
            try
            {
                string json = JsonConvert.SerializeObject(obj);
                return json;
            }
            catch
            {
                return null;
            }
        }
    }
}
