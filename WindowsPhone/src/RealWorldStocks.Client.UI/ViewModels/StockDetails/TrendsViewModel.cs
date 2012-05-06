using System;
using Caliburn.Micro;
using RealWorldStocks.Client.Core.Web.Services;

namespace RealWorldStocks.Client.UI.ViewModels.StockDetails
{
    public class TrendsViewModel : Screen
    {
        private readonly IStocksWebService _stocksWebService;

        public TrendsViewModel(IStocksWebService stocksWebService)
        {
            _stocksWebService = stocksWebService;
            DisplayName = "trends";
            TrendsUrl = new Uri("http://chart.finance.yahoo.com/t?s=MSFT&lang=en-US&region=US&width=300&height=180");
        }

        private Uri _trendsUrl;
        public Uri TrendsUrl
        {
            get { return _trendsUrl; }
            set
            {
                _trendsUrl = value;
                NotifyOfPropertyChange(() => TrendsUrl);
            }
        }

        public string Symbol { get; set; }
    }
}