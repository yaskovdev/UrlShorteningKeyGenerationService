namespace UrlShorteningKeyGenerationService.Services;

public interface IRandomKeyGenerator
{
    public string RandomKey(int length);
}
