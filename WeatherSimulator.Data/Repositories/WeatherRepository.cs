using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using WeatherSimulator.Core.Repositories;
using WeatherSimulator.Models.Entities;

namespace WeatherSimulator.Data.Repositories;

// TODO: Make a realization
public class WeatherRepository : IWeatherRepository
{
    public Task AddWeatherAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Weather> GetWeatherAsync(
        Expression<Func<Weather, bool>>? filter, 
        Func<IQueryable<Weather>, IIncludableQueryable<Weather, object>>? includes, 
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Weather>> GetWeatherHistoryASync(
        Expression<Func<Weather, bool>>? filter, 
        Func<IQueryable<Weather>, IIncludableQueryable<Weather, object>>? includes, 
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
