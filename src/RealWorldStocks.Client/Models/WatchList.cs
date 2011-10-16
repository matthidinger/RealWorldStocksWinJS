using System.Collections.Generic;

namespace RealWorldStocks.Client.Models
{
    public class WatchList
    {
        public List<StockSnapshot> Snapshots { get; set; }
    }
}