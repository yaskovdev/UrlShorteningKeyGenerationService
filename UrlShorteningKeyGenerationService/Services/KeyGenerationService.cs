using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static UrlShorteningKeyGenerationService.Services.IConstants;

namespace UrlShorteningKeyGenerationService.Services
{
    public class KeyGenerationService : IKeyGenerationService
    {
        private static readonly SemaphoreSlim Semaphore = new(1, 1);

        private readonly ICosmosDbService cosmosDbService;
        private readonly Queue<string> cache = new(CacheCapacity);

        public KeyGenerationService(ICosmosDbService cosmosDbService)
        {
            this.cosmosDbService = cosmosDbService;
        }

        public async Task<IEnumerable<string>> TakeKeys(int limit)
        {
            await Semaphore.WaitAsync();
            try
            {
                await EnsureCacheHasEnoughItems(limit);
                return cache.DequeueMany(limit);
            }
            finally
            {
                Semaphore.Release();
            }
        }

        private async Task EnsureCacheHasEnoughItems(int limit)
        {
            if (cache.Count < limit)
            {
                var keys = await GetUrlKeys(CacheCapacity - cache.Count);
                await cosmosDbService.MarkUrlKeysAsTaken(keys);
                cache.EnqueueMany(keys);
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
