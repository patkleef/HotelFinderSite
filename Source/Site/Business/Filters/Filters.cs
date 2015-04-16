using System.Collections.Generic;

namespace Site.Business.Filters
{
    public class Filters
    {
        public IEnumerable<IFilter> FacilityFilters { get; set; }
        public IEnumerable<IFilter> CityFilters { get; set; } 
    }
}