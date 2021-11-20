using myproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if(Session["Id"] == null)
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
        
    }
}