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
        List<Cart> cartlist = new List<Cart>();
        // GET: Cart
        public ActionResult Index()
        {
            ViewData["mycart"] = cartlist;
            return View(cartlist);      
        }
        [HttpPost]
        public ActionResult AddtoCart(Product product, int? amount)
        {
            if (amount == null)
            {
                return HttpNotFound();
            }
            if (product == null)
            {
                return HttpNotFound();
            }
            Cart cart = new Cart(product, amount);
            cartlist.Add(cart);
            return RedirectToAction("Index", "Cart");
        }
    }
}
