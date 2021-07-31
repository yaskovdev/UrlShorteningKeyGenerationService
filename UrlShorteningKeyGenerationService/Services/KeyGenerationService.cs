using Microsoft.Extensions.Logging;

namespace UrlShorteningKeyGenerationService.Services
{
    public class KeyGenerationService : IKeyGenerationService
    {
        private readonly IRandomKeyGenerator keyGenerator;
        private readonly ICosmosDbService cosmosDbService;
        private readonly ILogger<KeyGenerationService> logger;

        public KeyGenerationService(IRandomKeyGenerator keyGenerator,
            ICosmosDbService cosmosDbService, ILogger<KeyGenerationService> logger)
        {
            this.keyGenerator = keyGenerator;
            this.cosmosDbService = cosmosDbService;
            this.logger = logger;
        }

        public void CreateRandomKey(object? context)
        {
            var randomKey = keyGenerator.RandomKey(6);
            logger.LogInformation("Generated key {RandomKey}", randomKey);
            cosmosDbService.AddUrlKeyAsync(new UrlKeyEntity(randomKey, false));
        }
    }
}
