using System;

namespace RealWorldStocks.Client.Core.Models
{
    public class News
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public DateTime ArticleDate { get; set; }
        public string Url { get; set; }
    }
}