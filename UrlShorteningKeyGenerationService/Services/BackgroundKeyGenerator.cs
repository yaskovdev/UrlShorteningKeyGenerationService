using static System.GC;
using static System.Threading.Tasks.Task;
using static System.TimeSpan;

namespace UrlShorteningKeyGenerationService.Services;

public class BackgroundKeyGenerator : IHostedService, IDisposable
{
    private readonly Timer timer;

    public BackgroundKeyGenerator(IRandomKeyCreationService randomKeyCreationService)
    {
        timer = new Timer(randomKeyCreationService.CreateRandomKey);
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        timer.Change(Zero, FromMilliseconds(5000));
        return CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => CompletedTask;

    public void Dispose()
    {
        SuppressFinalize(this);
        timer.Dispose();
    }
}
