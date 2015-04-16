using Site.Business.Geo.City;
using Site.Business.HotelFacility;

namespace Site.Business.Filters
{
    public class FilterBuilder : IFilterBuilder
    {
        public IFilter FromFacility(Facility facility)
        {
            return new FacilityFilter
            {
                Id = facility.Id,
                Title = facility.Title,
                Icon = facility.Icon,
                IsSelected = false,
                Value = facility.Value
            };
        }

        public IFilter FromCity(City city, int count)
        {
            return new CityFilter
            {
                Title = city.Title,
                Coordinates = city.Coordinates,
                Country = city.Country,
                Count = count,
                Background = city.Background
            };
        }
    }
}