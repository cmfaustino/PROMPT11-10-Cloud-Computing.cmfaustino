using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace Receiver
{
    public class Receiver
    {

        const string QueueName = "IssueTrackingQueue";
        static string ServiceNamespace;
        static string IssuerName;
        static string IssuerKey;

        static void Main(string[] args)
        {
            GetUserCredentials();
            TokenProvider credentials = null;
            Uri serviceUri = null;
            CreateTokenProviderAndServiceUri(out credentials, out serviceUri);
            MessagingFactory factory = null;
            try
            {
                NamespaceManager namespaceClient = new NamespaceManager(serviceUri, credentials);
                if (namespaceClient == null)
                {
                    Console.WriteLine("\nUnexpected Error: NamespaceManager is NULL");
                    return;
                }
                QueueDescription queueDescription = namespaceClient.GetQueue(Receiver.QueueName);
                if (queueDescription == null)
                {
                    Console.WriteLine("\nUnexpected Error: QueueDescription is NULL");
                    return;
                }
                QueueClient myQueueClient = CreateQueueClient(serviceUri, credentials, out factory);
                Console.WriteLine("\nReceiving messages from Queue '{0}'...", Receiver.QueueName);
                // Numero actual de mensagens na queue
                long messageCount = queueDescription.MessageCount;
                ReceiveNMessagesFromQueue(myQueueClient, messageCount);
                Console.WriteLine("\nEnd of scenario, press ENTER to exit.");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception {0}", e.ToString());
                throw;
            }
            finally
            {
                if (factory != null) factory.Close();
            }
        }

        //Recebe messageCount mensagens da queue, com timeout de 5 segundos por mensagem.
        //Imprimir na consola o id e o body de cada mensagem
        static void ReceiveNMessagesFromQueue(QueueClient myQueueClient, long messageCount)
        {
            //TODO
        }
        static void CreateTokenProviderAndServiceUri(out TokenProvider credentials, out Uri serviceUri)
        {
            //TODO
        }
        //Criar o QueueClient a partir do uri do serviço e das credenciais em modo PeekLock
        static QueueClient CreateQueueClient(Uri serviceUri, TokenProvider credentials, out MessagingFactory factory)
        {
            //TODO
        }
        static void GetUserCredentials()
        {
            //TODO
        }
    }
}
