using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.Linq.Expressions;
using PayPalMvc.Enums;

namespace PayPalMvc
{
    /// <summary>
    /// Configuration data
    /// </summary>
    public class Configuration
    {
        public static CultureInfo CultureForTransactionEncoding = new CultureInfo("en-gb");

        // Static PayPal properties
        public const string Version = "109.0";

        private const string LiveUrl = "https://api-3t.paypal.com/nvp";
        private const string RedirectLiveUrl = "https://www.paypal.com/webscr?cmd=_express-checkout&token={0}";

        private const string SandboxUrl = "https://api-3t.sandbox.paypal.com/nvp";
        //private const string SandboxUrl = "https://api-3t.sandbox.paypal.com/nvp?LOCALECODE=us&LANDINGPAGE=Billing&SOLUTIONTYPE=Sole&USERSELECTEDFUNDINGSOURCE=CreditCard";

        private const string RedirectSandboxUrl = "https://www.sandbox.paypal.com/webscr?cmd=_express-checkout&token={0}";
        //private const string RedirectSandboxUrl = "https://www.sandbox.paypal.com/webscr?cmd=_express-checkout&token={0}&LOCALECODE=us&LANDINGPAGE=Billing&SOLUTIONTYPE=Sole&USERSELECTEDFUNDINGSOURCE=CreditCard";

        // Application specific properties
        private string merchantUserName;
        private string merchantPassword;
        private string signature;

        private string cancelAction;
        private string returnAction;

        /// <summary>
        /// User name. This is required.
        /// </summary>
        public string MerchantUserName
        {
            get
            {
                if (string.IsNullOrEmpty(merchantUserName))
                    throw new ArgumentNullException("MerchantUserName", "MerchantUserName must be specified in the configuration.");
                return merchantUserName;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("MerchantUserName", "MerchantUserName must be specified in the configuration.");
                merchantUserName = value;
            }
        }


        /// <summary>
        /// User Password. This is required. 
        /// </summary>
        public string MerchantPassword
        {
            get
            {
                if (string.IsNullOrEmpty(merchantPassword))
                    throw new ArgumentNullException("MerchantPassword", "MerchantPassword must be specified in the configuration.");
                return merchantPassword;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("MerchantPassword", "MerchantPassword must be specified in the configuration.");
                merchantPassword = value;
            }
        }

        /// <summary>
        /// User Signature. This is required. 
        /// </summary>
        public string Signature
        {
            get
            {
                if (string.IsNullOrEmpty(signature))
                    throw new ArgumentNullException("Signature", "Signature must be specified in the configuration.");
                return signature;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Signature", "Signature must be specified in the configuration.");
                signature = value;
            }
        }

        /// <summary>
        /// Cancel Action. This is required. 
        /// </summary>
        public string CancelAction
        {
            get
            {
                if (string.IsNullOrEmpty(cancelAction))
                    throw new ArgumentNullException("CancelAction", "CancelAction must be specified in the configuration.");
                return cancelAction;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("CancelAction", "CancelAction must be specified in the configuration.");
                cancelAction = value;
            }
        }

        /// <summary>
        /// Return Action. This is required. 
        /// </summary>
        public string ReturnAction
        {
            get
            {
                if (string.IsNullOrEmpty(returnAction))
                    throw new ArgumentNullException("ReturnAction", "ReturnAction must be specified in the configuration.");
                return returnAction;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("ReturnAction", "ReturnAction must be specified in the configuration.");
                returnAction = value;
            }
        }

        /// <summary>
        /// Server mode (sandbox, live)
        /// </summary>
        public ServerMode Mode { get; set; }

        static Configuration currentConfiguration;

        /// <summary>
        /// Sets up the configuration using a manually generated Configuration instance rather than using the Web.config file. 
        /// </summary>
        /// <param name="configuration"></param>
        public static void Configure(Configuration configuration)
        {
            currentConfiguration = configuration;
        }

        /// <summary>
        /// Gets the current configuration. If none has been specified using Configuration.Configure, it is loaded from the web.config
        /// </summary>
        public static Configuration Current
        {
            get
            {
                if (currentConfiguration == null)
                    currentConfiguration = LoadConfigurationFromConfigFile();

                return currentConfiguration;
            }
        }

        /// <summary>
        /// The PayPal Server URL
        /// </summary>
        public string PayPalAPIUrl
        {
            get
            {
                switch (Mode)
                {
                    case ServerMode.Sandbox:
                        return SandboxUrl;
                    case ServerMode.Live:
                        return LiveUrl;
                }
                return null;
            }
        }

        /// <summary>
        /// The PayPal Redirect URL
        /// </summary>
        public string PayPalRedirectUrl
        {
            get
            {
                switch (Mode)
                {
                    case ServerMode.Sandbox:
                        return RedirectSandboxUrl;
                    case ServerMode.Live:
                        return RedirectLiveUrl;
                }
                return null;
            }
        }

        static Configuration LoadConfigurationFromConfigFile()
        {
            var section = ConfigurationManager.GetSection("payPal") as NameValueCollection;

            if (section == null)
                return new Configuration();

            var configuration = new Configuration
            {
                Mode = (ServerMode)Enum.Parse(typeof(ServerMode), (GetValue(x => x.Mode, section) ?? "Sandbox")),
                MerchantUserName = GetValue(x => x.MerchantUserName, section),
                MerchantPassword = GetValue(x => x.MerchantPassword, section),
                Signature = GetValue(x => x.Signature, section),
                CancelAction = GetValue(x => x.CancelAction, section),
                ReturnAction = GetValue(x => x.ReturnAction, section)
            };

            return configuration;
        }

        static string GetValue(Expression<Func<Configuration, object>> expression, NameValueCollection collection)
        {
            var body = expression.Body as MemberExpression;
            if (body == null && expression.Body is UnaryExpression)
                body = ((UnaryExpression)expression.Body).Operand as MemberExpression;

            string name = body.Member.Name;
            return collection[name];
        }
    }
}