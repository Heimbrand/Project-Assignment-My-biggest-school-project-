using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HagaDropsIt.Shared.Interfaces
{
    public interface IBlobTools
    {
        string GenerateBlobName(string fileName);

        string ExtractBlobNameFromUrl(string url);

    }
}
