using System.ComponentModel.DataAnnotations;

namespace HagaDropsIt.Client.ChatBot.Entities
{
    public sealed class OpenAIOptions
    {
        [Required]
        public string ChatModelId { get; set; } = string.Empty;

        [Required]
        public string ApiKey { get; set; } = string.Empty;
    }
}
