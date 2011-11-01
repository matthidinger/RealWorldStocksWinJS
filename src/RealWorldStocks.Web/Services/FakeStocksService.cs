using System;
using System.Collections.Generic;
using System.Linq;
using RealWorldStocks.Client.Core.Models;

namespace RealWorldStocks.Web.Services
{
    public class FakeStocksService : IStocksService
    {
        public IEnumerable<StockSnapshot> GetSnapshots(string[] symbols)
        {
            var rand = new Random();
            var model = symbols.
                Select((symbol, index) => new StockSnapshot
                {
                    Symbol = symbol,
                    OpeningPrice = rand.Next(5, 600),
                    LastPrice = rand.Next(5, 600),
                    Volume = rand.Next(50000, 50000000),
                    Company = "Company " + index,
                    DaysChange = rand.Next(-25, 25),
                    DaysChangePercentFormatted = "1.45%",
                    PreviousClose = rand.Next(5, 600)
                })
                .ToList();

            return model;
        }
    }
}