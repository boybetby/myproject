namespace myproject.Migrations
{
    using myproject.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<myproject.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(myproject.Models.ApplicationDbContext context)
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

            var furniture = new List<Product>
                        {
                            new Product{productname="Kệ chữ A bốn tầng", category=Category.furniture, price=1350000 , description="Kệ chữ A bốn tầng được thiết kế đặc biệt thích hợp dùng bày trí ngoài vườn, cùng các loại cây xanh từ cây dáng rũ, cây thân thảo, đến thân bụi, xương rồng, v.v. Ngoài ra, bạn có thể sắp xếp thêm các vật dụng làm vườn như bình tưới nước.", image="../../images/furniture/f_1.png"},
                            new Product{productname="Kệ chữ A bốn tầng đa năng", category=Category.furniture, price=3000000 , description="Kệ chữ A bốn tầng lớn đa dụng hơn và có thể bày trí cả trong nhà và ngoài vườn. Đặc biệt, nếu bạn vừa muốn trang trí các vật dụng như sách hay giỏ cho kệ vừa muốn có thể thường xuyên tưới nước cho các chậu cây của mình, thì chiếc hộc của Kệ sẽ là người bạn đồng hành hoàn hảo đó.", image="../../images/furniture/f_2.png"},
                            new Product{productname="Kệ xe đẩy Gỗ", category=Category.furniture, price=2500000 , description="Kệ xe đẩy gỗ được ưu ái là thiết kế mới - mang tính tối ưu, đa nhiệm và năng động nhất trong các anh em nhà kệ, với hai tầng kệ đựng đồ cùng 2 bánh xe trước và 2 bánh xe sau có khoá. Chỉ với hai tầng chứa đồ, bạn sẽ khá ưng bụng với người bạn 4-bánh-xe linh hoạt, tiện nghi này.", image="../../images/furniture/f_3.png"},
                            new Product{productname="Đôn Gỗ x Sọt Tre", category=Category.furniture, price=400000 , description="Ở Yên, thiết kế chân gỗ - 3 đế trụ được dành riêng cho các em giỏ mây, sọt tre, phù hợp trang trí các góc nhà, gian phòng cần thanh lọc không khí. Sọt tre với chiếc dáng bầu được đan tỉ mỉ, xen lẫn tạo nên một vẻ đẹp cực kỳ thu hút nhưng không kém phần sang trọng, tối giản với tông màu vàng ngà.", image="../../images/furniture/f_4.png"},

                            new Product{productname="Kệ chữ A 4 tầng có tủ", category=Category.furniture, price=1350000 , description="Kệ chữ A bốn tầng được thiết kế đặc biệt thích hợp dùng bày trí ngoài vườn, cùng các loại cây xanh từ cây dáng rũ, cây thân thảo, đến thân bụi, xương rồng, v.v. Ngoài ra, bạn có thể sắp xếp thêm các vật dụng làm vườn như bình tưới nước.", image="../../images/furniture/f_5.png"},
                            new Product{productname="Kệ chữ A 4 tầng to", category=Category.furniture, price=3000000 , description="Kệ chữ A bốn tầng lớn đa dụng hơn và có thể bày trí cả trong nhà và ngoài vườn. Đặc biệt, nếu bạn vừa muốn trang trí các vật dụng như sách hay giỏ cho kệ vừa muốn có thể thường xuyên tưới nước cho các chậu cây của mình, thì chiếc hộc của Kệ sẽ là người bạn đồng hành hoàn hảo đó.", image="../../images/furniture/f_6.png"},
                            new Product{productname="Kệ 2 tầng", category=Category.furniture, price=2500000 , description="Kệ xe đẩy gỗ được ưu ái là thiết kế mới - mang tính tối ưu, đa nhiệm và năng động nhất trong các anh em nhà kệ, với hai tầng kệ đựng đồ cùng 2 bánh xe trước và 2 bánh xe sau có khoá. Chỉ với hai tầng chứa đồ, bạn sẽ khá ưng bụng với người bạn 4-bánh-xe linh hoạt, tiện nghi này.", image="../../images/furniture/f_7.png"},
                            new Product{productname="Kệ 2 tầng có bánh lăn", category=Category.furniture, price=400000 , description="Ở Yên, thiết kế chân gỗ - 3 đế trụ được dành riêng cho các em giỏ mây, sọt tre, phù hợp trang trí các góc nhà, gian phòng cần thanh lọc không khí. Sọt tre với chiếc dáng bầu được đan tỉ mỉ, xen lẫn tạo nên một vẻ đẹp cực kỳ thu hút nhưng không kém phần sang trọng, tối giản với tông màu vàng ngà.", image="../../images/furniture/f_8.png"},
                            new Product{productname="Kệ đứng chữ A", category=Category.furniture, price=1350000 , description="Kệ chữ A bốn tầng được thiết kế đặc biệt thích hợp dùng bày trí ngoài vườn, cùng các loại cây xanh từ cây dáng rũ, cây thân thảo, đến thân bụi, xương rồng, v.v. Ngoài ra, bạn có thể sắp xếp thêm các vật dụng làm vườn như bình tưới nước.", image="../../images/furniture/f_9.png"},
                            new Product{productname="Kệ đứng chữ A kèm cây", category=Category.furniture, price=3000000 , description="Kệ chữ A bốn tầng lớn đa dụng hơn và có thể bày trí cả trong nhà và ngoài vườn. Đặc biệt, nếu bạn vừa muốn trang trí các vật dụng như sách hay giỏ cho kệ vừa muốn có thể thường xuyên tưới nước cho các chậu cây của mình, thì chiếc hộc của Kệ sẽ là người bạn đồng hành hoàn hảo đó.", image="../../images/furniture/f_10.png"},

                        };
            furniture.ForEach(s => context.Products.Add(s));
            context.SaveChanges();

            var workshop = new List<Product>
                        {
                            new Product{productname="Workshop Terrarium Size L", category=Category.workshop, price=1000000 , description="", image="../../images/workshops/ws_1.jpg"},
                            new Product{productname="Workshop Terrarium Size M", category=Category.workshop, price=500000 , description="", image="../../images/workshops/ws_2.jpg"},
                            new Product{productname="Workshop Kokedama", category=Category.workshop, price=350000 , description="", image="../../images/workshops/ws_3.jpg"},
                        };
            workshop.ForEach(s => context.Products.Add(s));
            context.SaveChanges();
        }
    }
}
