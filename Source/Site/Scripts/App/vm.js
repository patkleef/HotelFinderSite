define('vm',
    ['vm.searchresults', 'vm.hotel'],
    function (searchresults, hotel) {
        return {
            searchresults: searchresults,
            hotel: hotel
        };
    });