using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayPalMvc.Enums
{
    public enum PaymentStatus
    {
        None,
        CanceledReversal, // Canceled-Reversal
        Completed,
        Denied,
        Expired,
        Failed,
        InProgress, // In-Progress
        PartiallyRefunded, // Partially-Refunded
        Pending,
        Refunded,
        Reversed,
        Processed,
        Voided,
        CompletedFundsHeld, // Completed-Funds-Held
        OtherOrUnknown,
    }

    public class PaymentStatuses
    {
        /// <summary>
        /// Utility method for converting a string into a CheckoutStatus. 
        /// </summary>
        public static PaymentStatus ConvertStringToPayPalCheckoutStatus(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                if (input.StartsWith("None"))
                {
                    return PaymentStatus.None;
                }

                if (input.StartsWith("Completed"))
                {
                    return PaymentStatus.Completed;
                }

                if (input.StartsWith("Expired"))
                {
                    return PaymentStatus.Expired;
                }

                if (input.StartsWith("Failed"))
                {
                    return PaymentStatus.Failed;
                }

                if (input.StartsWith("In-Progress"))
                {
                    return PaymentStatus.InProgress;
                }

                if (input.StartsWith("Pending"))
                {
                    return PaymentStatus.Pending;
                }

                if (input.StartsWith("Processed"))
                {
                    return PaymentStatus.Processed;
                }

                if (input.StartsWith("Voided"))
                {
                    return PaymentStatus.Voided;
                }
            }
            return PaymentStatus.OtherOrUnknown;
        }
    }
}
