using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.Business.Search
{
    public class SearchQuery
    {
        public string Query { get; set; }
        public int RatingFilter { get; set; }
        public double PriceFrom { get; set; }
        public double PriceTo { get; set; }
        public int[] FacilityFilter { get; set; }
        public string CityFilter { get; set; }
        public string SortBy { get; set; }
        public bool IsAutoComplete { get; set; }
        public int CurrentPage { get; set; }
    }
}