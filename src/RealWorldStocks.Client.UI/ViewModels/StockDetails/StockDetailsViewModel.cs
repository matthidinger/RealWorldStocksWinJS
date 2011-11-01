using Caliburn.Micro;
using RealWorldStocks.Client.Core.Models;

namespace RealWorldStocks.Client.UI.ViewModels.StockDetails
{
    public class StockDetailsViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private readonly INavigationService _navigation;

        public StockDetailsViewModel(INavigationService navigation, InfoViewModel info)
        {
            _navigation = navigation;

            //info.Snapshot = Snapshot;
            //Items.Add(info);
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
    }
}