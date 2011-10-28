using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Threading;

namespace RealWorldStocks.Client.Core.Data
{
    public static class HttpClient
    {
        public static void BeginGetRequest<T>(HttpRequest<T> request, Action<HttpResponse<T>> callback)
        {
            BeginGetRequest(request.Url, callback);
        }

        public static void BeginGetRequest<T>(string url, Action<HttpResponse<T>> callback)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            Debug.WriteLine("HTTP Request: {0}", request.RequestUri);
            try
            {
                request.BeginGetResponse(iar =>
                {
                    HttpWebResponse response = null;
                    try
                    {
                        response = (HttpWebResponse)request.EndGetResponse(iar);
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            if(response.Headers["compression"] != null)
                            {
                                // TODO: Decompress
                            }
                            //using (var gzip = new GZipInputStream(response.GetResponseStream()))
                            using (var reader = new StreamReader(response.GetResponseStream()))
                            {
                                string json = reader.ReadToEnd().Replace("&amp;", "&");
                                Debug.WriteLine("HTTP Response: {0}\r\n", json);
                                var model = SerializationHelper.Deserialize<T>(json);

                                Deployment.Current.Dispatcher.BeginInvoke(() => callback(new HttpResponse<T>(model)));
                            }
                        }
                        else
                        {
                            throw new WebException("Error getting the web service data");
                        }
                    }
                    catch (WebException ex)
                    {
                        var httpException = new HttpException(ex);
                        Debug.WriteLine(ex);
                        Deployment.Current.Dispatcher.BeginInvoke(() => callback(new HttpResponse<T>(httpException)));
                    }
                    finally
                    {
                        if (response != null) response.Close();
                    }
                }, null);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                Deployment.Current.Dispatcher.BeginInvoke(() => callback(new HttpResponse<T>(new HttpException())));
            }
        }
    }
}