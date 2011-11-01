using System.Collections.Generic;
using System.Linq;

namespace RealWorldStocks.Client.Core.Models
{
    public class WatchList : JsonSerializedCollection<WatchList, StockSnapshot>
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


            foreach (var snapshot in query)
            {
                snapshot.Orig.Company = snapshot.New.Company;
                snapshot.Orig.LastPrice = snapshot.New.LastPrice;
                snapshot.Orig.DaysChange = snapshot.New.DaysChange;
                snapshot.Orig.DaysChangePercentFormatted = snapshot.New.DaysChangePercentFormatted;
                snapshot.Orig.OpeningPrice = snapshot.New.OpeningPrice;
                snapshot.Orig.PreviousClose = snapshot.New.PreviousClose;
                snapshot.Orig.Volume = snapshot.New.Volume;
            }
        }
    }
}