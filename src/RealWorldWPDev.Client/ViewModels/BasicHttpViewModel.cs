using System.Collections.Generic;
using Caliburn.Micro;
using RealWorldWPDev.Client.Data;
using RealWorldWPDev.Client.Models;
using RealWorldWPDev.Client.Services;

namespace RealWorldWPDev.Client.ViewModels
{
    public class BasicHttpViewModel : Screen
    {
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
            var result = StockWebService.GetSnapshot(Symbol).AsResult();
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