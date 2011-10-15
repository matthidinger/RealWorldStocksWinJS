using System;
using System.Linq;
using System.Web.Mvc;
using RealWorldWPDev.Client.Models;

namespace RealWorldWPDev.Web.Controllers
{
    public class StocksController : Controller
    {
        public ActionResult GetSnapshots(string[] symbols)
        {
            //Thread.Sleep(5000);
            var rand = new Random();
            var model = symbols.
                Select(symbol => new StockSnapshot
                                     {
                                         Symbol = symbol,
                                         OpeningPrice = rand.Next(5, 600),
                                         LastPrice = rand.Next(5, 600),
                                         Volume = rand.Next(50000, 50000000)
                                     })
                .ToList();
                            

            return Json(model, JsonRequestBehavior.AllowGet);
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

            return Json(snapshot, JsonRequestBehavior.AllowGet);
        }
    }
}
