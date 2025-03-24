using Microsoft.EntityFrameworkCore;
using ServiceC.Domain;
using ServiceC.Interfaces;

namespace ServiceC.Db
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public DbSet<Weather> WeatherRecords { get; set; }

        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) { }
    }
}