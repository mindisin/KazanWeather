using ServiceA.interfaces;
using ServiceA.Services;

namespace ServiceA.Handlers
{
    public class WeatherApiHandler(
        IGetWeather weatherService,
        ILogger<WeatherApiHandler> logger,
        IConfiguration configuration,
        ILogger<WeatherProducer> producerLogger): IWeatherApiHandler
    { 
        private readonly WeatherProducer _producer = new(configuration, producerLogger);

        public async Task WeatherApiHandleAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Вызов сервиса для получения данных о погоде");
            var result = await weatherService.GetWeatherWithApi(cancellationToken);
            logger.LogInformation($"Данные о погоде получены: {result}");
            
            await _producer.ProduceAsync(result ?? throw new InvalidOperationException(), cancellationToken);
        }
    }
}