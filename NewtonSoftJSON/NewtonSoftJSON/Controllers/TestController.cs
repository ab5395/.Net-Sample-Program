using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewtonSoftJSON.Models;

namespace NewtonSoftJSON.Controllers
{
    public class TestController : Controller
    {
        Product product = new Product();
        // GET: Test
        public ActionResult Index()
        {
            product.Name = "Apple";
            product.ExpiryDate = new DateTime(2008, 12, 28);
            product.Price = 3.99;
            product.Sizes = new string[] { "Small", "Medium", "Large" }.ToList();



            return View();
        }
    }
}