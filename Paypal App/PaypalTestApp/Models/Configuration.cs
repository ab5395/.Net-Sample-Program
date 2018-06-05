using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PayPal.Api;

namespace PaypalTestApp.Models
{
    public static class Configuration
    {
        //these variables will store the clientID and clientSecret
        //by reading them from the web.config
        public readonly static string ClientId;
        public readonly static string ClientSecret;

        static Configuration()
        {
            var config = GetConfig();
            ClientId = config["clientId"];
            ClientSecret = config["clientSecret"];
        }

        // getting properties from the web.config
        public static Dictionary<string, string> GetConfig()
        {
            //return PayPal.Manager.ConfigManager.Instance.GetProperties();

            return new Dictionary<string, string>() {
                { "clientId", "AaWORZ2zzMqccbG5VNVcugGjl6Y7n4cWVLylDi8iI_WpVK2FPbRZD9ZCVvY_klHc5IVpYzm2BipdUtT8" },
                { "clientSecret", "EGHudZOGjC25dPjTxwAkH6Hy9wXSLQkUQpgOxPDRVkhDjUAkLQmDiBv50p313Rh2SjLJgepK4A9TKns3" },
                { "requestRetries", "1" },
                { "connectionTimeout", "360000" },
                { "mode", "sandbox" }
            };
        }

        private static string GetAccessToken()
        {
            // getting accesstocken from paypal                
            string accessToken = new OAuthTokenCredential
                (ClientId, ClientSecret, GetConfig()).GetAccessToken();

            return accessToken;
        }

        public static APIContext GetAPIContext()
        {
            // return apicontext object by invoking it with the accesstoken
            APIContext apiContext = new APIContext(GetAccessToken());
            apiContext.Config = GetConfig();
            return apiContext;
        }
    }
}