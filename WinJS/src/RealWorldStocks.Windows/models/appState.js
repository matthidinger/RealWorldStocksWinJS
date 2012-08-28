(function () {
    "use strict";
    var services = new RealWorldStocks.Services();
    var localSettings = Windows.Storage.ApplicationData.current.roamingSettings;

    var defaults = [
        new RealWorldStocks.Models.StockSnapshot("MSFT"),
        new RealWorldStocks.Models.StockSnapshot("AAPL"),
        new RealWorldStocks.Models.StockSnapshot("GOOG"),
        new RealWorldStocks.Models.StockSnapshot("NOK"),
        new RealWorldStocks.Models.StockSnapshot("T"),
        new RealWorldStocks.Models.StockSnapshot("F"),
        new RealWorldStocks.Models.StockSnapshot("GE"),
        new RealWorldStocks.Models.StockSnapshot("XOM"),
        new RealWorldStocks.Models.StockSnapshot("NFLX")
    ];

    var watchList = new WinJS.Binding.List([], {
        binding: true
    });

    watchList.findSnapshot = function (symbol) {
        /// <returns type="RealWorldStocks.Models.StockSnapshot" />
        var found = this.filter(function (item) { return item.symbol === symbol; });
        if (found.length > 0) {
            return found[0];
        }
        return null;
    };

    watchList.refresh = function () {
        var that = this;
        var symbols = this.map(function (item) {
            return item.symbol;
        });

        services.getQuotes(symbols).then(function (snapshots) {
            snapshots.forEach(function (snapshot) {
                RealWorldStocks.Utils.extend(that.findSnapshot(snapshot.symbol), snapshot);
            });
        });
        
    };

    WinJS.Namespace.define("RealWorldStocks.AppState", {

        watchList: watchList,

        persist: function () {
            return new WinJS.Promise(function (complete) {

                var raw = [];
                watchList.forEach(function (item) {
                    raw.push(item.backingData);
                });
                
                localSettings.values["watchList"] = JSON.stringify(raw);
                complete();
            });

        },


        restore: function () {
            return new WinJS.Promise(function (complete) {

                var stored = localSettings.values["watchList"];
                if (stored) {
                    var raw = JSON.parse(stored);
                    raw.forEach(function (item) {
                        watchList.push(item);
                    });

                } else {
                    defaults.forEach(function (item) {
                        watchList.push(item);
                    });
                }

                complete();
            });

        },


    });

})();
 