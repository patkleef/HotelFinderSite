using System.Collections.Generic;
using EPiServer.Find.Api.Facets;
using EPiServer.Sample.Hotels;
using Site.Business.Geo.City;
using Site.Business.Search.Result;

namespace Site.Business.Search
{
    public interface ISearchService
    {
        /// <summary>
        /// Get default hotel from search
        /// </summary>
        /// <returns></returns>
        Hotel GetDefaultHotel();

        /// <summary>
        /// Get hotel by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Hotel GetHotelById(int id);

        /// <summary>
        /// Get hotels in aread
        /// </summary>
        /// <param name="hotel"></param>
        /// <param name="kilometers"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        IEnumerable<SearchResultItem> GetHotelsInArea(Hotel hotel, int kilometers, int number);

        /// <summary>
        /// Get populair cities
        /// </summary>
        /// <param name="cities"></param>
        /// <returns></returns>
        TermsFacet GetPopulairCities(IEnumerable<City> cities);

        /// <summary>
        /// Search autocomplete
        /// First filter the LocationsString after that return the item from the Location array
        /// We would like to search on country, city etc.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        SearchResult Search(SearchQuery query);

        /// <summary>
        /// Search
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<SearchResultItem> SearchAutoComplete(SearchQuery query);
    }
}