using Amazon.Runtime;
using Azure;
using Azure.Storage.Blobs;
using HagaDropsIt.Shared.Interfaces;
using HagaDropsIt.Shared.ReturnTypes;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace HagaDropsIt.Blob.Repositories
{ 
    public class BlobStorageRepository
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly HttpClient _httpClient;
    

        public BlobStorageRepository(HttpClient httpClient, BlobServiceClient blobServiceClient, ILogger<BlobStorageRepository> logger)
        {
            _httpClient = httpClient;
            _blobServiceClient = blobServiceClient ?? throw new ArgumentNullException(nameof(blobServiceClient));
           
        }


        public async Task<OperationResult<Stream>> GetBlobAsync(string containerName, string blobName)
        {
            try
            {
                var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
                var blobClient = blobContainerClient.GetBlobClient(blobName);

                if (!await blobClient.ExistsAsync())
                {
                    return OperationResult<Stream>.Fail($"Blob '{blobName}' does not exist.");
                }

                var blobDownloadInfo = await blobClient.DownloadAsync();
                return OperationResult<Stream>.Ok(blobDownloadInfo.Value.Content);
            }
            catch (Exception ex)
            {
                return OperationResult<Stream>.Fail($"Failed to get blob: {ex.Message}");
            }
        }

        public async Task<OperationResult> UploadBlobAsync(string containerName, string blobName, Stream content)
        {
            try
            {
                var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
                var blobClient = blobContainerClient.GetBlobClient(blobName);

                await blobClient.UploadAsync(content, overwrite: true);
                return OperationResult.Ok();
            }
            catch (Exception ex)
            {
                return OperationResult.Fail($"Failed to upload blob: {ex.Message}");
            }
        }


        public async Task<OperationResult> DeleteBlobAsync(string containerName, string blobName)
        {
            try
            {
                var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
                var blobClient = blobContainerClient.GetBlobClient(blobName);
                await blobClient.DeleteIfExistsAsync();
                return OperationResult.Ok();
            }
            catch (Exception ex)
            {
                return OperationResult.Fail($"Failed to delete blob: {ex.Message}");
            }
        }

        public async Task<OperationResult<List<string>>> ListBlobsAsync(string containerName)
        {
            try
            {
                var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
                var blobs = new List<string>();
                await foreach (var blobItem in blobContainerClient.GetBlobsAsync())
                {
                    blobs.Add(blobItem.Name);
                }
                return OperationResult<List<string>>.Ok(blobs);
            }
            catch (Exception ex)
            {
                return OperationResult<List<string>>.Fail($"Failed to list blobs: {ex.Message}");
            }
        }

        public async Task<OperationResult<bool>> BlobExistsAsync(string containerName, string blobName)
        {
            try
            {
                var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
                var blobClient = blobContainerClient.GetBlobClient(blobName);
                bool exists = await blobClient.ExistsAsync();
                return OperationResult<bool>.Ok(exists);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Fail($"Failed to check if blob exists: {ex.Message}");
            }
        }
    

        public async Task<OperationResult> CreateBlobContainerAsync(string containerName)
        {
            try
            {
                var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
                await blobContainerClient.CreateIfNotExistsAsync();
                return OperationResult.Ok();
            }
            catch (Exception ex)
            {
                return OperationResult.Fail($"Failed to create container: {ex.Message}");
            }
        }

        public async Task<OperationResult> DeleteBlobContainerAsync(string containerName)
        {
            try
            {
                var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
                await blobContainerClient.DeleteIfExistsAsync();
                return OperationResult.Ok();
            }
            catch (Exception ex)
            {
                return OperationResult.Fail($"Failed to delete container: {ex.Message}");
            }
        }

        public async Task<OperationResult<bool>> ContainerExistsAsync(string containerName)
        {
            try
            {
                var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
                bool exists = await blobContainerClient.ExistsAsync();
                return OperationResult<bool>.Ok(exists);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Fail($"Failed to check container existence: {ex.Message}");
            }
        }

        public async Task<OperationResult<List<string>>> ListBlobContainersAsync()
        {
            try
            {
                var containers = new List<string>();
                await foreach (var containerItem in _blobServiceClient.GetBlobContainersAsync())
                {
                    containers.Add(containerItem.Name);
                }
                return OperationResult<List<string>>.Ok(containers);
            }
            catch (Exception ex)
            {
                return OperationResult<List<string>>.Fail($"Failed to list containers: {ex.Message}");
            }
        }


    }
}
