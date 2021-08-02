namespace UrlShorteningKeyGenerationService.Services
{
    public interface IRandomKeyGenerator
    {
        string RandomKey(int length);
    }
}
