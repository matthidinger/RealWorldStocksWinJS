using System.Windows.Navigation;

namespace RealWorldStocks.UI.Phone.Views
{
    public partial class HomeView
    {
        public HomeView()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            DataContext = App.ViewModel.Home;
            await App.ViewModel.Home.LoadAsync();
        }
    }
}