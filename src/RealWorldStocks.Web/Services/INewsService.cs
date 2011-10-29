using System.Collections.Generic;
using RealWorldStocks.Client.Core.Models;

namespace RealWorldStocks.Web.Services
{
    public interface INewsService
    {
        IEnumerable<News> GetNews(string[] symbols, int gmtOffset);
    }
}