using System;
using System.IO;
using System.Net;
using Caliburn.Micro;

namespace RealWorldStocks.Client.Data
{
    public static class HttpClient
    {
        private static readonly ILog Log = LogManager.GetLog(typeof(HttpClient));

        public static void BeginGetRequest<T>(HttpGetRequest<T> request, Action<HttpResponse<T>> callback)
        {
            BeginGetRequest(request.Url, callback);
        }

        public static void BeginGetRequest<T>(string url, Action<HttpResponse<T>> callback)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            Log.Info("Beginning HTTP Request: {0}", request.RequestUri);
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
                                Log.Info("Received HTTP Response: {0}\r\n", json);
                                var model = SerializationHelper.Deserialize<T>(json);

                                Execute.OnUIThread(() => callback(new HttpResponse<T>(model)));
                            }
                        }
                        else
                        {
                            throw new WebException("Error getting the web service data");
                        }
                    }
                    catch (WebException ex)
                    {
                        var httpException = new HttpGetException(ex);
                        Log.Error(ex);
                        Execute.OnUIThread(() => callback(new HttpResponse<T>(httpException)));
                    }
                    finally
                    {
                        if (response != null) response.Close();
                    }
                }, null);
            }
            catch(Exception ex)
            {
                Log.Error(ex);
                Execute.OnUIThread(() => callback(new HttpResponse<T>(new HttpGetException())));
            }
        }
    }
}