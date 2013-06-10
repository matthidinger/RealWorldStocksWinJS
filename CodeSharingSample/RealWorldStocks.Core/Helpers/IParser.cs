using System;

namespace RealWorldStocks.Core
{
    public interface IParser
    {
        object Parse(string json);
        object ToObject(string json, Type type);
        T ToObject<T>(string json);
        string Serialize(object message);
    }
}