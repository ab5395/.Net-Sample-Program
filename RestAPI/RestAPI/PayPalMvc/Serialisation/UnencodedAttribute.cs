using System;

namespace PayPalMvc {
	/// <summary>
	/// Specifies that a property should not be URL Encoded when being serialized by the HttpPostSerialzier
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class UnencodedAttribute : Attribute {
	}
}