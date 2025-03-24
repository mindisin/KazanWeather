using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using ServiceC.Configurations;
using ServiceC.Db;
using ServiceC.Graphql;
using ServiceC.Interfaces;
using ServiceC.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

var kestrelOptions = configuration.GetSection(nameof(KestrelOptions)).Get<KestrelOptions>()!;


builder.WebHost.ConfigureKestrel((options) =>
{
    var restOptions = kestrelOptions.Options.First(x => x.EndpointType == EndpointType.Rest);
    options.ListenAnyIP(restOptions.Port, listenOptions => { listenOptions.Protocols = HttpProtocols.Http1; });

    var grpcOptions = kestrelOptions.Options.First(x => x.EndpointType == EndpointType.Grpc);
    options.ListenAnyIP(grpcOptions.Port, listenOptions => { listenOptions.Protocols = HttpProtocols.Http2; });
});


services.AddLogging();

services.AddDbContext<IAppDbContext, AppDbContext>(opt =>
    opt.UseNpgsql(configuration["ConnectionString:Default"]));

services.AddGrpc();

services
    .AddGraphQLCore()
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddProjections()
    .AddSorting();

services.AddScoped<IDbInitializer, DbInitializer>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    await dbInitializer.InitializeAsync(new CancellationToken());
}

app.MapGraphQL("/graphql");
app.UseRouting();

app.MapGrpcService<WeatherSetter>();

app.Run();