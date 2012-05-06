using System.Collections.Generic;
using System.Net;
using System;

namespace RealWorldStocks.Client.Core.Web
{
    public class HttpException : Exception
    {
        public HttpException()
        {
            Errors = new List<string>
                         {
                             "We had a problem connecting to the web service, please try again in a few moments."
                         };

        }

        public HttpException(string message) : base(message)
        {
        }

        public HttpException(string message, Exception inner) : base(message, inner)
        {
        }


        public HttpException(WebException ex)
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