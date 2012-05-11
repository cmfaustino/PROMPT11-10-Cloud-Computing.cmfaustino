using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace Sender
{
    public class Sender
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
                Console.WriteLine("\nCreating Queue '{0}'...", Sender.QueueName);
                //Eliminar se a queue já existir
                if (namespaceClient.QueueExists(Sender.QueueName))
                    namespaceClient.DeleteQueue(Sender.QueueName);
                namespaceClient.CreateQueue(Sender.QueueName);
                QueueClient myQueueClient = CreateQueueClient(serviceUri, credentials, out factory);
                List<BrokeredMessage> messageList = new List<BrokeredMessage>();
                messageList.Add(CreateIssueMessage("1", "First message "));
                messageList.Add(CreateIssueMessage("2", "Second message "));
                messageList.Add(CreateIssueMessage("3", "Third message "));
                SendListOfMessages(messageList, myQueueClient);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception {0}", e.ToString());
                throw;
            }
            finally
            {
                if (factory != null)
                    factory.Close();
            }
        }

        static void CreateTokenProviderAndServiceUri(out TokenProvider credentials, out Uri serviceUri)
        {
            //TODO
            // HACK: constante adicional
            const string serviceSheme = "sb";

            credentials = TokenProvider.CreateSharedSecretTokenProvider(IssuerName, IssuerKey);
            serviceUri = ServiceBusEnvironment.CreateServiceUri(serviceSheme, ServiceNamespace, string.Empty);
        }
        //Criar o QueueClient a partir do uri do serviço e das credenciais
        static QueueClient CreateQueueClient(Uri serviceUri, TokenProvider credentials, out MessagingFactory factory)
        {
            //TODO
            factory = MessagingFactory.Create(serviceUri, credentials);
            return factory.CreateQueueClient(QueueName);
        }
        //Enviar uma lista de mensagens para o cliente queue e imprimir as mensagens enviadas na consola
        static void SendListOfMessages(List<BrokeredMessage> list, QueueClient myQueueClient)
        {
            //TODO
            list.ForEach(myQueueClient.Send);
        }
        //inicializar o ServiceNamespace, IssuerName e IssuerKey com input da consola.
        static void GetUserCredentials()
        {
            //TODO
            ServiceNamespace = "cmfservicebusnamespace";
            IssuerName = "owner";
            IssuerKey = "CZzrHKiB2QjRCJjBC5kFwXVrWV4w5qpBphpwpt43Tmw=";
        }
        //Criar uma mensagem com body issueBody e Id issueid
        private static BrokeredMessage CreateIssueMessage(string issueId, string issueBody)
        {
            //TODO
            BrokeredMessage brokeredMessage = new BrokeredMessage(issueBody);
            brokeredMessage.MessageId = issueId;
            return brokeredMessage;
        }

    }
}
