(function () {
    "use strict";

    WinJS.Namespace.define("RealWorldStocks.Converters", {

        percent: WinJS.Binding.converter(function (num) {
            return num.toFixed(2).toString() + "%";
        }),

        daysChangeColor: WinJS.Binding.converter(function (daysChange) {
            return daysChange >= 0 ? "Green" : "Red";
        }),

        daysChangeArrow: WinJS.Binding.converter(function (daysChange) {
            return daysChange >= 0 ? "/images/greenArrow.svg" : "/images/redArrow.svg";
        })

    });
})();
