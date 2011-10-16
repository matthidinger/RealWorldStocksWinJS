using System;
using RealWorldStocks.Client.Models;

namespace RealWorldStocks.Client.Data
{
    public abstract class HttpService
    {
        protected string BaseUrl { get; set; }

        protected HttpGetRequest<T> CreateHttpGetRequest<T>(string url, QueryString queryString)
        {
            if (string.IsNullOrEmpty(BaseUrl))
                throw new InvalidOperationException("The BaseUrl has not been set for this service");

            queryString = queryString ?? new QueryString();
            queryString.Add("isTrial", ApplicationSettings.Current.IsTrial.ToString());
            queryString.Add("clientVersion", ApplicationSettings.Current.ClientVersion);

            var myLocation = GlobalData.Current.MyLocation;
            if (!myLocation.IsUnknown)
            {
                queryString.Add("myLat", myLocation.Latitude.ToString());
                queryString.Add("myLong", myLocation.Longitude.ToString());
            }

            var fullUrl = BaseUrl + url + queryString;
            return new HttpGetRequest<T>(fullUrl);
        }
    }
}