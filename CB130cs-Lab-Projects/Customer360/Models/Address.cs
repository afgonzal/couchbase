using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Customer360.Models
{
    public class Address
    {
        public string line1 { get; set; }
        public string line2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string postalCode { get; set; }
    }
}