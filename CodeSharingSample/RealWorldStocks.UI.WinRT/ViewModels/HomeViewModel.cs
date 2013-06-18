using System.Collections.ObjectModel;
using System.Threading.Tasks;
using RealWorldStocks.Core;
using RealWorldStocks.Core.Models;
using RealWorldStocks.UI.WinRT.Models;

namespace RealWorldStocks.UI.WinRT.ViewModels
{
    public class HomeViewModel : Core.BaseViewModels.HomeViewModelBase
    {
        public ObservableCollection<Group> Groups { get; private set; }

        public HomeViewModel()
        {
            Groups = new ObservableCollection<Group>();
        }

        public override async Task LoadAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            await WatchList.InitializeAsync();
            
            WatchListItems.Repopulate(await WatchList.Current.RefreshSnapshotsAsync());
            News.Repopulate(await WatchList.Current.RefreshNewsAsync());

            Groups.Repopulate(new Group[]
            {
                new Group<StockSnapshot>("Watch List")
                {
                    Items = WatchListItems,
                },
                new Group<News>("News")
                {
                    Items = News
                },
            });

            IsBusy = false;
        }
    }
}