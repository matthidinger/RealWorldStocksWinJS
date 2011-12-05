using System;

namespace RealWorldStocks.Web.Helpers
{
    public static class ConvertHelper
    {
        public static T ConvertTo<T>(this string value)
        {
            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}