using myproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myproject.Models
{
    public class Cart : Product
    {
        Product product { get; set; }
        int? amount;
        public Cart(Product product, int? amount)
        {
            this.product = product;
            this.amount = amount;
        }
        public Cart() { }
    }

}