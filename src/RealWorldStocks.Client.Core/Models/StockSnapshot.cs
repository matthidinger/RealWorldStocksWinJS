namespace RealWorldStocks.Client.Core.Models
{
    public class StockSnapshot : NotifyObject
    {
        public StockSnapshot()
        {
            
        }

        public StockSnapshot(string symbol)
        {
            Symbol = symbol;
        }

        private string _symbol;
        public string Symbol
        {
            get { return _symbol; }
            set
            {
                _symbol = value;
                NotifyOfPropertyChange(() => Symbol);
            }
        }

        private string _company;
        public string Company
        {
            get { return _company; }
            set
            {
                _company = value;
                NotifyOfPropertyChange(() => Company);
            }
        }

        private decimal _openingPrice;
        public decimal OpeningPrice
        {
            get { return _openingPrice; }
            set
            {
                _openingPrice = value;
                NotifyOfPropertyChange(() => OpeningPrice);
            }
        }

        private decimal _lastPrice;
        public decimal LastPrice
        {
            get { return _lastPrice; }
            set
            {
                _lastPrice = value;
                NotifyOfPropertyChange(() => LastPrice);
            }
        }

        private decimal _daysChange;
        public decimal DaysChange
        {
            get { return _daysChange; }
            set
            {
                _daysChange = value;
                NotifyOfPropertyChange(() => DaysChange);
            }
        }

        private string _daysChangePercentFormatted;
        public string DaysChangePercentFormatted
        {
            get { return _daysChangePercentFormatted; }
            set
            {
                _daysChangePercentFormatted = value;
                NotifyOfPropertyChange(() => DaysChangePercentFormatted);
            }
        }

        private int _volume;
        public int Volume
        {
            get { return _volume; }
            set
            {
                _volume = value;
                NotifyOfPropertyChange(() => Volume);
            }
        }

        private decimal _previousClose;
        public decimal PreviousClose
        {
            get { return _previousClose; }
            set
            {
                _previousClose = value;
                NotifyOfPropertyChange(() => PreviousClose);
            }
        }


        public string DaysChangeFormatted
        {
            get { return DaysChange > 0 ? string.Format("+{0:0.00}", DaysChange) : DaysChange.ToString("0.00"); }
        }
    }
}