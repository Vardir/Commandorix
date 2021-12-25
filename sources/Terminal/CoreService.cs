using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Vardirsoft.Commandorix.Terminal;

public sealed class CoreService : BackgroundService
{
  private readonly ILogger<CoreService> _logger;

  public CoreService(ILogger<CoreService> logger)
  {
    _logger = logger;
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    while (!stoppingToken.IsCancellationRequested)
    {
      _logger.LogInformation("Worker running at: {Time}", DateTimeOffset.Now);
      await Task.Delay(1000, stoppingToken);
    }
  }
}