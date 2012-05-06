(function () {
    "use strict";

    WinJS.Namespace.define("RealWorldStocks", {

        Services: WinJS.Class.define(null, {

            getQuotes: function (symbols) {
                /// <summary>
                /// Gets the most recent quotes for the symbols
                /// </summary>
                /// <param name="symbols" type="Array">
                /// The array of symbols 
                /// </param>

                var url = "http://finance.yahoo.com/d/quotes.csv?s=" + symbols.join() + "&f=snol1vpc1p2ghyra2j1abe8";

                return WinJS.xhr({
                    url: url
                }).then(function (response) {
                    var snapshots = [];

                    var data = RealWorldStocks.Utils.csvToArray(response.responseText);
                    data.forEach(function (item) {
                        var snapshot = RealWorldStocks.Models.StockSnapshot.parse(item);
                        snapshots.push(snapshot);
                    });

                    return snapshots;
                });
            },

        })

    });
})();
