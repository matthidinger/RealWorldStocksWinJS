using System.Threading.Tasks;
using RealWorldStocks.Core;
using RealWorldStocks.Core.Models;

namespace RealWorldStocks.UI.Phone.ViewModels
{
    public class HomeViewModel : Core.BaseViewModels.HomeViewModelBase
    {
        public override async Task LoadAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            await WatchList.InitializeAsync();

            WatchListItems.Repopulate(await WatchList.Current.RefreshSnapshotsAsync());
            News.Repopulate(await WatchList.Current.RefreshNewsAsync());
            IsBusy = false;
        }
    }
}