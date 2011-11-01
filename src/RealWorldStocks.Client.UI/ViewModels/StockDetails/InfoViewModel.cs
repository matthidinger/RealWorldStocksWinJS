using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using Caliburn.Micro;
using RealWorldStocks.Client.Core.Models;
using RealWorldStocks.Client.Core.Web.Services;
using RealWorldStocks.Client.UI.Framework;

namespace RealWorldStocks.Client.UI.ViewModels.StockDetails
{
    public class InfoViewModel : Screen //, IRefreshable
    {
        private readonly IStocksWebService _stocksWebService;

        public InfoViewModel(IStocksWebService stocksWebService)
        {
            _stocksWebService = stocksWebService;
            DisplayName = "info";
        }

        public StockSnapshot Snapshot { get; set; }
        public string Symbol { get; set; }

        //protected override void OnInitialize()
        //{
        //    RefreshData();
        //}

        //public void RefreshData()
        //{
        //    BusyIndictator.Show("Loading most recent snapshot...");

        //    // TODO: Move this to OnViewReady in CM 1.3
        //    ThreadPool.QueueUserWorkItem(callback =>
        //    {
        //        Thread.Sleep(1000);
        //        Coroutine.BeginExecute(UpdateDetails().GetEnumerator());
        //    });
        //}

        private IEnumerable<IResult> UpdateDetails()
        {
            var request = _stocksWebService.GetSnapshot(Symbol).Execute();
            yield return request;

            if (!request.Response.HasError)
            {
                // TODO: Bind
            }
            else
            {
                MessageBox.Show("We had troubles updating your watch list, please try again in a few moments",
                    "Unable to contact server", MessageBoxButton.OK);
            }

            yield return BusyIndictator.HideResult();
        }

    }
}