using ServiceB.Interfaces;

namespace ServiceB.Handlers
{
    public class ServiceBBackgroundService(ILogger<ServiceBBackgroundService> logger,
         IServiceScopeFactory factory) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("WeatherBackgroundService запущен.");

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    logger.LogInformation("Создание scope");
                    using var scope = factory.CreateScope();
                    var handler = scope.ServiceProvider.GetRequiredService<IWeatherHandler>();
                    logger.LogInformation("Вызов HandleRequestAsync");
                    await handler.HandleRequestAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Ошибка при вызове HandleRequestAsync");
                }
            }

            logger.LogInformation("WeatherBackgroundService остановлен.");
        }
    }
}

