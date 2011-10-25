using System.Collections.Generic;
using RealWorldStocks.Client.Core.Models;

namespace RealWorldStocks.Web.Services
{
    public interface INewsService
    {
        List<News> GetNews(string[] symbols);
    }
}