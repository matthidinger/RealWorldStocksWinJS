(function () {
    "use strict";

    var ui = WinJS.UI;
    var appView = Windows.UI.ViewManagement.ApplicationView;
    var appViewState = Windows.UI.ViewManagement.ApplicationViewState;
    var watchList = RealWorldStocks.AppState.watchList;

    WinJS.UI.Pages.define("/pages/home/home.html", {

        ready: function (element, options) {

            watchList.refresh();

            //window.setInterval(function () {
            //    watchList.findSnapshot("MSFT").lastPrice = new Date().getSeconds();
            //}, 1000);

            document.getElementById("addStockButton").addEventListener("click", this.addStockToWatchList, false);
            document.getElementById("deleteStockButton").addEventListener("click", this.removeStockFromWatchList, false);

            this.updateLayout(element, appView.value);
        },


        // This function updates the page layout in response to viewState changes.
        updateLayout: function (element, viewState) {
            var listView = element.querySelector(".watchList").winControl;
            var layout = viewState === appViewState.snapped ? new ui.ListLayout() : new ui.GridLayout();

            ui.setOptions(listView, {
                itemDataSource: watchList.dataSource,
                layout: layout
            });
        },

        addStockToWatchList: function () {
            var symbol = document.getElementById("addStockSymbol").value;

            if (symbol && !watchList.findSnapshot(symbol)) {
                watchList.push(new RealWorldStocks.Models.StockSnapshot(symbol));
                watchList.refresh();
            }

            // Cleanup the UI
            document.getElementById("addStockSymbol").value = "";
            var flyout = document.getElementById("addStockFlyout").winControl;
            flyout.hide();
        },

        removeStockFromWatchList: function () {

        }

    });

})();
