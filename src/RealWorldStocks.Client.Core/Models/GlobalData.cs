using System.Collections.Generic;
using System.Device.Location;

namespace RealWorldStocks.Client.Core.Models
{
    public class SnapshotCache : Dictionary<string, StockSnapshot>
    {
        public StockSnapshot GetFromCache(string key)
        {
            if (ContainsKey(key))
                return this[key];

            return null;
        }
    }

    public class GlobalData : NotifyObject
    {
        private GlobalData()
        {
            
        }

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

        private SnapshotCache _cachedStops;
        public SnapshotCache Snapshots
        {
            get
            {
                if (_cachedStops == null)
                    _cachedStops = new SnapshotCache();

                return _cachedStops;
            }
        }
    }
}