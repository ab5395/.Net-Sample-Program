using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayPalMvc.Enums
{
    public enum CheckoutStatus
    {
        PaymentActionNotInitiated,
        PaymentActionFailed,
        PaymentActionInProgress,
        PaymentCompleted,
        Unknown,
    }

    public class CheckoutStatuses
    {
        /// <summary>
        /// Utility method for converting a string into a CheckoutStatus. 
        /// </summary>
        public static CheckoutStatus ConvertStringToPayPalCheckoutStatus(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                if (input.StartsWith("PaymentActionNotInitiated"))
                {
                    return CheckoutStatus.PaymentActionNotInitiated;
                }

                if (input.StartsWith("PaymentActionFailed"))
                {
                    return CheckoutStatus.PaymentActionFailed;
                }

                if (input.StartsWith("PaymentActionInProgress"))
                {
                    return CheckoutStatus.PaymentActionInProgress;
                }

                if (input.StartsWith("PaymentCompleted"))
                {
                    return CheckoutStatus.PaymentCompleted;
                }
            }
            return CheckoutStatus.Unknown;
        }
    }
}
