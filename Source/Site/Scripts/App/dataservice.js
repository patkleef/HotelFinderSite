define('dataservice',
    [
        'dataservice.search',
        'dataservice.filters'
    ],
    function (search, filters) {
        return {
            search: search,
            filters: filters
        };
    });