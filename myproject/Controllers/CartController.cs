using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using myproject.Models;
using System.Net.Mail;
using System.IO;

namespace myproject.Controllers
{
    public class CartController : Controller
    {
        private Random random = new Random();
        private ApplicationDbContext db = new ApplicationDbContext();
        public List<Cart> setCart()
        {
            
            List<Cart> cartlist = Session["Cart"] as List<Cart>;
            if (cartlist == null)
            {
                cartlist = new List<Cart>();
                Session["Cart"] = cartlist;
            }
            return cartlist;
        }
        // GET: Cart
        public ActionResult Index()
        {
            List<Cart> cartlist = setCart();
            return View(cartlist);
        }

        

        [HttpPost]
        public ActionResult AddtoCart(int id, int amount)
        {
            List<Cart> cartlist = setCart();
            Cart product = cartlist.Find(n => n.productID == id);

            if (product == null)
            {
                product = new Cart(id, amount);
                cartlist.Add(product);
                Session["Number"] = cartlist.Count();

                TempData["Success"] = "1";

                return RedirectToAction("Details", "Products", new { id = id });

            }
            else
            {
                TempData["Success"] = "2";
                product.amount += amount;
                
                return RedirectToAction("Details","Products", new { id = id });
            }
            
        }

        public ActionResult Remove(int id)
        {
            List<Cart> cartlist = setCart();
            Cart product = cartlist.Find(n => n.productID == id);

            if (product != null)
            {
               
                cartlist.Remove(product);
                Session["Number"] = cartlist.Count();
                return View("Index",cartlist);
            }
            
            return View("Index",cartlist);
        }

        public ActionResult Info()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Confirm(Order order,string province, string district, string ward, int ShippingFee)
        {
            if (ModelState.IsValid) { 
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                string orderID = new string(Enumerable.Repeat(chars, 10).Select(s => s[random.Next(s.Length)]).ToArray());
                long totalprice = 0;
                List<Cart> cartlist = setCart();
                Order addorder = new Order();
                addorder.Name = order.Name;
                addorder.Email = order.Email;
                addorder.PhoneNumber = order.PhoneNumber;
                addorder.Address = order.Address;
                addorder.OrderID = orderID;
                addorder.Province = province;
                addorder.District = district;
                addorder.Ward = ward;
                foreach (var items in cartlist)
                {
                    totalprice += items.amount * items.price;

                }
                addorder.Date = DateTime.Now;
                addorder.TotalPrice = totalprice;
                Session["Order"] = addorder;
                //foreach (var item in cartlist)
                //{
                //    OrderDetail orderdetails = new OrderDetail();
                //    orderdetails.Order = addorder;
                //    orderdetails.ProductID = item.productID;
                //    orderdetails.amount = item.amount;
                //    db.OrderDetails.Add(orderdetails);
                //}

                //string MailSend = "yengreenliving@gmail.com";
                //string Password = "yenmail@123";
                //using (MailMessage m = new MailMessage(MailSend, order.Email))
                //{
                //    string youraddress = order.Address + " " + ward + " " + district + " " + province;
                //    m.Subject = "Thank You For Chosing Us";
                //    m.Body = ("Your order's total is: " + totalprice + " <br /> Your order will be delivery to " + youraddress + " within 3 to 4 days <br />THANKS YOU!");
                //    //if (emailInfo.Attacment.ContentLength > 0)
                //    //{
                //    //    string filename = Path.GetFileName(emailInfo.Attacment.FileName);
                //    //    m.Attachments.Add(new Attachment(emailInfo.Attacment.InputStream, filename));
                //    //}
                //    m.IsBodyHtml = true;
                //    using (SmtpClient smtp = new SmtpClient())
                //    {
                //        smtp.Host = "smtp.gmail.com";
                //        smtp.EnableSsl = true;
                //        NetworkCredential networkCred = new NetworkCredential(MailSend, Password);
                //        smtp.UseDefaultCredentials = true;
                //        smtp.Credentials = networkCred;
                //        smtp.Port = 587;
                //        smtp.Send(m);
                //        ViewBag.Message = "An email have sent to " + order.Email + " ! Please check your email";
                //    }
                //}
                string youraddress = order.Address + ", " + ward + ", " + district + ", " + province;
                ViewBag.Address = youraddress;
                ViewBag.Name = order.Name;
                ViewBag.Phone = order.PhoneNumber;
                ViewBag.ShippingFee = ShippingFee;
                return View(cartlist);
            }
            return View("Info");
        }
        
        public ActionResult Thanks(long finalprice)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string orderID = new string(Enumerable.Repeat(chars, 10).Select(s => s[random.Next(s.Length)]).ToArray());
            List<Cart> cartlist = setCart();
            Order addorder = new Order();

            addorder = Session["Order"] as Order;

            //addorder.Name = order1.Name;
            //addorder.Email = order1.Name;
            //addorder.PhoneNumber = order1.PhoneNumber;
            //addorder.Address = order1.Address;
            //addorder.OrderID = orderID;
            //addorder.Province = order1.Province;
            //addorder.District = order1.District;
            //addorder.Ward = order1.Ward;
                
            //addorder.Date = DateTime.Now;
            //addorder.TotalPrice = finalprice;
            db.Orders.Add(addorder);
            foreach (var item in cartlist)
            {
                OrderDetail orderdetails = new OrderDetail();
                orderdetails.Order = addorder;
                orderdetails.ProductID = item.productID;
                orderdetails.amount = item.amount;
                db.OrderDetails.Add(orderdetails);
            }

            string MailSend = "yengreenliving@gmail.com";
            string Password = "yenmail@123";
            using (MailMessage m = new MailMessage(MailSend, addorder.Email))
            {
                string youraddress = addorder.Address + " " + addorder.Ward + " " + addorder.District + " " + addorder.Province;
                m.Subject = "Thank You For Chosing Us";
                m.Body = ("Your order's total is: " + finalprice + " <br /> Your order will be delivery to " + youraddress + " within 3 to 4 days <br />THANKS YOU!");
                //if (emailInfo.Attacment.ContentLength > 0)
                //{
                //    string filename = Path.GetFileName(emailInfo.Attacment.FileName);
                //    m.Attachments.Add(new Attachment(emailInfo.Attacment.InputStream, filename));
                //}
                m.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential networkCred = new NetworkCredential(MailSend, Password);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = networkCred;
                    smtp.Port = 587;
                    smtp.Send(m);
                    ViewBag.Message = "An email have sent to " + addorder.Email + " ! Please check your email";
                }
            }
            db.SaveChanges();
            return View();
         
        }
    }
}
