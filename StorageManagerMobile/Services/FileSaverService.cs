using CommunityToolkit.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagerMobile.Services
{
    public class FileSaverService : IFileSaver
    {
        public Task<FileSaverResult> SaveAsync(string initialPath, string fileName, Stream stream, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<FileSaverResult> SaveAsync(string fileName, Stream stream, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
