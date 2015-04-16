using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using EPiServer.Find;
using EPiServer.Find.Api.Facets;
using EPiServer.Sample.Hotels;
using Site.Business.Search;
using Site.Models;
using Site.Business.Geo.City;

namespace Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISearchService _searchService;
        private readonly IPopulairCitiesService _populairCityService;

        public HomeController()
        {
            _searchService = new SearchService();
            _populairCityService = new PopulairCitiesService();
        }

        // GET: Home
        public ActionResult Index()
        {
            var model = new HomeViewModel();

            var search = HotelHelpers.HotelClient.Search<Hotel>().Skip(1000).Take(100).GetResult();

            var list = new List<string>();
            foreach (var item in search)
            {
                list.AddRange(item.Features);
            }

            var newList = list.Distinct();

            var items = string.Join("\n", newList);

            return View(model);
        }
    }
}