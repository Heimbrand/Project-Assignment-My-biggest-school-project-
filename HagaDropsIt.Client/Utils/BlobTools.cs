using HagaDropsIt.Shared.Interfaces;

namespace HagaDropsIt.Client.Utils
{
    public class BlobTools : IBlobTools
    {
        private readonly ILogger<BlobTools> Logger;

        public BlobTools(ILogger<BlobTools> logger)
        {
            Logger = logger;
        }
        public string GenerateBlobName(string fileName)
        {
            return $"{Guid.NewGuid()}-{fileName}";
        }

        public string ExtractBlobNameFromUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                return string.Empty;

            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                Logger.LogWarning("Invalid URL format: {Url}", url);
                return string.Empty;
            }

            try
            {
                var uri = new Uri(url);
                return uri.Segments.Last().Trim('/');
            }
            catch (UriFormatException ex)
            {
                Logger.LogError(ex, "Failed to extract blob name from URL: {Url}", url);
                return string.Empty;
            }
        }

    }
}
