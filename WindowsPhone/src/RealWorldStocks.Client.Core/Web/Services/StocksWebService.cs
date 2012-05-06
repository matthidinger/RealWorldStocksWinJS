using System.Collections.Generic;
using System.Linq;
using RealWorldStocks.Client.Core.Models;

namespace RealWorldStocks.Client.Core.Web.Services
{
    public interface IStocksWebService
    {
        HttpRequest<StockSnapshot> GetSnapshot(string symbol);
        HttpRequest<IEnumerable<StockSnapshot>> GetWatchListSnapshots();
        HttpRequest<IEnumerable<News>> GetNewsForWatchList();
    }

    public class StocksWebService : HttpService, IStocksWebService
    {
        public StocksWebService()
        {
#if DEBUG
            BaseUrl = DynamicLocalhost.ReplaceLocalhost("http://localhost/RealWorldStocks.Web/");
#else
            BaseUrl = "http://services.mydomain.com/v1/";
#endif
        }

        public HttpRequest<StockSnapshot> GetSnapshot(string symbol)
        {
            var queryString = new QueryString
                                  {
                                      {"symbol", symbol}
                                  };

            return CreateHttpRequest<StockSnapshot>("Stocks/GetSnapshot", queryString);
        }

        public HttpRequest<IEnumerable<StockSnapshot>> GetWatchListSnapshots()
        {
            var queryString = new QueryString();
            queryString.AddMany("symbols", WatchList.Current.Select(m => m.Symbol));

            return CreateHttpRequest<IEnumerable<StockSnapshot>>("Stocks/GetSnapshots", queryString);
        }

        public HttpRequest<IEnumerable<News>> GetNewsForWatchList()
        {
            var queryString = new QueryString();
            queryString.AddMany("symbols", WatchList.Current.Select(m => m.Symbol));

            return CreateHttpRequest<IEnumerable<News>>("Stocks/GetNews", queryString);
        }
    }
}