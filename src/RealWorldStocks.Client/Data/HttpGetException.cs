using System.Collections.Generic;
using System.Net;
using System;

namespace RealWorldStocks.Client.Data
{
    public class HttpGetException : Exception
    {
        public HttpGetException()
        {
            Errors = new List<string>
                         {
                             "We had a problem connecting to the web service, please try again in a few moments."
                         };
        }

        public HttpGetException(WebException ex)
            : this()
        {
            // TODO: Get friendly errors
            //if (ex.Status == WebExceptionStatus.UnknownError)
            //{
            //    using (var reader = new StreamReader(ex.Response.GetResponseStream()))
            //    {
            //        var json = reader.ReadToEnd();
            //        var friendlyErrors = SerializationHelper.Deserialize<List<string>>(json);
            //        if(friendlyErrors != null)
            //            Errors.RepopulateList(friendlyErrors);
            //    }
            //}
        }

        public List<string> Errors { get; set; }
    }
}