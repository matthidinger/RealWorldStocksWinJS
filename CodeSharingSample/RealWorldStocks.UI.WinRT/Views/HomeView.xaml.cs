namespace RealWorldStocks.UI.WinRT.Views
{
    public sealed partial class HomeView
    {
        private bool _isNewPageInstance;

        public HomeView()
        {
            InitializeComponent();
            _isNewPageInstance = true;
        }

        protected override async void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            if (_isNewPageInstance)
            {
                DataContext = App.ViewModel.Home;
                await App.ViewModel.Home.LoadAsync();
            }

            _isNewPageInstance = false;
            base.OnNavigatedTo(e);

        }

        
        //protected override async void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        //{
        //    await WatchList.InitializeAsync();

        //    var groups = new ObservableCollection<Group>
        //    {
        //        new Group<StockSnapshot>("Watch List")
        //        {
        //            Items = await WatchList.Current.RefreshSnapshotsAsync()
        //        },
        //        new Group<News>("News")
        //        {
        //            Items = await WatchList.Current.RefreshNewsAsync()
        //        },
        //    };

        //    DefaultViewModel["Groups"] = groups;
        //}
    }
}
