using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace RealWorldStocks.Client.Core.Data
{
    public static class SerializationHelper
    {
        // TODO: Bring JSON.NET back with CM 1.3 to fix a method access exception
        //public static T Deserialize<T>(string serialized)
        //{
        //    if (string.IsNullOrEmpty(serialized))
        //        return default(T);

        //    //return JsonConvert.DeserializeObject<T>(serialized);
        //}

        //public static string Serialize<T>(T obj)
        //{
        //    return JsonConvert.SerializeObject(obj);
        //}

        public static string Serialize<T>(T obj)
        {
            var ser = new DataContractJsonSerializer(typeof (T));
            var stream = new MemoryStream(); 
            var sr = new StreamReader(stream); 
            ser.WriteObject(stream, obj); 
            sr.BaseStream.Position = 0; 
            string s = sr.ReadToEnd();
            return s;
        }

        public static T Deserialize<T>(string json)
        {
            using(var stream = new MemoryStream())
            using (var sr = new StreamWriter(stream))
            {
                sr.Write(json);
                sr.Flush();
                sr.BaseStream.Position = 0;

                var ser = new DataContractJsonSerializer(typeof (T));
                return (T) ser.ReadObject(stream);
            }
        }

    }
}