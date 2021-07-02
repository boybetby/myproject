using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using myproject.Models;
using myproject.DAL;

namespace myproject.Controllers
{
    public class CartController : Controller
    {

        private ProductContext db = new ProductContext();
        public List<Cart> setCart()
        {
            List<Cart> cartlist = Session["Cart"] as List<Cart>;
            if(cartlist == null)
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
            Cart product = cartlist.Find(n => n.productID ==id);
            
            if(product == null){
                product = new Cart(id, amount);
                cartlist.Add(product);
                return RedirectToAction("Index","Home");
            }
            else {
                product.amount += amount;
                return RedirectToAction("Index", "Home");
            }
            
        }
    }
}
