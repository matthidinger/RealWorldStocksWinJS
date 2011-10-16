using System;
using Caliburn.Micro;

namespace RealWorldStocks.Client.Core.Data
{
    /// <summary>
    /// Caliburn.Micro IResult allowing HttpClient requests to participate in coroutines
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HttpRequestResult<T> : IResult
    {
        private readonly HttpRequest<T> _request;

        public HttpResponse<T> Response { get; private set; }

        public HttpRequestResult(HttpRequest<T> request)
        {
            _request = request;
        }

        public void Execute(ActionExecutionContext context)
        {
            HttpClient.BeginGetRequest(_request,
                response =>
                {
                    Response = response;
                    Completed(this, new ResultCompletionEventArgs());
                });
        }

        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };
    }

}