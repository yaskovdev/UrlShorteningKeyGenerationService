using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using static System.GC;
using static System.Threading.Tasks.Task;
using static System.TimeSpan;

namespace UrlShorteningKeyGenerationService.Services
{
    public class BackgroundKeyGenerator : IHostedService, IDisposable
    {
        private readonly Timer timer;

        public BackgroundKeyGenerator(IKeyGenerationService keyGenerationService)
        {
            timer = new Timer(keyGenerationService.CreateRandomKey);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer.Change(Zero, FromSeconds(1));
            return CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) =>
            CompletedTask;

        public void Dispose()
        {
            SuppressFinalize(this);
            timer.Dispose();
        }
    }
}
