using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.StorageClient;

namespace QueueUtilities
{
    public static class TestingQueues
    {
        private const string ACCOUNTNAME = "hellocloudcmfaustino";
        private const string ACCOUNTKEY = "9Gm6jmuU+Ktw+M1MnI8QYaxXSRtfrXUfINY4dUgt7Vm23Ka89kedT+R7FBwMMqW20FKjEsqB/x8GkSvTEit/MQ==";
        private const string ACCOUNT = ACCOUNTNAME; // apenas para nao alterar o metodo TestingQueue7

        private static void Separator()
        {
            //throw new NotImplementedException();
            Console.WriteLine();
        }

        public static void TestingQueues1()
        {
            QueueUtilities queueUtil = new
            QueueUtilities("DefaultEndpointsProtocol=http;AccountName=" + ACCOUNTNAME + ";AccountKey=" + ACCOUNTKEY + "");
            Console.Write("Creating queue ");
            if (queueUtil.CreateQueue("samplequeue1"))
                Console.WriteLine("true");
            else
                Console.WriteLine("false");
            Separator();
            if (queueUtil.CreateQueue("samplequeue2"))
                Console.WriteLine("true");
            else
                Console.WriteLine("false");
            Separator();
        }

        public static void TestingQueues2()
        {
            QueueUtilities queueUtil = new
            QueueUtilities("DefaultEndpointsProtocol=http;AccountName=" + ACCOUNTNAME + ";AccountKey=" + ACCOUNTKEY + "");
            List<CloudQueue> queues;
            Console.Write("List queues ");
            if (queueUtil.ListQueues(out queues))
                foreach (CloudQueue queue in queues)
                    Console.Write(queue.Name + " ");
            Console.WriteLine();
            Separator();
            Console.Write("Delete queue ");
            if (queueUtil.DeleteQueue("samplequeue0"))
                Console.WriteLine("true");
            else
                Console.WriteLine("false");
            Separator();
        }

        public static void TestingQueues3()
        {
            QueueUtilities queueUtil = new
            QueueUtilities("DefaultEndpointsProtocol=http;AccountName=" + ACCOUNTNAME + ";AccountKey=" + ACCOUNTKEY + "");
            NameValueCollection metadata = new NameValueCollection();
            Console.WriteLine("Get queue metadata ");
            Separator();
            if (queueUtil.GetQueueMetadata("samplequeue1", out metadata))
                if (metadata != null)
                {
                    for (int i = 0; i < metadata.Count; i++)
                    {
                        Console.WriteLine(metadata.GetKey(i) + ": " + metadata.Get(i));
                    }
                }
                else
                    Console.WriteLine("false");
            Separator();
            metadata.Add("property1", "Value1");
            metadata.Add("property2", "Value2");
            metadata.Add("property3", "Value3");
            Console.WriteLine("Set queue metadata ");
            if (queueUtil.SetQueueMetadata("samplequeue1", metadata))
                Console.WriteLine("true");
            else
                Console.WriteLine("false");
            Separator();
            Console.WriteLine("Get queue metadata ");
            if (queueUtil.GetQueueMetadata("samplequeue1", out metadata))
                if (metadata != null)
                {
                    for (int i = 0; i < metadata.Count; i++)
                    {
                        Console.WriteLine(metadata.GetKey(i) + ": " + metadata.Get(i));
                    }
                }
                else
                    Console.WriteLine("false");
            Separator();
        }

        public static void TestingQueues4()
        {
            QueueUtilities queueUtil = new
            QueueUtilities("DefaultEndpointsProtocol=http;AccountName=" + ACCOUNTNAME + ";AccountKey=" + ACCOUNTKEY + "");
            CloudQueueMessage message = null;
            Console.Write("Peek a message ");
            if (queueUtil.PeekMessage("samplequeue1", out message))
            {
                Console.WriteLine("true");
                Console.WriteLine("MessageId: " + message.Id + " popReceipt=" + message.PopReceipt);
                Console.WriteLine("POPReceipt; " + message.PopReceipt);
                Console.WriteLine(message.AsString);
            }
            else
                Console.WriteLine("false");
            Separator();
            message = new CloudQueueMessage("<Order id=\"1001\">This is test message 1</Order>");
            Console.Write("Put message ");
            if (queueUtil.PutMessage("samplequeue1", message))
                Console.WriteLine("true");
            else
                Console.WriteLine("false");
            Separator();
            message = null;
            Separator();
            message = null;
            Console.Write("Peek message ");
            if (queueUtil.PeekMessage("samplequeue1", out message))
            {
                Console.WriteLine("true");
                Console.WriteLine("MessageId: " + message.Id + " popReceipt=" + message.PopReceipt);
                Console.WriteLine("POPReceipt; " + message.PopReceipt);
                Console.WriteLine(message.AsString);
            }
            else
                Console.WriteLine("false");
        }

        public static void TestingQueue5()
        {
            QueueUtilities queueUtil =
                new QueueUtilities("DefaultEndpointsProtocol=http;AccountName=" + ACCOUNTNAME + ";AccountKey=" + ACCOUNTKEY + "");
            CloudQueueMessage message = null;
            message = new CloudQueueMessage("<Order id=\"1002\">This is test message 2</Order>");
            Console.Write("Put message ");
            if (queueUtil.PutMessage("samplequeue1", message))
                Console.WriteLine("true");
            else
                Console.WriteLine("false");
            message = null;
            Console.Write("Get message ");
            if (queueUtil.GetMessage("samplequeue1", out message))
            {
                Console.WriteLine("true");
                Console.WriteLine("MessageId: " + message.Id + " popReceipt=" + message.PopReceipt);
                Console.WriteLine("POPReceipt; " + message.PopReceipt);
                Console.WriteLine(message.AsString);
            }
            else
                Console.WriteLine("false");
            Separator();
            List<CloudQueueMessage> messages;
            Console.Write("List queues ");
            if (queueUtil.GetMessages("samplequeue1", out messages, 10))
                foreach (CloudQueueMessage queue in messages)
                    Console.Write(message.AsString + " ");
            Console.WriteLine();
            Separator();
            Console.WriteLine("Clear messages ");
            if (queueUtil.ClearMessages("samplequeue1"))
                Console.WriteLine("true");
            else
                Console.WriteLine("false");
            Separator();
            Console.WriteLine("Delete message ");
            if (queueUtil.DeleteMessage("samplequeue1", message))
                Console.WriteLine("true");
            else
                Console.WriteLine("false");
            Separator();
            message = null;
            Console.WriteLine("Get message ");
            if (queueUtil.GetMessage("samplequeue1", out message))
            {
                Console.WriteLine("true");
                Console.WriteLine("MessageId: " + message.Id + " popReceipt=" + message.PopReceipt);
                Console.WriteLine("POPReceipt; " + message.PopReceipt);
                Console.WriteLine(message.AsString);
            }
            else
                Console.WriteLine("false");
            Separator();
        }

        public static void TestingQueue6()
            // HACK: Que conclusões retira com este teste?
        {
            QueueUtilities queueUtil = new
            QueueUtilities("DefaultEndpointsProtocol=http;AccountName=" + ACCOUNTNAME + ";AccountKey=" + ACCOUNTKEY + "");
            CloudQueueMessage message = null;
            List<CloudQueueMessage> messages;
            message = new CloudQueueMessage("<Order id=\"1003\">This is test message 2</Order>");
            Console.Write("Put message with live initial visible delay ... seconds and with expiration time ... seconds");
            queueUtil.PutMessage("samplequeue1", message, TimeSpan.FromSeconds(0.1), TimeSpan.FromSeconds(3.1));
            queueUtil.GetMessages("samplequeue1", out messages, 10);
            foreach (CloudQueueMessage queue in messages)
                Console.Write(message.AsString + " ");
            Console.WriteLine();
            Separator();
        }

        public static void TestingQueue7()
            // HACK: Existe algum problema com este teste? Se sim, altere-o. Que conclusões retira com este teste?
        {
            QueueUtilities queueUtil = new
            QueueUtilities("DefaultEndpointsProtocol=http;AccountName=" + ACCOUNT + ";AccountKey=" + ACCOUNTKEY + "");
            queueUtil.ClearMessages("samplequeue1");
            CloudQueueMessage message = null;
            List<CloudQueueMessage> messages;
            message = new CloudQueueMessage("<Order id=\"1006\">This is test message 6</Order>");
            Console.Write("Put message with time to live… and expiration time…");
            queueUtil.PutMessage("samplequeue1", message, new TimeSpan(1, 0, 0), new TimeSpan(0, 0, 0));
            queueUtil.GetMessages("samplequeue1", out messages, 10);
            foreach (CloudQueueMessage ms in messages)
                Console.WriteLine(ms.AsString + " ");
            Separator();
            CloudQueueMessage myMessage = queueUtil.GetMessageRef("samplequeue1");
            Console.WriteLine(myMessage.AsString);
            queueUtil.UpdateMessage("samplequeue1", myMessage, new TimeSpan(0, 0, 1));
            queueUtil.GetMessages("samplequeue1", out messages, 10);
            foreach (CloudQueueMessage queue in messages)
                Console.WriteLine(message.AsString + " ");
            Console.WriteLine();
            Separator();
        }
    }
}
