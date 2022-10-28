using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceProcess;
using System.Text;

namespace MyService
{
    public partial class AgentService : ServiceBase
    {
        
        public AgentService()
        {
            InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {
            MyServiceHost.Start();
        }

        protected override void OnStop()
        {
            MyServiceHost.Stop();
        }
    }


    internal class MyServiceHost
    {
        private static ServiceHost _serviceHost = null;

        internal static void Start()
        {
            if (_serviceHost != null) _serviceHost.Close();

            string address_HTTP = @"http://localhost:9001/DailyHelperAgent";
            //string address_TCP = @"net.tcp://localhost:9001/DailyHelperAgent";

            Uri[] address_base = { new Uri(address_HTTP)/*, new Uri(address_TCP) */};

            _serviceHost = new ServiceHost(typeof(MyServiceLib.Service1), address_base);

            _serviceHost.Description.Behaviors.Add(new ServiceMetadataBehavior());

            BasicHttpBinding binding_http = new BasicHttpBinding();
            _serviceHost.AddServiceEndpoint(typeof(MyServiceLib.IService1), binding_http, address_HTTP);
            _serviceHost.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding(), "mex");

            //NetTcpBinding binding_tcp = new NetTcpBinding();
            //binding_tcp.Security.Mode = SecurityMode.Transport;
            //binding_tcp.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            //binding_tcp.Security.Message.ClientCredentialType = MessageCredentialType.Windows;
            //binding_tcp.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;

            //_serviceHost.AddServiceEndpoint(typeof(MyServiceLib.IService1), binding_tcp, address_TCP);
            //_serviceHost.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexTcpBinding(), "mex");

            _serviceHost.Open();
        }

        internal static void Stop()
        {
            if (_serviceHost != null)
            {
                _serviceHost.Close();
                _serviceHost = null;
            }
        }
    }
}
