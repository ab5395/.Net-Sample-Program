using PayPalMvc.Enums;
namespace PayPalMvc {
	/// <summary>
	/// Response received from a checkout details request
	/// </summary>
    public class GetExpressCheckoutDetailsResponse : CommonPaymentResponse
    {
        // PayPal Response properties
        public CheckoutStatus CHECKOUTSTATUS { get; set; }
        public string PAYMENTREQUEST_0_INVNUM { get; set; } // Should be the same as the baskedId we passed in as the tracking reference for the transaction registration
        public string PAYERSTATUS { get; set; }
        public string PAYERID { get; set; } // Stored
        public string EMAIL { get; set; }
        public string SALUTATION { get; set; }
        public string FIRSTNAME { get; set; }
        public string MIDDLENAME { get; set; }
        public string LASTNAME { get; set; }

        // Human Readable re-mapped properties
        public string TrackingReference { get { return PAYMENTREQUEST_0_INVNUM; } } // Stored

        public string ToString // Stored
        {
            get
            {
                return string.Format("Checkout Status: [{0}] Payer Status: [{1}] Name: [{2} {3} {4} {5}] PayPal Email: [{6}]", CHECKOUTSTATUS.ToString(), PAYERSTATUS, SALUTATION, FIRSTNAME, MIDDLENAME, LASTNAME, EMAIL);
            }
        }
	}
}