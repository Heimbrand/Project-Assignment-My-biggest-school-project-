using Azure.Storage.Blobs;
using HagaDropsIt.Shared.ReturnTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HagaDropsIt.Shared.Interfaces
{
    public interface IFileHandlerServices
    {
        Task<OperationResult> UploadImageFromUrlAsync(string containerName, string blobName, string imageUrl);
    }

}
