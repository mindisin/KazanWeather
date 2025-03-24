using Microsoft.EntityFrameworkCore;
using ServiceC.Domain;

namespace ServiceC.Interfaces
{
    public interface IAppDbContext
    {
        public DbSet<Weather> WeatherRecords { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}