using Microsoft.Extensions.Logging;

namespace UrlShorteningKeyGenerationService.Services
{
    public class RandomKeyCreationService : IRandomKeyCreationService
    {
        private readonly IRandomKeyGenerator keyGenerator;
        private readonly ICosmosDbService cosmosDbService;
        private readonly ILogger<RandomKeyCreationService> logger;

        public RandomKeyCreationService(IRandomKeyGenerator keyGenerator,
            ICosmosDbService cosmosDbService, ILogger<RandomKeyCreationService> logger)
        {
            this.keyGenerator = keyGenerator;
            this.cosmosDbService = cosmosDbService;
            this.logger = logger;
        }

        public void CreateRandomKey(object? context)
        {
            var randomKey = keyGenerator.RandomKey(6);
            logger.LogInformation("Generated key {RandomKey}", randomKey);
            cosmosDbService.AddUrlKeyAsync(new UrlKeyEntity(randomKey));
        }
    }
}
