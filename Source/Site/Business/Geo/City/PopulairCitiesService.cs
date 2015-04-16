using System;
using System.Collections.Generic;
using System.Linq;

namespace Site.Business.Geo.City
{
    public class PopulairCitiesService : IPopulairCitiesService
    {
        private readonly Coordinates _amsterdam = new Coordinates(52.370216, 4.895168);
        private readonly Coordinates _beijing = new Coordinates(39.904211, 116.407395);
        private readonly Coordinates _sydney = new Coordinates(-33.867487, 151.206990);
        private readonly Coordinates _berlin = new Coordinates(52.520007, 13.404954);
        private readonly Coordinates _paris = new Coordinates(48.856614, 2.352222);
        private readonly Coordinates _newyork = new Coordinates(40.712784, -74.005941);

        private readonly List<City> _cities;

        public PopulairCitiesService()
        {
            _cities = new List<City>();
            _cities.Add(new City { Country = "NL", Title = "Amsterdam", Coordinates = _amsterdam, Background = "cities_amsterdam.jpg" });
            _cities.Add(new City { Country = "US", Title = "New York", Coordinates = _newyork, Background = "cities_newyork.jpg" });
            _cities.Add(new City { Country = "CN", Title = "Beijing", Coordinates = _beijing, Background = "cities_bejing.jpg" });
            _cities.Add(new City { Country = "DE", Title = "Berlin", Coordinates = _berlin, Background = "cities_berlin.jpg" });
            _cities.Add(new City { Country = "FR", Title = "Paris", Coordinates = _paris, Background = "cities_paris.jpg" });
            _cities.Add(new City { Country = "AU", Title = "Sydney", Coordinates = _sydney, Background = "cities_sydney.jpg" });
        }

        public IEnumerable<City> GetCities()
        {
            return _cities;
        }

        public City GetByCountry(string countryCode)
        {
            return
                _cities.FirstOrDefault(c => c.Country.Equals(countryCode, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}