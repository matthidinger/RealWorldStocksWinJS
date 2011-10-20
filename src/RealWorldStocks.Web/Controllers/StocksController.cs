using System;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using System.Net;
using System.Diagnostics;
using System.Globalization;

using RealWorldStocks.Client.Core.Models;
using RealWorldStocks.Web.Helpers;
using RealWorldStocks.Web.Extensions;
using System.Text.RegularExpressions;

namespace RealWorldStocks.Web.Controllers
{
    [AllowJsonGet]
    [NoCache]
    public class StocksController : Controller
    {
        public ActionResult GetSnapshots(string[] symbols)
        {
            var client = new WebClient();
            var yahooData = client.DownloadString(
                string.Format("http://finance.yahoo.com/d/quotes.csv?s={0}&f=snol1k3", String.Join("+", symbols))
                ).TrimEnd('\r', '\n');

            var model = from line in yahooData.Split(new string[] { Environment.NewLine }, StringSplitOptions.None)
                        let columns = line.SplitCSV().ToList()
                        select new StockSnapshot
                        {
                            Symbol = columns[0],
                            Company = columns[1],
                            OpeningPrice = decimal.Parse(columns[2], CultureInfo.InvariantCulture),
                            LastPrice = decimal.Parse(columns[3], CultureInfo.InvariantCulture),
                            Volume = int.Parse(columns[4].Replace(Environment.NewLine, string.Empty))
                        };

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
            return GetSnapshots(new[] { symbol });
        }
    }
}
