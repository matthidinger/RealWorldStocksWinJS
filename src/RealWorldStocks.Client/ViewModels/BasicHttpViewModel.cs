using System.Collections.Generic;
using Caliburn.Micro;
using RealWorldStocks.Client.Data;
using RealWorldStocks.Client.Data.Services;
using RealWorldStocks.Client.Models;

namespace RealWorldStocks.Client.ViewModels
{
    public class BasicHttpViewModel : Screen
    {
        private readonly IStocksWebService _stocksWebService;

        public BasicHttpViewModel(IStocksWebService stocksWebService)
        {
            _stocksWebService = stocksWebService;
        }

        private string _symbol;
        public string Symbol
        {
            get { return _symbol; }
            set
            {
                _symbol = value;
                NotifyOfPropertyChange(() => Symbol);
                NotifyOfPropertyChange(() => CanGetSnapshot);
            }
        }

        public bool CanGetSnapshot
        {
            get { return !string.IsNullOrEmpty(Symbol); }
        }


        public IEnumerable<IResult> GetSnapshot()
        {
            var result = _stocksWebService.GetSnapshot(Symbol).AsResult();
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