
using HagaDropsIt.Shared.Interfaces;
using HagaDropsIt.Shared.ReturnTypes;
using Newtonsoft.Json;
using System.Net.Http;
namespace HagaDropsIt.Client.Services
{
    public class BlobStorageServices : IBlobStorageServices
    {
        private readonly HttpClient _httpClient;

        public BlobStorageServices(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("HagaDropsItAPI");
        }

        public async Task<OperationResult<Stream>> GetBlobAsync(string containerName, string blobName)
        {
            var response = await _httpClient.GetAsync($"api/blobstorage/{containerName}/{blobName}");
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                return OperationResult<Stream>.Ok(stream);
            }
            else
            {
                return OperationResult<Stream>.Fail(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<OperationResult<string>> UploadBlobAsync(string containerName, string blobName, Stream content)
        {
            if (content.CanSeek)
            {
                content.Position = 0;
            }

            using (var contentToSend = new StreamContent(content))
            {
                contentToSend.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                var uri = $"api/blobstorage/{containerName}/{blobName}";

                var response = await _httpClient.PostAsync(uri, contentToSend);
                if (response.IsSuccessStatusCode)
                {
                    string url = await response.Content.ReadAsStringAsync();
                    return OperationResult<string>.Ok(url);
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    return OperationResult<string>.Fail(error);
                }
            }
        }


        public async Task<OperationResult<bool>> DeleteBlobAsync(string containerName, string blobName)
        {
            var response = await _httpClient.DeleteAsync($"api/blobstorage/{containerName}/{blobName}");
            if (response.IsSuccessStatusCode)
            {
                return OperationResult<bool>.Ok(true);
            }
            else
            {
                return OperationResult<bool>.Ok(false);
            }
        }

        public async Task<OperationResult<List<string>>> ListBlobsAsync(string containerName)
        {
            var response = await _httpClient.GetAsync($"api/blobstorage/{containerName}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var blobs = JsonConvert.DeserializeObject<List<string>>(content);
                return OperationResult<List<string>>.Ok(blobs);
            }
            else
            {
                return OperationResult<List<string>>.Fail(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<OperationResult<bool>> BlobExistsAsync(string containerName, string blobName)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/blobstorage/{containerName}/exists/{blobName}");
                if (response.IsSuccessStatusCode)
                {

                    string responseBody = await response.Content.ReadAsStringAsync();
                    bool exists = bool.Parse(responseBody);
                    return OperationResult<bool>.Ok(true);
                }
                else 
                {
                    return OperationResult<bool>.Ok(false);
                }
               
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Fail($"Error checking blob existence: {ex.Message}");
            }
        }

        public async Task<OperationResult> UploadImageFromUrlAsync(string containerName, string blobName, string imageUrl)
        {
            Console.WriteLine(imageUrl);
           
            var encodedImageUrl = Uri.EscapeDataString(imageUrl);

            
            var requestUrl = $"api/blobstorage/{containerName}/{blobName}/url/{encodedImageUrl}";

        
            var response = await _httpClient.PostAsync(requestUrl, null);
            if (response.IsSuccessStatusCode)
            {
                return OperationResult.Ok();
            }
            else
            {
                return OperationResult.Fail(await response.Content.ReadAsStringAsync());
            }
        }


        public async Task<OperationResult> CreateBlobContainerAsync(string containerName)
        {
            var response = await _httpClient.PostAsync($"api/blobstorage/{containerName}", null);
            if (response.IsSuccessStatusCode)
            {
                return OperationResult.Ok();
            }
            else
            {
                return OperationResult.Fail(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<OperationResult> DeleteBlobContainerAsync(string containerName)
        {
            var response = await _httpClient.DeleteAsync($"api/blobstorage/{containerName}");
            if (response.IsSuccessStatusCode)
            {
                return OperationResult.Ok();
            }
            else
            {
                return OperationResult.Fail(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<OperationResult<bool>> ContainerExistsAsync(string containerName)
        {
            var response = await _httpClient.GetAsync($"api/blobstorage/{containerName}/exists");
            if (response.IsSuccessStatusCode)
            {
                return OperationResult<bool>.Ok(true);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return OperationResult<bool>.Ok(false);
            }
            else
            {
                return OperationResult<bool>.Fail(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<OperationResult<List<string>>> ListBlobContainersAsync()
        {
            var response = await _httpClient.GetAsync("api/blobstorage");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var containers = JsonConvert.DeserializeObject<List<string>>(content);
                if (containers == null)
                {
                  return  OperationResult<List<string>>.Fail("Failed to deserialize blob containers");
                }
                return OperationResult<List<string>>.Ok(containers);
            }
            else
            {
                return OperationResult<List<string>>.Fail(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<OperationResult<string>> GetBlobSasUrl(string containerName, string blobName)
        {
            var response = await _httpClient.GetAsync($"api/blobstorage/{containerName}/{blobName}/sas");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return OperationResult<string>.Ok(content);
            }
            else
            {
                return OperationResult<string>.Fail(await response.Content.ReadAsStringAsync());
            }
        }


    }


}
