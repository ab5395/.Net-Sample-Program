namespace PayPalMvc.Enums
{
	/// <summary>
	/// Response types that could be received from PayPal
	/// </summary>
	public enum ResponseType {
		Unknown,
		Success,
        SuccessWithWarning,
        Failure,
        FailureWithWarning,
	}

    public class ResponseTypes
    {
        /// <summary>
        /// Utility method for converting a string into a ResponseType. 
        /// </summary>
        public static ResponseType ConvertStringToPayPalResponseType(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                if (input.StartsWith("Success"))
                {
                    return ResponseType.Success;
                }

                if (input.StartsWith("SuccessWithWarning"))
                {
                    return ResponseType.SuccessWithWarning;
                }

                if (input.StartsWith("Failure"))
                {
                    return ResponseType.Failure;
                }

                if (input.StartsWith("FailureWithWarning"))
                {
                    return ResponseType.FailureWithWarning;
                }
            }
            return ResponseType.Unknown;
        }
    }

}