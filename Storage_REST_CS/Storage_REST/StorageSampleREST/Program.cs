using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace StorageSampleREST
{
    public class Program
    {
        // Specify your storage project credentials here.
        // NOTE: Best practice is not to embed credentials in code or leave in the clear.
        const string StorageAccount = "MYSTORAGEACCOUNT";
        const string StorageKey = "MYSTORAGEKEY";

        static void Main(string[] args)
        {
            TestBlobStorage();
            TestQueueStorage();
            TestTableStorage();

            Console.WriteLine("\nPress <Enter> to end");
            Console.Read();
        }

        // Perform blob storage operations.

        static void TestBlobStorage()
        {
            BlobHelper BlobHelper = new BlobHelper(StorageAccount, StorageKey);

            try
            {
                Separator();

                List<string> containers;

                Console.Write("List containers ");
                containers = BlobHelper.ListContainers();
                if (containers != null)
                {
                    Console.WriteLine("true");
                    if (containers != null)
                    {
                        foreach (string containerName in containers)
                        {
                            Console.Write(containerName + " ");
                        }
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                Console.Write("Create container ");
                if (BlobHelper.CreateContainer("samplecontainer1"))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                Console.Write("Create container ");
                if (BlobHelper.CreateContainer("samplecontainer2"))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                Console.Write("Delete container ");
                if (BlobHelper.DeleteContainer("samplecontainer0"))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                Console.Write("Set container ACL ");
                if (BlobHelper.SetContainerACL("samplecontainer1", "container"))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                string accessLevel;

                Console.Write("Get container ACL ");
                accessLevel = BlobHelper.GetContainerACL("samplecontainer1");
                if (accessLevel != null)
                {
                    Console.WriteLine("true " + accessLevel);
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                string accessPolicyXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" +
                                        "<SignedIdentifiers>\n" +
                                        "  <SignedIdentifier>\n" +
                                        "    <Id>Policy1</Id>\n" +
                                        "    <AccessPolicy>\n" +
                                        "      <Start>2011-01-01T09:38:05Z</Start>\n" +
                                        "      <Expiry>2011-12-31T09:38:05Z</Expiry>\n" +
                                        "      <Permission>rwd</Permission>\n" +
                                        "    </AccessPolicy>\n" +
                                        "  </SignedIdentifier>\n" +
                                        "  <SignedIdentifier>\n" +
                                        "    <Id>Policy2</Id>\n" +
                                        "    <AccessPolicy>\n" +
                                        "      <Start>2010-01-01T09:38:05Z</Start>\n" +
                                        "      <Expiry>2012-12-31T09:38:05Z</Expiry>\n" +
                                        "      <Permission>r</Permission>\n" +
                                        "    </AccessPolicy>\n" +
                                        "  </SignedIdentifier>\n" +
                                        "</SignedIdentifiers>\n";

                Console.Write("Set container access policy ");
                if (BlobHelper.SetContainerAccessPolicy("samplecontainer1", "container", accessPolicyXml))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                Console.Write("Get container access policy ");
                accessPolicyXml = BlobHelper.GetContainerAccessPolicy("samplecontainer1");
                if (accessPolicyXml != null)
                {
                    Console.WriteLine("true");
                    Console.WriteLine(accessPolicyXml);
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                string eTag, lastModified;

                Console.Write("Get container properties ");
                if (BlobHelper.GetContainerProperties("samplecontainer1", out eTag, out lastModified))
                {
                    Console.WriteLine("true");
                    Console.WriteLine("Etag: " + eTag);
                    Console.WriteLine("LastModifiedUtc: " + lastModified);
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                SortedList<string, string> metadata = new SortedList<string, string>();
                metadata.Add("property1", "Value1");
                metadata.Add("property2", "Value2");
                metadata.Add("property3", "Value3");

                Console.Write("Set container metadata ");
                if (BlobHelper.SetContainerMetadata("samplecontainer1", metadata))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                Console.Write("Get container metadata ");
                metadata = BlobHelper.GetContainerMetadata("samplecontainer1");
                if (metadata != null)
                {
                    Console.WriteLine("true");
                    foreach (KeyValuePair<string, string> item in metadata)
                    {
                        Console.WriteLine(item.Key + ": " + item.Value);
                    }
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                //Console.Write("Delete container ");
                //if (DeleteContainer("samplecontainer1"))
                //{
                //    Console.WriteLine("true");
                //}
                //else
                //{
                //    Console.WriteLine("false");
                //}

                Separator();

                List<string> blobs;
                Console.Write("List blobs ");
                blobs = BlobHelper.ListBlobs("samplecontainer1");
                if (blobs != null)
                {
                    Console.WriteLine("true");
                    if (blobs != null)
                    {
                        foreach (string blobName in blobs)
                        {
                            Console.WriteLine(blobName);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                Console.Write("Put blob ");
                if (BlobHelper.PutBlob("samplecontainer1", "blob1.txt", "This is a text blob!"))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                Console.Write("Put page blob ");
                if (BlobHelper.PutBlob("samplecontainer1", "pageblob1.txt", 2048))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                string page0 = new string('A', 512);

                Console.Write("Put page 0 ");
                if (BlobHelper.PutPage("samplecontainer1", "pageblob1.txt", page0, 0, 512))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                string page1 = new string('B', 512);

                Console.Write("Put page 1 ");
                if (BlobHelper.PutPage("samplecontainer1", "pageblob1.txt", page1, 512, 512))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                page0 = String.Empty;

                Console.Write("Get page 0 ");
                page0 = BlobHelper.GetPage("samplecontainer1", "pageblob1.txt", 0, 512);
                if (page0 != null)
                {
                    Console.WriteLine("true");
                    Console.WriteLine(page0);
                }
                else
                {
                    Console.WriteLine("false");
                }

                page1 = String.Empty;

                Console.Write("Get page 1 ");
                page1 = BlobHelper.GetPage("samplecontainer1", "pageblob1.txt", 512, 512);
                if (page1 != null)
                {
                    Console.WriteLine("true");
                    Console.WriteLine(page1);
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                string[] regions = null;

                Console.Write("Get page regions ");
                regions = BlobHelper.GetPageRegions("samplecontainer1", "pageblob1.txt");
                if (regions != null)
                {
                    Console.WriteLine("true");
                    if (regions != null)
                    {
                        for (int i = 0; i < regions.Length; i++)
                        {
                            Console.WriteLine(regions[i]);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                string content;

                Console.Write("Get blob ");
                content = BlobHelper.GetBlob("samplecontainer1", "blob1.txt");
                if (content != null)
                {
                    Console.WriteLine("true");
                    Console.WriteLine(content);
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                Console.Write("Copy blob ");
                if (BlobHelper.CopyBlob("samplecontainer1", "blob1.txt", "samplecontainer1", "blob2.txt"))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                Console.Write("Snapshot blob ");
                if (BlobHelper.SnapshotBlob("samplecontainer1", "blob1.txt"))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                Console.Write("Delete blob ");
                if (BlobHelper.DeleteBlob("samplecontainer1", "blob2.txt"))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                metadata.Clear();
                metadata.Add("x-ms-blob-content-type", "text/html");

                Console.Write("Set blob properties ");
                if (BlobHelper.SetBlobProperties("samplecontainer1", "blob1.txt", metadata))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                SortedList<string, string> properties = new SortedList<string, string>();

                Console.Write("Get blob properties ");
                properties = BlobHelper.GetBlobProperties("samplecontainer1", "blob1.txt");
                if (properties != null)
                {
                    Console.WriteLine("true");
                    foreach (KeyValuePair<string, string> item in properties)
                    {
                        Console.WriteLine(item.Key + ": " + item.Value);
                    }
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                Console.Write("Put blob If Unchanged 1 ");
                if (BlobHelper.PutBlobIfUnchanged("samplecontainer1", "blob1.txt", "This is a text blob!", properties["ETag"]))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                Console.Write("Put blob If Unchanged 2 ");
                if (BlobHelper.PutBlobIfUnchanged("samplecontainer1", "blob1.txt", "This is a text blob!", "BadETag"))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }

                Console.Write("Put blob with MD5 ");
                if (BlobHelper.PutBlobWithMD5("samplecontainer1", "blob1.txt", "This is a text blob!"))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                metadata.Clear();
                metadata.Add("property1", "Value1");
                metadata.Add("property2", "Value2");

                Console.Write("Set blob metadata ");
                if (BlobHelper.SetBlobMetadata("samplecontainer1", "blob1.txt", metadata))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                Console.Write("Get blob metadata ");
                metadata = BlobHelper.GetBlobMetadata("samplecontainer1", "blob1.txt");
                if (metadata != null)
                {
                    Console.WriteLine("true");
                    foreach (KeyValuePair<string, string> item in metadata)
                    {
                        Console.WriteLine(item.Key + ": " + item.Value);
                    }
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                string leaseAction = "acquire";
                string leaseId = null;
                Console.Write("Lease blob - acquire ");
                leaseId = BlobHelper.LeaseBlob("samplecontainer1", "blob1.txt", leaseAction, leaseId);
                if (leaseId != null)
                {
                    Console.WriteLine("true " + leaseId);
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                leaseAction = "release";
                Console.Write("Lease blob - release ");
                if (BlobHelper.LeaseBlob("samplecontainer1", "blob1.txt", leaseAction, leaseId) != null)
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                string[] blockIds = new string[3];

                Console.Write("Put block 0 ");
                if (BlobHelper.PutBlock("samplecontainer1", "largeblob1.txt", 0, blockIds, "AAAAAAAAAA"))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                Console.Write("Put block 1 ");
                if (BlobHelper.PutBlock("samplecontainer1", "largeblob1.txt", 1, blockIds, "BBBBBBBBBB"))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                Console.Write("Put block 2 ");
                if (BlobHelper.PutBlock("samplecontainer1", "largeblob1.txt", 2, blockIds, "CCCCCCCCCC"))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                string[] getblockids = null;

                Console.Write("Get block list ");
                blockIds = BlobHelper.GetBlockList("samplecontainer1", "largeblob1.txt");
                if (blockIds != null)
                {
                    Console.WriteLine("true");
                    if (getblockids != null)
                    {
                        for (int i = 0; i < getblockids.Length; i++)
                        {
                            Console.WriteLine("Block " + i + " id = " + getblockids[i]);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                Console.Write("Put block list ");
                if (BlobHelper.PutBlockList("samplecontainer1", "largeblob1.txt", blockIds))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION " + ex.ToString());
            }
        }

        // Perform queue storage operations.

        static void TestQueueStorage()
        {
            QueueHelper QueueHelper = new QueueHelper(StorageAccount, StorageKey);

            try
            {
                Separator();

                List<string> queues;

                Console.Write("List queues ");
                queues = QueueHelper.ListQueues();
                if (queues != null)
                {
                    Console.WriteLine("true");
                    if (queues != null)
                    {
                        foreach (string queueName in queues)
                        {
                            Console.Write(queueName + " ");
                        }
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                Console.Write("Create queue ");
                if (QueueHelper.CreateQueue("samplequeue1"))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                Console.Write("Delete queue ");
                if (QueueHelper.DeleteQueue("samplequeue0"))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                SortedList<string, string> metadata = new SortedList<string, string>();
                metadata.Add("property1", "Value1");
                metadata.Add("property2", "Value2");
                metadata.Add("property3", "Value3");

                Console.Write("Set queue metadata ");
                metadata = QueueHelper.SetQueueMetadata("samplequeue1");
                if (metadata != null)
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                Console.Write("Get queue metadata ");
                metadata = QueueHelper.GetQueueMetadata("samplequeue1");
                if (metadata != null)
                {
                    Console.WriteLine("true");
                    foreach (KeyValuePair<string, string> item in metadata)
                    {
                        Console.WriteLine(item.Key + ": " + item.Value);
                    }
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                string message;

                Console.Write("Peek message ");
                message = QueueHelper.PeekMessage("samplequeue1");
                if (message != null)
                {
                    Console.WriteLine("true");
                    Console.WriteLine(message);
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                Console.Write("Clear messages ");
                if (QueueHelper.ClearMessages("samplequeue1"))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                Console.Write("Peek message ");
                message = QueueHelper.PeekMessage("samplequeue1");
                if (message != null)
                {
                    Console.WriteLine("true");
                    Console.WriteLine(message);
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                message = "<Order id=\"1001\">This is test message 1</Order>";

                Console.Write("Put message ");
                if (QueueHelper.PutMessage("samplequeue1", message))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                message = String.Empty;
                string messageId = String.Empty;
                string popReceipt = String.Empty;

                Console.Write("Get message ");
                if (QueueHelper.GetMessage("samplequeue1", out message, out messageId, out popReceipt))
                {
                    Console.WriteLine("true messageId=" + messageId + " popReceipt=" + popReceipt);
                    Console.WriteLine(message);
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                message = String.Empty;

                Console.Write("Delete message messageId=" + messageId + " popReceipt=" + popReceipt + " ");
                if (QueueHelper.DeleteMessage("samplequeue1", messageId, popReceipt))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                message = String.Empty;

                Console.Write("Get message ");
                if (QueueHelper.GetMessage("samplequeue1", out message, out messageId, out popReceipt))
                {
                    Console.WriteLine("true messageId=" + messageId + " popReceipt=" + popReceipt);
                    Console.WriteLine(message);
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                message = String.Empty;

                Console.Write("Get message ");
                if (QueueHelper.GetMessage("samplequeue1", out message, out messageId, out popReceipt))
                {
                    Console.WriteLine("true messageId=" + messageId + " popReceipt=" + popReceipt);
                    Console.WriteLine(message);
                }
                else
                {
                    Console.WriteLine("false");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION " + ex.ToString());
            }
        }

        // Perform table storage operations.

        static void TestTableStorage()
        {
            TableHelper TableHelper = new TableHelper(StorageAccount, StorageKey);

            try
            {
                Separator();

                List<string> tables;

                Console.Write("List tables ");
                tables = TableHelper.ListTables();
                if (tables != null)
                {
                    Console.WriteLine("true");
                    foreach (string tableName in tables)
                    {
                        Console.Write(tableName + " ");
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                Console.Write("Create table ");
                if (TableHelper.CreateTable("sampletable"))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                Console.Write("Insert entity ");
                if (TableHelper.InsertEntity("sampletable", "USA", "Pallmann",
                    new Contact { LastName="Pallmann", FirstName="David", Email="dpallmann@hotmail.com", Country = "USA" }))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                Console.Write("Insert entity ");
                if (TableHelper.InsertEntity("sampletable", "USA", "Smith",
                    new Contact { LastName = "Smith", FirstName = "John", Email = "john.smith@hotmail.com", Country = "USA" }))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                Console.Write("Insert entity ");
                if (TableHelper.InsertEntity("sampletable", "USA", "Jones",
                    new Contact { LastName = "Jones", FirstName = "Tom", Email = "tom.jones@hotmail.com", Country = "USA" }))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                Console.Write("Insert entity ");
                if (TableHelper.InsertEntity("sampletable", "USA", "Peters",
                    new Contact { LastName = "Peters", FirstName = "Sally", Email = "sally.peters@hotmail.com", Country = "USA" }))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                Console.Write("Update entity ");
                if (TableHelper.ReplaceUpdateEntity("sampletable", "USA", "Pallmann",
                    new Contact { LastName = "Pallmann", FirstName = "David", Country = "USA" }))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                Console.Write("Merge entity ");
                if (TableHelper.MergeUpdateEntity("sampletable", "USA", "Pallmann",
                    new Contact { Email = "david.pallmann@hotmail.com" }))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                string entityXml;

                Console.Write("Get entity ");
                entityXml = TableHelper.GetEntity("sampletable", "USA", "Pallmann");
                if (entityXml != null)
                {
                    Console.WriteLine("true");
                    Console.WriteLine(entityXml);
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                entityXml = String.Empty;

                Console.Write("Query entities ");
                entityXml = TableHelper.QueryEntities("sampletable", "PartitionKey eq 'USA'");
                if (entityXml != null)
                {
                    Console.WriteLine("true");
                    Console.WriteLine(entityXml);
                }
                else
                {
                    Console.WriteLine("false");
                }

                Separator();

                Console.Write("Delete entity ");
                if (TableHelper.DeleteEntity("sampletable", "USA", "Smith"))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }

                //Separator();

                //Console.Write("Delete table ");
                //if (TableHelper.DeleteTable("sampletable"))
                //{
                //    Console.WriteLine("true");
                //}
                //else
                //{
                //    Console.WriteLine("false");
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION " + ex.ToString());
            }
        }

        private static void Separator()
        {
            Console.WriteLine("----------------------------------------");
        }

        internal class Contact
        {
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string Country { get; set; }
        }

    }


}
