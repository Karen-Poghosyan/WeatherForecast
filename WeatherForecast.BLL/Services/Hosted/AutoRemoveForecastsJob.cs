using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using WeatherForecast.Core.Abstractions.BLL;
using Timer = System.Threading.Timer;

namespace WeatherForecast.BLL.Services.Hosted
{
    public class AutoRemoveForecastsJob : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<AutoRemoveForecastsJob> _logger;
        public AutoRemoveForecastsJob(IServiceProvider serviceProvider, ILogger<AutoRemoveForecastsJob> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                Task.Run(async () =>
                {
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        var nextRunTime = DateTime.Today.AddHours(18);
                        if (DateTime.Now >= nextRunTime)
                        {
                            nextRunTime = nextRunTime.AddDays(1);
                        }

                        var timeToNextRun = nextRunTime - DateTime.Now;

                        await Task.Delay(timeToNextRun, cancellationToken);

                        using (var scope = _serviceProvider.CreateAsyncScope())
                        {
                            var weatherForecastService = scope.ServiceProvider.GetService<IWeatherForecastService>();

                            var oneMonthAgo = DateTime.Today.AddMonths(-1);
                            await weatherForecastService.RemoveForecastsByDateAsync(oneMonthAgo);
                        }
                    }
                
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while removing old forecasts");
                _logger.LogError($"AutoRemoveForecastsJob -- {ex.Message}");
                _logger.LogError($"AutoRemoveForecastsJob -- {ex.InnerException}");
                _logger.LogError($"AutoRemoveForecastsJob -- {ex.StackTrace}");
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
