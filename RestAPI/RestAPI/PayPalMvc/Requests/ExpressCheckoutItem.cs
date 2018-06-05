using PayPalMvc.Enums;
using System.Collections.Generic;
using System.Globalization;
namespace PayPalMvc
{
    /// <summary>
    /// Optional Express Checkout items
    /// </summary>
    public class ExpressCheckoutItem
    {
        readonly int quantity;
        readonly decimal amount;
        readonly string name;
        readonly string description;

        public ExpressCheckoutItem(int quantity, decimal amount, string name, string description = null)
        {
            this.quantity = quantity;
            this.amount = amount;
            this.name = name;
            this.description = description;
        }

        // Note "_mIndex" gets replaced with the item number when this object gets serialized
        public string L_PAYMENTREQUEST_0_QTY_mIndex
        {
            get { return quantity.ToString(); }
        }

        public string L_PAYMENTREQUEST_0_AMT_mIndex
        {
            get { return amount.ToString(new CultureInfo("en-US", false)); }
        }

        public string L_PAYMENTREQUEST_0_NAME_mIndex
        {
            get { return name; }
        }

        /// <summary>
        /// Optional Item description
        /// </summary>
        public string L_PAYMENTREQUEST_0_DESC_mIndex
        {
            get { return description; }
        }
    }
}