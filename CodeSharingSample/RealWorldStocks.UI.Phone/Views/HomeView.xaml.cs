using System.Windows.Navigation;
using Microsoft.Phone.Controls;
namespace RealWorldStocks.UI.Phone.Views
{
    public partial class HomeView : PhoneApplicationPage
    {
        private bool _isNewPageInstance;

        public HomeView()
        {
            InitializeComponent();
            _isNewPageInstance = true;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (_isNewPageInstance)
            {
                DataContext = App.ViewModel.Home;
                await App.ViewModel.Home.LoadAsync();
            }

            _isNewPageInstance = false;
        }

        //private void Refresh()
        //{
        //    App.ViewModel.Home.Refresh();
        //}
    }
}