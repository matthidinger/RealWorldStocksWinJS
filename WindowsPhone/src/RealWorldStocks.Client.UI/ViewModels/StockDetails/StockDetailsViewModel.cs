using Caliburn.Micro;
using RealWorldStocks.Client.Core.Models;

namespace RealWorldStocks.Client.UI.ViewModels.StockDetails
{
    public class StockDetailsViewModel : Conductor<IScreen>.Collection.OneActive, IRefreshable
    {
        private readonly INavigationService _navigation;
        private readonly InfoViewModel _info;
        private readonly TrendsViewModel _trends;
        private readonly NewsViewModel _news;

        public StockDetailsViewModel(INavigationService navigation, InfoViewModel info, TrendsViewModel trends, NewsViewModel news)
        {
            _navigation = navigation;
            _info = info;
            _trends = trends;
            _news = news;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            _info.Snapshot = Snapshot;
            _info.Symbol = Symbol;
            Items.Add(_info);

            _trends.Symbol = Symbol;
            Items.Add(_trends);
            Items.Add(_news);
        }

        private string _symbol;
        public string Symbol
        {
            get { return _symbol; }
            set
            {
                _symbol = value;
                NotifyOfPropertyChange(() => Symbol);
            }
        }

        public StockSnapshot Snapshot
        {
            get { return GlobalData.Current.Snapshots[Symbol]; }
        }

        public void RefreshData()
        {
            var refreshableChild = ActiveItem as IRefreshable;
            if(refreshableChild != null)
            {
                refreshableChild.RefreshData();
            }
        }
    }
}