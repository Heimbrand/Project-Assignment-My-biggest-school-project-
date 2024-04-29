namespace HagaDropsIt.Client.ChatBot.Interfaces
{
    public interface IDallE3Plugin
    {
        Task<string> GenerateImageAsync(string prompt);
    }

}
