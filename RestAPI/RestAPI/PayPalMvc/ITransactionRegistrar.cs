using System.Web.Routing;
using System.Collections.Generic;

namespace PayPalMvc {
	public interface ITransactionRegistrar {
        
        /// <summary>
        /// Setup the Express Checkout request with PayPal
        /// This sets up the sale for X value in Y currency against a sale description (with optional items)
        /// </summary>
        /// <param name="currencyCode">Currency Code to use for sale</param>
        /// <param name="amount">Total amount of sale</param>
        /// <param name="description">Description that PayPal will show to users for this sale (this will appear in the order history)</param>
        /// <param name="trackingReference">Unique tracking references for this sale</param>
        /// <param name="serverURL">Your server URL (Cancel/Return Actions get appended to this)</param>
        /// <param name="purchaseItems">Optional list of individual items being sold in the single payment transaction (note these are NOT stored by PayPal against the order)</param>
        /// <param name="userEmail">Optional email for user making purchase</param>
        /// <returns>SetExpressCheckoutResponse from PayPal</returns>
        SetExpressCheckoutResponse SendSetExpressCheckout(string currencyCode, decimal amount, string description, string trackingReference, string serverURL, List<ExpressCheckoutItem> purchaseItems = null, string userEmail = null);

        /// <summary>
        /// Get PayPal purchase status for the sale and the PayPal account details used for purchase
        /// </summary>
        /// <param name="token">The Express Checkout token for this sale</param>
        /// <returns>GetExpressCheckoutDetailsResponse from PayPal</returns>
        GetExpressCheckoutDetailsResponse SendGetExpressCheckoutDetails(string token);

        /// <summary>
        /// Request payment to be taken by PayPal for the sale
        /// </summary>
        /// <param name="token">The Express Checkout token for this sale</param>
        /// <param name="payerId">The PayerId of the PayPal account used for this purchase</param>
        /// <param name="currencyCode">Currency Code to use for sale</param>
        /// <param name="amount">Total amount of sale</param>
        /// <returns>DoExpressCheckoutPaymentResponse from PayPal</returns>
        DoExpressCheckoutPaymentResponse SendDoExpressCheckoutPayment(string token, string payerId, string currencyCode, decimal amount);
    }
}