using System;
using Caliburn.Micro;
using Microsoft.Phone.Scheduler;
using RealWorldStocks.Client.Core.Models;
using RealWorldStocks.Client.UI.ViewModels.StockDetails;

namespace RealWorldStocks.Client.UI.ViewModels.Home
{
    public class HomeViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private readonly INavigationService _navigation;
        private readonly HomeWatchListViewModel _watchList;
        private readonly HomeNewsViewModel _news;

        public HomeViewModel(INavigationService navigation, HomeWatchListViewModel watchList, HomeNewsViewModel news)
        {
            _navigation = navigation;

            _watchList = watchList;
            _watchList.ActivateWith(this);

            _news = news;
            Items.Add(_news);
        }

        protected override void OnViewReady(object view)
        {
            ActivateItem(_watchList);
        }

        public void LoadSymbol(StockSnapshot snapshot)
        {
            _navigation.UriFor<StockDetailsViewModel>()
                .WithParam(m => m.Symbol, snapshot.Symbol)
                .Navigate();
        }


        public void ActivateAgent()
        {
            //_navigation.UriFor<BasicHttpViewModel>().Navigate();

            var periodicTask = new PeriodicTask("StocksAgent")
                                   {
                                       Description = "Update stocks",
                                       ExpirationTime = DateTime.Now.AddDays(14)
                                   };

            // If the agent is already registered with the system
            if (ScheduledActionService.Find(periodicTask.Name) == null)
            {
                ScheduledActionService.Add(periodicTask);
            }


            ScheduledActionService.LaunchForTest(periodicTask.Name, TimeSpan.FromSeconds(1));
        }
    }
}