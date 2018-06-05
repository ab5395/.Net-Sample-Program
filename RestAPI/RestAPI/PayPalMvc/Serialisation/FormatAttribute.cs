using System;

namespace PayPalMvc {
	/// <summary>
	/// Specifies a format to use when serializing.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class FormatAttribute : Attribute {
		public string Format { get; private set; }

		public FormatAttribute(string format) {
			Format = format;
		}
	}
}