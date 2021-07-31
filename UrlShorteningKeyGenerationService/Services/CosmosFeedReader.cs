using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace UrlShorteningKeyGenerationService.Services
{
    public static class CosmosFeedReader
    {
        public static async Task<IEnumerable<T>> ReadFully<T>(FeedIterator<T> feed) =>
            feed.HasMoreResults ? (await feed.ReadNextAsync()).Concat(await ReadFully(feed)) : ImmutableList<T>.Empty;
    }
}
