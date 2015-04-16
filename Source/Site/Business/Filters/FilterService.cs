using System;
using System.Collections.Generic;
using System.Linq;
using Site.Business.Geo.City;
using Site.Business.HotelFacility;
using Site.Business.Search;

namespace Site.Business.Filters
{
    /// <summary>
    /// Filter service
    /// </summary>
    public class FilterService : IFilterService
    {
        private const int _numberPopularCities = 6;

        private readonly IFacilityService _facilityService;
        private readonly ISearchService _searchService;
        private readonly IPopulairCitiesService _popularCityService;
        private readonly IFilterBuilder _filterBuilder;

        /// <summary>
        /// Public constructor
        /// </summary>
        public FilterService()
        {
            _facilityService = new FacilityService();
            _searchService = new SearchService();
            _popularCityService = new PopulairCitiesService();
            _filterBuilder = new FilterBuilder();
        }

        /// <summary>
        /// Get facility filters
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IFilter> GetFacilityFilters()
        {
            return _facilityService.GetAll().Select(f => _filterBuilder.FromFacility(f));
        }

        /// <summary>
        /// Get city filters
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IFilter> GetCityFilters()
        {
            var cities = _popularCityService.GetCities();
            if (cities == null)
            {
                return Enumerable.Empty<IFilter>();
            }
            var facet = _searchService.GetPopulairCities(cities);

            var list = facet.Terms
                .OrderByDescending(f => f.Count)
                .Take(_numberPopularCities)
                .Select(
                    x =>
                        _filterBuilder.FromCity(
                            cities.FirstOrDefault(
                                c => c.Country.Equals(x.Term, StringComparison.InvariantCultureIgnoreCase)), x.Count));
            return list;
        }
    }
}