using System.Collections.Generic;

namespace Site.Business.Filters
{
    public interface IFilterService
    {
        /// <summary>
        /// Get facility filters
        /// </summary>
        /// <returns></returns>
        IEnumerable<IFilter> GetFacilityFilters();

        /// <summary>
        /// Get city filters
        /// </summary>
        /// <returns></returns>
        IEnumerable<IFilter> GetCityFilters();
    }
}