using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace QueueUtilities
{
    class QueueUtilities
    {
        private readonly string _connectionString;
        private CloudStorageAccount _cloudStorageAccount; // = null;
        private CloudQueueClient _cloudQueueClient; // = null;

        #region metodos comuns a varios TestingQueues

        private void InitStorageAccountAndQueueClient()
        {
            if (_cloudStorageAccount == null)
            {
                _cloudStorageAccount = CloudStorageAccount.Parse(_connectionString);
            }

            if (_cloudQueueClient == null)
            {
                _cloudQueueClient = _cloudStorageAccount.CreateCloudQueueClient();
            }
        }

        private CloudQueue GetQueueRefWithInitStorAccAndQueueCl(string queueName)
        {
            InitStorageAccountAndQueueClient();
            return _cloudQueueClient.GetQueueReference(queueName);
        }

        private bool ExecuteMethodsOnQueueExistsOrNot(string samplequeue1, object[] args,
                                    Delegate dDelegateQueueExists, Delegate dDelegateQueueDoesntExist)
        {
            // TODO: metodo que receba 2 delegates a executar(uma para queue existente, outro caso contrario)
            CloudQueue cloudQueue = GetQueueRefWithInitStorAccAndQueueCl(samplequeue1);
            try
            {
                cloudQueue.FetchAttributes(); // verificar se queue existe... da' excepcao, se existe
                // TODO: codigo de dDelegateQueueExists
            }
            catch (StorageClientException)
            {
                // TODO: codigo de dDelegateQueueDoesntExist
                return false;
                //throw;
            }
            return true;
        }

        #endregion metodos comuns a varios TestingQueues

        #region metodos utilizados a partir do TestingQueues1

        public QueueUtilities(string connectionString)
        {
            // HACK: TO_DO: Complete member initialization
            //this.
                _connectionString = connectionString;
        }

        public bool CreateQueue(string samplequeue1)
        {
            //throw new NotImplementedException();
            CloudQueue queue = GetQueueRefWithInitStorAccAndQueueCl(samplequeue1);
            return queue.CreateIfNotExist();
        }

        #endregion metodos utilizados a partir do TestingQueues1

        #region metodos utilizados Async a partir do TestingQueues1

        public bool CreateQueueAsync(string samplequeue1) // Task<bool>
        {
            //throw new NotImplementedException();
            CloudQueue queue = GetQueueRefWithInitStorAccAndQueueCl(samplequeue1);
            Task<bool> task = Task.Factory.FromAsync<bool>(queue.BeginCreateIfNotExist, queue.EndCreateIfNotExist, null);
            return task.Result; // thread/task bloqueada 'a espera da resposta/result
            //return task; // retorno correcto, para ser utilizado em TPL, com o tipo de retorno correcto
        }

        #endregion metodos utilizados Async a partir do TestingQueues1

        #region metodos utilizados a partir do TestingQueues2

        public bool ListQueues(out List<CloudQueue> queues)
        {
            //throw new NotImplementedException();
            InitStorageAccountAndQueueClient();
            queues = _cloudQueueClient.ListQueues().ToList();
            queues.ForEach(c => Console.WriteLine(c.Name));
            return queues.Any();
        }

        public bool DeleteQueue(string samplequeue0)
        {
            //throw new NotImplementedException();
            CloudQueue cloudQueue = GetQueueRefWithInitStorAccAndQueueCl(samplequeue0);
            try
            {
                cloudQueue.FetchAttributes(); // verificar se queue existe... da' excepcao, se existe
            }
            catch (StorageClientException)
            {
                return false;
                //throw;
            }
            cloudQueue.Delete();
            return true;
        }

        #endregion metodos utilizados a partir do TestingQueues2

        #region metodos utilizados Async a partir do TestingQueues2

        public bool CreateQueueAsync(string samplequeue1) // Task<bool>
        {
            //throw new NotImplementedException();
            CloudQueue queue = GetQueueRefWithInitStorAccAndQueueCl(samplequeue1);
            Task<bool> task = Task.Factory.FromAsync<bool>(queue.BeginCreateIfNotExist, queue.EndCreateIfNotExist, null);
            return task.Result; // thread/task bloqueada 'a espera da resposta/result
            //return task; // retorno correcto, para ser utilizado em TPL, com o tipo de retorno correcto
        }

        public bool ListQueues(out List<CloudQueue> queues)
        {
            //throw new NotImplementedException();
            InitStorageAccountAndQueueClient();
            queues = _cloudQueueClient.ListQueues().ToList();
            queues.ForEach(c => Console.WriteLine(c.Name));
            return queues.Any();
        }

        public bool DeleteQueue(string samplequeue0)
        {
            //throw new NotImplementedException();
            CloudQueue cloudQueue = GetQueueRefWithInitStorAccAndQueueCl(samplequeue0);
            try
            {
                cloudQueue.FetchAttributes(); // verificar se queue existe... da' excepcao, se existe
            }
            catch (StorageClientException)
            {
                return false;
                //throw;
            }
            cloudQueue.Delete();
            return true;
        }

        #endregion metodos utilizados Async a partir do TestingQueues2

        #region metodos utilizados a partir do TestingQueues3

        public bool GetQueueMetadata(string samplequeue1, out NameValueCollection metadata)
        {
            //throw new NotImplementedException();
            CloudQueue cloudQueue = GetQueueRefWithInitStorAccAndQueueCl(samplequeue1);
            try
            {
                cloudQueue.FetchAttributes(); // verificar se queue existe... da' excepcao, se existe
                metadata = cloudQueue.Metadata;
            }
            catch (StorageClientException)
            {
                metadata = null;
                return false;
                //throw;
            }
            return true;
        }

        public bool SetQueueMetadata(string samplequeue1, NameValueCollection metadata)
        {
            //throw new NotImplementedException();
            CloudQueue cloudQueue = GetQueueRefWithInitStorAccAndQueueCl(samplequeue1);
            try
            {
                cloudQueue.FetchAttributes(); // verificar se queue existe... da' excepcao, se existe
                //cloudQueue.Metadata = metadata;
                var metadataOriginal = cloudQueue.Metadata;
                metadataOriginal.Clear();
                metadataOriginal.Add(metadata);
                cloudQueue.SetMetadata();
            }
            catch (StorageClientException)
            {
                metadata = null;
                return false;
                //throw;
            }
            return true;
        }

        #endregion metodos utilizados a partir do TestingQueues3

        #region metodos utilizados a partir do TestingQueues4

        public bool PeekMessage(string samplequeue1, out CloudQueueMessage message)
        {
            //throw new NotImplementedException();
            CloudQueue cloudQueue = GetQueueRefWithInitStorAccAndQueueCl(samplequeue1);
            try
            {
                cloudQueue.FetchAttributes(); // verificar se queue existe... da' excepcao, se existe
                message = cloudQueue.PeekMessage();
            }
            catch (StorageClientException)
            {
                message = null;
                return false;
                //throw;
            }
            return true;
        }

        public bool PutMessage(string samplequeue1, CloudQueueMessage message) // 1de2
        {
            //throw new NotImplementedException();
            CloudQueue cloudQueue = GetQueueRefWithInitStorAccAndQueueCl(samplequeue1);
            try
            {
                cloudQueue.FetchAttributes(); // verificar se queue existe... da' excepcao, se existe
                cloudQueue.AddMessage(message);
            }
            catch (StorageClientException)
            {
                return false;
                //throw;
            }
            return true;
        }

        #endregion metodos utilizados a partir do TestingQueues4

        #region metodos utilizados a partir do TestingQueue5

        public bool GetMessage(string samplequeue1, out CloudQueueMessage message)
        {
            //throw new NotImplementedException();
            CloudQueue cloudQueue = GetQueueRefWithInitStorAccAndQueueCl(samplequeue1);
            try
            {
                cloudQueue.FetchAttributes(); // verificar se queue existe... da' excepcao, se existe
                message = cloudQueue.GetMessage();
            }
            catch (StorageClientException)
            {
                message = null;
                return false;
                //throw;
            }
            return true;
        }

        public bool GetMessages(string samplequeue1, out List<CloudQueueMessage> messages, int i)
        {
            //throw new NotImplementedException();
            CloudQueue cloudQueue = GetQueueRefWithInitStorAccAndQueueCl(samplequeue1);
            try
            {
                cloudQueue.FetchAttributes(); // verificar se queue existe... da' excepcao, se existe
                messages = cloudQueue.GetMessages(i).ToList();
            }
            catch (StorageClientException)
            {
                messages = null;
                return false;
                //throw;
            }
            return true;
        }

        public bool ClearMessages(string samplequeue1)
        {
            //throw new NotImplementedException();
            CloudQueue cloudQueue = GetQueueRefWithInitStorAccAndQueueCl(samplequeue1);
            try
            {
                cloudQueue.FetchAttributes(); // verificar se queue existe... da' excepcao, se existe
                cloudQueue.Clear();
            }
            catch (StorageClientException)
            {
                return false;
                //throw;
            }
            return true;
        }

        public bool DeleteMessage(string samplequeue1, CloudQueueMessage message)
        {
            //throw new NotImplementedException();
            CloudQueue cloudQueue = GetQueueRefWithInitStorAccAndQueueCl(samplequeue1);
            try
            {
                cloudQueue.FetchAttributes(); // verificar se queue existe... da' excepcao, se existe
                cloudQueue.DeleteMessage(message);
            }
            catch (StorageClientException)
            {
                return false;
                //throw;
            }
            return true;
        }

        #endregion metodos utilizados a partir do TestingQueue5

        #region metodos utilizados a partir do TestingQueue6

        // HACK: Que conclusões retira com este teste?

        //internal void
        public bool PutMessage(string p, CloudQueueMessage message, TimeSpan timeSpan, TimeSpan timeSpan_2) // 2de2
        {
            //throw new NotImplementedException();
            CloudQueue cloudQueue = GetQueueRefWithInitStorAccAndQueueCl(p);
            try
            {
                cloudQueue.FetchAttributes(); // verificar se queue existe... da' excepcao, se existe
                // TQ6 - "Put message with live initial visible delay ... seconds and with expiration time ... seconds"
                // TQ7 - "Put message with time to live… and expiration time…" - ERRATA: nao time to live, sim init vis delay
                cloudQueue.AddMessage(message, timeSpan_2, timeSpan);
            }
            catch (StorageClientException)
            {
                return false;
                //throw;
            }
            return true;
        }

        #endregion metodos utilizados a partir do TestingQueue6

        #region metodos utilizados a partir do TestingQueue7

        // HACK: Existe algum problema com este teste? Se sim, altere-o. Que conclusões retira com este teste?

        public CloudQueueMessage GetMessageRef(string samplequeue1)
        {
            //throw new NotImplementedException();
            CloudQueueMessage cloudQueueMessage;
            GetMessage(samplequeue1, out cloudQueueMessage);
            return cloudQueueMessage;
        }

        public void UpdateMessage(string samplequeue1, CloudQueueMessage myMessage, TimeSpan timeSpan)
        {
            //throw new NotImplementedException();
            CloudQueue cloudQueue = GetQueueRefWithInitStorAccAndQueueCl(samplequeue1);
            try
            {
                cloudQueue.FetchAttributes(); // verificar se queue existe... da' excepcao, se existe
                cloudQueue.UpdateMessage(myMessage, timeSpan, MessageUpdateFields.Visibility);
            }
            catch (StorageClientException)
            {
                //throw;
            }
        }

        #endregion metodos utilizados a partir do TestingQueue7

        // Exercício 2 - Implemente alguns dos métodos anteriores, utilizando a sua versão assíncrona.
    }
}
