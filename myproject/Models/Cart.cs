using myproject.DAL;
using myproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myproject.Models
{
    public class Cart
    {
        private ProductContext db = new ProductContext();
        public int productID { get; set; }
        public string productname { get; set; }
        public string description { get; set; }
        public long price { get; set; }
        public Category? category { get; set; }
        public string image { get; set; }
        public int amount { get; set; }

        public Cart(int id, int amount)
        {
            this.productID = id;
            Product product = db.Products.FirstOrDefault(s => s.productID == id);
            this.productname = product.productname;
            this.description = product.description;
            this.price = product.price;
            this.category = product.category;
            this.image = product.image;
            this.amount = amount;
        }
    }

}