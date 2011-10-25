using System.Linq;

namespace RealWorldStocks.Client.Core.Models
{
    public class WatchList : JsonSerializedCollection<WatchList, StockSnapshot>
    {
        public override string StorageFilename
        {
            get { return "WatchList.txt"; }
        }


        public StockSnapshot GetBySymbol(string symbol)
        {
            return this.SingleOrDefault(m => m.Symbol == symbol);
        }
    }
}