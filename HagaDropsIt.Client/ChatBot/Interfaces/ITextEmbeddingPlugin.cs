namespace HagaDropsIt.Client.ChatBot.Interfaces
{
    public interface ITextEmbeddingPlugin
    {
        Task<double[]> GetTextEmbeddingAsync(string text);
    }
}
