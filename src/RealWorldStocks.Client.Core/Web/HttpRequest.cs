namespace RealWorldStocks.Client.Core.Web
{
    public class HttpRequest<TModel>
    {
        public HttpRequest(string url)
        {
            Url = url;
        }

        public string Url { get; private set; }
        public HttpResponse<TModel> Response { get; set; }
    }
}