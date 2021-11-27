using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using myproject.Models;
using PagedList;
namespace myproject.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;

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
        [Authorize]
        public ActionResult ProductsList()
        {
            return View(db.Products.ToList());
        }
        [Authorize]
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
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "productID,productname,description,price,category,image")] Product product, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if ((int)product.category == 0)
                {
                    string chuoi = "../../images/products/" + product.image;
                    product.image = chuoi;
                    if (image.ContentLength > 0)
                    {
                        string _path = Path.Combine(Server.MapPath("~/images/products"), Path.GetFileName(image.FileName));
                        image.SaveAs(_path);
                    }
                }
                else if ((int)product.category == 1)
                {
                    string chuoi = "../../images/furniture/" + product.image;
                    product.image = chuoi;
                    if (image.ContentLength > 0)
                    {
                        string _path = Path.Combine(Server.MapPath("~/images/furniture"), Path.GetFileName(image.FileName));
                        image.SaveAs(_path);
                    }
                }
                else
                {
                    string chuoi = "../../images/workshops/" + product.image;
                    product.image = chuoi;
                    if (image.ContentLength > 0)
                    {
                        string _path = Path.Combine(Server.MapPath("~/images/workshops"), Path.GetFileName(image.FileName));
                        image.SaveAs(_path);
                    }
                }
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }
        [ActionName("Edit")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult EmployeeList()
        {
            ApplicationRole role = new ApplicationRole();
            db.Roles.Add(role);
            return View(db.Users.ToList());
        }
        [Authorize(Roles = "Admin")]
        public ActionResult EventSubscriberList()
        {
            return View(db.EventSubscribers.ToList());
        }
    }
}
