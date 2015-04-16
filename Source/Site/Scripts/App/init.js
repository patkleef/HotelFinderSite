(function() {
    var root = this;

    requirejs.config(
    {
        baseUrl: '/Scripts/App/' /* script default location */
    });

    function define3rdPartyModules() {
        // These are already loaded via bundles. 
        // We define them and put them in the root object.
        define('jquery', [], function () { return root.jQuery; });
        define('ko', [], function () { return root.ko; });
    }

    function boot() {
        require(
            ['bootstrapper'],
            function (bs) {
                bs.run();
            });
    }

    define3rdPartyModules();
    boot();
})();