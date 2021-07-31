using System.Collections.Generic;
using System.Threading.Tasks;

namespace UrlShorteningKeyGenerationService.Services
{
    public interface ICosmosDbService
    {
        Task AddUrlKeyAsync(UrlKeyEntity entity);
        Task<IEnumerable<UrlKeyEntity>> GetUrlKeysAsync(string query);
        Task MarkUrlKeysAsTaken(IEnumerable<string> urlKeys);
    }
}
