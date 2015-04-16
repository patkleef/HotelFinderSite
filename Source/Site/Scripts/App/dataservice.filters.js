define('dataservice.filters',
    [],
    function () {
        getAll = function (callback, query) {
            return $.get("/api/Filter/Get", { query: query }, callback);
        }
        return {
            getAll: getAll
        }
    });