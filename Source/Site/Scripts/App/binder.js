define('binder',
    ['ko', 'vm', 'jquery'],
    function (ko, vm, $) {
        var bindSearchResultsViewModel = function () {
            ko.applyBindings(new vm.searchresults.initialize(), document.getElementById("search-results-page"));
        }
        var bindHomeViewModel = function () {
            ko.applyBindings(new vm.hotel.initialize(), document.getElementById("hotel-page"));
        }
        return {
            bindSearchResultsViewModel: bindSearchResultsViewModel,
            bindHomeViewModel: bindHomeViewModel
        };
    });