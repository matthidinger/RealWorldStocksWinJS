using System;
using System.Collections.Generic;
using System.Threading;
using Caliburn.Micro;
using Microsoft.Phone.Shell;
using RealWorldStocks.Client.Core.Data.Services;
using RealWorldStocks.Client.Core.Helpers;
using RealWorldStocks.Client.Core.Models;
using RealWorldStocks.Client.UI.Framework;
using RealWorldStocks.Client.UI.Helpers;

namespace RealWorldStocks.Client.UI.ViewModels.Home
{
    public class HomeNewsViewModel : Screen, IRefreshable, IAppBarController
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

        private IEnumerable<IResult> UpdateNews()
        {
            var request = _stocksWebService.GetNewsForWatchList().Execute();
            yield return request;


            // TODO: Handle errors, need to hide busy indicator on failure. Research best way to handle in CM
            if (!request.Response.HasError)
            {
                News.Repopulate(request.Response.Model);
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
                Coroutine.BeginExecute(UpdateNews().GetEnumerator());
            });
        }

        public void LoadNews(News news)
        {
             //TODO: Navigate to news page
        }



        public IApplicationBar ApplicationBar
        {
            get
            {
                var appBar = new ApplicationBar();
                appBar.Buttons.Add(AppBarHelper.RefreshButton);
                return appBar;
            }
        }

        public event EventHandler AppBarChanged = delegate { };
    }
}