namespace PayPalMvc {
	/// <summary>
	/// Response received from a transaction registration
	/// </summary>
	public class SetExpressCheckoutResponse : CommonResponse {
		/// <summary>
		/// Transaction Token
		/// </summary>
		public string TOKEN { get; set; } // Stored
	}
}