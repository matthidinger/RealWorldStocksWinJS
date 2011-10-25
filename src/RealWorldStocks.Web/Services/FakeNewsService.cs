using System;
using System.Collections.Generic;
using System.Linq;
using RealWorldStocks.Client.Core.Models;

namespace RealWorldStocks.Web.Services
{
    public class FakeNewsService : INewsService
    {
        public List<News> GetNews(string[] symbols)
        {
            var model = symbols.
                Select((symbol, index) =>
                       new News
                           {
                               Title = "News " + index,
                               Summary = string.Join(" ", Enumerable.Repeat("Lorem ipsum", index + 1).ToArray()),
                               ArticleDate = DateTime.Now.AddDays(1 - index),
                               Url = "http://www.matthidinger.com"
                           })
                .ToList();

            return model;
        }
    }
}