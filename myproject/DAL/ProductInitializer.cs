using myproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myproject.DAL
{
    public class ProductInitializer : System.Data.Entity.
    DropCreateDatabaseIfModelChanges<ProductContext>
    {
        protected override void Seed(ProductContext context)
        {
            var products = new List<Product>
            {
                new Product{productname="Si Cẩm Thạch", category=Category.product, price=80000 , description="", image="../../img/product/pd_1.png"},
                new Product{productname="Si Cẩm Thạch", category=Category.product, price=80000 , description="", image="../../img/product/pd_2.png"},
                new Product{productname="Si Cẩm Thạch", category=Category.product, price=80000 , description="", image="../../img/product/pd_3.png"},
                new Product{productname="Si Cẩm Thạch", category=Category.product, price=80000 , description="", image="../../img/product/pd_4.png"},
                new Product{productname="Si Cẩm Thạch", category=Category.product, price=80000 , description="", image="../../img/product/pd_5.png"},
                new Product{productname="Si Cẩm Thạch", category=Category.product, price=80000 , description="", image="../../img/product/pd_6.png"},
                new Product{productname="Si Cẩm Thạch", category=Category.product, price=80000 , description="", image="../../img/product/pd_7.png"},
                new Product{productname="Si Cẩm Thạch", category=Category.product, price=80000 , description="", image="../../img/product/pd_8.png"},
                new Product{productname="Si Cẩm Thạch", category=Category.product, price=80000 , description="", image="../../img/product/pd_9.png"},
                new Product{productname="Si Cẩm Thạch", category=Category.product, price=80000 , description="", image="../../img/product/pd_10.png"},
                new Product{productname="Si Cẩm Thạch", category=Category.product, price=80000 , description="", image="../../img/product/pd_11.png"},
                new Product{productname="Si Cẩm Thạch", category=Category.product, price=80000 , description="", image="../../img/product/pd_12.png"},
                new Product{productname="Si Cẩm Thạch", category=Category.product, price=80000 , description="", image="../../img/product/pd_13.png"},
                new Product{productname="Si Cẩm Thạch", category=Category.product, price=80000 , description="", image="../../img/product/pd_14.png"},
                new Product{productname="Si Cẩm Thạch", category=Category.product, price=80000 , description="", image="../../img/product/pd_15.png"},
                new Product{productname="Si Cẩm Thạch", category=Category.product, price=80000 , description="", image="../../img/product/pd_16.png"}
            };
            products.ForEach(s => context.Products.Add(s));
            context.SaveChanges();
        }
    }
}