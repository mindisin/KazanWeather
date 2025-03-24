using Grpc.Core;
using ServiceC.Domain;
using ServiceC.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ServiceC.Services
{
    public class WeatherSetter(IAppDbContext dbContext, ILogger<WeatherSetter> logger) : SetWeatherService.SetWeatherServiceBase
    {
        public override async Task<VoidMessage> SetWeather(WeatherRequest request, ServerCallContext context)
        {
            var serializedRequest = JsonSerializer.Serialize(request);

            logger.LogInformation($"SetWeather request: {serializedRequest}");
            var weather = new Weather
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTime.UtcNow,   
                WeatherDate = DateTime.UtcNow,
                WindSpeed = request.WindSpeed,
                Temperature = request.Temperature,
                TemperatureFeelsLike = request.TemperatureFeelsLike,
                Description = (WeatherType)request.Description
            };
            logger.LogInformation($"Создан объект weather");
            
            await dbContext.WeatherRecords.AddAsync(weather, context.CancellationToken);
            await dbContext.SaveChangesAsync(context.CancellationToken);

            logger.LogInformation("Данные сохранены");
            
            return await Task.FromResult(new VoidMessage());
        }
    }
}