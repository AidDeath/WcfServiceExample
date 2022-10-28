using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;
using System.Linq;

namespace MyService
{
    [RunInstaller(true)]
    public partial class AgentServiceInstaller : System.Configuration.Install.Installer
    {
        public AgentServiceInstaller()
        {
            // InitializeComponent();
            process = new ServiceProcessInstaller();
            process.Account = ServiceAccount.LocalSystem;

            service = new ServiceInstaller();
            service.ServiceName = "AAAAA";
            service.DisplayName = "AAAA"; //"Daily Helper Agent";
            service.Description = "Agent for running Daily Helper Routines";
            service.StartType = ServiceStartMode.Automatic;

            Installers.Add(process);
            Installers.Add(service);
        }
    }
}
