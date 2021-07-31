using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UrlShorteningKeyGenerationService.Services;

namespace UrlShorteningKeyGenerationService.Controllers
{
    [ApiController]
    [Route("/api/v1/keys")]
    public class KeyGenerationController : ControllerBase
    {
        private const int DefaultKeyLength = 6;

        private readonly IKeyGenerationService keyGenerationService;
        private readonly ILogger<KeyGenerationController> logger;

        public KeyGenerationController(IKeyGenerationService keyGenerationService,
            ILogger<KeyGenerationController> logger)
        {
            this.keyGenerationService = keyGenerationService;
            this.logger = logger;
        }

        [HttpGet]
        public string Get(int? keyLength)
        {
            var length = keyLength ?? DefaultKeyLength;
            logger.LogInformation("Going to generate a random key of length {Length}", length);
            return keyGenerationService.RandomKey(length);
        }
    }
}
