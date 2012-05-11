using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using Microsoft.ServiceBus;

namespace EchoService
{
    class Program
    {
        static void Main(string[] args)
        {
            // HACK: 3 constantes adicionais, para se poderem alterar 'a vontade
            const string serviceSheme = "sb";
            const string serviceNamespace = "cmfservicebusnamespace"; // "catiavaz";
            const string servicePath = "EchoService";

            string issuerName = "owner"; // "owner";
            string issuerSecret = "CZzrHKiB2QjRCJjBC5kFwXVrWV4w5qpBphpwpt43Tmw="; // "MYKEY";
            TransportClientEndpointBehavior sharedSecretServiceBusCredential = new
                TransportClientEndpointBehavior();
            sharedSecretServiceBusCredential.TokenProvider =
                TokenProvider.CreateSharedSecretTokenProvider(issuerName, issuerSecret);
            // Cria o URI do serviço baseado no namespace do serviço
            Uri address = ServiceBusEnvironment.CreateServiceUri(serviceSheme, serviceNamespace, servicePath);
            
            // Cria o serviço host
            ServiceHost host = new ServiceHost(typeof(EchoService), address);
            
            //Cria o comportamento para o endpoint
            IEndpointBehavior serviceRegistrySettings = new ServiceRegistrySettings(DiscoveryType.Public);

            //Adiciona as credenciais do Service Bus a todos os endpoints configurados na configuração
            foreach (ServiceEndpoint endpoint in host.Description.Endpoints)
            {
                endpoint.Behaviors.Add(serviceRegistrySettings);
                endpoint.Behaviors.Add(sharedSecretServiceBusCredential);
            }

            // Abre o serviço.
            host.Open();

            Console.WriteLine("Service address: " + address); // sb://cmfservicebusnamespace.servicebus.windows.net/EchoService/
            Console.WriteLine("Press [Enter] to exit");
            Console.ReadLine();

            // Fecha o serviço.
            host.Close();
        }
    }
}
