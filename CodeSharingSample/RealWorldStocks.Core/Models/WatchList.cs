using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace RealWorldStocks.Core.Models
{
    public class WatchList : PersistentCollection<WatchList, StockSnapshot>
    {
        public override string StorageFilename
        {
            get { return "WatchList.txt"; }
        }

        protected override IEnumerable<StockSnapshot> DefaultItems
        {
            get
            {
                yield return new StockSnapshot("MSFT");
                yield return new StockSnapshot("NOK");
                yield return new StockSnapshot("AAPL");
                yield return new StockSnapshot("GOOG");
                yield return new StockSnapshot("AMZN");
                yield return new StockSnapshot("NFLX");
                yield return new StockSnapshot("T");
                yield return new StockSnapshot("S");
            }
        }


        public StockSnapshot GetBySymbol(string symbol)
        {
            return this.SingleOrDefault(m => m.Symbol == symbol);
        }

        public void UpdateSnapshots(IList<StockSnapshot> stockSnapshots)
        {

            var query = from s in this
                        join newS in stockSnapshots on s.Symbol equals newS.Symbol
                        select new
                                   {
                                       Orig = s,
                                       New = newS
                                   };

            // TODO: I don't like this... Must be a better way to update them

            foreach (var snapshot in query)
            {
                snapshot.Orig.Company = snapshot.New.Company;
                snapshot.Orig.LastPrice = snapshot.New.LastPrice;
                snapshot.Orig.DaysChange = snapshot.New.DaysChange;
                snapshot.Orig.DaysChangePercentFormatted = snapshot.New.DaysChangePercentFormatted;
                snapshot.Orig.OpeningPrice = snapshot.New.OpeningPrice;
                snapshot.Orig.PreviousClose = snapshot.New.PreviousClose;
                snapshot.Orig.LowPrice = snapshot.New.LowPrice;
                snapshot.Orig.HighPrice = snapshot.New.HighPrice;
                snapshot.Orig.DivAndYield = snapshot.New.DivAndYield;
                snapshot.Orig.PERatio = snapshot.New.PERatio;
                snapshot.Orig.AverageVolume = snapshot.New.AverageVolume;
                snapshot.Orig.Volume = snapshot.New.Volume;
                snapshot.Orig.MarketCap = snapshot.New.MarketCap;
                snapshot.Orig.Ask = snapshot.New.Ask;
                snapshot.Orig.Bid = snapshot.New.Bid;
                snapshot.Orig.OneYearEstimate = snapshot.New.OneYearEstimate;

            }
        }

        public async Task<ObservableCollection<StockSnapshot>> RefreshSnapshotsAsync()
        {
            var service = new YahooStocksService();
            var updated = await service.GetSnapshotsAsync(this.Select(m => m.Symbol).ToArray());
            UpdateSnapshots(updated);
            return updated;
        }

        public Task<ObservableCollection<News>> RefreshNewsAsync()
        {
            var service = new YahooNewsService();
            return service.GetNewsAsync(this.Select(m => m.Symbol).ToArray());
        }
    }
}