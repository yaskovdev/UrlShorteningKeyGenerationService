using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShorteningKeyGenerationService.Services
{
    public class KeyGenerationService : IKeyGenerationService
    {
        private readonly ICosmosDbService cosmosDbService;

        public KeyGenerationService(ICosmosDbService cosmosDbService)
        {
            this.cosmosDbService = cosmosDbService;
        }

        public async Task<IEnumerable<string>> TakeKeys(int limit)
        {
            var entities = await cosmosDbService.GetUrlKeysAsync(limit);
            var keys = entities
                .Select(it => it.Id)
                .ToImmutableList();
            await cosmosDbService.MarkUrlKeysAsTaken(keys);
            return keys;
        }
    }
}
