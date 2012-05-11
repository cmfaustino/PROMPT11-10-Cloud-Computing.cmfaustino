using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.StorageClient;

namespace TableUtilities
{
    public static class TestingTables
    {
        private const string ACCOUNTNAME = "hellocloudcmfaustino";
        private const string ACCOUNTKEY = "9Gm6jmuU+Ktw+M1MnI8QYaxXSRtfrXUfINY4dUgt7Vm23Ka89kedT+R7FBwMMqW20FKjEsqB/x8GkSvTEit/MQ==";

        private static void Separator()
        {
            //throw new NotImplementedException();
            Console.WriteLine();
        }

        public static void TestingTables1()
        {
            TableUtilities tableUtil = new TableUtilities("DefaultEndpointsProtocol=http;AccountName=" + ACCOUNTNAME + ";AccountKey=" + ACCOUNTKEY + "");
            List<string> tables;
            Console.WriteLine("List tables ");
            if (tableUtil.ListTables(out tables))
            {
                Console.WriteLine("true");
                if (tables != null)
                {
                    foreach (string tableName in tables)
                    {
                        Console.Write(tableName + " ");
                    }
                    Console.WriteLine();
                }
            }
            else
                Console.WriteLine("false");
            Separator();
            Console.WriteLine("Create table ");
            if (tableUtil.CreateTable("sampletable"))
                Console.WriteLine("true");
            else
                Console.WriteLine("false");
            Separator();
        }

        public static void TestingTables2() // TestingQueues2
        {
            TableUtilities tableUtil = new
                TableUtilities("DefaultEndpointsProtocol=http;AccountName=" + ACCOUNTNAME + ";AccountKey=" + ACCOUNTKEY + "");
            Console.WriteLine("Insert entity ");
            if (tableUtil.InsertEntity("sampletable",
                new Contact("USA", "Pallmann") { LastName = "Pallmann", FirstName = "David",
                    Email = "dpallmann@hotmail.com", Country = "USA" }))
                Console.WriteLine("true");
            else
                Console.WriteLine("false");
            
            Separator();
            Console.WriteLine("Insert entity ");
            if (tableUtil.InsertEntity("sampletable",
                new Contact("USA", "Smith") { LastName = "Smith", FirstName = "John", Email =
                    "john.smith@hotmail.com", Country = "USA" }))
                Console.WriteLine("true");
            else
                Console.WriteLine("false");
            
            Separator();
            Console.Write("Insert entity ");
            if (tableUtil.InsertEntity("sampletable",
                new Contact("USA", "Jones") { LastName = "Jones", FirstName = "Tom", Email =
                    "tom.jones@hotmail.com", Country = "USA" }))
                Console.WriteLine("true");
            else
                Console.WriteLine("false");
            
            Separator();
            Console.Write("Insert entity ");
            if (tableUtil.InsertEntity("sampletable",
                new Contact("USA", "Peters") { LastName = "Peters", FirstName = "Sally", Email =
                    "sally.peters@hotmail.com", Country = "USA" }))
                Console.WriteLine("true");
            else
                Console.WriteLine("false");
            Separator();
        }

        public static void TestingTables3()
        {
            TableUtilities tableUtil = new TableUtilities("DefaultEndpointsProtocol=http;AccountName=" + ACCOUNTNAME + ";AccountKey=" + ACCOUNTKEY + "");
            Console.Write("Update entity ");
            if (tableUtil.ReplaceUpdateEntity("sampletable", "USA", "Pallmann",
            new Contact("USA", "Pallmann")
            {
                LastName = "Pallmann",
                FirstName = "David",
                Email =
                    "david.pallmann@hotmail.com",
                Country = "USA"
            }))
                Console.WriteLine("true");
            else
                Console.WriteLine("false");
            Separator();
            Console.Write("Replace Update entity ");
            if (tableUtil.ReplaceUpdateEntity("sampletable", "USA", "Peters",
            new Contact("USA", "Peters")
            {
                LastName = "Peters",
                FirstName = "Sally",
                Country = "USA"
            }))
                Console.WriteLine("true");
            else
                Console.WriteLine("false");
            Separator();
        }

        public static void TestingTables4()
        {
            TableUtilities tableUtil = new
            TableUtilities("DefaultEndpointsProtocol=http;AccountName=" + ACCOUNTNAME + ";AccountKey=" + ACCOUNTKEY + "");
            Console.Write("Merge Update entity. Preserves the unchanged properties. ");
            if (tableUtil.MergeUpdateEntity("sampletable", "USA", "Peters",
            new MiniContact("USA", "Peters")
            {
                LastName = "Peters",
                FirstName = "Sally",
                Email =
                    "sally.peters@hotmail.com"
            }))
                Console.WriteLine("true");
            else
                Console.WriteLine("false");
        }

        public static void TestingTables5()
        {
            TableUtilities tableUtil = new
            TableUtilities("DefaultEndpointsProtocol=http;AccountName=" + ACCOUNTNAME + ";AccountKey=" + ACCOUNTKEY + "");
            Contact contact = null;
            Separator();
            Console.Write("Get entity ");
            if (tableUtil.GetEntity<Contact>("sampletable", "USA", "Pallmann", out contact))
            {
                Console.WriteLine("true");
                if (contact != null)
                {
                    Console.WriteLine("Contact.LastName: " + contact.LastName);
                    Console.WriteLine("Contact.FirstName: " + contact.FirstName);
                    Console.WriteLine("Contact.Email: " + contact.Email);
                    Console.WriteLine("Contact.Phone: " + contact.Phone);
                    Console.WriteLine("Contact.Country: " + contact.Country);
                }
                else
                    Console.WriteLine("Contact <NULL>");
            }
            else
                Console.WriteLine("false");
            Separator();
            //Contact
                contact = null;
            Console.Write("Get entity ");
            if (tableUtil.GetEntity<Contact>("sampletable", "USA", "Smith", out contact))
            {
                Console.WriteLine("true");
                if (contact != null)
                {
                    Console.WriteLine("Contact.LastName: " + contact.LastName);
                    Console.WriteLine("Contact.FirstName: " + contact.FirstName);
                    Console.WriteLine("Contact.Email: " + contact.Email);
                    Console.WriteLine("Contact.Phone: " + contact.Phone);
                    Console.WriteLine("Contact.Country: " + contact.Country);
                }
                else
                    Console.WriteLine("Contact <NULL>");
            }
            else
            {
                Console.WriteLine("false");
            }
            Separator();
        }

        public static void TestingTables6()
        {
            TableUtilities tableUtil = new
            TableUtilities("DefaultEndpointsProtocol=http;AccountName=" + ACCOUNTNAME + ";AccountKey=" + ACCOUNTKEY + "");
            Console.Write("Query entities ");
            IEnumerable<Contact> entities = tableUtil.QueryEntities<Contact>("sampletable").Where(e =>
            e.PartitionKey == "USA").AsTableServiceQuery<Contact>();
            if (entities != null)
            {
                Console.WriteLine("true");
                foreach (Contact contact1 in entities)
                {
                    Console.WriteLine("Contact.LastName: " + contact1.LastName);
                    Console.WriteLine("Contact.FirstName: " + contact1.FirstName);
                    Console.WriteLine("Contact.Email: " + contact1.Email);
                    Console.WriteLine("Contact.Phone: " + contact1.Phone);
                    Console.WriteLine("Contact.Country: " + contact1.Country);
                    Console.WriteLine();
                }
            }
            else
                Console.WriteLine("<NULL>");
        }

        public static void TestingTables7()
        {
            TableUtilities tableUtil = new
            TableUtilities("DefaultEndpointsProtocol=http;AccountName=" + ACCOUNTNAME + ";AccountKey=" + ACCOUNTKEY + "");
            Separator();
            Console.Write("Delete entity ");
            if (tableUtil.DeleteEntity<Contact>("sampletable", "USA", "Smith"))
                Console.WriteLine("true");
            else
                Console.WriteLine("false");
        }
    }
}
