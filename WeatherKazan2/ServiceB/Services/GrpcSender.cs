using Grpc.Core;
using ServiceB.Interfaces;

namespace ServiceB.Services
{
    public class GrpcSender(ILogger<GrpcSender> logger,
        SetWeatherService.SetWeatherServiceClient client): IGrpcSender
    {
        public async Task SendToGrpcAsync(WeatherRequest request, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Попытка выполнить запрос к gRPC-сервису");
                await client.SetWeatherAsync(request, cancellationToken: cancellationToken);
                logger.LogInformation("Запрос отправлен успешно");
            }
            catch (RpcException rpcEx)
            {
                logger.LogError(rpcEx, "Ошибка при выполнении запроса к gRPC-сервису");
            }
        }
    }
}
