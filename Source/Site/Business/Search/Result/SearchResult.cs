using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.Business.Search.Result
{
    public class SearchResult
    {
        public string SearchTerm { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int TotalResults { get; set; }

        public IEnumerable<SearchResultItem> Results { get; set; }
    }
}