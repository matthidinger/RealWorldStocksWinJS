using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using RealWorldStocks.Core.Http;
using RealWorldStocks.Core.Models;

namespace RealWorldStocks.UI.Phone
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            await WatchList.InitializeAsync();
            
            WatchListItems.ItemsSource = await WatchList.Current.RefreshSnapshotsAsync();

            NewsItems.ItemsSource = await WatchList.Current.RefreshNewsAsync();
        }
    }
}