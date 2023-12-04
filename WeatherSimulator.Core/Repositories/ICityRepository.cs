using WeatherSimulator.Models.Entities;
using WheatherSimulator.Models.DTOs;

namespace WeatherSimulator.Core.Repositories;

public interface ICityRepository
{
    Task AddCityAsync(City city);
    Task<IEnumerable<CityDTO>> GetCities();
}
