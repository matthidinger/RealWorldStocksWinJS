using System;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using RealWorldStocks.Client.Core.Models;
using RealWorldStocks.Web.Helpers;

namespace RealWorldStocks.Web.Controllers
{
    [AllowJsonGet]
    [NoCache]
    public class StocksController : Controller
    {
        public ActionResult GetSnapshots(string[] symbols)
        {
            // Sleep here to highlight the Loading indicator in the app
            Thread.Sleep(2000);
            var rand = new Random();
            var model = symbols.
                Select((symbol, index) => new StockSnapshot
                                     {
                                         Symbol = symbol,
                                         Company = "Company " + index,
                                         OpeningPrice = rand.Next(5, 600),
                                         LastPrice = rand.Next(5, 600),
                                         Volume = rand.Next(50000, 50000000)
                                     })
                .ToList();
                            

            return Json(model);
        }

        public ActionResult GetNews(string[] symbols)
        {
            // Sleep here to highlight the Loading indicator in the app
            Thread.Sleep(2000);
            var model = symbols.
                Select((symbol, index) => new News
                {
                    Title = "News " + index,
                    Summary = string.Join(" ", Enumerable.Repeat("Lorem ipsum", index + 1).ToArray()),
                    ArticleDate = DateTime.Now.AddDays(1 - index),
                    Url = "http://www.matthidinger.com"
                })
                .ToList();


            return Json(model);
        }


        public ActionResult GetSnapshot(string symbol)
        {
            var rand = new Random();
            var snapshot = new StockSnapshot
                               {
                                   Symbol = symbol,
                                   OpeningPrice = rand.Next(5, 600),
                                   LastPrice = rand.Next(5, 600),
                                   Volume = rand.Next(50000, 50000000)
                               };

            return Json(snapshot);
        }
    }
}
