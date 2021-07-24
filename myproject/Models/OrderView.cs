using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myproject.Models
{
    public class OrderView
    {
        public Order Order { get; set; }
        public Cart Cart { get; set; }
    }
}