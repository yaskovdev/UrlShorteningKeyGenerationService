using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using static System.Threading.Tasks.Task;
using static UrlShorteningKeyGenerationService.Services.CosmosFeedReader;

namespace UrlShorteningKeyGenerationService.Services
{
    public class CosmosDbService : ICosmosDbService
    {
        private readonly Container container;

        public CosmosDbService(CosmosClient dbClient, string databaseName, string containerName)
        {
            container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddUrlKeyAsync(UrlKeyEntity entity)
        {
            await container.CreateItemAsync(entity, new PartitionKey(entity.Id));
        }

        public Task<IEnumerable<UrlKeyEntity>> GetUrlKeysAsync(string query) =>
            ReadFully(container.GetItemQueryIterator<UrlKeyEntity>(new QueryDefinition(query)));

        public async Task MarkUrlKeysAsTaken(IEnumerable<string> urlKeys)
        {
            var updates = urlKeys
                .Select(key => container.UpsertItemAsync(new UrlKeyEntity(key, true), new PartitionKey(key)));
            await WhenAll(updates);
        }
    }
}
