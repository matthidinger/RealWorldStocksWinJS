namespace RealWorldStocks.Client.Core.Data
{
    public class HttpResponse<T>
    {
        public HttpResponse(HttpException ex)
        {
            Error = ex;
        }

        public HttpResponse(T response)
        {
            Model = response;
        }

        public T Model { get; private set; }

        public HttpException Error { get; private set; }

        public bool HasError
        {
            get { return Error != null; }
        }
    }
}