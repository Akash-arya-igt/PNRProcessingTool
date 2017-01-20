namespace IGT.PNRProcessingService.WinService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AutoPNRProcessingServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.AutoPNRProcessingServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // AutoPNRProcessingServiceProcessInstaller
            // 
            this.AutoPNRProcessingServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.AutoPNRProcessingServiceProcessInstaller.Password = null;
            this.AutoPNRProcessingServiceProcessInstaller.Username = null;
            // 
            // AutoPNRProcessingServiceInstaller
            // 
            this.AutoPNRProcessingServiceInstaller.ServiceName = "IGT.AutoPNRProcessing.WorkflowService";
            this.AutoPNRProcessingServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.AutoPNRProcessingServiceProcessInstaller,
            this.AutoPNRProcessingServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller AutoPNRProcessingServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller AutoPNRProcessingServiceInstaller;
    }
}