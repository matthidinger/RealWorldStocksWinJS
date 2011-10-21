using System;
using System.Collections.Generic;
using System.Threading;
using Caliburn.Micro;
using Microsoft.Phone.Shell;
using RealWorldStocks.Client.Core.Data;
using RealWorldStocks.Client.Core.Data.Services;
using RealWorldStocks.Client.Core.Helpers;
using RealWorldStocks.Client.Core.Models;
using RealWorldStocks.Client.UI.Framework;
using RealWorldStocks.Client.UI.Helpers;

namespace RealWorldStocks.Client.UI.ViewModels.Home
{
    public class HomeWatchListViewModel : Screen, IRefreshable, IAppBarController
    {
        private readonly IStocksWebService _stocksWebService;

        public HomeWatchListViewModel(IStocksWebService stocksWebService)
        {
            _stocksWebService = stocksWebService;
            DisplayName = "watch list";
            WatchList = new BindableCollection<StockSnapshot>();
        }

        public BindableCollection<StockSnapshot> WatchList { get; set; }

        protected override void OnInitialize()
        {
            RefreshData();
        }

        private IEnumerable<IResult> UpdateWatchList()
        {
            var request = _stocksWebService.GetWatchListSnapshots().Execute();
            yield return request;

            // TODO: Handle errors
            if (!request.Response.HasError)
            {
                WatchList.RepopulateObservableCollection(request.Response.Model);
            }
            else
            {

            }

            yield return BusyIndictator.HideResult();
        }

        public void RefreshData()
        {
            BusyIndictator.Show("Loading watch list...");

            // TODO: Move this to OnViewReady in CM 1.3
            // For now sleep for a bit to let the panorama load smoothly
            ThreadPool.QueueUserWorkItem(callback =>
            {
                Thread.Sleep(1000);
                Coroutine.BeginExecute(UpdateWatchList().GetEnumerator());
            });
        }

        public IApplicationBar ApplicationBar
        {
            get
            {
                var appBar = new ApplicationBar();
                appBar.Buttons.Add(AppBarHelper.AddButton);
                appBar.Buttons.Add(AppBarHelper.RefreshButton);
                return appBar;
            }
        }

        public event EventHandler AppBarChanged = delegate { };
    }
}