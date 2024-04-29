using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using HagaDropsIt.Shared.ReturnTypes;

namespace HagaDropsIt.Shared.Interfaces
{
    public interface IBlobStorageServices
    {
        Task<OperationResult<Stream>> GetBlobAsync(string containerName, string blobName);
        Task<OperationResult<string>> UploadBlobAsync(string containerName, string blobName, Stream content);
        Task<OperationResult<bool>> DeleteBlobAsync(string containerName, string blobName);
        Task<OperationResult<List<string>>> ListBlobsAsync(string containerName);
        Task<OperationResult<bool>> BlobExistsAsync(string containerName, string blobName);
        Task<OperationResult> CreateBlobContainerAsync(string containerName);
        Task<OperationResult> DeleteBlobContainerAsync(string containerName);
        Task<OperationResult<bool>> ContainerExistsAsync(string containerName);
        Task<OperationResult<List<string>>> ListBlobContainersAsync();
        Task<OperationResult<string>> GetBlobSasUrl(string containerName, string blobName);
    }

}
