using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Azure.Storage;
using HagaDropsIt.Shared.Interfaces;
using HagaDropsIt.Shared.ReturnTypes;

namespace HagaDropsIt.Client.Services;


public class FileHandlerServices : IFileHandlerServices
{
private readonly IHttpClientFactory _httpClientFactory;
private readonly IBlobStorageServices _blobStorageServices;

public FileHandlerServices(IHttpClientFactory httpClientFactory, IBlobStorageServices blobStorageServices)
{
    _httpClientFactory = httpClientFactory;
    _blobStorageServices = blobStorageServices;
}

public async Task<OperationResult> UploadImageFromUrlAsync(string containerName, string blobName, string imageUrl)
{
    HttpClient httpClient = _httpClientFactory.CreateClient("FileHandlerServices");

    try
    {
        using (var response = await httpClient.GetAsync(imageUrl, HttpCompletionOption.ResponseHeadersRead))
        {
            response.EnsureSuccessStatusCode();
            string extension = Path.GetExtension(new Uri(imageUrl).AbsolutePath);
            if (!blobName.EndsWith(extension, StringComparison.OrdinalIgnoreCase))
            {
                blobName += extension; 
            }

            using (var streamToReadFrom = await response.Content.ReadAsStreamAsync())
            {
                return await _blobStorageServices.UploadBlobAsync(containerName, blobName, streamToReadFrom);
            }
        }
    }
    catch (HttpRequestException httpEx)
    {
        return OperationResult.Fail($"HTTP request failed: {httpEx.Message}");
    }
    catch (Exception ex)
    {
        return OperationResult.Fail($"Failed to upload image from URL: {ex.Message}");
    }
}    

}