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
                new Product{productname="Si Cẩm Thạch", category=Category.product, price=80000 , description="", image="../../images/products/pd_1.png"},
                new Product{productname="Trầu Bà Vàng Mini", category=Category.product, price=70000 , description="", image="../../images/products/pd_2.png"},
                new Product{productname="Đuôi công lá  nhỏ", category=Category.product, price=80000 , description="", image="../../images/products/pd_3.png"},
                new Product{productname="Cây giữ tiền", category=Category.product, price=130000 , description="", image="../../images/products/pd_4.png"},
                new Product{productname="Vạn Niên Tùng", category=Category.product, price=80000 , description="", image="../../images/products/pd_5.png"},
                new Product{productname="Xương rồng Thanh Sơn", category=Category.product, price=130000 , description="", image="../../images/products/pd_6.png"},
                new Product{productname="Trường Sinh", category=Category.product, price=80000 , description="", image="../../images/products/pd_7.png"},
                new Product{productname="Cỏ Gương", category=Category.product, price=50000 , description="", image="../../images/products/pd_8.png"},
                new Product{productname="Lan Ý", category=Category.product, price=130000 , description="", image="../../images/products/pd_9.png"},
                new Product{productname="Ngũ Gia Bì", category=Category.product, price=70000 , description="", image="../../images/products/pd_10.png"},
                new Product{productname="Đuôi Công", category=Category.product, price=170000 , description="", image="../../images/products/pd_11.png"},
                new Product{productname="Trầu Bà Lỗ", category=Category.product, price=170000 , description="", image="../../images/products/pd_12.png"},
                new Product{productname="Trầu Bà Thanh Xuân", category=Category.product, price=350000 , description="", image="../../images/products/pd_13.png"},
                new Product{productname="Thiết Mộc Lan", category=Category.product, price=150000 , description="", image="../../images/products/pd_14.png"},
                new Product{productname="Bàng Singapore", category=Category.product, price=350000 , description="", image="../../images/products/pd_15.png"},
                new Product{productname="Đại Phú", category=Category.product, price=450000 , description="", image="../../images/products/pd_16.png"}
            };
            products.ForEach(s => context.Products.Add(s));
            context.SaveChanges();
        }
    }
}