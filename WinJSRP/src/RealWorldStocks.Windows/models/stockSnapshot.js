(function () {
    "use strict";
    
    WinJS.Namespace.define("RealWorldStocks.Models", {

        StockSnapshot: WinJS.Class.define(function (symbol) {
            /// <param name="symbol" type="String">
            /// The symbol this snapshot is for
            /// </param>


            // All fields must be defined for automatic WinJS.Binding.as() support
            this.symbol = symbol;
            this.company = null;
            this.openingPrice = 0.0;
            this.lastPrice = 0.0;
            this.volume = 0;
            this.previousClose = 0.0;
            this.daysChange = 0.0;
            this.daysChangePercent = 0.0;
        }, {

        }, {
            parse: function (csv) {
                /// <param name="csv" type="Array">
                /// The array of CSV data
                /// </param>
                /// <returns type="RealWorldStocks.Models.StockSnapshot" />
                var snapshot = new RealWorldStocks.Models.StockSnapshot(csv[0]);
                snapshot.company = csv[1];
                snapshot.openingPrice = parseFloat(csv[2]);
                snapshot.lastPrice = parseFloat(csv[3]);
                snapshot.volume = parseInt(csv[4]);
                snapshot.previousClose = parseFloat(csv[5]);
                snapshot.daysChange = parseFloat(csv[6]);
                snapshot.daysChangePercent = parseFloat(csv[7]);
                return snapshot;
            }
        })
    });
})();