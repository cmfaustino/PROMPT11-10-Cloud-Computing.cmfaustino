using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace TableUtilities
{
    class TableUtilities
    {
        private readonly string _connectionString;
        private CloudStorageAccount _cloudStorageAccount; // = null;
        private CloudTableClient _cloudTableClient; // = null;
        private TableServiceContext _tableServiceContext; // = null;

        #region metodos comuns a varios TestingTables

        private void InitStorageAccountAndTableClientAndContext()
        {
            if (_cloudStorageAccount == null)
            {
                _cloudStorageAccount = CloudStorageAccount.Parse(_connectionString);
            }

            if (_cloudTableClient == null)
            {
                _cloudTableClient = _cloudStorageAccount.CreateCloudTableClient();
            }

            if (_tableServiceContext == null)
            {
                _tableServiceContext = _cloudTableClient.GetDataServiceContext();
            }
        }

        #endregion metodos comuns a varios TestingTables

        #region metodos utilizados a partir do TestingTables1

        public TableUtilities(string connectionString)
        {
            //throw new NotImplementedException();

            // HACK: TO_DO: Complete member initialization
            //this.
                _connectionString = connectionString;
        }

        public bool ListTables(out List<string> tables)
        {
            //throw new NotImplementedException();
            InitStorageAccountAndTableClientAndContext();
            tables = _cloudTableClient.ListTables().ToList();
            return tables.Any();
        }

        public bool CreateTable(string sampletable)
        {
            //throw new NotImplementedException();
            InitStorageAccountAndTableClientAndContext();
            return _cloudTableClient.CreateTableIfNotExist(sampletable);
        }

        #endregion metodos utilizados a partir do TestingTables1

        #region metodos utilizados a partir do TestingTables2 - TestingQueues2

        public bool InsertEntity(string sampletable, Contact p1)
        {
            //throw new NotImplementedException();
            InitStorageAccountAndTableClientAndContext();
            try
            {
                _tableServiceContext.AddObject(sampletable, p1);
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            //_tableServiceContext.SaveChanges();
            _tableServiceContext.SaveChangesWithRetries(SaveChangesOptions.None);
            return true;
        }

        #endregion metodos utilizados a partir do TestingTables2 - TestingQueues2

        #region metodos utilizados a partir do TestingTables3

        public bool ReplaceUpdateEntity(string sampletable, string usa, string pallmann, Contact p3)
        {
            //throw new NotImplementedException();
            InitStorageAccountAndTableClientAndContext();
            Contact contactToChange = _tableServiceContext.CreateQuery<Contact>(sampletable)
                .SingleOrDefault(c => (c.PartitionKey == usa) && (c.RowKey == pallmann));
            return true;
        }

        #endregion metodos utilizados a partir do TestingTables3

        #region metodos utilizados a partir do TestingTables4

        public bool MergeUpdateEntity(string sampletable, string usa, string peters, MiniContact miniContact)
        {
            throw new NotImplementedException();
        }

        #endregion metodos utilizados a partir do TestingTables4

        #region metodos utilizados a partir do TestingTables5

        public bool GetEntity<T>(string sampletable, string usa, string pallmann, out T contact)
        {
            throw new NotImplementedException();
        }

        #endregion metodos utilizados a partir do TestingTables5

        #region metodos utilizados a partir do TestingTables6

        public CloudTableQuery<Contact> QueryEntities<T>(string sampletable)
        {
            throw new NotImplementedException();
        }

        #endregion metodos utilizados a partir do TestingTables6

        #region metodos utilizados a partir do TestingTables7

        public bool DeleteEntity<T>(string sampletable, string usa, string smith)
        {
            throw new NotImplementedException();
        }

        #endregion metodos utilizados a partir do TestingTables7
    }
}
