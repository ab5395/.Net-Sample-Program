using PayPalMvc.Enums;
namespace PayPalMvc {
	/// <summary>
	/// Response received from a transaction registration
	/// </summary>
	public abstract class CommonPaymentResponse : CommonResponse {

        /// <summary>
        /// Transaction Token
        /// </summary>
        public string TOKEN { get; set; } // Stored

        // For payment error capturing
        public string PAYMENTREQUEST_0_ERRORCODE { get; set; }
        public string PAYMENTREQUEST_0_SHORTMESSAGE { get; set; }
        public string PAYMENTREQUEST_0_LONGMESSAGE { get; set; }
        public string PAYMENTREQUEST_0_SEVERITYCODE { get; set; }

        public string PaymentErrorToString // Stored
        {
            get
            {
                if (PAYMENTREQUEST_0_ERRORCODE != null || PAYMENTREQUEST_0_SEVERITYCODE != null || PAYMENTREQUEST_0_SHORTMESSAGE != null || PAYMENTREQUEST_0_LONGMESSAGE != null)
                    return string.Format("Payment Error Code: {0} Severity: {1} Message: {2} ({3})", PAYMENTREQUEST_0_ERRORCODE, PAYMENTREQUEST_0_SEVERITYCODE, PAYMENTREQUEST_0_SHORTMESSAGE, PAYMENTREQUEST_0_LONGMESSAGE);
                else
                    return null;
            }
        }
	}
}