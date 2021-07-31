using System.Collections.Generic;
using System.Threading.Tasks;

namespace UrlShorteningKeyGenerationService.Services
{
    public interface IKeyGenerationService
    {
        Task<IEnumerable<string>> TakeKeys(int defaultLimit);
    }
}
