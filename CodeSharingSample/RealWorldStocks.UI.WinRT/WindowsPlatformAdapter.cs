using System;
using System.Threading.Tasks;
using RealWorldStocks.Core;
using RealWorldStocks.Core.Storage;
using RealWorldStocks.UI.Storage;

namespace RealWorldStocks.UI
{
    public class WindowsPlatformAdapter : PlatformAdapter
    {
        public override void NavigateTo(string sourcePageName, object parameter = null, string query = null)
        {
            var type = Type.GetType(String.Format("RealWorldStocks.UI.Views.{0}", sourcePageName));

            App.RootFrame.Navigate(type, parameter ?? sourcePageName);
        }

        public override void NavigateBack()
        {
            App.RootFrame.GoBack();
        }


        public override Task InvokeAsync(Action actionToInvoke)
        {
            return App.RootFrame.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => actionToInvoke()).AsTask();
        }

        private readonly ISettingsStore _settings = new WindowsSettingsStore();

        public override ISettingsStore Settings
        {
            get { return _settings; }
        }
    }
}
