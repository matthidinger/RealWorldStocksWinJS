﻿using System.Linq;
using System.Threading;
using System.Web.Mvc;
using RealWorldStocks.Web.Helpers;
using RealWorldStocks.Web.Services;

namespace RealWorldStocks.Web.Controllers
{
    [AllowJsonGet]
    [NoCache]
    public class StocksController : Controller
    {
        private readonly IStocksService _stocksService;
        private readonly INewsService _newsService;

        public StocksController()
        {
            _stocksService = new YahooStocksService();
            _newsService = new YahooFinanceNewsService();
        }

        public ActionResult GetSnapshots(string[] symbols)
        {
            //Thread.Sleep(3000);
            var model = _stocksService.GetSnapshots(symbols);
            return Json(model);
        }

        public ActionResult GetNews(string[] symbols)
        {
            //Thread.Sleep(2000);
            var model = _newsService.GetNews(symbols);
            return Json(model);
        }


        public ActionResult GetSnapshot(string symbol)
        {
            return GetSnapshots(new[] { symbol });
        }
    }
}
