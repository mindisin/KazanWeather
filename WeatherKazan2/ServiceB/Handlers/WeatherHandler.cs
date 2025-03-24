using Grpc.Core;
using ServiceB.Interfaces;
using ServiceB.Models;
using ServiceB.Services;
using System.Text.Json;

namespace ServiceB.Handlers;

public class WeatherHandler(ILogger<WeatherHandler> logger,
     IServiceScopeFactory factory): IWeatherHandler
{
    public async Task HandleRequestAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Запуск обработки сообщений из Kafka");

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    logger.LogInformation("Создание scope");
                    using var scope = factory.CreateScope();
                    var consumer = scope.ServiceProvider.GetRequiredService<WeatherConsumer>();

                    // Ожидание сообщения от потребителя Kafka
                    var message = consumer.ConsumeMessages(cancellationToken);
                    logger.LogInformation($"Получено сообщение: {message}");

                    // Десериализация сообщения в объект

                    //var weatherData = JsonConvert.DeserializeObject<dynamic>(message);
                    var weatherData = JsonSerializer.Deserialize<WeatherModel>(message);

                    if (weatherData == null)
                    {
                        logger.LogError("Не удалось десериализовать сообщение из Kafka");
                        continue; // Переход к следующему сообщению
                    }


                    // Чисто для отчетности
                    var SerWer = JsonSerializer.Serialize(weatherData);
                    logger.LogInformation($"У нас такая строка бяка бука сервис б {SerWer}");

                    var descriptionWithSign = weatherData.Description.Replace(" ", "_");

                    var description = (int)Enum.Parse(typeof(WeatherType), descriptionWithSign);
                    var temperature = weatherData.Temperature;
                    var temperatureFeelsLike = weatherData.TemperatureFeelsLike;
                    var windSpeed = weatherData.WindSpeed;


                    var weatherRequest = new WeatherRequest
                    {
                        Description = description,
                        Temperature = temperature,
                        TemperatureFeelsLike = temperatureFeelsLike,
                        WindSpeed = windSpeed
                    };

                    var grpcSender = scope.ServiceProvider.GetRequiredService<IGrpcSender>();
                    // Отправка данных в gRPC-сервис
                    await grpcSender.SendToGrpcAsync(weatherRequest, cancellationToken);
                }
                catch (JsonException jsonEx)
                {
                    logger.LogError(jsonEx, "Ошибка при десериализации сообщения");
                }
                catch (RpcException rpcEx)
                {
                    logger.LogError(rpcEx, "Ошибка при отправке данных в gRPC-сервис");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Общая ошибка при обработке сообщения");
                }
            }
        }
        catch (OperationCanceledException)
        {
            logger.LogInformation("Обработка сообщений прервана");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обработке сообщений из Kafka");
        }
        finally
        {
            logger.LogInformation("Обработка сообщений завершена");
        }
    }
}