using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.Models
{
    public class BaseViewModel
    {
        public string Page { get; set; }

        public BaseViewModel(string page)
        {
            Page = page;
        }
    }
}