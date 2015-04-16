using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.Sample.Hotels;
using Site.Business.HotelFacility;
using Site.Business.Search.Result;

namespace Site.Models
{
    public class HotelViewModel : BaseViewModel
    {
        public HotelViewModel() : base("hotel")
        {
        }

        public Hotel Hotel { get; set; }
        public IEnumerable<SearchResultItem> HotelsInArea { get; set; }
        public IEnumerable<Facility> ColumnLeftFacilities { get; set; }
        public IEnumerable<Facility> ColumnRightFacilities { get; set; } 
    }
}