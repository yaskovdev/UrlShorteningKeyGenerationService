namespace UrlShorteningKeyGenerationService.Services;

public interface ICosmosDbService
{
    Task AddUrlKeyAsync(UrlKeyEntity entity);

    Task<IEnumerable<UrlKeyEntity>> GetUrlKeysAsync(int limit);

    Task MarkUrlKeysAsTaken(IEnumerable<string> urlKeys);
}
