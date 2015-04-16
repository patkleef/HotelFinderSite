using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer.Find;
using EPiServer.Find.Api.Facets;
using EPiServer.Sample.Hotels;
using Site.Business.Geo.City;
using Site.Business.HotelFacility;
using Site.Business.Search.Result;

namespace Site.Business.Search
{
    /// <summary>
    /// Search service class
    /// </summary>
    public class SearchService : ISearchService
    {
        private const int _pageSize = 9;
        private const int _maxAutocompleteResults = 4;

        private readonly IClient _client;
        private readonly ISearchResultItemBuilder _searchResultItemBuilder;
        private readonly IFacilityService _facilityService;
        private readonly IPopulairCitiesService _popularCitiesService;
        
        /// <summary>
        /// Public constructor
        /// </summary>
        public SearchService()
        {
            _client = HotelHelpers.HotelClient;
            _searchResultItemBuilder = new SearchResultItemBuilder();
            _facilityService = new FacilityService();
            _popularCitiesService = new PopulairCitiesService();
        }

        /// <summary>
        /// Get default hotel from search
        /// </summary>
        /// <returns></returns>
        public Hotel GetDefaultHotel()
        {
            var items = _client.Search<Hotel>();
            return items.GetResult().First();
        }

        /// <summary>
        /// Get hotel by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Hotel GetHotelById(int id)
        {
            var items = _client.Search<Hotel>().Filter(h => h.Id.Match(id));
            return items.GetResult().First();
        }

        /// <summary>
        /// Get hotels in area, use Geo search
        /// </summary>
        /// <param name="hotel"></param>
        /// <param name="kilometers"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public IEnumerable<SearchResultItem> GetHotelsInArea(Hotel hotel, int kilometers, int number)
        {
            var search = _client.Search<Hotel>()
                            .Filter(h => h.GeoCoordinates.WithinDistanceFrom(hotel.GeoCoordinates, kilometers.Kilometers()));

            return search.Take(number).GetResult().Select(_searchResultItemBuilder.FromHotel);
        }

        /// <summary>
        /// Get populair cities, use Geo search
        /// </summary>
        /// <param name="cities"></param>
        /// <returns></returns>
        public TermsFacet GetPopulairCities(IEnumerable<City> cities)
        {
            if (cities == null)
            {
                return new TermsFacet();
            }

            var search = _client.Search<Hotel>();

            var cityList = cities.ToList();
            foreach (var city in cityList)
            {
                search = search.OrFilter(h => h.GeoCoordinates.WithinDistanceFrom(
                new GeoLocation(city.Coordinates.Latitude, city.Coordinates.Longitude), 20.Kilometers()));
            }

            search = search.TermsFacetFor(q => q.Location.Country.ISO2);

            var results = search.GetResult();

            return (TermsFacet)results.Facets["Location.Country.ISO2"];
        }

        /// <summary>
        /// Search autocomplete
        /// First filter the LocationsString after that return the item from the Location array
        /// We would like to search on country, city etc.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<SearchResultItem> SearchAutoComplete(SearchQuery query)
        {
            if (string.IsNullOrEmpty(query.Query))
            {
                return Enumerable.Empty<SearchResultItem>();
            }

            var search = _client.Search<Hotel>().Filter(x => x.LocationsString.AnyWordBeginsWith(query.Query));

            var results = search.GetResult()
                        .Select(h => h.Locations.FirstOrDefault(x => x.IndexOf(query.Query, StringComparison.InvariantCultureIgnoreCase) != -1))
                        .Distinct()
                        .Take(_maxAutocompleteResults);

            return results.Select(q => new SearchResultItem { Title = q });
        }

        /// <summary>
        /// Search and filter on price, facilities, rating and city when selected.
        /// Default order by price
        /// Use paging functionality for the infinite scroll
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public SearchResult Search(SearchQuery query)
        {
            var search = _client.Search<Hotel>();
            if (!IsCityFilterActive(query)) // If city filter is active don't perform a search
            {
                search = search.For(query.Query);
            }

            search = GetFilters(search, query);
            search = GetOrderBy(search, query);

            var page = query.CurrentPage == 0 ? 1 : query.CurrentPage;

            var result = search.Take(_pageSize).Skip((page - 1) * 10).GetResult();

            var searchResult = new SearchResult();
            searchResult.TotalResults = result.TotalMatching;

            var totalPages = result.TotalMatching / _pageSize;
            if (result.TotalMatching % _pageSize > 0)
            {
                totalPages++;
            }

            searchResult.CurrentPage = page;
            searchResult.TotalPages = totalPages;
            searchResult.Results = result.Select(_searchResultItemBuilder.FromHotel);

            return searchResult;
        }

        /// <summary>
        /// Get filters
        /// </summary>
        /// <param name="search"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        private ITypeSearch<Hotel> GetFilters(ITypeSearch<Hotel> search, SearchQuery query)
        {
            search = GetPriceFilter(search, query);
            search = GetRatingFilter(search, query);
            search = GetFacilityFilter(search, query);
            search = GetCityFilter(search, query);

            return search;
        }

        /// <summary>
        /// Filter on price
        /// </summary>
        /// <param name="search"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        private ITypeSearch<Hotel> GetPriceFilter(ITypeSearch<Hotel> search, SearchQuery query)
        {
            if (query.PriceTo > 0)
            {
                search = search.Filter(x => x.PriceUSD.InRange((int)query.PriceFrom, (int)query.PriceTo));
            }
            return search;
        }

        /// <summary>
        /// Filter on rating
        /// </summary>
        /// <param name="search"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        private ITypeSearch<Hotel> GetRatingFilter(ITypeSearch<Hotel> search, SearchQuery query)
        {
            if (query.RatingFilter > 0)
            {
                search = search.Filter(x => x.StarRating.Match(query.RatingFilter));
            }
            return search;
        }

        /// <summary>
        /// Use this to filter on the Features list. This Filter use the Match method for the filtering for each selected facility
        /// </summary>
        /// <param name="search"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        private ITypeSearch<Hotel> GetFacilityFilter(ITypeSearch<Hotel> search, SearchQuery query)
        {
            if (query.FacilityFilter != null)
            {
                foreach (var item in query.FacilityFilter)
                {
                    var facility = _facilityService.GetById(item);
                    if (facility != null)
                    {
                        search = search.Filter(x => x.Features.Match(facility.Value));
                    }
                }
            }
            return search;
        }
        
        /// <summary>
        /// Use this to filter for only specific cities. Unfortunately there isn't a city property available. So when searching
        /// for hotels in particular city Geo search is used. For this example site a range of 20 kilometer is used. 
        /// </summary>
        /// <param name="search"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        private ITypeSearch<Hotel> GetCityFilter(ITypeSearch<Hotel> search, SearchQuery query)
        {
            var cities =_popularCitiesService.GetCities();

            var city = cities.FirstOrDefault(c => c.Title.Equals(query.Query, StringComparison.InvariantCultureIgnoreCase));
            if (city != null)
            {
                search = search.Filter(h => h.GeoCoordinates.WithinDistanceFrom(
                new GeoLocation(city.Coordinates.Latitude, city.Coordinates.Longitude), 20.Kilometers()));
            }
            return search;
        }

        /// <summary>
        /// Is city filter active
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private bool IsCityFilterActive(SearchQuery query)
        {
            var cities = _popularCitiesService.GetCities();

            var city = cities.FirstOrDefault(c => c.Title.Equals(query.Query, StringComparison.InvariantCultureIgnoreCase));

            return city != null;
        }

        /// <summary>
        /// Get order by 
        /// </summary>
        /// <param name="search"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        private ITypeSearch<Hotel> GetOrderBy(ITypeSearch<Hotel> search, SearchQuery query)
        {
            switch (query.SortBy)
            {
                case "price":
                    return search.OrderBy(h => h.PriceUSD);
                case "ratings":
                    return search.OrderByDescending(h => h.StarRating);
                case "Popularity":
                    return search.OrderByDescending(h => h.StarRating);
                default:
                    return search.OrderByDescending(h => h.PriceUSD);
            }
        }
    }
}