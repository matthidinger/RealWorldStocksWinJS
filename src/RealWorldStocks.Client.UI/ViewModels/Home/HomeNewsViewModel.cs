using System.Collections.Generic;
using System.Threading;
using Caliburn.Micro;
using RealWorldStocks.Client.Core.Data;
using RealWorldStocks.Client.Core.Data.Services;
using RealWorldStocks.Client.Core.Helpers;
using RealWorldStocks.Client.Core.Models;
using RealWorldStocks.Client.UI.Framework;

namespace RealWorldStocks.Client.UI.ViewModels.Home
{
    public class HomeNewsViewModel : Screen, IRefreshable
    {
        private readonly IStocksWebService _stocksWebService;

        public HomeNewsViewModel(IStocksWebService stocksWebService)
        {
            DisplayName = "news";
            _stocksWebService = stocksWebService;
            News = new BindableCollection<News>();
        }


        public BindableCollection<News> News { get; set; }

        protected override void OnInitialize()
        {
            RefreshData();
        }

        private IEnumerable<IResult> UpdateWatchList()
        {
            var request = _stocksWebService.GetNewsForWatchList().Execute();
            yield return request;

            // TODO: Handle errors, need to hide busy indicator on failure. Research best way to handle in CM
            if (!request.Response.HasError)
            {
                News.RepopulateObservableCollection(request.Response.Model);
            }
            else
            {

            }

            yield return BusyIndictator.HideResult();
        }

        public void RefreshData()
        {
            BusyIndictator.Show("Finding the latest news...");

            // TODO: Move this to OnViewReady in CM 1.3
            ThreadPool.QueueUserWorkItem(callback =>
            {
                Thread.Sleep(1000);
                Coroutine.BeginExecute(UpdateWatchList().GetEnumerator());
            });
        }

        public void LoadNews(News news)
        {
            // TODO: Navigate to news page
        }

    }
}