using Microsoft.Phone.Controls;
using RealWorldStocks.Client.UI.Helpers;

namespace RealWorldStocks.Client.UI.Views.Home
{
    public partial class HomeView : PhoneApplicationPage
    {
        public HomeView()
        {
            InitializeComponent();
            AppBarHelper.BindAppBar(this);
        }
    }
}