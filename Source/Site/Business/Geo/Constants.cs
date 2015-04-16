using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.Business.Geo
{
    public static class Constants
    {
        public static class CityCoordinates
        {
            public static readonly Coordinates AMSTERDAM = new Coordinates(52.370216, 4.895168);
            public static readonly Coordinates TOKYO = new Coordinates(35.689487, 139.691706);
            public static readonly Coordinates LONDON = new Coordinates(51.507351, -0.127758);
            public static readonly Coordinates BERLIN = new Coordinates(52.520007, 13.404954);
            public static readonly Coordinates BARCELONA = new Coordinates(41.385064, 2.173403);
            public static readonly Coordinates NEWYORK = new Coordinates(40.712784, -74.005941);
        }
    }
}