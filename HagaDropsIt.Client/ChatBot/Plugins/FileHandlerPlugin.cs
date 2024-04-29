using HagaDropsIt.Client.Services;
using HagaDropsIt.Shared.Interfaces;
using HagaDropsIt.Shared.ReturnTypes;
using System.ComponentModel;
using System.Threading.Tasks;

namespace HagaDropsIt.Client.ChatBot.Plugins
{
    public class FileHandlerPlugin
    {
        private IFileHandlerServices _fileHandlerServices;

        public FileHandlerPlugin(IFileHandlerServices fileHandlerServices)
        {
            _fileHandlerServices = fileHandlerServices;
        }

        [Microsoft.SemanticKernel.KernelFunction, Description("Upload to blob storage from a URL, the blob name is the image description if the user does not provide a name")]
        public Task<OperationResult> UploadImageFromUrl(string containerName, string blobName, string imageUrl)
        {
            return _fileHandlerServices.UploadImageFromUrlAsync(containerName, blobName, imageUrl);
        }
    }
}
