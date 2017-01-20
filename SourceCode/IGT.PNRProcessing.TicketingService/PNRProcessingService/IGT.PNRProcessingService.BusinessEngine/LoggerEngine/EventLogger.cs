using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGT.PNRProcessingService.BusinessEngine.LoggerEngine
{
    public static class EventLogger
    {
        public static void WriteLog(string strMsg)
        {
            string strSource = "Robotics";
            if (!EventLog.SourceExists(strSource))

            {
                EventLog.CreateEventSource(strSource, "RoboticsLogs");
            }

            // Create an EventLog instance and assign its source.

            EventLog eventLog = new EventLog();

            // Setting the source

            eventLog.Source = strSource;



            // Write an entry to the event log.

            eventLog.WriteEntry(strMsg, EventLogEntryType.Error, 1002);
        }
    }
}
