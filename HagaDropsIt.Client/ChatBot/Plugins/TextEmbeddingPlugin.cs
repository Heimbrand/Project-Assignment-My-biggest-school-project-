using HagaDropsIt.Client.ChatBot.Interfaces;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.SemanticKernel;
using System.ComponentModel;


namespace HagaDropsIt.Client.ChatBot.Plugins
{
    public class TextEmbeddingPlugin : ITextEmbeddingPlugin
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public TextEmbeddingPlugin(HttpClient httpClient, string apiKey)
        {
            _httpClient = httpClient;
            _apiKey = apiKey;
        }

        [KernelFunction, Description("Get the text embedding of a given text.")]
        public async Task<double[]> GetTextEmbeddingAsync(string text)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/text-embeddings")
            {
                Headers = { { "Authorization", $"Bearer {_apiKey}" } },
                Content = new StringContent(JsonConvert.SerializeObject(new { model = "text-embedding-ada-002", input = text }), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<TextEmbeddingResponse>(responseString);
            if (result.Data != null && result.Data.Length > 0)
            {
                return result.Data[0].Embedding;
            }
            throw new InvalidOperationException("No embedding data received from the API.");
        }
    }

    public class TextEmbeddingResponse
    {
        [JsonPropertyName("data")]
        public EmbeddingData[] Data { get; set; }
    }

    public class EmbeddingData
    {
        [JsonPropertyName("embedding")]
        public double[] Embedding { get; set; }
    }

}
