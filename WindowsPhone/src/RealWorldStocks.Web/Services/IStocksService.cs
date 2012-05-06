using System.Collections.Generic;
using RealWorldStocks.Client.Core.Models;

namespace RealWorldStocks.Web.Services
{
    public interface IStocksService
    {
        IEnumerable<StockSnapshot> GetSnapshots(string[] symbols);
    }
}