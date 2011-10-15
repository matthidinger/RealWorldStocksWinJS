using Caliburn.Micro;
using RealWorldWPDev.Client.Models;
using RealWorldWPDev.Client.ViewModels.StockDetails;

namespace RealWorldWPDev.Client.ViewModels.Home
{
    public class HomeViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private readonly INavigationService _navigation;

        private readonly HomeWatchListViewModel _watchList;
        private readonly HomeNewsViewModel _news;

        public HomeViewModel(INavigationService navigation)
        {
            _navigation = navigation;

            _watchList = new HomeWatchListViewModel(this);
            _watchList.ActivateWith(this);

            _news = new HomeNewsViewModel(this);
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