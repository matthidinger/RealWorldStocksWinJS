using System;
using RealWorldStocks.Client.Core.Models;

namespace RealWorldStocks.Client.Core.Data
{
    public abstract class HttpService
    {
        protected string BaseUrl { get; set; }

        protected HttpRequest<TResult> CreateHttpRequest<TResult>(string url, QueryString queryString)
        {
            if (string.IsNullOrEmpty(BaseUrl))
                throw new InvalidOperationException("The BaseUrl has not been set for this service");

            // IN EVERY OUTGOING WEB REQUEST:
            // Automatically send the current Client Version 
            // Whether the app is running in Trial Mode 
            queryString = queryString ?? new QueryString();
            queryString.Add("isTrial", ApplicationSettings.Current.IsTrial.ToString());
            queryString.Add("clientVersion", ApplicationSettings.Current.ClientVersion);


            // This app doesn't use the Location service but this shows how it's possible
            // to automatically append myLat and myLong to every outgoing request.
            //var myLocation = GlobalData.Current.MyLocation;
            //if (!myLocation.IsUnknown)
            //{
            //    queryString.Add("myLat", myLocation.Latitude.ToString());
            //    queryString.Add("myLong", myLocation.Longitude.ToString());
            //}

            var fullUrl = BaseUrl + url + queryString;
            return new HttpRequest<TResult>(fullUrl);
        }
    }
}