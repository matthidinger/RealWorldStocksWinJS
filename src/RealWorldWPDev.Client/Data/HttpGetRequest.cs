namespace RealWorldWPDev.Client.Data
{
    public class HttpGetRequest<TModel>
    {
        public HttpGetRequest(string url)
        {
            Url = url;
        }

        public string Url { get; private set; }
        public HttpResponse<TModel> Response { get; set; }
    }
}