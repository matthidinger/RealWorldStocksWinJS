using System.Linq;
using System.Threading;
using System.Web.Mvc;
using RealWorldStocks.Web.Helpers;
using RealWorldStocks.Web.Services;

namespace RealWorldStocks.Web.Controllers
{
    [AllowJsonGet]
    [NoClientCache]
    [Compress]
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
            //Thread.Sleep(20000);
            var model = _stocksService.GetSnapshots(symbols);
            return Json(model);
        }

        public ActionResult GetNews(string[] symbols, int utcOffset)
        {
            //Thread.Sleep(2000);
            var model = _newsService.GetNews(symbols, utcOffset);
            return Json(model);
        }

        [OutputCache(Duration = 60)]
        public ActionResult GetSnapshot(string symbol)
        {
            return Json(_stocksService.GetSnapshots(new[] { symbol}).First());
        }
    }
}
