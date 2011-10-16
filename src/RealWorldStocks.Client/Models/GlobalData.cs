using System.Device.Location;
using Caliburn.Micro;

namespace RealWorldStocks.Client.Models
{
    public class GlobalData : PropertyChangedBase
    {
        public bool B { get; set; }
        private static GlobalData _current;
        public static GlobalData Current
        {
            get
            {
                if (_current == null)
                    _current = new GlobalData();

                return _current;
            }
            set { _current = value; }
        }

        private GeoCoordinate _myLocation;
        public GeoCoordinate MyLocation
        {
            get
            {
                if (_myLocation == null)
                    _myLocation = new GeoCoordinate();

                return _myLocation;
            }
            set
            {
                if (value == null)
                    return;

                _myLocation = value;
                NotifyOfPropertyChange(() => MyLocation);
            }
        }
    }
}