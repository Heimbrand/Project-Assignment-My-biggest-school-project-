using HagaDropsIt.Client.ChatBot.Interfaces;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace HagaDropsIt.Client.ChatBot.Plugins
{
    public class DallE3Plugin : IDallE3Plugin
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public DallE3Plugin(HttpClient httpClient, string apiKey)
        {
            _httpClient = httpClient;
            _apiKey = apiKey;
        }

        [KernelFunction, Description("Generate an image using DALL-E 3.")]
        public async Task<string> GenerateImageAsync(string prompt)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/images/generations")
            {
                Headers = { { "Authorization", $"Bearer {_apiKey}" } },
                Content = new StringContent(JsonConvert.SerializeObject(new { model = "dall-e-3", prompt = prompt }), Encoding.UTF8, "application/json")
            };

            try
            {
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();

                var jsonResult = JsonConvert.DeserializeObject<OpenAIImageResponse>(responseString);
                if (jsonResult == null || jsonResult.Data == null || jsonResult.Data.Length == 0)
                {
                    throw new ApplicationException("No image data was returned.");
                }

                return jsonResult.Data[0].Url;
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Failed to generate image with DALL-E 3.", ex);
            }
            catch (JsonException ex)
            {
                throw new ApplicationException("Error parsing the JSON response from DALL-E 3.", ex);
            }
        }
    }

    public class OpenAIImageResponse
    {
        public OpenAIImageData[] Data { get; set; }
    }

    public class OpenAIImageData
    {
        public string Url { get; set; }
    }
}
