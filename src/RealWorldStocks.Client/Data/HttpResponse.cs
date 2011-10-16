namespace RealWorldStocks.Client.Data
{
    public class HttpResponse<T>
    {
        public HttpResponse(HttpGetException ex)
        {
            Error = ex;
        }

        public HttpResponse(T response)
        {
            Model = response;
        }

        public T Model { get; private set; }

        public HttpGetException Error { get; private set; }

        public bool HasError
        {
            get { return Error != null; }
        }
    }
}