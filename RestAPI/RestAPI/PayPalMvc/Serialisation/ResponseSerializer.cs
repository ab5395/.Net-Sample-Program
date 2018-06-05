using System;
using System.Reflection;
using PayPalMvc.Enums;

namespace PayPalMvc {
	/// <summary>
	/// Used for deserializing SagePay response data. 
	/// </summary>
	public class ResponseSerializer {

        /// <summary>
		/// Deserializes the response into an instance of type T.
		/// </summary>
		public void Deserialize<T>(string input, T objectToDeserializeInto)
        {
			Deserialize(typeof (T), input, objectToDeserializeInto);
		}

		/// <summary>
		/// Deserializes the response into an object of type T.
		/// </summary>
		public T Deserialize<T>(string input) where T : new()
        {
			var instance = new T();
			Deserialize(typeof (T), input, instance);
			return instance;
		}

		/// <summary>
		/// Deserializes the response into an object of the specified type.
		/// </summary>
		public void Deserialize(Type type, string input, object objectToDeserializeInto)
        {
			if (string.IsNullOrEmpty(input)) return;

			var bits = input.Split(new[] {"&"}, StringSplitOptions.RemoveEmptyEntries);

			foreach (var nameValuePairCombined in bits) {
                int index = nameValuePairCombined.IndexOf('=');
                if (index < 0)
                {
                    Logging.LogMessage("Could not deserialize NameValuePair: " + nameValuePairCombined);
                    continue;
                }
                string name = nameValuePairCombined.Substring(0, index);
				string value = nameValuePairCombined.Substring(index + 1);
                PropertyInfo prop = type.GetProperty(name, BindingFlags.Public | BindingFlags.Instance);

				if (prop == null) {
					// Ignore any additional NVPs that we don't have properties for instead of throwing exception
                    // This does mean we only capture the first of any errors returned into L_ERRORCODE0 etc
                    continue; // throw new InvalidOperationException(string.Format("Could not find a property on Type '{0}' named '{1}'", type.Name, name));
				}

				object convertedValue;

				if (prop.PropertyType == typeof (ResponseType))
                    convertedValue = ResponseTypes.ConvertStringToPayPalResponseType(value);
                else if (prop.PropertyType == typeof(CheckoutStatus))
                    convertedValue = CheckoutStatuses.ConvertStringToPayPalCheckoutStatus(value);
                else if (prop.PropertyType == typeof(PaymentStatus))
                    convertedValue = PaymentStatuses.ConvertStringToPayPalCheckoutStatus(value);
                else
                    convertedValue = Convert.ChangeType(value, prop.PropertyType);

				prop.SetValue(objectToDeserializeInto, convertedValue, null);
			}
		}

		/// <summary>
		/// Deserializes the response into an object of the specified type.
		/// </summary>
		public object Deserialize(Type type, string input)
        {
			var instance = Activator.CreateInstance(type);
			Deserialize(type, input, instance);
			return instance;
		}
	}
}