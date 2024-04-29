using HagaDropsIt.Client.ChatBot.Entities;

namespace HagaDropsIt.Client.ChatBot.Interfaces
{
    public interface IChatService
    {
        Task<ChatMessage> GetResponseAsync(string userInput);

        event Func<ChatMessage, Task> MessageReceived;

        string ExtractImageUrl(string message);

    }
}
