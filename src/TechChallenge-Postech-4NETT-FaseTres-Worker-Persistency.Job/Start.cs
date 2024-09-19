using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Postech.GroupEight.TechChallenge.ContactManagement.Job;

public class Start : BackgroundService
{
    private readonly ILogger<Start> _logger;

    public Start(ILogger<Start> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker execution started at: {time}", DateTimeOffset.Now);
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running and waiting for messages at: {time}", DateTimeOffset.Now);
            await Task.Delay(10000, stoppingToken);
        }
        _logger.LogInformation("Worker execution stopped at: {time}", DateTimeOffset.UtcNow);
    }

}
