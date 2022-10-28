using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace MyService
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            File.AppendAllText("logYeah.txt", "Стартуем! \n");
            try
            {

                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new AgentService()
                };
                ServiceBase.Run(ServicesToRun);
            }
            catch (Exception ex)
            {
                File.AppendAllText("logYeah.txt", ex.GetBaseException().Message);
            }

        }
    }
}
