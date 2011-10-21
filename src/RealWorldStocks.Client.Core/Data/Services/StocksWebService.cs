using System.Collections.Generic;
using RealWorldStocks.Client.Core.Models;

namespace RealWorldStocks.Client.Core.Data.Services
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
                                      {"Symbol", symbol}
                                  };

            return CreateHttpRequest<StockSnapshot>("Stocks/GetSnapshot", queryString);
        }

        public HttpRequest<IEnumerable<StockSnapshot>> GetWatchListSnapshots()
        {
            var queryString = new QueryString
                                  {
                                      {"symbols", "MSFT"},
                                      {"symbols", "NOK"},
                                      {"symbols", "AAPL"},
                                      {"symbols", "GOOG"},
                                      {"symbols", "AMZN"},
                                      {"symbols", "NFLX"},
                                      {"symbols", "IBM"},
                                      {"symbols", "A"},
                                      {"symbols", "T"},
                                      {"symbols", "S"},
                                  };

            return CreateHttpRequest<IEnumerable<StockSnapshot>>("Stocks/GetSnapshots", queryString);
        }

        public HttpRequest<IEnumerable<News>> GetNewsForWatchList()
        {
            var queryString = new QueryString
                                  {
                                      {"symbols", "MSFT"},
                                      {"symbols", "AAPL"},
                                      {"symbols", "GOOG"},
                                      {"symbols", "AMZN"},
                                      {"symbols", "NFLX"}
                                  };

            return CreateHttpRequest<IEnumerable<News>>("Stocks/GetNews", queryString);
        }
    }
}