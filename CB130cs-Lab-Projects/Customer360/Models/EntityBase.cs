using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Customer360.Models
{
    public class EntityBase<T>
    {
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
        public string type
        {
            get { return typeof(T).Name.ToLower(); }
        }
        public string id { get; set; }
    }
}