using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myproject.Models
{
    public enum Category
    {
        product, furniture, workshop
    }
    public class Product
    { 
        public int productid { get; set; }
        public string productname { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public Category? category { get; set; }
        public string image { get; set; }
    }
}