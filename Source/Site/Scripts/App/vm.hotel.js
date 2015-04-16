define('vm.hotel',
    ['ko', 'jquery'],
    function (ko, $) {
        var initialize = function() {
            var self = this;
            
            function initMaps() {
                var element = $(document.getElementById('hotel-map'));
                var myLatlng = new google.maps.LatLng(element.data("latitude"), element.data("longitude"));
                var mapOptions = {
                    center: myLatlng,
                    zoom: 12
                };
                var map = new google.maps.Map(document.getElementById('hotel-map'),
                    mapOptions);

                var marker = new google.maps.Marker({
                    position: myLatlng,
                    map: map,
                    icon: {url: 'Images/map-hotel-marker.png', size: new google.maps.Size(50, 50)},
                    title: element.data("hotel-title")
                });
            }

            $(document).ready(function() {
                $(window).scroll(function() {
                    if ($(this).scrollTop() < 200) {
                        $(".hotel-book-footer").slideUp("fast");
                    } else {
                        $(".hotel-book-footer").slideDown("fast");
                    }
                });

                $('.jcarousel').jcarousel({
                    wrap: 'circular'
                }).jcarouselAutoscroll({
                    interval: 2000
                });

                initMaps();
            });

        };
        return {
            initialize: initialize
        }
    });