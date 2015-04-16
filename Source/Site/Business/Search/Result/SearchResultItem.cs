using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.Sample.Hotels;

namespace Site.Business.Search.Result
{
    public class SearchResultItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public double StarRating { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
    }
}