using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;
using RealWorldStocks.Core;
using RealWorldStocks.Core.Storage;
using RealWorldStocks.UI.WinRT.Storage;

namespace RealWorldStocks.UI.WinRT
{
    public class WindowsPlatformAdapter : PlatformAdapter
    {
        public override string ReadCompressedResponseStream(HttpWebResponse response)
        {
            string result;

            using (var gzipStream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress))
            using (var sr = new StreamReader(gzipStream))
            {
                result = sr.ReadToEnd();
            }

            return result;
        }

        public override void NavigateTo(string sourcePageName, object parameter = null, string query = null)
        {
            var type = Type.GetType(String.Format("RealWorldStocks.UI.WinRT.Views.{0}", sourcePageName));

            if (parameter != null)
                App.RootFrame.Navigate(type, parameter);
            else
                App.RootFrame.Navigate(type, sourcePageName);
        }

        public override void NavigateBack()
        {
            App.RootFrame.GoBack();
        }

        public override async void DelayInvoke(Action actionToInvoke, TimeSpan timeSpan)
        {
            await Task.Delay(timeSpan);
            BeginInvoke(actionToInvoke);
        }

        public override void BeginInvoke(Action actionToInvoke)
        {
            App.RootFrame.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => actionToInvoke());
        }

        private readonly ISettingsStore _settings = new WindowsSettingsStore();

        public override ISettingsStore Settings
        {
            get { return _settings; }
        }
    }
}
