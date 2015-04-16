using System.Web.Http;
using Site.Business.Filters;

namespace Site.Controllers.Api
{
    /// <summary>
    /// Filter API controller
    /// </summary>
    public class FilterController : ApiController
    {
        private readonly IFilterService _filterService;
        

        /// <summary>
        /// Public constructor
        /// </summary>
        public FilterController()
        {
            _filterService = new FilterService();
        }

        /// <summary>
        /// Get filters
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get()
        {
            var filters = new Filters
            {
                FacilityFilters = _filterService.GetFacilityFilters(),
                CityFilters = _filterService.GetCityFilters()
            };
            return Json(filters);
        }
    }
}