using static System.Linq.Enumerable;

namespace UrlShorteningKeyGenerationService.Services;

public class RandomKeyGenerator : IRandomKeyGenerator
{
    private const string Chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    private static readonly Random Random = new();

    public string RandomKey(int length) =>
        new(RandomCharArray(length));

    private static char[] RandomCharArray(int length) =>
        Repeat(Chars, length)
            .Select(s => s[Random.Next(s.Length)])
            .ToArray();
}
