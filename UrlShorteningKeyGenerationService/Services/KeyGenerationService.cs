using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace UrlShorteningKeyGenerationService.Services
{
    public class KeyGenerationService : IKeyGenerationService
    {
        private static readonly SemaphoreSlim Semaphore = new(1, 1);

        private readonly ICosmosDbService cosmosDbService;

        public KeyGenerationService(ICosmosDbService cosmosDbService)
        {
            this.cosmosDbService = cosmosDbService;
        }

        public async Task<IEnumerable<string>> TakeKeys(int limit)
        {
            await Semaphore.WaitAsync();
            try
            {
                var keys = await GetUrlKeys(limit);
                await cosmosDbService.MarkUrlKeysAsTaken(keys);
                return keys;
            }
            finally
            {
                Semaphore.Release();
            }
        }

        private async Task<IReadOnlyCollection<string>> GetUrlKeys(int limit)
        {
            var entities = await cosmosDbService.GetUrlKeysAsync(limit);
            return entities
                .Select(it => it.Id)
                .ToImmutableList();
        }
    }
}
