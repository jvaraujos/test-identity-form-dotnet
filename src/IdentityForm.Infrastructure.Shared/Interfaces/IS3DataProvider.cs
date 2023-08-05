using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityForm.Infrastructure.Shared.Interfaces
{
    public interface IS3DataProvider
    {
        Task<Stream> GetStreamAsync(string objectKey, bool publicBucket = false, CancellationToken cancellationToken = default);
        Task<Stream> PostStreamAsync(string filePath, Stream stream, bool publicBucket = false, CancellationToken cancellationToken = default);
        Task DeteteObjectAsync(string objectKey, bool publicBucket = false, CancellationToken cancellationToken = default);
    }
}
