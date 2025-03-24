namespace ServiceB.Interfaces;

public interface IWeatherHandler
{
    public Task HandleRequestAsync(CancellationToken cancellationToken);
}