using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using WeatherSimulator.Models.Entities;

namespace WeatherSimulator.Core.Repositories;

public interface IWeatherRepository
{
    Task AddWeatherAsync();
    Task<Weather> GetWeatherAsync(
        Expression<Func<Weather, bool>>? filter, 
        Func<IQueryable<Weather>, IIncludableQueryable<Weather, object>>? includes, 
        CancellationToken cancellationToken);
    Task<IEnumerable<Weather>> GetWeatherHistoryASync(
        Expression<Func<Weather, bool>>? filter,
        Func<IQueryable<Weather>, IIncludableQueryable<Weather, object>>? includes,
        CancellationToken cancellationToken);
}
