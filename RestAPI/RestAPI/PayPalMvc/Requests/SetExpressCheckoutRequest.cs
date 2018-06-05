using PayPalMvc.Enums;
using System.Collections.Generic;
using System.Globalization;
namespace PayPalMvc
{
    /// <summary>
    /// Represents a transaction registration that is sent to PayPal. 
    /// This should be serialized using the HttpPostSerializer.
    /// </summary>
    public class SetExpressCheckoutRequest : CommonRequest
    {
        readonly string landingpage;
        readonly string solutiontype;
        readonly string usercreditcard;
        readonly string locatecode;
        readonly PaymentAction paymentAction;
        
        readonly string currencyCode;
        readonly decimal totalAmount;
        readonly string paymentDescription;
        readonly string trackingReference;

        readonly List<ExpressCheckoutItem> items;

        readonly string serverURL;
        readonly string email;

        // See ITransactionRegister for parameter descriptions
        public SetExpressCheckoutRequest(string currencyCode, decimal totalAmount, string paymentDescription, string trackingReference, string serverURL, List<ExpressCheckoutItem> purchaseItems = null, string userEmail = null)
        {
            base.method = RequestType.SetExpressCheckout;
            this.landingpage = "Billing";
            this.solutiontype = "Sole";
            this.usercreditcard = "CreditCard";
            this.locatecode = "us";
            this.paymentAction = PaymentAction.Sale;

            this.currencyCode = currencyCode;
            this.totalAmount = totalAmount;
            this.paymentDescription = paymentDescription;
            this.trackingReference = trackingReference;

            this.serverURL = serverURL;
            this.items = purchaseItems;
            this.email = userEmail;
        }

        public string PAYMENTREQUEST_0_PAYMENTACTION
        {
            get { return paymentAction.ToString(); }
        }

        public string PAYMENTREQUEST_0_CURRENCYCODE
        {
            get { return currencyCode; }
        }

        public string PAYMENTREQUEST_0_AMT
        {
            get { return totalAmount.ToString(new CultureInfo("en-US", false)); }
        }

        public string PAYMENTREQUEST_0_DESC
        {
            get { return paymentDescription; }
        }

        public string PAYMENTREQUEST_0_INVNUM
        {
            get { return trackingReference; }
        }

        public string LANDINGPAGE
        {
            get { return landingpage; }
        }
        public string SOLUTIONTYPE
        {
            get { return solutiontype; }
        }

        public string USERSELECTEDFUNDINGSOURCE
        {
            get { return usercreditcard; }
        }
        public string LOCALECODE
        {
            get { return locatecode; }
        }

        [Optional]
        public string EMAIL
        {
            get { return email; }
        }

        public string RETURNURL
        {
            get { return serverURL + Configuration.Current.ReturnAction; }
        }

        public string CANCELURL
        {
            get { return serverURL + Configuration.Current.CancelAction; }
        }

        // Optional List of Items in this purchase
        [Optional]
        public List<ExpressCheckoutItem> Items
        {
            get { return this.items; }
        }
    }
}