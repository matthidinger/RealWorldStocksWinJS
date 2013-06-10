using RealWorldStocks.Core.Models;

#if NETFX_CORE
namespace RealWorldStocks.UI.WinRT.ViewModels
#else
namespace RealWorldStocks.UI.Phone.ViewModels
#endif
{
    public class MainViewModel : NotifyObject
    {
        private HomeViewModel _home;
        public HomeViewModel Home
        {
            get { return _home ?? (_home = new HomeViewModel()); }
        }

    }
}