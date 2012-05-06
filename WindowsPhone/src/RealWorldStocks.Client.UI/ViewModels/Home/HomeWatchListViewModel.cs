using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using Caliburn.Micro;
using Microsoft.Phone.Shell;
using RealWorldStocks.Client.Core.Web.Services;
using RealWorldStocks.Client.Core.Models;
using RealWorldStocks.Client.UI.Framework;

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

        public ObservableCollection<StockSnapshot> WatchListItems
        {
            get { return WatchList.Current; }
        }

        // TODO: Need better way of managing the BusyIndicator to handle change pages and coming back with an outstanding HTTP request

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
            BusyIndictator.Hide();
        }


        protected override void OnInitialize()
        {
            RefreshData();
        }

        public void RefreshData()
        {
            BusyIndictator.Show("Getting the latest quotes...");

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