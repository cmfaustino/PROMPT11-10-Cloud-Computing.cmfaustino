using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.StorageClient;

namespace BlobUtilities
{
    static class BlobExtensionMethods
    {
        public static CloudBlob GetCloudBlobFromCloudBlobContainer(this CloudBlobContainer cloudBlobContainer, string cloudBlobName)
        {
            string filename = Path.GetFileName(cloudBlobName);
            //CloudBlob cloudBlob =
            return cloudBlobContainer.GetBlobReference(filename);
        }
    }
}
