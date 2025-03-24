using ServiceB;
using ServiceB.Handlers;
using ServiceB.Interfaces;
using ServiceB.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddGrpcClient<SetWeatherService.SetWeatherServiceClient>(options =>
    options.Address = new Uri("http://localhost:5002"));

services.AddLogging();

services.AddTransient<IWeatherHandler, WeatherHandler>();
services.AddTransient<IGrpcSender, GrpcSender>();
services.AddTransient<WeatherConsumer>();
services.AddHostedService<ServiceBBackgroundService>();

var app = builder.Build();

app.Run();