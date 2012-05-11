using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Microsoft.ServiceBus;

namespace EchoClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // HACK: 3 constantes adicionais, para se poderem alterar 'a vontade
            const string serviceSheme = "sb";
            // HACK: linha seguinte descomentada, para nao se estar sempre a escrever
            const string serviceNamespace = "cmfservicebusnamespace"; // "catiavaz";
            const string servicePath = "EchoService";

            // HACK: declaracao de 2 constantes, para nao se estar sempre a escrever
            string issuerName = "owner"; // "owner";
            string issuerSecret = "CZzrHKiB2QjRCJjBC5kFwXVrWV4w5qpBphpwpt43Tmw="; // "MYKEY";

            ServiceBusEnvironment.SystemConnectivity.Mode = ConnectivityMode.AutoDetect;
            Console.Write("Your Service Namespace: ");
            //string serviceNamespace = Console.ReadLine(); // HACK: linha comentada, para nao se estar sempre a escrever
            Console.Write("Your Issuer Name: ");
            //string issuerName = Console.ReadLine(); // HACK: linha comentada, para nao se estar sempre a escrever
            Console.Write("Your Issuer Secret: ");
            //string issuerSecret = Console.ReadLine(); // HACK: linha comentada, para nao se estar sempre a escrever

            Uri serviceUri = ServiceBusEnvironment.CreateServiceUri(serviceSheme, serviceNamespace, servicePath);

            TransportClientEndpointBehavior sharedSecretServiceBusCredential = new
                TransportClientEndpointBehavior();
            sharedSecretServiceBusCredential.TokenProvider =
                TokenProvider.CreateSharedSecretTokenProvider(issuerName, issuerSecret);

            ChannelFactory<IEchoChannel> channelFactory =
                new ChannelFactory<IEchoChannel>("RelayEndpoint", new EndpointAddress(serviceUri));

            channelFactory.Endpoint.Behaviors.Add(sharedSecretServiceBusCredential);

            IEchoChannel channel = channelFactory.CreateChannel();
            channel.Open();

            Console.WriteLine("Enter text to echo (or [Enter] to exit):");

            string input = Console.ReadLine();
            while (input != String.Empty)
            {
                try
                {
                    Console.WriteLine("Server echoed: {0}", channel.Echo(input));
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
                input = Console.ReadLine();
            }

            channel.Close();
            channelFactory.Close();
        }
    }
}
