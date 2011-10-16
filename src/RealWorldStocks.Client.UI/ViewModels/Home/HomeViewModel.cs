using Caliburn.Micro;
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


        public void BasicHttp()
        {
            _navigation.UriFor<BasicHttpViewModel>().Navigate();
        }
    }
}