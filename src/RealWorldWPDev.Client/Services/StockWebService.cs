using System.Collections.Generic;
using RealWorldWPDev.Client.Data;
using RealWorldWPDev.Client.Models;

namespace RealWorldWPDev.Client.Services
{
    public static class StockWebService
    {
        private static HttpGetRequest<T> CreateHttpGetRequest<T>(string url, QueryString queryString)
        {
            queryString = queryString ?? new QueryString();
            queryString.Add("isTrial", ApplicationSettings.Current.IsTrial.ToString());
            queryString.Add("clientVersion", ApplicationSettings.Current.ClientVersion);

            var myLocation = GlobalData.Current.MyLocation;
            if (!myLocation.IsUnknown)
            {
                queryString.Add("myLat", myLocation.Latitude.ToString());
                queryString.Add("myLong", myLocation.Longitude.ToString());
            }

            var fullUrl = ApplicationSettings.Current.WebServiceBaseUrl + url + queryString;
            return new HttpGetRequest<T>(fullUrl);
        }


        public static HttpGetRequest<StockSnapshot> GetSnapshot(string symbol)
        {
            var queryString = new QueryString();
            queryString.Add("Symbol", symbol);
            return CreateHttpGetRequest<StockSnapshot>("Stocks/GetSnapshot", queryString);
        }

        public static HttpGetRequest<IEnumerable<StockSnapshot>> GetWatchListSnapshots()
        {
            var queryString = new QueryString();

            queryString.Add("symbols", "MSFT");
            queryString.Add("symbols", "AAPL");
            queryString.Add("symbols", "GOOG");
            queryString.Add("symbols", "AMZN");
            queryString.Add("symbols", "NFLX");

            return CreateHttpGetRequest<IEnumerable<StockSnapshot>>("Stocks/GetSnapshots", queryString);
        }
    }
}