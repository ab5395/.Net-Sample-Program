using PayPalMvc.Enums;
namespace PayPalMvc {
	/// <summary>
	/// Response received from a take payment request
	/// </summary>
    public class DoExpressCheckoutPaymentResponse : CommonPaymentResponse
    {
        // PayPal Response properties
        public string LANDINGPAGE { get; set; }
        public string SOLUTIONTYPE { get; set; }

        public PaymentStatus PAYMENTINFO_0_PAYMENTSTATUS { get; set; }
        public string PAYMENTINFO_0_PAYMENTTYPE { get; set; }
        public string PAYMENTINFO_0_TRANSACTIONID { get; set; } 
        public string PAYMENTINFO_0_TRANSACTIONTYPE { get; set; }
        public string PAYMENTINFO_0_ORDERTIME { get; set; }
        public string PAYMENTINFO_0_AMT { get; set; }
        public string PAYMENTINFO_0_CURRENCYCODE { get; set; }
        public string PAYMENTINFO_0_FEEAMT { get; set; }
        public string PAYMENTINFO_0_PENDINGREASON { get; set; } // Can check why PaymentStatus is Pending

        // Human Readable re-mapped properties
        public PaymentStatus PaymentStatus { get { return PAYMENTINFO_0_PAYMENTSTATUS; } }
        public string PaymentTransactionId { get { return PAYMENTINFO_0_TRANSACTIONID; } } // Stored
        public decimal PaymentAmount { get { return decimal.Parse(PAYMENTINFO_0_AMT); } }
        public decimal PaymentPortionPayPalFees { get { return decimal.Parse(PAYMENTINFO_0_FEEAMT); } }

        public string ToString // Stored
        {
            get
            {
                return string.Format("Payment Status: [{0}] Payment Type: [{1}] Transaction Type: [{2}] Order Time: [{3}] Currency Code: [{4}] Amount: [{5}] Fees: [{6}]", PaymentStatus, PAYMENTINFO_0_PAYMENTTYPE, PAYMENTINFO_0_TRANSACTIONTYPE, PAYMENTINFO_0_ORDERTIME, PAYMENTINFO_0_CURRENCYCODE, PAYMENTINFO_0_AMT, PAYMENTINFO_0_FEEAMT);
            }
        }
	}
}