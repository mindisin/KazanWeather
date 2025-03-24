namespace ServiceB.Interfaces
{
    public interface IGrpcSender
    {
        public Task SendToGrpcAsync(WeatherRequest request, CancellationToken cancellationToken);
        
    }
}
