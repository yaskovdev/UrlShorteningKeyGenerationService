namespace UrlShorteningKeyGenerationService.Services;

public interface IKeyGenerationService
{
    Task<IEnumerable<string>> TakeKeys(int limit);
}
