using System;
using System.Diagnostics;
using System.Net;
using System.Runtime.Serialization;
using System.Threading;
using System.Windows;
using SharpGIS;

namespace RealWorldStocks.Client.Core.Web
{
    public static class HttpClient
    {
        public static TimeSpan Timeout = TimeSpan.FromSeconds(30);

        public static void BeginRequest<T>(HttpRequest<T> request, Action<HttpResponse<T>> callback)
        {
            BeginRequest(request.Url, callback);
        }

        public static void BeginRequest<T>(string url, Action<HttpResponse<T>> callback)
        {
            var client = new GZipWebClient();

            var timer = new Timer(state => client.CancelAsync(), null, Timeout, TimeSpan.FromMilliseconds(-1));
            
            Debug.WriteLine("HTTP Request: {0}", url);
            client.DownloadStringCompleted += (s, e) => ProcessResponse(callback, e);
            client.DownloadStringAsync(new Uri(url, UriKind.Absolute), timer);
        }

        private static void ProcessResponse<T>(Action<HttpResponse<T>> callback, DownloadStringCompletedEventArgs e)
        {
            var timer = (Timer) e.UserState;
            if (timer != null)
                timer.Dispose();
            
            try
            {
                if (e.Error == null)
                {
                    string json = e.Result.Replace("&amp;", "&");
                    Debug.WriteLine("HTTP Response: {0}\r\n", json);
                    var model = SerializationHelper.Deserialize<T>(json);

                    Deployment.Current.Dispatcher.BeginInvoke(() => callback(new HttpResponse<T>(model)));
                }
                else
                {
                    throw new WebException("Error getting the web service data", e.Error);
                }
            }
            catch (SerializationException ex)
            {
                var httpException = new HttpException("Unable to deserialize the model", ex);
                Debug.WriteLine(ex);
                Deployment.Current.Dispatcher.BeginInvoke(() => callback(new HttpResponse<T>(httpException)));
            }
            catch (WebException ex)
            {
                var httpException = new HttpException(ex);
                Debug.WriteLine(ex);
                Deployment.Current.Dispatcher.BeginInvoke(() => callback(new HttpResponse<T>(httpException)));
            }
        }
    }
}