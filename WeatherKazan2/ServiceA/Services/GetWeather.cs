using Newtonsoft.Json;
using ServiceA.interfaces;
using ServiceA.Models;

namespace ServiceA.Services
{
    public class GetWeather(IConfiguration configuration, HttpClient client, ILogger<GetWeather> logger)
        : IGetWeather
    {
        private readonly string? _apiKey = configuration["WeatherConfig:ApiKey"];
        private readonly string? _lat = configuration["WeatherConfig:Lat"];
        private readonly string? _lon = configuration["WeatherConfig:Lon"];
        private readonly string? _domain = configuration["WeatherConfig:Domain"];

        public async Task<string?> GetWeatherWithApi(CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Составление uri");
                var uri = $"https://{_domain}/data/2.5/weather?lat={_lat}&lon={_lon}&appid={_apiKey}&units=metric";
                logger.LogInformation("Uri готова");

                logger.LogInformation("Обращение к api");
                var res = await client.GetStringAsync(uri, cancellationToken);
                logger.LogInformation($"строка от api получена {res}");

                logger.LogInformation("десереализация");
                var weatherJson = JsonConvert.DeserializeObject<dynamic>(res);
                logger.LogInformation("десереализировано");


                if (weatherJson is null)
                {
                    logger.LogError("Получение json погоды произошло неверно");
                    throw new JsonException("Погоды не будет (Непогода)");
                }
                    
                var weatherResult = new Weather
                {
                    Description = weatherJson.weather[0].description,
                    Temperature = weatherJson.main.temp,
                    TemperatureFeelsLike = weatherJson.main.feels_like,
                    WindSpeed = weatherJson.wind.speed
                };

                var serOb = JsonConvert.SerializeObject(weatherResult);
                logger.LogInformation($"Объект сериализован: {serOb}");

                return serOb;

            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Произошла ошибка в процессе выполнения метода GetWeatherWithApi");
                return null;
            }
        }
    }
}