using Microsoft.Azure.Cosmos;
using static System.Threading.Tasks.Task;
using static UrlShorteningKeyGenerationService.Services.CosmosFeedReader;

namespace UrlShorteningKeyGenerationService.Services;

public class CosmosDbService : ICosmosDbService
{
    private const string Query = "SELECT * FROM c WHERE NOT c.taken OFFSET 0 LIMIT @limit";

    private readonly Container container;

    public CosmosDbService(CosmosClient dbClient, string databaseName, string containerName)
    {
        container = dbClient.GetContainer(databaseName, containerName);
    }

    public async Task AddUrlKeyAsync(UrlKeyEntity entity)
    {
        await container.CreateItemAsync(entity, new PartitionKey(entity.Id));
    }

    public Task<IEnumerable<UrlKeyEntity>> GetUrlKeysAsync(int limit) =>
        ReadFully(container.GetItemQueryIterator<UrlKeyEntity>(BuildSelectTopQuery(limit)));

    public async Task MarkUrlKeysAsTaken(IEnumerable<string> urlKeys)
    {
        var updates = urlKeys
            .Select(key => container.UpsertItemAsync(new UrlKeyEntity(key, true), new PartitionKey(key)));
        await WhenAll(updates);
    }

    private static QueryDefinition BuildSelectTopQuery(int limit) =>
        new QueryDefinition(Query).WithParameter("@limit", limit);
}
