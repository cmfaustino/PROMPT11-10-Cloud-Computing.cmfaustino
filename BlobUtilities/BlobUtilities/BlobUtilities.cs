using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace BlobUtilities
{
    class BlobUtilities
    {
        private readonly string _connectionString;
        private CloudStorageAccount _cloudStorageAccount; // = null;
        private CloudBlobClient _cloudBlobClient; // = null;

        private void InitStorageAccountAndBlobClient()
        {
            if (_cloudStorageAccount == null)
            {
                _cloudStorageAccount = CloudStorageAccount.Parse(_connectionString);
            }

            if (_cloudBlobClient == null)
            {
                _cloudBlobClient = _cloudStorageAccount.CreateCloudBlobClient();
            }
        }

        private CloudBlobContainer GetContainerRefWithInitStorAccAndBlobCl(string containerName)
        {
            InitStorageAccountAndBlobClient();
            return _cloudBlobClient.GetContainerReference(containerName);
        }

        #region elementos utilizados a partir do TestBlobStorage1

        public BlobUtilities(string connectionString)
        {
            // HACK: TO_DO: Complete member initialization
            //this.
                _connectionString = connectionString;
        }

        public bool ListContainers(out List<CloudBlobContainer> containers)
        {
            //throw new NotImplementedException();
            InitStorageAccountAndBlobClient();
            containers = _cloudBlobClient.ListContainers().ToList();
            containers.ForEach(c => Console.WriteLine(c.Name));
            return containers.Any();
        }

        public bool CreateContainer(string samplecontainer2)
        {
            //throw new NotImplementedException();
            CloudBlobContainer cloudBlobContainer = GetContainerRefWithInitStorAccAndBlobCl(samplecontainer2);
            return cloudBlobContainer.CreateIfNotExist();
        }

        public bool DeleteContainer(string samplecontainer0)
        {
            //throw new NotImplementedException();
            CloudBlobContainer cloudBlobContainer = GetContainerRefWithInitStorAccAndBlobCl(samplecontainer0);
            try
            {
                cloudBlobContainer.FetchAttributes();
            }
            catch (StorageClientException)
            {
                return false;
                //throw;
            }
            cloudBlobContainer.Delete();
            return true;
        }

        #endregion elementos utilizados a partir do TestBlobStorage1

        #region elementos utilizados a partir do TestBlobStorage2 (depois do TestBlobStorage3)

        public bool CopyBlob(string samplecontainer1, string blob1Txt, string s, string blob2Txt)
        {
            throw new NotImplementedException();
        }

        public bool SnapshotBlob(string samplecontainer1, string blob1Txt)
        {
            throw new NotImplementedException();
        }

        public bool DeleteBlob(string samplecontainer1, string blob2Txt)
        {
            throw new NotImplementedException();
        }

        public bool ListBlobs(string samplecontainer1, out List<CloudBlob> blobs)
        {
            throw new NotImplementedException();
        }

        #endregion elementos utilizados a partir do TestBlobStorage2 (depois do TestBlobStorage3)

        #region elementos utilizados a partir do TestBlobStorage3 (antes do TestBlobStorage2)

        public bool PutBlob(string samplecontainer1, string blob1Txt, string thisIsATextBlob) // 1de2
        {
            //throw new NotImplementedException();
            CloudBlobContainer cloudBlobContainer = GetContainerRefWithInitStorAccAndBlobCl(samplecontainer1);
            try
            {
                CloudBlob cloudBlob = cloudBlobContainer.GetCloudBlobFromCloudBlobContainer(blob1Txt);
                cloudBlob.UploadText(thisIsATextBlob);
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
            return true;
        }

        public bool PutBlob(string samplecontainer1P, string blob1Txtp2, int thisIsATextBlobp3Size) // 2de2
        {
            //throw new NotImplementedException();
            CloudBlobContainer cloudBlobContainer = GetContainerRefWithInitStorAccAndBlobCl(samplecontainer1P);
            try
            {
                CloudBlob cloudBlob = cloudBlobContainer.GetCloudBlobFromCloudBlobContainer(blob1Txtp2);
                cloudBlob.UploadText(new string(' ', thisIsATextBlobp3Size)); // TODO: duvida - se esta' bem... e' com PutPageBlock
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
            return true;
        }

        #endregion elementos utilizados a partir do TestBlobStorage3 (antes do TestBlobStorage2)

        #region elementos utilizados a partir do TestBlobStorage4

        public bool PutBlock(string samplecontainer1, string largeblob1Txt, int p2, string[] blockIds, string aaaaaaaaaa)
        {
            throw new NotImplementedException();
        }

        public bool GetBlockList(string samplecontainer1, string largeblob1Txt, out string[] getblockids)
        {
            throw new NotImplementedException();
        }

        public bool PutBlockList(string samplecontainer1, string largeblob1Txt, string[] blockIds)
        {
            throw new NotImplementedException();
        }

        #endregion elementos utilizados a partir do TestBlobStorage4

        #region elementos utilizados a partir do TestBlobStorage5

        public bool PutPage(string samplecontainer1, string pageblob1Txt, string page0, int p3, int p4)
        {
            throw new NotImplementedException();
        }

        public bool GetPage(string samplecontainer1, string pageblob1Txt, int p2, int p3, out string page0)
        {
            throw new NotImplementedException();
        }

        public bool GetPageRegions(string samplecontainer1, string pageblob1Txt, out PageRange[] ranges)
        {
            throw new NotImplementedException();
        }

        public bool GetBlob(string samplecontainer1, string blob1Txt, out string content)
        {
            throw new NotImplementedException();
        }

        #endregion elementos utilizados a partir do TestBlobStorage5

        #region elementos utilizados a partir do TestBlobStorage6

        public bool SetBlobProperties(string samplecontainer1, string blob1Txt, SortedList<string, string> properties)
        {
            throw new NotImplementedException();
        }

        public bool GetBlobProperties(string samplecontainer1, string blob1Txt, out SortedList<string, string> properties)
        {
            throw new NotImplementedException();
        }

        #endregion elementos utilizados a partir do TestBlobStorage6

        #region elementos utilizados a partir do TestBlobStorage7

        public bool GetContainerACL(string samplecontainer1, out string accessLevel)
        {
            throw new NotImplementedException();
        }

        public bool SetContainerAccessPolicy(string samplecontainer1, SortedList<string, SharedAccessPolicy> policies)
        {
            throw new NotImplementedException();
        }

        public bool GetContainerAccessPolicy(string samplecontainer1, out SortedList<string, SharedAccessPolicy> policies)
        {
            throw new NotImplementedException();
        }

        #endregion elementos utilizados a partir do TestBlobStorage7

        #region elementos utilizados a partir do TestBlobStorage8

        public bool GenerateSharedAccessSignature(string samplecontainer1, SharedAccessPolicy policy1, out string signature) // 1de2
        {
            throw new NotImplementedException();
        }

        public bool GenerateSharedAccessSignature(string samplecontainer1p, string policy1p_2, out string signature) // 2de2
        {
            throw new NotImplementedException();
        }

        #endregion elementos utilizados a partir do TestBlobStorage8

        #region elementos utilizados a partir do TestBlobStorage9

        public bool LeaseBlob(string samplecontainer1, string blob1Txt, string leaseAction, ref string leaseId)
        {
            throw new NotImplementedException();
        }

        #endregion elementos utilizados a partir do TestBlobStorage9
    }
}
