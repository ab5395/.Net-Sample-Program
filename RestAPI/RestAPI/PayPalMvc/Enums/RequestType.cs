namespace PayPalMvc.Enums
{
	/// <summary>
	/// Request Types that can be sent to PayPal
	/// </summary>
	public enum RequestType {
		SetExpressCheckout,
		GetExpressCheckoutDetails,
        DoExpressCheckoutPayment,
	}

}