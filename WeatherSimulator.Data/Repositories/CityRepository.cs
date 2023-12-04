using WeatherSimulator.Core.Repositories;
using WeatherSimulator.Models.Entities;
using WheatherSimulator.Models.DTOs;

namespace WeatherSimulator.Data.Repositories;

// TODO: Make a realization
public class CityRepository : ICityRepository
{
    public Task AddCityAsync(City city)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CityDTO>> GetCities()
    {
        throw new NotImplementedException();
    }
}
