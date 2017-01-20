using System;
using System.Web;
using System.Configuration;
using System.ServiceProcess;


namespace WebAppConfigConsole.Models
{
    public static class WinServiceInfo
    {
        private static string _serviceName = ConfigurationManager.AppSettings["WinServiceName"];

        public static string GetCurrentServiceStatus()
        {
            ServiceController sc = new ServiceController(_serviceName);
            return sc.Status.ToString();
        }

        public static bool StopService()
        {
            bool isServiceStoped = false;

            try
            {
                ServiceController sc = new ServiceController(_serviceName);

                if (sc.Status == ServiceControllerStatus.Running)
                {
                    sc.Stop();
                    sc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(50));
                    isServiceStoped = true;
                }
            }
            catch (Exception ex)
            {
                //Log error for ex
            }

            return isServiceStoped;
        }

        public static bool StartService()
        {
            bool isServiceStarted = false;

            try
            {
                ServiceController sc = new ServiceController(_serviceName);

                if (sc.Status == ServiceControllerStatus.Stopped)
                {
                    sc.Start();
                    sc.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(50));
                    isServiceStarted = true;
                }
            }
            catch (Exception ex)
            {
                // log error for ex
            }

            return isServiceStarted;
        }

        public static bool ReStartService()
        {
            bool isServiceRestarted = false;

            try
            {
                ServiceController sc = new ServiceController(_serviceName);

                if (sc.Status == ServiceControllerStatus.Running
                    || sc.Status == ServiceControllerStatus.StopPending)
                {
                    if (sc.Status == ServiceControllerStatus.Running)
                        sc.Stop();

                    sc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(50));
                    sc.Start();
                    sc.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(50));
                    isServiceRestarted = true;
                }
            }
            catch (Exception ex)
            {
                // log error for ex
            }

            return isServiceRestarted;
        }
    }
}