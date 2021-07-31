using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrlShorteningKeyGenerationService.Services;

namespace UrlShorteningKeyGenerationService.Controllers
{
    [ApiController]
    [Route("/api/v1/keys")]
    public class KeyGenerationController : ControllerBase
    {
        private const int DefaultLimit = 10;

        private readonly ICosmosDbService cosmosDbService;

        public KeyGenerationController(ICosmosDbService cosmosDbService)
        {
            this.cosmosDbService = cosmosDbService;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> Get(int? limit)
        {
            var query = $"SELECT * FROM c WHERE NOT c.taken ORDER BY c.id OFFSET 0 LIMIT {limit ?? DefaultLimit}";
            var entities = await cosmosDbService.GetUrlKeysAsync(query);
            var keys = entities
                .Select(it => it.Id)
                .ToImmutableList();
            await cosmosDbService.MarkUrlKeysAsTaken(keys);
            return keys;
        }
    }
}
