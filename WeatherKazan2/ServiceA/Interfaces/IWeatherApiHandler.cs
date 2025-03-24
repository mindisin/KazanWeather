namespace ServiceA.interfaces;

public interface IWeatherApiHandler
{
    public Task WeatherApiHandleAsync(CancellationToken cancellationToken);

}