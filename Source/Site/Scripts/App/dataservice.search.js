define('dataservice.search',
    [],
    function () {
        search = function (callback, query) {
            return $.get("/api/Search/Search", { query: query }, callback);
        }
        filter = function (callback, query, filterItemId) {
            return $.post("/api/Search/Search", { query: query, selectedFilterId: filterItemId }, callback);
        }
        return {
            search: search,
            filter: filter
        }
    });