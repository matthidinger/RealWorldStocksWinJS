using System.Collections.Generic;

namespace RealWorldStocks.Client.Core.Models
{
    public class WatchList
    {
        public List<StockSnapshot> Snapshots { get; set; }
    }
}