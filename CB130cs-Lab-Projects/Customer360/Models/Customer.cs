using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Customer360.Models
{
    public class Customer : EntityBase<Customer>
    {
        public string username { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public Address billingAddress { get; set; }
    }
}