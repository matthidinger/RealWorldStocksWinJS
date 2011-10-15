using System.Collections.Generic;
using Caliburn.Micro;
using RealWorldWPDev.Client.Data;
using RealWorldWPDev.Client.Models;
using RealWorldWPDev.Client.Services;

namespace RealWorldWPDev.Client.ViewModels
{
    public class StockQuoteViewModel : Screen
    {
        protected override void OnViewReady(object view)
        {
            Coroutine.BeginExecute(GetStockSymbol().GetEnumerator());
        }

        private IEnumerable<IResult> GetStockSymbol()
        {
            var result = StockWebService.GetSnapshot("MSFT").AsResult();
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