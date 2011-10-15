namespace RealWorldWPDev.Client.Data
{
    public static class CoroutineHelper
    {
        /// <summary>
        /// Turn the HttpGetRequest into an IResult for coroutine participation
        /// </summary>
        public static LoadDataResult<T> AsResult<T>(this HttpGetRequest<T> query)
        {
            return new LoadDataResult<T>(query);
        }
    }
}