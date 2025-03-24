using ServiceA.Models;

namespace ServiceA.interfaces
{
    public interface IGetWeather
    {
        public Task<string?> GetWeatherWithApi(CancellationToken cancellationToken);
    }
}