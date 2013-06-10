using System.Collections.ObjectModel;
using System.Threading.Tasks;
using RealWorldStocks.Core.Models;

#if NETFX_CORE
namespace RealWorldStocks.UI.WinRT.ViewModels
#else
namespace RealWorldStocks.UI.Phone.ViewModels
#endif
{
    public class HomeViewModel : NotifyObject
    {
        public HomeViewModel()
        {
            WatchListItems = new ObservableCollection<StockSnapshot>();
            News = new ObservableCollection<News>();
        }

        public ObservableCollection<StockSnapshot> WatchListItems { get; private set; }

        public ObservableCollection<News> News { get; private set; }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                NotifyOfPropertyChange(() => IsBusy);
            }
        }

        public async Task LoadAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            await WatchList.InitializeAsync();

            WatchListItems = await WatchList.Current.RefreshSnapshotsAsync();
            News = await WatchList.Current.RefreshNewsAsync();
            IsBusy = false;
        }
    }
}