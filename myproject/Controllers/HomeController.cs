﻿using myproject.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace myproject.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Users()
        {
            if(TempData["SavedSuccessfully"] != null)
            {
                ViewBag.SavedSuccessfully = TempData["SavedSuccessfully"].ToString();
            }
            
            if (Session["Id"] == null)
            {
                return RedirectToAction("Index");
            }
            int id = (int)Session["Id"];
            User user = db.userModels.Find(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult UserInfo(User userchange, string province, string district, string ward)
        {
            int id = (int)Session["Id"];
            User user = db.userModels.Find(id);
            user.FullName = userchange.FullName;
            user.Email = userchange.Email;
            user.PhoneNumber = userchange.PhoneNumber;
            user.Address = userchange.Address;
            user.Province = province;
            user.District = district;
            user.Ward = ward;
            db.SaveChanges();
            TempData["SavedSuccessfully"] = "1";
            
            return RedirectToAction("Users");
        }
        [HttpPost]
        public ActionResult Signup(String username, String fullname, String password, String confirmpassword)
        {
            User newUser = new User();
            newUser.Username = username;
            newUser.FullName = fullname;
            newUser.Password = password;
            newUser.Confirm = confirmpassword;

            db.userModels.Add(newUser);

            db.SaveChanges();

            var checkUser = db.userModels.SingleOrDefault(u => u.Username == username && u.Password == password);
            if (checkUser != null)
            {
                Session["Id"] = checkUser.Id;
                Session["FullName"] = checkUser.FullName;
                int id = (int)Session["Id"];
                Session["InvalidUser"] = null;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (username == null || password == null)
            {
                Session["InvalidUser"] = "Username or Password is empty";
                return RedirectToAction("Index");
            }
            else
            {
                var checkUser = db.userModels.SingleOrDefault(u => u.Username == username && u.Password == password);
                if (checkUser != null)
                {
                    Session["Id"] = checkUser.Id;
                    Session["FullName"] = checkUser.FullName;
                    int id = (int)Session["Id"];
                    Session["InvalidUser"] = null;
                    return RedirectToAction("Index");
                }
                else
                {
                    Session["InvalidUser"] = "Invalid Username or Password";
                    return RedirectToAction("Index");
                }

            }
        }

        public ActionResult LoginToUsersGuides(string username, string password)
        {
            if (username == null || password == null)
            {
                Session["InvalidUser"] = "Username or Password is empty";
                return RedirectToAction("Index");
            }
            else
            {
                var checkUser = db.userModels.SingleOrDefault(u => u.Username == username && u.Password == password);
                if (checkUser != null)
                {
                    Session["Id"] = checkUser.Id;
                    Session["FullName"] = checkUser.FullName;
                    int id = (int)Session["Id"];
                    Session["InvalidUser"] = null;
                    return RedirectToAction("UsersGuides");
                }
                else
                {
                    Session["InvalidUser"] = "Invalid Username or Password";
                    return RedirectToAction("Index");
                }
            }
        }

        public ActionResult Logout()
        {
            int id = (int)Session["Id"];
            var user = db.userModels.Find(id);
            if (user != null)
            {
                Session["Id"] = null;
                return RedirectToAction("Index");
            }
            else
            {
                Session["Id"] = null;
                Session["Username"] = null;
                return RedirectToAction("Index");
            }
        }

        public ActionResult Guides(string name, string family, string difficulty, string clade1, string clade2)
        {
            var guides = from s in db.Guides
                           select s;

            if (!String.IsNullOrEmpty(name))
            {
                guides = guides.Where(s => s.GuideTitle.ToUpper().Contains(name.ToUpper()) || s.GuideDescription.ToUpper().Contains(name.ToUpper()) || s.Guide_Level.ToUpper().Contains(name.ToUpper()) || s.difficulty_hashtag.ToUpper().Contains(name.ToUpper()));
            }

            if (!String.IsNullOrEmpty(family))
            {
                guides = guides.Where(s => s.family_hasttag.ToUpper().Contains(family.ToUpper()));
            }

            if (!String.IsNullOrEmpty(difficulty))
            {
                guides = guides.Where(s => s.difficulty_hashtag.ToUpper().Contains(difficulty.ToUpper()));
            }

            if (!String.IsNullOrEmpty(clade1))
            {
                guides = guides.Where(s => s.clade_hashtag.ToUpper().Contains(clade1.ToUpper()));
            }

            if (!String.IsNullOrEmpty(clade2))
            {
                guides = guides.Where(s => s.clade_hashtag.ToUpper().Contains(clade2.ToUpper())); 
            }
            

            return View(guides.ToList());
        }

        public ActionResult GuideDetail(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guide guide = db.Guides.Find(id);
            if (guide == null)
            {
                return HttpNotFound();
            }
            return View(guide);
        }

        public ActionResult UsersGuides()
        {
            int Id = (int)Session["Id"];

            List<Detail> userProducts = getProducts(Id);
            List<Guide> Guide = getGuide(userProducts);
            
            return View(Guide);
        }

        public List<Detail> getProducts(int userid)
        {
            List<Detail> userProducts = new List<Detail>();

            var orders = from s in db.Orders
                           select s;
            orders = orders.Where(s => s.UserID == userid);
            
            foreach (Order order in orders)
            {
                List<Detail> products = db.Details.Where(s => s.OrderID == order.OrderID).ToList();
                userProducts.AddRange(products);
            }
           
            return userProducts;
        }

        

        public List<Guide> getGuide(List<Detail> userProducts)
        {
            List<Guide> userGuide = new List<Guide>();           
            List<Product> Products = new List<Product>();

            foreach(var product in userProducts)
            {
                var findproduct = db.Products.Find(product.ProductID);
                if (Products.Any(m => m.productID == findproduct.productID))
                {

                }
                else
                {
                    Products.Add(findproduct);
                }
            }

            List<Guide> guides = new List<Guide>();
            string[] separatingStrings = { " " };

            foreach (var product in Products)
            {
                Guide productguide = new Guide();

                String[] hashtag_clade = product.clade_hashtag.Split(' ');

                guides = db.Guides.Where(m => m.family_hasttag.Contains(product.family_hasttag)).ToList();
                int max = 0;
                
                foreach (var guide in guides)
                {
                    
                    int matchcount = 0;
                    foreach (String clade in hashtag_clade)
                    {
                        if (guide.clade_hashtag.Contains(clade)) matchcount += 1;
                    }

                    if (matchcount == hashtag_clade.Length)
                    {
                        if (userGuide.Any(m => m.Id == guide.Id))
                        {
                            productguide = guide;
                        }
                        else
                        {
                            productguide = guide;
                            userGuide.Add(guide);
                        }
                        break;
                    }
                    else
                    {
                        if(max < matchcount)
                        {
                            productguide = guide;
                            max = matchcount;
                        }
                    }
                }
                if (userGuide.Any(m => m.Id == productguide.Id))
                {

                }
                else
                {
                    userGuide.Add(productguide);
                }
            }
         return userGuide;
        }
    }
}