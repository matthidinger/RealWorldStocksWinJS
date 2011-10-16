using Caliburn.Micro;

namespace RealWorldStocks.Client.UI.ViewModels.StockDetails
{
    public class StockDetailsViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private readonly INavigationService _navigation;

        public StockDetailsViewModel(INavigationService navigation)
        {
            _navigation = navigation;
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

        public string Company { get; set; }
    }
}