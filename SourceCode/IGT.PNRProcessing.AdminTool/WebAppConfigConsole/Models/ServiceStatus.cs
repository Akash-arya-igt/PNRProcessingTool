using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppConfigConsole.Models
{
    public class ServiceStatus
    {
        public string ServiceCurrentStatus { get; set; }
        public bool IsStartVisible { get; set; }
        public bool IsRestartVisible { get; set; }
        public bool IsStopVisible { get; set; }
    }
}