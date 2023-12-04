using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WeatherSimulator.Core.Services;
using WeatherSimulator.Data.Context;
using WeatherSimulator.Models.Entities;
using WeatherSimulator.Models.Exceptions;
using WheatherSimulator.Models.DTOs;

namespace WeatherSimulator.Logic.Services;

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
    private const string ERROR_TEXT = "Error during adding city to DB";
    private const string OPERATION_CANCELED = "Operation was canceled";

    private readonly WeatherDataContext _dbContext = weatherDataContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger _logger = logger;

    /// <summary>
    /// Добавляет новый город в БД
    /// </summary>
    /// <param name="city"></param>
    /// <returns></returns>
    public async Task AddCityAsync(City city, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Start adding a city with name {city.Name}");

        var lowerCityName = city.Name.ToLower();
        var cityInDb = await _dbContext.Cities.FirstOrDefaultAsync(c => c.Name.ToLower() == lowerCityName);

        if (cityInDb != null)
        {
            _logger.LogInformation($"A city with name {city.Name} already exists");
            throw new ItemExistsException($"An item already exists in the database");
        }

        _dbContext.Cities.Add(city);

        try
        {
            _logger.LogInformation($"Adding city to DB");
            await _dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation($"{city.Name} city was successfully added");
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogError($"{ERROR_TEXT}. {ex.Message}");
            throw;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError($"{ERROR_TEXT}. {ex.Message}");
            throw;
        }
        catch (OperationCanceledException ex)
        {
            _logger.LogError($"{OPERATION_CANCELED}. {ex.Message}");
            throw;
        }
        catch(Exception ex) 
        {
            _logger.LogError($"{ERROR_TEXT}. {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Получает все города из БД
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<CityDTO>> GetCitiesAsync(CancellationToken cancellationToken)
    {
        IEnumerable<CityDTO>? dto;

        try
        {
            _logger.LogError($"Getting city names");

            var cityNames = await _dbContext.Cities.ToListAsync(cancellationToken);

            dto = _mapper.Map<IEnumerable<CityDTO>>(cityNames);

            _logger.LogError($"Cities have been successfully gotten");
        }
        catch (OperationCanceledException ex)
        {
            _logger.LogError($"{OPERATION_CANCELED}. {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error during getting cities from DB. {ex.Message}");
            throw;
        }

        return dto;
    }
}
