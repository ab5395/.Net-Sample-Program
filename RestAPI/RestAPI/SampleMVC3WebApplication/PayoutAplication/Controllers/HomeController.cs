using Newtonsoft.Json.Linq;
using PayoutAplication.Models;
using PayPal.Api;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace PayoutAplication.Controllers
{
    public class HomeController : Controller
    {
        protected RequestFlow flow;
        public ActionResult Index()
        {
          //  payoutFunction();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public void payoutFunction()
        {
            // ### Api Context
            // Pass in a `APIContext` object to authenticate 
            // the call and to send a unique request id 
            // (that ensures idempotency). The SDK generates
            // a request id if you do not pass one explicitly. 
            // See [Configuration.cs](/Source/Configuration.html) to know more about APIContext.
            var apiContext = Configuration.GetAPIContext();

            // ### Initialize `Payout` Object
            // Initialize a new `Payout` object with details of the batch payout to be created.
            var payout = new Payout
            {
                // #### sender_batch_header
                // Describes how the payments defined in the `items` array are to be handled.
                sender_batch_header = new PayoutSenderBatchHeader
                {
                    sender_batch_id = "batch_" + System.Guid.NewGuid().ToString().Substring(0, 8),
                    email_subject = "You have a payment"
                },
                // #### items
                // The `items` array contains the list of payout items to be included in this payout.
                // If `syncMode` is set to `true` when calling `Payout.Create()`, then the `items` array must only
                // contain **one** item.  If `syncMode` is set to `false` when calling `Payout.Create()`, then the `items`
                // array can contain more than one item.
                items = new List<PayoutItem>
                {
                    new PayoutItem
                    {
                        recipient_type = PayoutRecipientType.EMAIL,
                        amount = new Currency
                        {
                            value = "6",
                            currency = "USD"
                        },
                        receiver = "demo.narolainfotech@gmail.com",
                        note = "Thank you for shopping.",
                        sender_item_id = "item_1"
                    },
                    //new PayoutItem
                    //{
                    //    recipient_type = PayoutRecipientType.EMAIL,
                    //    amount = new Currency
                    //    {
                    //        value = "7",
                    //        currency = "USD"
                    //    },
                    //    receiver = "ng@narola.email",
                    //    note = "Thank you for coming.",
                    //    sender_item_id = "item_2"
                    //},
                    //new PayoutItem
                    //{
                    //    recipient_type = PayoutRecipientType.EMAIL,
                    //    amount = new Currency
                    //    {
                    //        value = "2.00",
                    //        currency = "USD"
                    //    },
                    //    receiver = "ng-facilitator_api1.narola.email",
                    //    note = "Thank you.",
                    //    sender_item_id = "item_3"
                    //}
                }
            };
            // ### Payout.Create()
            // Creates the batch payout resource.
            // `syncMode = false` indicates that this call will be performed **asynchronously**,
            // and will return a `payout_batch_id` that can be used to check the status of the payouts in the batch.
            // `syncMode = true` indicates that this call will be performed **synchronously** and will return once the payout has been processed.
            // > **NOTE**: The `items` array can only have **one** item if `syncMode` is set to `true`.
            var createdPayout = payout.Create(apiContext);

            PayoutBatch payoutPayment = Payout.Get(apiContext, createdPayout.batch_header.payout_batch_id);

            Thread.Sleep(60000);
            if (payoutPayment.links != null && payoutPayment.links.Count > 0)
            {
                var client = new RestClient(payoutPayment.links.First().href);
                var request = new RestRequest(Method.GET);
                request.AddHeader("authorization", apiContext.AccessToken);
                request.AddHeader("content-type", "application/json");
                IRestResponse response = client.Execute(request);
            }
        }
    }
}