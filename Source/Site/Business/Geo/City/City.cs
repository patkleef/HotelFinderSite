using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.Business.Geo.City
{
    public class City
    {
        public string Title { get; set; }
        public string Country { get; set; }
        public Coordinates Coordinates { get; set; }
        public string Background { get; set; }
    }
}