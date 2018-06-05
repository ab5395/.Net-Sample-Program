using PayPalMvc.Enums;
using System.Globalization;
namespace PayPalMvc
{
    /// <summary>
    /// Represents a transaction registration that is sent to PayPal. 
    /// This should be serialized using the HttpPostSerializer.
    /// </summary>
    public class DoExpressCheckoutPaymentRequest : CommonRequest
    {
        readonly PaymentAction paymentAction;
        
        readonly string token;
        readonly string payerId;

        readonly string currencyCode;
        readonly decimal amount;

        // See ITransactionRegister for parameter descriptions
        public DoExpressCheckoutPaymentRequest(string token, string payerId, string currencyCode, decimal amount)
        {
            base.method = RequestType.DoExpressCheckoutPayment;
            this.paymentAction = PaymentAction.Sale;

            this.token = token;
            this.payerId = payerId;

            this.currencyCode = currencyCode;
            this.amount = amount;
        }

        public string TOKEN
        {
            get { return token; }
        }

        public string PAYERID
        {
            get { return payerId; }
        }

        public string PAYMENTREQUEST_0_CURRENCYCODE
        {
            get { return currencyCode; }
        }

        public string PAYMENTREQUEST_0_AMT
        {
            get { return amount.ToString(new CultureInfo("en-US", false)); }
        }

        public string PAYMENTREQUEST_0_PAYMENTACTION 
        {
            get { return paymentAction.ToString(); }
        }
    }
}