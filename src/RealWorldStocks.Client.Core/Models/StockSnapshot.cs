namespace RealWorldStocks.Client.Core.Models
{
    public class StockSnapshot
    {
        public string Symbol { get; set; }
        public string Company { get; set; }

        public decimal OpeningPrice { get; set; }
        public decimal LastPrice { get; set; }
        public decimal DaysChange { get; set; }

        public string DaysChangePercentFormatted { get; set; }

        public string DaysChangeFormatted
        {
            get { return DaysChange > 0 ? string.Format("+{0:0.00}", DaysChange) : DaysChange.ToString("0.00"); }
        }

        public decimal DaysRangeMin { get; set; }
        public decimal DaysRangeMax { get; set; }

        public int Volume { get; set; }

        public decimal PreviousClose { get; set; }
    }
}