using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WeatherSimulator.Core.Services;
using WeatherSimulator.Data.Context;
using WeatherSimulator.Models.Entities;
using WheatherSimulator.Models.DTOs;

namespace WeatherSimulator.Data.Services;

/// <summary>
/// Сервис логики обрботки городов
/// </summary>
/// <param name="weatherDataContext"></param>
/// <param name="logger"></param>
/// <param name="mapper"></param>
public class CityService(
    WeatherDataContext weatherDataContext, 
    ILogger<CityService> logger, 
    IMapper mapper) : ICityService
{
    private readonly WeatherDataContext _dbContext = weatherDataContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger _logger = logger;

    /// <summary>
    /// Добавляет новый город в БД
    /// </summary>
    /// <param name="city"></param>
    /// <returns></returns>
    public async Task AddCityAsync(City city)
    {
        _logger.LogInformation($"Start adding a city with name {city.Name}");

        var cityInDb = await _dbContext.Cities.FirstOrDefaultAsync(c => c.Name.ToLower() == city.Name.ToLower());

        if (cityInDb != null)
        {
            _logger.LogInformation($"A city with name {city.Name} already exists");
            return;
        }

        _dbContext.Cities.Add(city);

        try
        {
            _logger.LogInformation($"Adding city to DB");
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"{city.Name} city was successfully added");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error during adding {city.Name} city to DB. {ex.Message}");
        }
    }

    /// <summary>
    /// Получает все города из БД
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<CityDTO>> GetCities()
    {
        var dto = Enumerable.Empty<CityDTO>();

        try
        {
            _logger.LogError($"Getting city names");

            var cityNames = await _dbContext.Cities.ToListAsync();

            dto = _mapper.Map<IEnumerable<CityDTO>>(cityNames);

            _logger.LogError($"Cities have been successfully gotten");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error during getting cities from DB. {ex.Message}");
        }
        
        return dto;
    }
}
