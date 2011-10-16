using System.Collections.Generic;
using Caliburn.Micro;
using RealWorldStocks.Client.Data;
using RealWorldStocks.Client.Data.Services;
using RealWorldStocks.Client.Framework;
using RealWorldStocks.Client.Helpers;
using RealWorldStocks.Client.Models;

namespace RealWorldStocks.Client.ViewModels.Home
{
    public class HomeWatchListViewModel : Screen
    {
        private readonly IStocksWebService _stocksWebService;

        public HomeWatchListViewModel(IStocksWebService stocksWebService)
        {
            _stocksWebService = stocksWebService;
            DisplayName = "watch list";
            WatchList = new BindableCollection<StockSnapshot>();
        }

        protected override void OnViewLoaded(object view)
        {
            // TODO: Move this to OnViewReady in CM 1.3
            Coroutine.BeginExecute(UpdateWatchList().GetEnumerator());
        }

        private IEnumerable<IResult> UpdateWatchList()
        {
            yield return BusyIndictator.Show("Loading watch list...");

            var request = _stocksWebService.GetWatchListSnapshots().AsResult();
            yield return request;

            // TODO: Handle errors
            if(!request.Response.HasError)
            {
                WatchList.RepopulateObservableCollection(request.Response.Model);
            }
            else
            {
                
            }

            yield return BusyIndictator.Hide();
        }

        public BindableCollection<StockSnapshot> WatchList { get; set; } 
    }
}