using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace RealWorldStocks.Core
{
    public static class SerializationHelper
    {
        public static T Deserialize<T>(string serialized)
        {
            if (string.IsNullOrEmpty(serialized))
                return default(T);

            return default(T);

            //var dc = new DataContractJsonSerializer(typeof(T));
            //var ms = new MemoryStream();
            //var writer = new StringWriter(new StringBuilder(serialized));

            //return (T)dc.ReadObject();
        }

        public static string Serialize<T>(T obj)
        {
            return null;
            //return JsonConvert.SerializeObject(obj);
        }
    }
}