using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace RealWorldStocks.Web.Extensions
{
    public static class StringExtensions
    {
        // Taken from Chris Howie (http://www.chrishowie.com/) in his answer on Stack Overflow.
        // Source: http://stackoverflow.com/questions/4150697/regular-expression-to-split-on-comma-except-if-quoted 
        public static IEnumerable<string> SplitCSV(this string csvString)
        {
            var sb = new StringBuilder();
            bool quoted = false;

            foreach (char c in csvString)
            {
                if (quoted)
                {
                    if (c == '"')
                        quoted = false;
                    else
                        sb.Append(c);
                }
                else
                {
                    if (c == '"')
                    {
                        quoted = true;
                    }
                    else if (c == ',')
                    {
                        yield return sb.ToString();
                        sb.Length = 0;
                    }
                    else
                    {
                        sb.Append(c);
                    }
                }
            }

            if (quoted)
                throw new ArgumentException("csvString", "Unterminated quotation mark.");

            yield return sb.ToString();
        }
    }
}