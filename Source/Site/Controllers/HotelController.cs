using System.Linq;
using System.Web.Mvc;
using Site.Business.HotelFacility;
using Site.Business.Search;
using Site.Models;

namespace Site.Controllers
{
    /// <summary>
    /// Hotels controller
    /// </summary>
    public class HotelController : Controller
    {
        private readonly ISearchService _searchService;
        private readonly IFacilityService _facilityService;

        /// <summary>
        /// Public constructor
        /// </summary>
        public HotelController()
        {
            _searchService = new SearchService();
            _facilityService = new FacilityService();
        }

        /// <summary>
        /// Index
        /// </summary>
        /// <param name="id">id of Hotel</param>
        /// <returns></returns>
        public ActionResult Index(int? id)
        {
            var viewModel = new HotelViewModel();
            if (id.HasValue)
            {
                viewModel.Hotel = _searchService.GetHotelById(id.Value);
            }
            else
            {
                viewModel.Hotel = _searchService.GetDefaultHotel();
            }
            
            viewModel.HotelsInArea = _searchService.GetHotelsInArea(viewModel.Hotel, 20, 3);

            var facilities = _facilityService.GetFacilitiesForHotel(viewModel.Hotel).ToList();

            var count = facilities.Count();
            var columnLeftSize = count / 2;
            var columnRightSize = count - columnLeftSize;

            viewModel.ColumnLeftFacilities = facilities.Take(columnLeftSize);
            viewModel.ColumnRightFacilities = facilities.Skip(columnLeftSize).Take(columnRightSize);

            return View(viewModel);
        }
    }
}