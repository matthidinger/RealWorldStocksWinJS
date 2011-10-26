﻿using System;
using System.Collections.Generic;
using System.Linq;
using RealWorldStocks.Client.Core.Models;

namespace RealWorldStocks.Web.Services
{
    public class FakeNewsService : INewsService
    {
        public IEnumerable<News> GetNews(string[] symbols)
        {
            var model = symbols.
                Select((symbol, index) =>
                       new News
                           {
                               Title = "News " + index,
                               ArticleDate = DateTime.Now.AddDays(1 - index),
                               Url = "http://www.matthidinger.com"
                           });

            return model;
        }   
    }
}