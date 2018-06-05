using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace PayPalMvc
{
    public class Logging
    {
        // Add your favourite logger here

        public static void LogMessage(string message)
        {
            DoTrace(message);
        }

        public static void LogLongMessage(string message, string longMessage)
        {
            DoTrace(message);
            DoTrace(longMessage);
        }

        private static void DoTrace(string message)
        {
            Trace.WriteLine(DateTime.Now + " - " + message);
        }
    }
}
