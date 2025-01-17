﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using myproject.Models;

namespace myproject.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Products
        public ActionResult Index(string searchString, string sort)
        {
            ViewBag.CurrentSort = sort;
            ViewBag.CurrentFilter = searchString;

            var products = from s in db.Products
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.category.ToString() == "product" && s.productname.ToUpper().Contains(searchString.ToUpper()));
            }
            else products = products.Where(s => s.category.ToString() == "product");

            if (!String.IsNullOrEmpty(sort))
            {
                if (sort == "HightoLow")
                {
                    products = products.OrderByDescending(s => s.price);
                }
                else
                {
                    products = products.OrderBy(s => s.price);
                }
            }
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
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
            if (TempData["Success"] != null)
            {
                var success = TempData["Success"].ToString();
                if (success == "1") ViewBag.Success = "1";
                if (success == "2") ViewBag.Success = "2";
            }

            return View(product);
        }       
    }
}
