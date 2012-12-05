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

        public async Task<ObservableCollection<StockSnapshot>> RefreshSnapshotsAsync()
        {
            var service = new YahooStocksService();
            var updated = await service.GetSnapshotsAsync(this.Select(m => m.Symbol).ToArray());
            return updated;
        }

        public Task<ObservableCollection<News>> RefreshNewsAsync()
        {
            var service = new YahooNewsService();
            return service.GetNewsAsync(this.Select(m => m.Symbol).ToArray());
        }
    }
}