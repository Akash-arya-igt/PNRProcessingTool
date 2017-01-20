using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppConfigConsole.Models;

namespace WebAppConfigConsole.Controllers
{
    public class WinServiceController : Controller
    {
        string strExePath = @"D:\IGT.RoboPNRProcessingService\IGT.PNRProcessingService.ConsoleApp.exe";
        // GET: WinService
        public ActionResult Index()
        {
            ServiceStatus objSrvcStatus = GetRoboStatus();
            return PartialView(objSrvcStatus);
        }

        public ActionResult ChangeStatus(string ChangeServiceStatus)
        {
            switch (ChangeServiceStatus)
            {
                case "START":
                    StartRobo();
                    break;
                case "STOP":
                    StopRobo();
                    break;
            }

            /*
            bool isStatusChanged = false;

            switch (ChangeServiceStatus)
            {
                case "START":
                    isStatusChanged = WinServiceInfo.StartService();
                    break;
                case "STOP":
                    isStatusChanged = WinServiceInfo.StopService();
                    break;
                case "RESTART":
                    isStatusChanged = WinServiceInfo.ReStartService();
                    break;
            }

            string strStatus = string.Empty;
            if (!isStatusChanged)
                strStatus = "Changing";

            ServiceStatus objSrvcStatus = GetServiceStatus(strStatus);
            return PartialView ("Index", objSrvcStatus);
            */

            ServiceStatus objSrvcStatus = GetRoboStatus();
            return PartialView("Index", objSrvcStatus);
        }
        
        public ServiceStatus GetServiceStatus(string Status)
        {
            ServiceStatus objServiceStatus = new ServiceStatus();

            if (string.IsNullOrEmpty(Status))
                objServiceStatus.ServiceCurrentStatus = WinServiceInfo.GetCurrentServiceStatus();
            else
                objServiceStatus.ServiceCurrentStatus = Status;

            objServiceStatus.IsStopVisible = false;
            objServiceStatus.IsStartVisible = false;
            objServiceStatus.IsRestartVisible = false;

            if (objServiceStatus.ServiceCurrentStatus == "Running")
            {
                objServiceStatus.IsStopVisible = true;
                objServiceStatus.IsRestartVisible = true;
            }
            else if (objServiceStatus.ServiceCurrentStatus == "Stopped")
            {
                objServiceStatus.IsStartVisible = true;
            }
            else
            {
                objServiceStatus.ServiceCurrentStatus = "Changing";
            }

            return objServiceStatus;
        }

        public ServiceStatus GetRoboStatus()
        {

            ServiceStatus objServiceStatus = new ServiceStatus();

            objServiceStatus.IsStopVisible = false;
            objServiceStatus.IsStartVisible = false;
            objServiceStatus.IsRestartVisible = false;
            objServiceStatus.ServiceCurrentStatus = "Running";
            if (false)
            {
                bool isServiceRunning = IsRoboServiceRunning();

                if (isServiceRunning)
                {
                    objServiceStatus.IsStopVisible = true;
                }
                else
                {
                    objServiceStatus.IsStartVisible = true;
                }
            }
            return objServiceStatus;
        }

        public bool IsRoboServiceRunning()
        {
            bool isSrvcRunning = false;

            try
            {
                isSrvcRunning = Process.GetProcessesByName("IGT.PNRProcessingService.ConsoleApp.exe")
                    .FirstOrDefault(p => p.MainModule.FileName.StartsWith(@"D:\IGT.RoboPNRProcessingService")) != default(Process);
            }
            catch(Exception ex)
            {
                isSrvcRunning = false;
            }

            return isSrvcRunning;
        }

        public void StopRobo()
        {
            var roboProcess = Process.GetProcessesByName("IGT.PNRProcessingService.ConsoleApp.exe")
                                .FirstOrDefault(p => p.MainModule.FileName.StartsWith(@"D:\IGT.RoboPNRProcessingService"));

            if (roboProcess != null)
                roboProcess.Close();
        }

        public void StartRobo()
        {
            Process roboProcess = new Process();
            //Working Directory Of .exe File. 
            roboProcess.StartInfo.WorkingDirectory = @"D:\IGT.RoboPNRProcessingService";//Request.MapPath("~/IGT.RoboPNRProcessingService");
            //exe File Name. 
            roboProcess.StartInfo.FileName = @"D:\IGT.RoboPNRProcessingService\IGT.PNRProcessingService.ConsoleApp.exe";//Request.MapPath("IGT.PNRProcessingService.ConsoleApp.exe");
            //Argement Which you have tp pass. 
            roboProcess.StartInfo.Arguments = " ";
            roboProcess.StartInfo.LoadUserProfile = true;
            //Process Start on exe.
            roboProcess.Start();
        }


    }
}
