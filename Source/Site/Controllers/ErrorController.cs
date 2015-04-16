using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Site.Business.Search;
using Site.Models;
using Site.Business.Geo.City;

namespace Site.Controllers
{
    public class ErrorController : Controller
    {
        public ErrorController()
        {
        }

        // GET: Error index
        public ActionResult Index()
        {
            var viewModel = new BaseViewModel("error");
            return View(viewModel);
        }
    }
}