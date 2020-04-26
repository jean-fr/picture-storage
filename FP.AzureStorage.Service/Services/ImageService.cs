using FP.AzureStorage.Service.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Threading.Tasks;

namespace FP.AzureStorage.Service
{
    public class ImageService : IImageService
    {
        private CloudStorageAccount _storageAccount;
        public ImageService(string accountName, string accountKey)
        {
            _storageAccount = new CloudStorageAccount(new StorageCredentials(accountName, accountKey), useHttps: true);
        }


        public async Task<string> Upload(FileModel file)
        {

            try
            {
                CloudBlobClient cloudBlobClient = _storageAccount.CreateCloudBlobClient();
                CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference("fpimages");
                await blobContainer.CreateIfNotExistsAsync(BlobContainerPublicAccessType.Blob,new BlobRequestOptions(),new OperationContext());

                CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(file.Name);

                await blockBlob.UploadFromStreamAsync(file.Stream);

                // blockBlob.UploadTextAsync()


                return blockBlob.Uri.AbsoluteUri;
            }
            catch (Exception ex)
            {

                //logger service to log ex

                return string.Empty;
            }
        }
    }
}
