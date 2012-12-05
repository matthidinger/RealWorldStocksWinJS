using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows;
using RealWorldStocks.Core;
using RealWorldStocks.Core.Storage;
using RealWorldStocks.UI.Phone.Storage;

namespace RealWorldStocks.UI.Phone
{
    public class PhonePlatformAdapter : PlatformAdapter
    {
        public override string ReadCompressedResponseStream(HttpWebResponse response)
        {
            string result;

            using (var sr = new StreamReader(response.GetCompressedResponseStream()))
            {
                result = sr.ReadToEnd();
            }

            return result;
        }

        public override void NavigateTo(string sourcePageName, object parameter = null, string query = null)
        {
            App.RootFrame.Navigate(new Uri(String.Format("/Views/{0}.xaml?{1}", sourcePageName, query), UriKind.Relative));
        }

        public override void NavigateBack()
        {
            App.RootFrame.GoBack();
        }

        public override async void DelayInvoke(Action actionToInvoke, TimeSpan timeSpan)
        {
            await Task.Delay((int)timeSpan.TotalMilliseconds);
            BeginInvoke(actionToInvoke);
        }

        public override void BeginInvoke(Action actionToInvoke)
        {
            Deployment.Current.Dispatcher.BeginInvoke(actionToInvoke);
        }


        private readonly ISettingsStore _fileStorage = new PhoneSettingsStore();
        public override ISettingsStore Settings
        {
            get { return _fileStorage; }
        }
    }
}
