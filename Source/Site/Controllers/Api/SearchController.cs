using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Site.Business.Search;

namespace Site.Controllers.Api
{
    public class SearchController : ApiController
    {
        private readonly ISearchService _searchService;

        /// <summary>
        /// Public constructor
        /// </summary>
        public SearchController()
        {
            _searchService = new SearchService();
        }


        /*[System.Web.Http.Route("api/search/searchautocomplete")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult SearchAutoComplete([FromUri]SearchQuery query)
        {
            return Json(_searchService.SearchAutoComplete(query));
        }*/

        // GET api/<controller>/5
        [System.Web.Http.HttpGet]
        public IHttpActionResult Search([FromUri]SearchQuery query)
        {
            if (query.IsAutoComplete)
            {
                return Json(_searchService.SearchAutoComplete(query));
            }
            return Json(_searchService.Search(query));
        }

        private HttpContextBase GetHttpContext(HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                var ctx = request.Properties["MS_HttpContext"] as HttpContextBase;
                return ctx;
            }
            return null;
        }
    }
}