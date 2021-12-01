using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using myproject.App_Start;
using myproject.Models;
using PagedList;
namespace myproject.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public AdminController()
        {
        }

        public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        [ActionName("Index")]
        [Authorize]
        public ActionResult Dashboard()
        {
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;
            var data_monthly = db.Orders.Where(m => m.Date.Month == currentMonth && m.Date.Year == currentYear).ToList();
            var data_annual = db.Orders.Where(m => m.Date.Year == currentYear).ToList();

            var pendding_orders = db.Orders.Count(t => t.Checked == false);

            double earningmonthly = 0;
            double earningannual = 0;

            foreach (Order order in data_monthly)
            {
                earningmonthly += order.TotalPrice;
            }

            foreach (Order order in data_annual)
            {
                earningannual += order.TotalPrice;
            }

            Session["earningmonthly"] = earningmonthly;
            Session["earningannual"] = earningannual;
            Session["pendding_orders"] = pendding_orders;
            return View();
        }
        [CustomAuthorize(Roles = "Admin, Product & Order Manager")]
        public ActionResult ProductsList()
        {
            return View(db.Products.ToList());
        }
        [CustomAuthorize(Roles = "Admin, Product & Order Manager")]
        public ActionResult OrdersList()
        {
            return View(db.Orders.OrderBy(m => m.Checked).ThenBy(n => n.Date).ToList());
        }
        // GET: Admin/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //POST: Admin/Create
        [HttpPost]
        [CustomAuthorize(Roles = "Admin, Product & Order Manager")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "productID,productname,description,price,category,image")] Product product, String imagebase64)
        {
            if (ModelState.IsValid)
            {
                product.image = imagebase64;
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }
        [ActionName("Edit")]
        [CustomAuthorize(Roles = "Admin, Product & Order Manager")]
        // GET: Products1/Edit/5
        public ActionResult ProductEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        // POST: Products1/Edit/5
        [ActionName("Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductEdit([Bind(Include = "productID,productname,description,price,category,image,clade_hashtag,family_hasttag,difficulty_hashtag")] Product product, String imagebase64)
        {
            if (ModelState.IsValid)
            {
                product.image = imagebase64;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit");
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "Admin, Product & Order Manager")]
        public ActionResult DeleteProduct(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("ProductsList");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult EmployeeList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var role in RoleManager.Roles.Where(s => s.Name != "Admin"))
                list.Add(new SelectListItem() { Value = role.Name, Text = role.Name });
            ViewBag.ManagerRole = list;
            return View();
        }
        [CustomAuthorize(Roles = "Admin, Event Manager")]
        public ActionResult EventSubscriberList()
        {
            return View(db.EventSubscribers.ToList());
        }
    }
}
