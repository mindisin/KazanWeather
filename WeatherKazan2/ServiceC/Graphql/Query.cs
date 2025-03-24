using ServiceC.Domain;
using ServiceC.Interfaces;
namespace ServiceC.Graphql
{
    public class Query
    {
        [UseOffsetPaging()]
        [UseProjection]
        [UseSorting()]
        public IQueryable<Weather> GetLatestTenRecords([Service] IAppDbContext dbContext) =>
            dbContext.WeatherRecords;
    }
}