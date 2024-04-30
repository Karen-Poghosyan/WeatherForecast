using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using WeatherForecast.Core.Abstractions.BLL;
using WeatherForecast.Core.Constants;
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
                        var nextRunTime = DateTime.Today.AddHours(TimeParameters.AutoUpdateHour);
                        if (DateTime.Now >= nextRunTime)
                        {
                            nextRunTime = nextRunTime.AddDays(TimeParameters.AutoUpdateIntereval);
                        }

                        var timeToNextRun = nextRunTime - DateTime.Now;

                        await Task.Delay(timeToNextRun, cancellationToken);

                        using (var scope = _serviceProvider.CreateAsyncScope())
                        {
                            var weatherForecastService = scope.ServiceProvider.GetService<IWeatherForecastService>();

                            var oneMonthAgo = DateTime.Today.AddMonths(TimeParameters.AutoRemoveMonths);
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
