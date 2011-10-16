using System.Collections.Generic;
using RealWorldStocks.Client.Models;

namespace RealWorldStocks.Client.Data.Services
{
    public interface IStocksWebService
    {
        HttpGetRequest<StockSnapshot> GetSnapshot(string symbol);
        HttpGetRequest<IEnumerable<StockSnapshot>> GetWatchListSnapshots();
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

        public HttpGetRequest<StockSnapshot> GetSnapshot(string symbol)
        {
            var queryString = new QueryString
                                  {
                                      {"Symbol", symbol}
                                  };

            return CreateHttpGetRequest<StockSnapshot>("Stocks/GetSnapshot", queryString);
        }

        public HttpGetRequest<IEnumerable<StockSnapshot>> GetWatchListSnapshots()
        {
            var queryString = new QueryString
                                  {
                                      {"symbols", "MSFT"},
                                      {"symbols", "AAPL"},
                                      {"symbols", "GOOG"},
                                      {"symbols", "AMZN"},
                                      {"symbols", "NFLX"}
                                  };

            return CreateHttpGetRequest<IEnumerable<StockSnapshot>>("Stocks/GetSnapshots", queryString);
        }
    }
}