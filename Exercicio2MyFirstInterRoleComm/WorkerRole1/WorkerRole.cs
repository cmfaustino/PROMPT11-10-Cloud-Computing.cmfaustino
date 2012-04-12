using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;

namespace WorkerRole1
{
    public class WorkerRole : RoleEntryPoint
    {
        public override void Run()
        {
            // Guiao - 2 - Passo 4.a. Para criar a queue
            CloudStorageAccount storageAccount =
                CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("StorageConnectionString"));

            // Guiao - 2 - Passo 4.b. Para criar o cliente da queue
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            // Guiao - 2 - Passo 4.c. Para retornar a referencia da queue
            CloudQueue queue = queueClient.GetQueueReference("myqueue");

            // Guiao - 2 - Passo 4.d. Para criar a queue se ainda nao existir
            queue.CreateIfNotExist();

            // This is a sample worker implementation. Replace with your logic.
            Trace.WriteLine("$projectname$ entry point called", "Information");

            while (true)
            {
                // este while foi criado para optimizar desempenho
                while (queue.RetrieveApproximateMessageCount() > 0)
                {
                    // este codigo foi movido para dentro do ciclo, para se verem varias mensagens

                    // Guiao - 2 - Passo 4.e. Obter e remover a mensagem da queue
                    CloudQueueMessage retrievedMessage = queue.GetMessage();
                    // este if foi criado para nao haver problemas na execucao continua do ciclo
                    if (retrievedMessage != null)
                    {
                        queue.DeleteMessage(retrievedMessage);

                        string s = retrievedMessage.AsString; // cod nao esta' no guiao
                        // Guiao - 2 - Passo 4.f. Adicionar ao traco a mensagem obtida
                        Trace.WriteLine("Received the message:" + s);
                    }
                }

                Thread.Sleep(10000);
                Trace.WriteLine("Working", "Information");
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            return base.OnStart();
        }
    }
}
