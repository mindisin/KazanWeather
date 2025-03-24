using ServiceA.Handlers;
using ServiceA.interfaces;
using ServiceA.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddHttpClient<IGetWeather, GetWeather>();

services.AddTransient<IGetWeather, GetWeather>();
services.AddTransient<IWeatherApiHandler, WeatherApiHandler>();
services.AddHostedService<ServiceABackgroundService>();

var app = builder.Build();


//app.MapGet("/weather", async (WeatherApiHandler handler, CancellationToken cancellationToken)
  //  => await handler.WeatherApiHandleAsync(cancellationToken));

app.Run();




// https://api.openweathermap.org/data/2.5/weather?lat=55.7887&lon=49.1221&appid=806aa9651e3c7bd7c1b1602699e26116&units=metric