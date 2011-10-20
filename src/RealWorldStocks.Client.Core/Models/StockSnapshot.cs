namespace RealWorldStocks.Client.Core.Models
{
    public class StockSnapshot
    {
        public string Symbol { get; set; }

        public decimal OpeningPrice { get; set; }
        public decimal LastPrice { get; set; }
        public decimal DaysChange
        {
            get { return LastPrice - OpeningPrice; }
        }

        public decimal DaysChangePercent { get; set; }

        public decimal DaysRangeMin { get; set; }
        public decimal DaysRangeMax { get; set; }

        public int Volume { get; set; }
    }
}