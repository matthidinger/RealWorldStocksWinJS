using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using RealWorldStocks.Client.Core.Models;

namespace RealWorldStocks.Web.Services
{
    public class YahooStocksService : IStocksService
    {
        public IEnumerable<StockSnapshot> GetSnapshots(string[] symbols)
        {
            // TODO: Change parsing to TryParse, sometimes API returns N/A

            var client = new WebClient();
            var yahooData = client.DownloadString(
                string.Format("http://finance.yahoo.com/d/quotes.csv?s={0}&f=snol1vpc1p2", String.Join("+", symbols))
                ).TrimEnd('\r', '\n');

            var model = from line in yahooData.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                        let columns = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)").Select(s => s.Replace("\"", "")).ToList()
                        select new StockSnapshot
                        {
                            Symbol = columns[0],
                            Company = columns[1],
                            OpeningPrice = decimal.Parse(columns[2], CultureInfo.InvariantCulture),
                            LastPrice = decimal.Parse(columns[3], CultureInfo.InvariantCulture),
                            Volume = int.Parse(columns[4]),
                            PreviousClose = decimal.Parse(columns[5], CultureInfo.InvariantCulture),
                            DaysChange = decimal.Parse(columns[6], CultureInfo.InvariantCulture),
                            DaysChangePercentFormatted = columns[7]
                        };

            return model;
        }
    }
}