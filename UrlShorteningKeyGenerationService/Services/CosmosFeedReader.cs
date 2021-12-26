using System.Collections.Immutable;
using Microsoft.Azure.Cosmos;

namespace UrlShorteningKeyGenerationService.Services;

public static class CosmosFeedReader
{
    public static async Task<IEnumerable<T>> ReadFully<T>(FeedIterator<T> feed) =>
        feed.HasMoreResults ? (await feed.ReadNextAsync()).Concat(await ReadFully(feed)) : ImmutableList<T>.Empty;
}
