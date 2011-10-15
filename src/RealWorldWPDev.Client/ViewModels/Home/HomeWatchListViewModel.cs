using System.Collections.Generic;
using Caliburn.Micro;
using RealWorldWPDev.Client.Data;
using RealWorldWPDev.Client.Framework;
using RealWorldWPDev.Client.Helpers;
using RealWorldWPDev.Client.Models;
using RealWorldWPDev.Client.Services;

namespace RealWorldWPDev.Client.ViewModels.Home
{
    public class HomeWatchListViewModel : Screen
    {
        private readonly HomeViewModel _homeViewModel;

        public HomeWatchListViewModel(HomeViewModel homeViewModel)
        {
            _homeViewModel = homeViewModel;
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

            var request = StockWebService.GetWatchListSnapshots().AsResult();
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