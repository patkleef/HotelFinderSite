using Site.Business.Geo.City;
using Site.Business.HotelFacility;

namespace Site.Business.Filters
{
    public interface IFilterBuilder
    {
        IFilter FromFacility(Facility facility);
        IFilter FromCity(City city, int count);
    }
}