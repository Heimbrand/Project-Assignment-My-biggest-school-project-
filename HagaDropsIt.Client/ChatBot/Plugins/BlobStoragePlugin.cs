using Microsoft.SemanticKernel;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using HagaDropsIt.Shared.Interfaces;
using HagaDropsIt.Shared.ReturnTypes;

public class BlobStoragePlugin
{
    private readonly IBlobStorageServices _blobStorageServices;

    public BlobStoragePlugin(IBlobStorageServices blobStorageServices)
    {
        _blobStorageServices = blobStorageServices;
    }

    [KernelFunction, Description("Get a blob from storage")]
    public async Task<OperationResult<Stream>> GetBlobAsync(string containerName, string blobName)
    {
        return await _blobStorageServices.GetBlobAsync(containerName, blobName);
    }

    [KernelFunction, Description("Upload a blob to storage")]
    public async Task<OperationResult> UploadBlobAsync(string containerName, string blobName, Stream content)
    {
        return await _blobStorageServices.UploadBlobAsync(containerName, blobName, content);
    }

    [KernelFunction, Description("Delete a blob from storage. ask the user to confirm")]
    public async Task<OperationResult> DeleteBlobAsync(string containerName, string blobName)
    {
        return await _blobStorageServices.DeleteBlobAsync(containerName, blobName);
    }

    [KernelFunction, Description("List blobs in a container")]
    public async Task<OperationResult<List<string>>> ListBlobsAsync(string containerName)
    {
        return await _blobStorageServices.ListBlobsAsync(containerName);
    }

    [KernelFunction, Description("Check if a blob exists in a container")]
    public async Task<OperationResult<bool>> BlobExistsAsync(string containerName, string blobName)
    {
        return await _blobStorageServices.BlobExistsAsync(containerName, blobName);
    }

    [KernelFunction, Description("Create a blob container. Container names must be lowercase and can only contain letters, numbers, and hyphens.")]
    public async Task<OperationResult> CreateBlobContainerAsync(string containerName)
    {
        return await _blobStorageServices.CreateBlobContainerAsync(containerName);
    }

    [KernelFunction, Description("Delete a blob container. ask the user to confirm")]
    public async Task<OperationResult> DeleteBlobContainerAsync(string containerName)
    {
        return await _blobStorageServices.DeleteBlobContainerAsync(containerName);
    }

    [KernelFunction, Description("Check if a blob container exists")]
    public async Task<OperationResult<bool>> ContainerExistsAsync(string containerName)
    {
        return await _blobStorageServices.ContainerExistsAsync(containerName);
    }

    [KernelFunction, Description("List blob containers")]
    public async Task<OperationResult<List<string>>> ListBlobContainersAsync()
    {
        return await _blobStorageServices.ListBlobContainersAsync();
    }

    [KernelFunction, Description("Get a SAS token for a blob")]
    public async Task<OperationResult<string>> GetBlobSasTokenAsync(string containerName, string blobName)
    {
        return await _blobStorageServices.GetBlobSasUrl(containerName, blobName);
    }
}
