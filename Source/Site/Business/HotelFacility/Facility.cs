using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.Business.HotelFacility
{
    /// <summary>
    /// Facility class
    /// </summary>
    public class Facility
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
        public string Icon { get; set; }
        public bool IsSelected { get; set; }
    }
}