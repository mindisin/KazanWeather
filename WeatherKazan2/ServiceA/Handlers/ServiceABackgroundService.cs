using ServiceA.interfaces;

namespace ServiceA.Handlers
{
    public class ServiceABackgroundService(ILogger<ServiceABackgroundService> logger,
        IServiceScopeFactory factory) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("WeatherBackgroundService запущен.");

            using var timer = new PeriodicTimer(TimeSpan.FromMinutes(1));

            while (await timer.WaitForNextTickAsync(cancellationToken))
            {
                try
                {
                    logger.LogInformation("Создание scope");
                    using var scope = factory.CreateScope();
                    var handler = scope.ServiceProvider.GetRequiredService<IWeatherApiHandler>();
                    logger.LogInformation("Вызов WeatherApiHandleAsyn");
                    await handler.WeatherApiHandleAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Произошла ошибка при вызове WeatherApiHandleAsync.");
                }
            }

            logger.LogInformation("WeatherBackgroundService остановлен.");
        }
    }
}