using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using Caliburn.Micro;
using Microsoft.Phone.Shell;
using RealWorldStocks.Client.Core.Data;
using RealWorldStocks.Client.Core.Data.Services;
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
        }

        public BindableCollection<StockSnapshot> WatchListItems
        {
            get { return WatchList.Current; }
        }

        protected override void OnInitialize()
        {
            RefreshData();
        }

        public void RefreshData()
        {
            BusyIndictator.Show("Loading watch list...");

            // TODO: Move this to OnViewReady in CM 1.3
            ThreadPool.QueueUserWorkItem(callback =>
            {
                Thread.Sleep(1000);
                Coroutine.BeginExecute(UpdateWatchList().GetEnumerator());
            });
        }

        private IEnumerable<IResult> UpdateWatchList()
        {
            var request = _stocksWebService.GetWatchListSnapshots().Execute();
            yield return request;

            if (!request.Response.HasError)
            {
                WatchList.Current.UpdateSnapshots(request.Response.Model.ToList());
            }
            else
            {
                MessageBox.Show("We had troubles updating your watch list, please try again in a few moments",
                    "Unable to contact server", MessageBoxButton.OK);
            }

            yield return BusyIndictator.HideResult();
        }



        // TODO: Come up with a system to disable the Refresh button when already refreshing?

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