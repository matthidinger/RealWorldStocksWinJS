﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using RealWorldStocks.Core.Http;

namespace RealWorldStocks.Core.Models
{
    public class YahooNewsService
    {
        public async Task<ObservableCollection<News>> GetNewsAsync(string[] symbols)
        {
            var url = string.Format("http://feeds.finance.yahoo.com/rss/2.0/headline?s={0}&region=US&lang=en-US", String.Join(",", symbols));

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);

            var doc = XDocument.Parse(response.Content);

            var model = doc.Descendants("item")
                .Select(item =>
                        new News
                        {
                            Title = item.Element("title").Value,
                            ArticleDate = DateTime.Parse(item.Element("pubDate").Value),
                            Url = item.Element("link").Value,
                        });

            return new ObservableCollection<News>(model);
        }
    }
}