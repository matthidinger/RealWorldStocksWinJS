using System;
using System.Threading.Tasks;
using System.Windows;
using RealWorldStocks.Core;
using RealWorldStocks.Core.Storage;
using RealWorldStocks.UI.Storage;

namespace RealWorldStocks.UI
{
    public class PhonePlatformAdapter : PlatformAdapter
    {
        public override void NavigateTo(string sourcePageName, object parameter = null, string query = null)
        {
            App.RootFrame.Navigate(new Uri(String.Format("/Views/{0}.xaml?{1}", sourcePageName, query), UriKind.Relative));
        }

        public override void NavigateBack()
        {
            App.RootFrame.GoBack();
        }

        public override Task InvokeAsync(Action actionToInvoke)
        {
            var tcs = new TaskCompletionSource<object>();
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                try
                {
                    actionToInvoke();
                    tcs.SetResult(null);
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }
    
            });
            return tcs.Task;
        }

        private readonly ISettingsStore _fileStorage = new PhoneSettingsStore();

        public override ISettingsStore Settings
        {
            get { return _fileStorage; }
        }
    }
}
