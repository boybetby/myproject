using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace myproject.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Detail> Details { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<PostComment> postComment { get; set; }
        public DbSet<User> userModels { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<EventSubscriber> EventSubscribers { get; set; }
        public ApplicationDbContext()
            : base("YenConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}