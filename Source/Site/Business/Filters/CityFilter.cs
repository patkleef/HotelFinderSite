using Site.Business.Geo.City;

namespace Site.Business.Filters
{
    public class CityFilter : City, IFilter
    {
        public int Count { get; set; }
    }
}