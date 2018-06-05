using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleMVC3WebApplication.Models;

namespace SampleMVC3WebApplication.Controllers
{
    public class HomeController : Controller
    {

        public void payoutFunction()
        {
            var apiContext = PaypalConfiguration.GetAPIContext();
        }


        public ActionResult Index()
        {
            payoutFunction();

            ViewBag.Mode = PayPalMvc.Configuration.Current.Mode;
            ViewBag.MerchantUserName = PayPalMvc.Configuration.Current.MerchantUserName;
            ViewBag.MerchantPassword = PayPalMvc.Configuration.Current.MerchantPassword;
            ViewBag.Signature = PayPalMvc.Configuration.Current.Signature;

            return View();
        }

        /// <summary>
        /// Simple single object checkout, the description is shown on PayPal when making the purchase
        /// </summary>
        public ActionResult CheckOut()
        {
            ApplicationCart cart = new ApplicationCart
            {
                Id = Guid.NewGuid(), // Unique purchase Id
                Currency = "USD",
                PurchaseDescription = "Left Handed Screwdriver",
                TotalPrice = 5.00M // This does NOT get shown to the user on PayPal when making a single object purchase, but is charged and appears in their order history
            };

            // Storing this in session, you might want to store in it a database
            Session["Cart"] = cart;

            return View(cart);
        }

        /// <summary>
        /// Multi item checkout using a cart of items, these are shown on PayPal when making the purchase
        /// </summary>
        public ActionResult CheckOutCart()
        {
            ApplicationCart cart = new ApplicationCart
            {
                Id = Guid.NewGuid(), // Unique cart Id
                Currency = "USD",
                PurchaseDescription = "Tickets", // This is what appears in the user's PayPal order history, not the individual items
                Items = new List<ApplicationCartItem>()
                {
                    new ApplicationCartItem
                    {
                        Quantity = 1,
                        Price = 5.00M,
                        Name = "Main Event Ticket",
                        Description = "The Main Event you've been waiting to see."
                    },
                    new ApplicationCartItem
                    {
                        Quantity = 2,
                        Price = 2.00M,
                        Name = "Gun Show Ticket",
                        // Description = "Optional for each item"
                    }
                }
            };

            Session["Cart"] = cart;
            
            return View("CheckOut", cart);
        }
    }
}
