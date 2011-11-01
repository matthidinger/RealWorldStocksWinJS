using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.ServiceModel.Syndication;
using RealWorldStocks.Client.Core.Models;

namespace RealWorldStocks.Web.Services
{
    public class YahooNewsService : INewsService
    {
        public IEnumerable<News> GetNews(string[] symbols, int utcOffset)
        {
            var url = string.Format("http://feeds.finance.yahoo.com/rss/2.0/headline?s={0}&region=US&lang=en-US", String.Join(",", symbols));
            var reader = XmlReader.Create(url, new XmlReaderSettings() { DtdProcessing = DtdProcessing.Parse });

            var formatter = new Rss20FeedFormatter();
            formatter.ReadFrom(reader);
            reader.Close();

            var model = formatter.Feed.Items.
                Select(item =>
                        new News
                        {
                            Title = item.Title.Text,
                            ArticleDate = item.PublishDate.ToOffset(TimeSpan.FromHours(utcOffset)).DateTime,
                            Url = item.Links[0].Uri.ToString(),
                        });

            return model;
        }
    }
}