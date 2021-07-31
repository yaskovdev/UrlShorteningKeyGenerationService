using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace UrlShorteningKeyGenerationService.Services
{
    public static class CosmosFeedReader
    {
        public static async Task<IEnumerable<T>> ReadFully<T>(FeedIterator<T> feed)
        {
            List<T> urlKeys = new();
            while (feed.HasMoreResults)
            {
                var response = await feed.ReadNextAsync();
                urlKeys.AddRange(response.ToList());
            }

            return urlKeys.ToImmutableList();
        }
    }
}
