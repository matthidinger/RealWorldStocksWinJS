using System.Globalization;

namespace RealWorldStocks.Client.Core.Models
{
    public class StockSnapshot
    {
        public string Symbol { get; set; }
        public string Company { get; set; }

        public decimal OpeningPrice { get; set; }
        public decimal LastPrice { get; set; }
        public decimal DaysChange
        {
            get { return LastPrice - OpeningPrice; }
        }

        public decimal DaysChangePercent
        {
            get { return LastPrice / OpeningPrice; }
        }

        public string DaysChangePercentFormatted
        {
            get
            {
                return DaysChange > 0 ? DaysChangePercent.ToString("P2") : string.Format("-{0:P2}", DaysChangePercent);
            }
        }

        public string DaysChangeFormatted
        {
            get { return DaysChange > 0 ? string.Format("+{0:00}", DaysChange) : DaysChange.ToString("00"); }
        }

        public decimal DaysRangeMin { get; set; }
        public decimal DaysRangeMax { get; set; }

        public int Volume { get; set; }
    }
}