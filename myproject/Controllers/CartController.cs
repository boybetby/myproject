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
using Newtonsoft.Json.Linq;

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
  
        public ActionResult CheckoutFail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string name, int amount)
        {
            List<Cart> cartlist = setCart();
            var cart = cartlist.FirstOrDefault(n => n.productname == name);
            cart.amount = amount;
            Session["Cart"] = cartlist;

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
            if(Session["Id"] != null)
            {
                int id = (int)Session["Id"];
                var user = db.userModels.Find(id);
                Session["Province"] = user.Province;
                Session["District"] = user.District;
                Session["Ward"] = user.Ward;
                Session["Name"] = user.FullName;
                Session["Email"] = user.Email;
                Session["Phone"] = user.PhoneNumber;
                Session["Address"] = user.Address;
            }
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
                addorder.UserID = (int)Session["Id"];
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
                string youraddress = order.Address + ", " + ward + ", " + district + ", " + province;
                ViewBag.Address = youraddress;
                ViewBag.Name = order.Name;
                ViewBag.Phone = order.PhoneNumber;
                ViewBag.ShippingFee = ShippingFee;
                return View(cartlist);
            }
            return View("Info");
        }

        [HttpPost]
        public ActionResult ProcessPayment(long finalprice, String method)
        {
            Order addorder = new Order();
            addorder = Session["Order"] as Order;
            addorder.PaymentMethod = method;
            List<Cart> cartlist = setCart();
            if (method == "momo") 
            {
                //long totalprice = 0;
                string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
                string partnerCode = "MOMO5RGX20191128";
                string accessKey = "M8brj9K6E22vXoDB";
                string serectKey = "nqQiVSgDMy809JoPF6OzP5OdBUB550Y4";
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                string orderId = new string(Enumerable.Repeat(chars, 10).Select(s => s[random.Next(s.Length)]).ToArray());
                string orderInfo = "YenConcept";
                string returnUrl = "https://localhost:44371/Cart/CheckoutFail";
                string notifyUrl = "https://localhost:44371/Cart/Thanks";
                //foreach (var items in cartlist)
                //{
                //    totalprice += items.amount * items.price;
                //}
                string requestId = Guid.NewGuid().ToString();
                string extraData = "";

                string rawHash = "partnerCode=" +
                    partnerCode + "&accessKey=" +
                    accessKey + "&requestId=" +
                    requestId + "&amount=" +
                    finalprice + "&orderId=" +
                    orderId + "&orderInfo=" +
                    orderInfo + "&returnUrl=" +
                    returnUrl + "&notifyUrl=" +
                    notifyUrl + "&extraData=" +
                    extraData;

                MoMoSecurity crypto = new MoMoSecurity();
                string signature = crypto.signSHA256(rawHash, serectKey);
                JObject message = new JObject
            {
                {"partnerCode", partnerCode },
                {"accessKey", accessKey},
                {"requestId", requestId },
                {"amount", finalprice.ToString() },
                {"orderId", orderId },
                {"orderInfo", orderInfo },
                {"returnUrl", returnUrl },
                {"notifyUrl", notifyUrl },
                {"requestType", "captureMoMoWallet" },
                {"signature", signature },
            };
                string responseFromMoMo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());
                JObject jmessage = JObject.Parse(responseFromMoMo);
                return Redirect(jmessage.GetValue("payUrl").ToString());
            }
            else
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                string orderID = new string(Enumerable.Repeat(chars, 10).Select(s => s[random.Next(s.Length)]).ToArray());
                db.Orders.Add(addorder);
                foreach (var item in cartlist)
                {
                    //OrderDetail orderdetails = new OrderDetail();
                    //orderdetails.Order = addorder;
                    //orderdetails.ProductID = item.productID;
                    //orderdetails.amount = item.amount;
                    //db.OrderDetails.Add(orderdetails);
                    Detail detail = new Detail();
                    detail.OrderID = addorder.OrderID;
                    detail.ProductID = item.productID;
                    detail.Amount = item.amount;
                    db.Details.Add(detail);
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
                Session.Remove("Cart");
                Session.Remove("Number");
                Session.Remove("Order");
                db.SaveChanges();
                return RedirectToAction("ViewThanks");
            }
            
        }

        public ActionResult ViewThanks()
        {

            return View();
        }

        [HttpPost]
        public ActionResult MomoPayment(long finalprice)
        {
            Order addorder = new Order();
            addorder = Session["Order"] as Order;
            List<Cart> cartlist = setCart();
            //long totalprice = 0;
            string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
            string partnerCode = "MOMO5RGX20191128";
            string accessKey = "M8brj9K6E22vXoDB";
            string serectKey = "nqQiVSgDMy809JoPF6OzP5OdBUB550Y4";
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string orderId = new string(Enumerable.Repeat(chars, 10).Select(s => s[random.Next(s.Length)]).ToArray());
            string orderInfo = "YenConcept";
            string returnUrl = "https://localhost:44371/Cart/CheckoutFail";
            string notifyUrl = "https://localhost:44371/Cart/Thanks";
            //foreach (var items in cartlist)
            //{
            //    totalprice += items.amount * items.price;
            //}
            string requestId = Guid.NewGuid().ToString();
            string extraData = "";

            string rawHash = "partnerCode=" +
                partnerCode + "&accessKey=" +
                accessKey + "&requestId=" +
                requestId + "&amount=" +
                finalprice + "&orderId=" +
                orderId + "&orderInfo=" +
                orderInfo + "&returnUrl=" +
                returnUrl + "&notifyUrl=" +
                notifyUrl + "&extraData=" +
                extraData;

            MoMoSecurity crypto = new MoMoSecurity();
            string signature = crypto.signSHA256(rawHash, serectKey);
            JObject message = new JObject
            {
                {"partnerCode", partnerCode },
                {"accessKey", accessKey},
                {"requestId", requestId },
                {"amount", finalprice.ToString() },
                {"orderId", orderId },
                {"orderInfo", orderInfo },
                {"returnUrl", returnUrl },
                {"notifyUrl", notifyUrl },
                {"requestType", "captureMoMoWallet" },
                {"signature", signature },
            };
            string responseFromMoMo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());
            JObject jmessage = JObject.Parse(responseFromMoMo);
            return Redirect(jmessage.GetValue("payUrl").ToString());
        }
        public ActionResult Thanks(long amount)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string orderID = new string(Enumerable.Repeat(chars, 10).Select(s => s[random.Next(s.Length)]).ToArray());
            List<Cart> cartlist = setCart();
            Order addorder = new Order();
            addorder = Session["Order"] as Order;
            db.Orders.Add(addorder);
            foreach (var item in cartlist)
            {
                //OrderDetail orderdetails = new OrderDetail();
                //orderdetails.Order = addorder;
                //orderdetails.ProductID = item.productID;
                //orderdetails.amount = item.amount;
                //db.OrderDetails.Add(orderdetails);
                Detail detail = new Detail();
                detail.OrderID = addorder.OrderID;
                detail.ProductID = item.productID;
                detail.Amount = item.amount;
                db.Details.Add(detail);
            }

            string MailSend = "yengreenliving@gmail.com";
            string Password = "yenmail@123";
            using (MailMessage m = new MailMessage(MailSend, addorder.Email))
            {
                string youraddress = addorder.Address + " " + addorder.Ward + " " + addorder.District + " " + addorder.Province;
                m.Subject = "Thank You For Chosing Us";
                m.Body = ("Your order's total is: " + amount + " <br /> Your order will be delivery to " + youraddress + " within 3 to 4 days <br />THANKS YOU!");
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
            Session.Remove("Cart");
            Session.Remove("Number");
            Session.Remove("Order");
            db.SaveChanges();
            return View();
         
        }
    }
}
