namespace UrlShorteningKeyGenerationService.Services
{
    public interface IKeyGenerationService
    {
        void CreateRandomKey(object? context);
    }
}
