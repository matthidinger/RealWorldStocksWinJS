using System.Collections.Generic;
using Caliburn.Micro;
using RealWorldStocks.Client.Core.Data;
using RealWorldStocks.Client.Core.Data.Services;
using RealWorldStocks.Client.Core.Models;

namespace RealWorldStocks.Client.UI.ViewModels
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
            var result = _stocksWebService.GetSnapshot("MSFT").Execute();
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