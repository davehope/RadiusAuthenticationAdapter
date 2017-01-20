using System;
using System.Diagnostics;

namespace RadiusAuthenticationAdapter
{
    class Logging
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Logging()
        {
        }


        /// <summary>
        /// Logs information to the Windows Application Event Log. An Event 
        /// Source must exist first.
        /// </summary>
        /// <param name="message">Message to log</param>
        public static void LogMessage(string message)
        {
            using (EventLog eventLog = new EventLog())
            {
                eventLog.Source = "RADIUS Authentication Adapter";
                eventLog.WriteEntry(message, EventLogEntryType.Information, 1001);
            }
        }
    }
}
