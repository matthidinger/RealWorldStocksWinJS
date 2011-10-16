namespace RealWorldStocks.Client.Core.Data
{
    public static class CoroutineHelper
    {
        /// <summary>
        /// Turn the HttpRequest into an IResult for coroutine participation
        /// </summary>
        public static HttpRequestResult<T> Execute<T>(this HttpRequest<T> query)
        {
            return new HttpRequestResult<T>(query);
        }
    }
}