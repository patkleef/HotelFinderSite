using System.Collections.Generic;

namespace Site.Business.Geo.City
{
    public interface IPopulairCitiesService
    {
        IEnumerable<City> GetCities();
        City GetByCountry(string countryCode);
    }
}