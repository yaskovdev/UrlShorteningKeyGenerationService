namespace UrlShorteningKeyGenerationService.Services
{
    public interface IKeyGenerationService
    {
        public string RandomKey(int length);
    }
}
