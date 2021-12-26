using System.Collections.Immutable;
using static System.Linq.Enumerable;

namespace UrlShorteningKeyGenerationService.Services;

public static class QueueExtensions
{
    public static void EnqueueMany<T>(this Queue<T> queue, IEnumerable<T> items)
    {
        foreach (var item in items) queue.Enqueue(item);
    }

    public static IReadOnlyCollection<T> DequeueMany<T>(this Queue<T> queue, int limit) =>
        Range(0, limit)
            .Select(_ => queue.Dequeue())
            .ToImmutableList();
}
