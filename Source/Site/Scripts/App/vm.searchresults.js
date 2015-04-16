define('vm.searchresults',
    ['ko', 'dataservice', 'jquery'],
    function (ko, dataservice, $) {
        var initialize = function() {
            var self = this;

            self.sortBy = ko.observable("price");
            self.searchQuery = ko.observable();
            self.searchDistanceQuery = ko.observable();
            self.results = ko.observableArray([]);
            self.showResults = ko.observable(false);
            self.currentPage = ko.observable(0);
            self.totalPages = ko.observable(0);
            self.autocompleteResults = ko.observableArray([]);
            self.showAutoCompleteResults = ko.observable(false);
            self.totalResults = ko.observable(0);
            self.facilityFilters = ko.observableArray([]);
            self.cityFilters = ko.observableArray([]);
            self.ratingFilter = ko.observable(0);

            self.canPerformSearchCall = true;

            self.searchButtonClicked = function () {
                performSearch(true, 1);
            };

            self.facilityFilterClicked = function(filter) {
                filter.selected(!filter.selected());
            }

            self.ratingFilterClicked = function(data, event) {
                var item = $(event.currentTarget).data("rating");
                if (item == self.ratingFilter()) {
                    self.ratingFilter(0);
                } else {
                    self.ratingFilter(item);
                }
            }

            self.sortResultsClicked = function (data, event) {
                if (self.searchQuery() != null && self.searchQuery().length > 0)
                {
                    var sortBy = $(event.currentTarget).data("sort");
                    self.sortBy(sortBy);
                    self.currentPage(1);
                
                    performSearch(false, 1);
                }
            };

            self.aboutLinkClicked = function(data, event) {
                $(".about-popup").fadeToggle("slow");
            }
            self.aboutCloseLinkClicked = function (data, event) {
                $(".about-popup").fadeToggle("slow");
            }

            self.advancedFiltersLinkClicked = function (data, event) {
                $(".advanced-filters-overlay").fadeToggle("slow");
            };

            self.applyFiltersClicked = function(data, event) {
                $(".advanced-filters-overlay").fadeToggle("slow");
            }

            self.closeFiltersClicked = function(data, event) {
                $(".advanced-filters-overlay").fadeToggle("slow");
            }

            self.cityFilterClicked = function (data, event) {
                self.searchQuery(this.title);

                performSearch(true, 1);
            };

            self.autocompleteItemClicked = function (data, event) {
                var item = $(event.currentTarget).data("value");
                self.searchQuery(item);
                self.showAutoCompleteResults(false);
                self.autocompleteResults([]);
            };

            self.searchKeyUp = function (data, event) {
                dataservice.search.search(initAutoCompleteResults, { isAutoComplete: true, query: $(event.currentTarget).val() });
            }

            self.showHotelResultItem = function (elem) {
                if ($(elem).hasClass("col-md-4") || $(elem).hasClass("col-xs-4")) {
                    $(elem).show("slow", function() {
                        var item = $(elem).find(".hotel-result-background-image");

                        var background = $(item).data("background");
                        $(item).css("backgroundImage", "url(" + background + ")");
                    });
                }
            }

            function performSearch(doAnimation, currentPage) {
                if (doAnimation) {
                    $("#bottom-search").hide("fast");

                    var height = "480px";
                    var marginTop = "0";
                    if ($(window).width() < 768) {
                        height = "240px";
                        marginTop = "10px";
                    }
                    $(".search-top-background").animate({
                        height: height,
                        minHeight: height
                    }, 500);

                    $(".search-container").animate({
                        marginTop: marginTop
                    }, 500);
                }

                self.currentPage(currentPage);
                dataservice.search.search(initResults,
                {
                    query: self.searchQuery(),
                    facilityFilter: getFacilityFiltersId(),
                    ratingFilter: self.ratingFilter(),
                    priceFrom: $("#price-slider").val()[0],
                    priceTo: $("#price-slider").val()[1],
                    sortBy: self.sortBy(),
                    currentPage: self.currentPage()
                });
            }

            function initFilters(data) {
                if (data.FacilityFilters.length > 0) {
                    $.each(data.FacilityFilters, function () {
                        self.facilityFilters.push(
                        {
                            id: this.Id,
                            title: this.Title,
                            icon: this.Icon,
                            selected: ko.observable(false)
                        });
                    });
                }
                if (data.CityFilters.length > 0) {
                    $.each(data.CityFilters, function () {
                        self.cityFilters.push(
                            {
                                title: this.Title,
                                latitude: this.Coordinates.Latitude,
                                longitude: this.Coordinates.Longitude,
                                icon: this.Icon,
                                count: this.Count,
                                background: this.Background
                            });
                    });
                }
                startBackgroundImageSlideshow();
            }

            function getFacilityFiltersId() {
                var arr = [];
                $.each(self.facilityFilters(), function() {
                    if (this.selected()) {
                        arr.push(this.id);
                    }
                });
                return arr;
            }

            function initAutoCompleteResults(data) {
                if(data.length > 0) {
                    self.showAutoCompleteResults(true);
                    self.autocompleteResults(data);
                }
            }

            function initResults(data) {
                if (self.currentPage() == 1) {
                    self.results.removeAll();
                    self.results(data.Results);
                    $(".hotel-results-container .row").css({ 'height': 'auto' });
                } else {
                    $.each(data.Results, function() {
                        self.results.push(this);
                    });
                }
                
                self.totalResults(data.TotalResults);
                self.currentPage(data.CurrentPage);
                self.totalPages(data.TotalPages);

                self.showAutoCompleteResults(false);
                self.autocompleteResults([]);

                self.showResults(true);
                self.canPerformSearchCall = true;
            }

            function startBackgroundImageSlideshow() {
                var el = $('.search-top-background');

                var numberOfBgs = $(".bottom-search .row div.col-md-2:visible").length;
                var bg = [];
                $.each(self.cityFilters(), function(index) {
                    if ((index + 1) <= numberOfBgs) {
                        bg.push("../Images/City-backgrounds/" + this.background);
                    }
                });

                var index = 1;
                var maxIndex = bg.length;
                setInterval(function () {
                    if (index == maxIndex) {
                        index = 0;
                    }
                    el.css('background-image', 'url(' + bg[index] + ')');

                    index++;
                }, 8000);
            }

            function ratingHoverEffect() {
                $(".filter-rating-icons i").hover(function () {
                    var rating = $(this).data("rating");
                    $.each($(this).parent().find("i"), function (index) {
                        if ((index + 1) <= rating) {
                            $(this).addClass("filter-rating-hover-selected");
                        }
                    });
                }, function () {
                    var rating = $(this).data("rating");
                    $.each($(this).parent().find("i"), function (index) {
                        if ((index + 1) <= rating) {
                            $(this).removeClass("filter-rating-hover-selected");
                        }
                    });
                });
            }

            function initPriceSlider() {
                $('#price-slider').noUiSlider({
                    start: [0, 3000],
                    step: 1,
                    connect: true,
                    range: {
                        'min': 0,
                        'max': 3000
                    }
                });
                $("#price-slider").Link('lower').to('-inline-<div class="price-slider-tooltip"></div>', function (value) {

                    // The tooltip HTML is 'this', so additional
                    // markup can be inserted here.
                    $(this).html('<span>$ ' + parseInt(value) + '</span>');
                });
                $("#price-slider").Link('upper').to('-inline-<div class="price-slider-tooltip"></div>', function (value) {

                    // The tooltip HTML is 'this', so additional
                    // markup can be inserted here.
                    $(this).html('<span>$ ' + parseInt(value) + '</span>');
                });
            }

            $(window).scroll(function (event) {
                if (self.showResults() && self.canPerformSearchCall) {
                    if ($(window).scrollTop() + $(window).height() == $(document).height()) {
                    //if ($(window).scrollTop() + $(window).height() > $(document).height() - 100) {
                        $(".hotel-results-container .row").css({ 'height': 'auto' });
                        self.currentPage(self.currentPage() + 1);
                        if (self.currentPage() < self.totalPages()) {
                            var height = $(".hotel-results-container .row").height();
                            $(".hotel-results-container .row").height(height + 200);

                            self.canPerformSearchCall = false;
                            setTimeout(function () {
                                performSearch(false, self.currentPage());
                            }, 500);
                        }
                    }
                }
            });

            $(document).click(function (event) {
                if (!$(event.currentTarget).hasClass("autocomplete-item")) {
                    self.autocompleteResults([]);
                    self.showAutoCompleteResults(false);
                }
            });

            dataservice.filters.getAll(initFilters, {});
            ratingHoverEffect();
            initPriceSlider();
        };
        return {
            initialize: initialize
        }
    });