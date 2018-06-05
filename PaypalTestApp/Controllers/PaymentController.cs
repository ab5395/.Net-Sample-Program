using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Web.Mvc;
using PayPal.Api;
using Configuration = PaypalTestApp.Models.Configuration;

#pragma warning disable 168

namespace PaypalTestApp.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PaymentWithCreditCard()
        {
            //create and item for which you are taking payment
            //if you need to add more items in the list
            //Then you will need to create multiple item objects or use some loop to instantiate object

            Item item = new Item
            {
                name = "Demo Item1",
                currency = "USD",
                price = "6",
                quantity = "2",
                sku = "dhghftghjn"
            };

            //Now make a List of Item and add the above item to it
            //you can create as many items as you want and add to this list
            List<Item> itms = new List<Item> {item};
            ItemList itemList = new ItemList {items = itms};

            //Address for the payment
            Address billingAddress = new Address
            {
                city = "NewYork",
                country_code = "US",
                line1 = "23rd street kew gardens",
                postal_code = "43210",
                state = "NY"
            };


            //Now Create an object of credit card and add above details to it
            //Please replace your credit card details over here which you got from paypal
            CreditCard crdtCard = new CreditCard
            {
                billing_address = billingAddress,
                cvv2 = "874",
                expire_month = 1,
                expire_year = 2020,
                first_name = "Aman",
                last_name = "Thakur",
                number = "1234567890123456",
                type = "visa"
            };
            //card cvv2 number
            //card expire date
            //card expire year
            //enter your credit card number here
            //credit card type here paypal allows 4 types

            // Specify details of your payment amount.
            Details details = new Details
            {
                shipping = "1",
                subtotal = "5",
                tax = "1"
            };

            // Specify your total payment amount and assign the details object
            Amount amnt = new Amount
            {
                currency = "USD",
                total = "7",
                details = details
            };
            // Total = shipping tax + subtotal.

            // Now make a transaction object and assign the Amount object

            var guid = Guid.NewGuid().ToString();

            Transaction tran = new Transaction
            {
                amount = amnt,
                description = "Description about the payment amount.",
                item_list = itemList,
                invoice_number = guid
            };

            // Now, we have to make a list of transaction and add the transactions object
            // to this list. You can create one or more object as per your requirements

            List<Transaction> transactions = new List<Transaction>
            {
                tran
            };

            // Now we need to specify the FundingInstrument of the Payer
            // for credit card payments, set the CreditCard which we made above

            FundingInstrument fundInstrument = new FundingInstrument
            {
                credit_card = crdtCard
            };

            // The Payment creation API requires a list of FundingIntrument

            List<FundingInstrument> fundingInstrumentList = new List<FundingInstrument>
            {
                fundInstrument
            };

            // Now create Payer object and assign the fundinginstrument list to the object
            Payer payr = new Payer
            {
                funding_instruments = fundingInstrumentList,
                payment_method = "credit_card"
            };

            // finally create the payment object and assign the payer object & transaction list to it
            Payment pymnt = new Payment
            {
                intent = "sale",
                payer = payr,
                transactions = transactions
            };

            try
            {
                //getting context from the paypal
                //basically we are sending the clientID and clientSecret key in this function
                //to the get the context from the paypal API to make the payment
                //for which we have created the object above.

                //Basically, apiContext object has a accesstoken which is sent by the paypal
                //to authenticate the payment to facilitator account.
                //An access token could be an alphanumeric string

                APIContext apiContext = Configuration.GetAPIContext();

                //Create is a Payment class function which actually sends the payment details
                //to the paypal API for the payment. The function is passed with the ApiContext
                //which we received above.

                Payment createdPayment = pymnt.Create(apiContext);

                //if the createdPayment.state is "approved" it means the payment was successful else not

                if (createdPayment.state.ToLower() != "approved")
                {
                    return View("FailureView");
                }
            }
            catch (PayPal.PayPalException ex)
            {
               // Logger.Log("Error: " + ex.Message);
                return View("FailureView");
            }

            return View("SuccessView");
        }


        public ActionResult PaymentWithPaypal()
        {
            //getting the apiContext as earlier
            APIContext apiContext = Configuration.GetAPIContext();

            try
            {
                string payerId = Request.Params["PayerID"];

                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist
                    //it is returned by the create function call of the payment class

                    // Creating a payment
                    // baseURL is the url on which paypal sendsback the data.
                    // So we have provided URL of this controller only
                    if (Request.Url != null)
                    {
                        string baseUri = Request.Url.Scheme + "://" + Request.Url.Authority +
                                         "/Payment/PaymentWithPayPal?";

                        //guid we are generating for storing the paymentID received in session
                        //after calling the create function and it is used in the payment execution

                        var guid = Convert.ToString((new Random()).Next(100000));

                        //CreatePayment function gives us the payment approval url
                        //on which payer is redirected for paypal account payment

                        var createdPayment = CreatePayment(apiContext, baseUri + "guid=" + guid);

                        //get links returned from paypal in response to Create function call

                        if (createdPayment != null)
                        {
                            var links = createdPayment.links.GetEnumerator();

                            string paypalRedirectUrl = null;

                            while (links.MoveNext())
                            {
                                Links lnk = links.Current;

                                if (lnk != null && lnk.rel.ToLower().Trim().Equals("approval_url"))
                                {
                                    //saving the payapalredirect URL to which user will be redirected for payment
                                    paypalRedirectUrl = lnk.href;
                                }
                            }

                            // saving the paymentID in the key guid
                            Session.Add(guid, createdPayment.id);

                            return Redirect(paypalRedirectUrl);
                        }
                    }
                }
                else
                {
                    // This section is executed when we have received all the payments parameters

                    // from the previous call to the function Create

                    // Executing a payment

                    var guid = Request.Params["guid"];

                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);

                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }

                    var transactionId = executedPayment.transactions[0].related_resources[0].sale.id;


                }
            }
            catch (Exception ex)
            {
                //Logger.log("Error" + ex.Message);
                return View("FailureView");
            }

           
            string txToken = Request.QueryString.Get("tx");

            return View("SuccessView");
        }

        [HttpGet]
        public ActionResult SuccessView()
        {

            string txToken = Request.QueryString.Get("tx");

            if (!string.IsNullOrEmpty(txToken))
            {

                string authToken, query;
                string strResponse, url, postUrl;

                authToken = ConfigurationManager.AppSettings["PDTToken"];

                query = string.Format("cmd=_notify-synch&tx={0}&at={1}", txToken, authToken);

                // Create the request back
                bool useSendbox = Convert.ToBoolean(ConfigurationManager.AppSettings["UseSandbox"]);
                if (useSendbox)
                    postUrl = "https://www.sandbox.paypal.com/cgi-bin/webscr";
                else
                    postUrl = "https://www.paypal.com/cgi-bin/webscr";
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(postUrl);

                // Set values for the request back
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = query.Length;

                // Write the request back IPN strings
                StreamWriter stOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
                stOut.Write(query);
                stOut.Close();

                // Do the request to PayPal and get the response
                StreamReader stIn = new StreamReader(req.GetResponse().GetResponseStream());
                strResponse = stIn.ReadToEnd();
                stIn.Close();

                // If response was SUCCESS, parse response string and output details
                if (strResponse.StartsWith("SUCCESS"))
                {
                    //PDTHolder pdt = PDTHolder.Parse(strResponse);
                    var data = strResponse;
                    // List<string> customList = !string.IsNullOrEmpty(pdt.Custom.TrimNull()) ? pdt.Custom.Trim().Split(new string[] { ";@;" }, StringSplitOptions.None).ToList() : null;
                    // string traineeId = null, tokenId = null;
                    // if (customList != null && customList.Count == 2)
                    // {
                    //     traineeId = customList[0];
                    //     tokenId = customList[1];
                    // }
                    // url = "Payment/PayPalResponse";
                    // RegistrationPayment objRegistrationPayment = new RegistrationPayment();
                    // objRegistrationPayment.TransactionID = pdt.TransactionId;
                    // objRegistrationPayment.FirstName = pdt.PayerFirstName;
                    // objRegistrationPayment.LastName = pdt.PayerLastName;
                    // objRegistrationPayment.Email = pdt.PayerEmail;
                    // objRegistrationPayment.TotalAmount = pdt.GrossTotal;
                    // objRegistrationPayment.Response = strResponse;
                    // objRegistrationPayment.GatewayName = "paypal";
                    // //objRegistrationPayment.TraineeID = Convert.ToInt32(traineeId);
                    //// AsapResponse<RegistrationPayment> response = WebClientBase.PostRequestJSON<AsapResponse<RegistrationPayment>>(url, objRegistrationPayment, traineeId, tokenId);
                    // ViewBag.PaypalPayment = "PaypalPayment";
                    //  ViewBag.TransactionID = response.jsonObj.TransactionID;
                }
                else
                {
                    //Label1.Text = "Oooops, something went wrong...";
                }
            }
            return View();
        }

        public ActionResult FailureView()
        {
            return View();
        }

        private Payment _payment;

        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            _payment = new Payment { id = paymentId };
            return _payment.Execute(apiContext, paymentExecution);
        }


        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            Random random = new Random();

            //similar to credit card create itemlist and add item objects to it
            var itemList = new ItemList() { items = new List<Item>() };

            itemList.items.Add(new Item()
            {
                name = "Item Name 1",
                currency = "USD",
                price = "10",
                quantity = "1",
                sku = random.Next(999, 99999999).ToString(),//"your invoice number",
            });

            var payer = new Payer { payment_method = "paypal" };

            // Configure Redirect Urls here with RedirectUrls object
            var redirUrls = new RedirectUrls
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };

            // similar as we did for credit card, do here and create details object
            var details = new Details()
            {
                tax = "1",
                shipping = "2",
                subtotal = "10"
            };

            // similar as we did for credit card, do here and create amount object
            var amount = new Amount()
            {
                currency = "USD",
                total = "13", // Total must be equal to sum of shipping, tax and subtotal.
                details = details
            };

            var transactionList = new List<Transaction>
            {
                new Transaction()
                {
                    description = "Transaction For Test.",
                    invoice_number = random.Next(999, 99999999).ToString(),//"your invoice number",
                    amount = amount,
                    item_list = itemList
                }
            };


            _payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            // Create a payment using a APIContext
            return _payment.Create(apiContext);
        }
    }
}