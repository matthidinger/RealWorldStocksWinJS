using Caliburn.Micro;

namespace RealWorldWPDev.Client.ViewModels.Home
{
    public class HomeNewsViewModel : Screen
    {
        private readonly HomeViewModel _homeViewModel;

        public HomeNewsViewModel(HomeViewModel homeViewModel)
        {
            _homeViewModel = homeViewModel;
            DisplayName = "news";
        }
    }
}