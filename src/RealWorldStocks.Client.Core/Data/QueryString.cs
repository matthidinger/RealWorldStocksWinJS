using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RealWorldStocks.Client.Core.Data
{
    public class QueryString : IEnumerable<KeyValuePair<string, string>>
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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return _params.GetEnumerator();
        }
    }
}