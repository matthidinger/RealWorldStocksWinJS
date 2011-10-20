using Caliburn.Micro;
using RealWorldStocks.Client.Core.Models;
using RealWorldStocks.Client.UI.ViewModels.StockDetails;

namespace RealWorldStocks.Client.UI.ViewModels.Home
{
    public class HomeViewModel : Conductor<IScreen>.Collection.OneActive, IRefreshable
    {
        private readonly INavigationService _navigation;

        public HomeViewModel(INavigationService navigation, HomeWatchListViewModel watchList, HomeNewsViewModel news, HomeQuoteViewModel quote)
        {
            _navigation = navigation;
            Items.Add(watchList);
            Items.Add(news);
            Items.Add(quote);
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
    }
}