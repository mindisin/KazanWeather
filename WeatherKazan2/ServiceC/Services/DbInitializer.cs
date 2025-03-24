using ServiceC.Domain;
using ServiceC.Interfaces;
using ServiceC.Db;
using Microsoft.EntityFrameworkCore;

namespace ServiceC.Services
{
    public class DbInitializer(AppDbContext dbContext, ILogger<DbInitializer> logger) : IDbInitializer
    {
        public async Task InitializeAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Автонакат миграций");
            await dbContext.Database.MigrateAsync(cancellationToken);
            logger.LogInformation("Миграции прменены");

            // Дополнительные действия, например, заполнение базы начальными данными
            if (!dbContext.WeatherRecords.Any())
            {
                logger.LogInformation("Добавление в бд трёх записей");
                dbContext.WeatherRecords.Add(new Weather
                {
                    Id = Guid.NewGuid(),
                    CreateDate = DateTime.UtcNow,
                    WeatherDate = DateTime.UtcNow,
                    WindSpeed = 3,
                    Description = (WeatherType)1,
                    Temperature = 3,
                    TemperatureFeelsLike = 4,
                });

                dbContext.WeatherRecords.Add(new Weather
                {
                    Id = Guid.NewGuid(),
                    CreateDate = DateTime.UtcNow,
                    WeatherDate = DateTime.UtcNow,
                    WindSpeed = 8,
                    Description = (WeatherType)13,
                    Temperature = 12,
                    TemperatureFeelsLike = 9,
                });

                dbContext.WeatherRecords.Add(new Weather
                {
                    Id = Guid.NewGuid(),
                    CreateDate = DateTime.UtcNow,
                    WeatherDate = DateTime.UtcNow,
                    WindSpeed = 2,
                    Description = (WeatherType)36,
                    Temperature = 31,
                    TemperatureFeelsLike = 36,
                });
                logger.LogInformation("Записи добавлены");
            }

            await dbContext.SaveChangesAsync(cancellationToken);
            logger.LogInformation("Изменения сохранены");
        }
    }
}
