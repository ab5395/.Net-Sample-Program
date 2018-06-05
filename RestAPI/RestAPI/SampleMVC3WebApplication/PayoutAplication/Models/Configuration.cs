using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayoutAplication.Models
{
    public static class Configuration
    {
        public readonly static string ClientId;
        public readonly static string ClientSecret;

        // Static constructor for setting the readonly static members.
        static Configuration()
        {
            var config = GetConfig();
            ClientId = config["clientId"];
            ClientSecret = config["clientSecret"];
        }

        // Create the configuration map that contains mode and other optional configuration details.
        public static Dictionary<string, string> GetConfig()
        {
            //return ConfigManager.Instance.GetProperties();
            //Default Paypal Test data
            //return new Dictionary<string, string>() {
            //    { "clientId", "AUASNhD7YM7dc5Wmc5YE9pEsC0o4eVOyYWO9ezXWBu2XTc63d3Au_s9c-v-U" },
            //    { "clientSecret", "EBq0TRAE-4R9kgCDKzVh09sm1TeNcuY-xJirid7LNtheUh5t5vlOhR0XSHt3" },
            //    { "requestRetries", "1" },
            //    { "connectionTimeout", "360000" },
            //    { "mode", "sandbox" }
            //};

            //NG Test Data
            return new Dictionary<string, string>() {
                { "clientId", "ARrIU1SKSsABGqpdlhGjO7utGXRyjJNMzZKiJ1PMc1WXf4yplj5U_fV6KG3bawFEPIwe13d-ZOGghj0S" },
                { "clientSecret", "ENPkXTZA_kOBY_1VMOs74D8TwD60pGHeda3zNRwiOiRlXmPa-JZD6y2UwKpZVNxRgBHWCpXhc7rv6Ouj" },
                { "requestRetries", "1" },
                { "connectionTimeout", "360000" },
                { "mode", "sandbox" }
            };
        }

        // Create accessToken
        private static string GetAccessToken()
        {
            string accessToken = new OAuthTokenCredential
               (ClientId, ClientSecret, GetConfig()).GetAccessToken();

            return accessToken;
        }

        // Returns APIContext object
        public static APIContext GetAPIContext()
        {
            // ### Api Context
            // Pass in a `APIContext` object to authenticate 
            // the call and to send a unique request id 
            // (that ensures idempotency). The SDK generates
            // a request id if you do not pass one explicitly. 
            var apiContext = new APIContext(GetAccessToken());
            apiContext.Config = GetConfig();

            // Use this variant if you want to pass in a request id  
            // that is meaningful in your application, ideally 
            // a order id.
            // String requestId = Long.toString(System.nanoTime();
            // APIContext apiContext = new APIContext(GetAccessToken(), requestId ));

            return apiContext;
        }

    }
}