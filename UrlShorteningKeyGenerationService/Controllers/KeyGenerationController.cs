using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrlShorteningKeyGenerationService.Services;

namespace UrlShorteningKeyGenerationService.Controllers
{
    [ApiController]
    [Route("/api/v1/keys")]
    public class KeyGenerationController : ControllerBase
    {
        private const int DefaultLimit = 10;

        private readonly IKeyGenerationService keyGenerationService;

        public KeyGenerationController(IKeyGenerationService keyGenerationService)
        {
            this.keyGenerationService = keyGenerationService;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> Get([FromQuery] GetKeysRequest request) =>
            await keyGenerationService.TakeKeys(request.Limit ?? DefaultLimit);
    }
}
