using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using IGT.PNRProcessingService.BusinessEngine.LoggerEngine;
using IGT.PNRProcessingService.BusinessEntities;

namespace IGT.PNRProcessing.ActivityLibrary
{

    public sealed class LogMsgActivity : CodeActivity
    {
        public InArgument<string> LogMsg { get; set; }

        public InArgument<NLogLevel> LoggingLevel { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            string strLogMsg = context.GetValue(this.LogMsg);
            EventLogger.WriteLog(strLogMsg);
            Console.WriteLine(strLogMsg);
            Console.ReadLine();
            NLogLevel enmLoggingLevel = context.GetValue(this.LoggingLevel);
            NLogManager._instance.LogMsg(enmLoggingLevel, strLogMsg);
        }
    }
}
