using System.Collections.Generic;
using Caliburn.Micro;
using RealWorldStocks.Client.Data;
using RealWorldStocks.Client.Data.Services;
using RealWorldStocks.Client.Models;

namespace RealWorldStocks.Client.ViewModels
{
    public class StockQuoteViewModel : Screen
    {
        private readonly IStocksWebService _stocksWebService;

        public StockQuoteViewModel(IStocksWebService stocksWebService)
        {
            _stocksWebService = stocksWebService;
        }

        protected override void OnViewReady(object view)
        {
            Coroutine.BeginExecute(GetStockSymbol().GetEnumerator());
        }

        private IEnumerable<IResult> GetStockSymbol()
        {
            var result = _stocksWebService.GetSnapshot("MSFT").AsResult();
            yield return result;

            Snapshot = result.Response.Model;
        }

        private StockSnapshot _snapshot;
        public StockSnapshot Snapshot
        {
            get { return _snapshot; }
            set
            {
                _snapshot = value;
                NotifyOfPropertyChange(() => Snapshot);
            }
        }
    }
}