using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;
using RealWorldStocks.Core;
using RealWorldStocks.Core.Storage;

namespace RealWorldStocks.UI.Android
{
    public class AndroidPlatformAdapter : PlatformAdapter
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

        public override async void DelayInvoke(Action actionToInvoke, TimeSpan timeSpan)
        {
            await Task.Delay((int)timeSpan.TotalMilliseconds);
            BeginInvoke(actionToInvoke);
        }

        public override void BeginInvoke(Action actionToInvoke)
        {
            actionToInvoke();
            //Deployment.Current.Dispatcher.BeginInvoke(actionToInvoke);
        }


        private readonly ISettingsStore _fileStorage = new AndroidSettingsStore();
        public override ISettingsStore Settings
        {
            get { return _fileStorage; }
        }
    }
}
