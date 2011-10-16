using System;
using Caliburn.Micro;

namespace RealWorldStocks.Client.Data
{
    /// <summary>
    /// Caliburn.Micro IResult allowing HttpClient requests to participate in coroutines
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LoadDataResult<T> : IResult
    {
        private readonly HttpGetRequest<T> _request;

        public HttpResponse<T> Response { get; private set; }

        public LoadDataResult(HttpGetRequest<T> request)
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