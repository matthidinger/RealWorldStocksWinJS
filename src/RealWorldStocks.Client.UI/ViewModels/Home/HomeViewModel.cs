using System;
using Caliburn.Micro;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using RealWorldStocks.Client.Core.Models;
using RealWorldStocks.Client.UI.Helpers;
using RealWorldStocks.Client.UI.ViewModels.StockDetails;

namespace RealWorldStocks.Client.UI.ViewModels.Home
{
    public class HomeViewModel : Conductor<IScreen>.Collection.OneActive, IRefreshable, IAppBarController
    {
        private readonly INavigationService _navigation;

        public HomeViewModel(INavigationService navigation, HomeWatchListViewModel watchList, HomeNewsViewModel news, HomeQuoteViewModel quote)
        {
            _navigation = navigation;
            Items.Add(watchList);
            Items.Add(news);
            Items.Add(quote);
        }

        protected override void OnViewAttached(object view, object context)
        {
            base.OnViewAttached(view, context);
            AppBarHelper.BindAppBar(view as PhoneApplicationPage);
        }

        protected override void OnActivationProcessed(IScreen item, bool success)
        {
            base.OnActivationProcessed(item, success);
            AppBarChanged(this, EventArgs.Empty);
        }

        public void LoadSymbol(StockSnapshot snapshot)
        {
            _navigation.UriFor<StockDetailsViewModel>()
                .WithParam(m => m.Symbol, snapshot.Symbol)
                .Navigate();
        }

        public void RefreshData()
        {
            var refreshableChild = ActiveItem as IRefreshable;
            if (refreshableChild != null)
                refreshableChild.RefreshData();
        }

        public IApplicationBar ApplicationBar
        {
            get
            {
                var childAppBarController = ActiveItem as IAppBarController;
                if(childAppBarController != null)
                {
                    childAppBarController.AppBarChanged += AppBarChanged;
                    return childAppBarController.ApplicationBar;
                }

                return new ApplicationBar { IsVisible = false};
            }
        }

        public event EventHandler AppBarChanged = delegate { };
    }
}