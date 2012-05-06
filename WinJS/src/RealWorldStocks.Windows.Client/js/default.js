// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232509
(function () {
    "use strict";

    var app = WinJS.Application;

    app.onactivated = function (eventObject) {
        if (eventObject.detail.kind === Windows.ApplicationModel.Activation.ActivationKind.launch) {
            if (eventObject.detail.previousExecutionState !== Windows.ApplicationModel.Activation.ApplicationExecutionState.terminated) {
                // TODO: This application has been newly launched. Initialize 
                // your application here.

                var splash = eventObject.detail.splashScreen;

                // Retrieve the window coordinates of the splash screen image.
                var coordinates = splash.imageLocation;

                // Position the extended splash screen image in the same location as the splash screen image.
                //document.getElementById("extendedSplashImage").style.left = coordinates.x + "px";
                //document.getElementById("extendedSplashImage").style.top = coordinates.y + "px";
                //document.getElementById("extendedSplashImage").style.height = coordinates.height + "px";
                //document.getElementById("extendedSplashImage").style.width = coordinates.width + "px";

                // Register an event handler to be executed when the splash screen has been dismissed.
                //splash.addEventListener("dismissed", loadInitialScreen, false);
                loadInitialScreen();

           
            } else {
                // TODO: This application has been reactivated from suspension. 
                // Restore application state here.
            }
        }
    };

    function loadInitialScreen() {

        RealWorldStocks.AppState.restore().then(function () {
            RealWorldStocks.navigateHome();
            WinJS.UI.processAll();
        });
    }


    app.oncheckpoint = function (eventObject) {
        // TODO: This application is about to be suspended. Save any state
        // that needs to persist across suspensions here. You might use the 
        // WinJS.Application.sessionState object, which is automatically
        // saved and restored across suspension. If you need to complete an
        // asynchronous operation before your application is suspended, call
        eventObject.setPromise(RealWorldStocks.AppState.persist());



    };

    app.start();
})();
