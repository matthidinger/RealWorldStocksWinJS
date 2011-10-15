using System;
using System.Collections.Generic;
using System.Linq;

namespace RealWorldWPDev.Client.Data
{
    public class QueryString
    {
        private readonly List<KeyValuePair<string, string>> _params = new List<KeyValuePair<string, string>>();

        public override string ToString()
        {
            return "?" + String.Join("&", _params.Select(param => String.Format("{0}={1}", param.Key, param.Value)).ToArray());
        }

        public void Add(string key, string value)
        {
            _params.Add(new KeyValuePair<string, string>(key, value));
        }
    }
}