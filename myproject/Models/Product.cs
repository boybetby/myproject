using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public int productID { get; set; }
        public string productname { get; set; }
        public string description { get; set; }
        public long price { get; set; }
        public Category? category { get; set; }
        public string image { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
       
    }

}