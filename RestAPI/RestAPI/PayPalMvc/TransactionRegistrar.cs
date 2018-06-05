using System.Web.Routing;
using System;
using System.Web.Configuration;
using System.Collections.Generic;

namespace PayPalMvc
{
	/// <summary>
	/// Default ITransactionRegistrar implementation
	/// </summary>
	public class TransactionRegistrar : ITransactionRegistrar
    {
		readonly Configuration configuration;
		readonly IHttpRequestSender requestSender;
        private HttpPostSerializer serializer;
        private ResponseSerializer deserializer;
		
        /// <summary>
		/// Creates a new instance of the TransactionRegistrar using the configuration specified in the web.conf, and an HTTP Request Sender.
		/// </summary>
		public TransactionRegistrar() : this(Configuration.Current, new HttpRequestSender()) {
		}

		/// <summary>
		/// Creates a new instance of the TransactionRegistrar
		/// </summary>
		public TransactionRegistrar(Configuration configuration, IHttpRequestSender requestSender)
        {
			this.configuration = configuration;
			this.requestSender = requestSender;
            this.serializer = new HttpPostSerializer();
            this.deserializer = new ResponseSerializer();
		}

        // See ITransactionRegistrar for parameter descriptions
        public SetExpressCheckoutResponse SendSetExpressCheckout(string currencyCode, decimal amount, string description, string trackingReference, string serverURL, List<ExpressCheckoutItem> purchaseItems = null, string userEmail = null)
        {
            SetExpressCheckoutRequest request = new SetExpressCheckoutRequest(currencyCode, amount, description, trackingReference, serverURL, purchaseItems, userEmail);
            
            string postData = serializer.Serialize(request);
            Logging.LogLongMessage("PayPal Send Request", "Serlialized Request to PayPal API: " + postData);
            
            string response = requestSender.SendRequest(Configuration.Current.PayPalAPIUrl, postData);
            string decodedResponse = System.Web.HttpUtility.UrlDecode(response, System.Text.Encoding.Default);
            Logging.LogLongMessage("PayPal Response Recieved", "Decoded Respose from PayPal API: " + decodedResponse);
            
            return deserializer.Deserialize<SetExpressCheckoutResponse>(decodedResponse);
        }

        // See ITransactionRegistrar for parameter descriptions
        public GetExpressCheckoutDetailsResponse SendGetExpressCheckoutDetails(string token)
        {
            GetExpressCheckoutDetailsRequest request = new GetExpressCheckoutDetailsRequest(token);
            
            string postData = serializer.Serialize(request);
            Logging.LogLongMessage("PayPal Send Request", "Serlialized Request to PayPal API: " + postData);
            
            string response = requestSender.SendRequest(Configuration.Current.PayPalAPIUrl, postData);
            string decodedResponse = System.Web.HttpUtility.UrlDecode(response, System.Text.Encoding.Default);
            Logging.LogLongMessage("PayPal Response Recieved", "Decoded Respose from PayPal API: " + decodedResponse);
            
            return deserializer.Deserialize<GetExpressCheckoutDetailsResponse>(decodedResponse);
        }

        // See ITransactionRegistrar for parameter descriptions
        public DoExpressCheckoutPaymentResponse SendDoExpressCheckoutPayment(string token, string payerId, string currencyCode, decimal amount)
        {
            DoExpressCheckoutPaymentRequest request = new DoExpressCheckoutPaymentRequest(token, payerId, currencyCode, amount);
            
            string postData = serializer.Serialize(request);
            Logging.LogLongMessage("PayPal Send Request", "Serlialized Request to PayPal API: " + postData);
            
            string response = requestSender.SendRequest(Configuration.Current.PayPalAPIUrl, postData);
            string decodedResponse = System.Web.HttpUtility.UrlDecode(response, System.Text.Encoding.Default);
            Logging.LogLongMessage("PayPal Response Recieved", "Decoded Respose from PayPal API: " + decodedResponse);
            
            return deserializer.Deserialize<DoExpressCheckoutPaymentResponse>(decodedResponse);
        }
	}
}