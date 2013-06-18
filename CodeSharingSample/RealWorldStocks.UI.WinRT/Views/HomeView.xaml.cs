namespace RealWorldStocks.UI.WinRT.Views
{
    public sealed partial class HomeView
    {
        public HomeView()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            DataContext = App.ViewModel.Home;
            await App.ViewModel.Home.LoadAsync();
        }
    }
}
