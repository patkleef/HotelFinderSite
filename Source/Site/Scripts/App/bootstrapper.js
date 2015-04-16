define('bootstrapper',
    ['binder', 'config'],
    function (binder, config) {
        var run = function () {
            if (config.page == "home") {
                binder.bindSearchResultsViewModel();
            }
            else if (config.page == "hotel") {
                binder.bindHomeViewModel();
            }
        };
        return {
            run: run
        };
    });