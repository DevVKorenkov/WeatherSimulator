using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WeatherSimulator.Core.Services;
using WeatherSimulator.Data.Context;
using WeatherSimulator.Models.Entities;
using WeatherSimulator.Models.Models;

namespace WeatherSimulator.Logic.Services;

/// <summary>
/// Сервис логики обработки погоды.
/// </summary>
/// <param name="wheatherDataContext"></param>
/// <param name="weatherParams"></param>
/// <param name="logger"></param>
public class WeatherService(
    WeatherDataContext wheatherDataContext, 
    IOptions<MokWeatherParams> weatherParams,
    ILogger<WeatherService> logger) : IWeatherService
{
    /// <summary>
    /// Номера месяцев для определения условно приемлимой температуры для генерации.
    /// </summary>
    private const int march = 4;
    private const int october = 10;

    private readonly WeatherDataContext _dbContext = wheatherDataContext;
    private readonly MokWeatherParams _mokWeatherParams = weatherParams.Value;
    private readonly ILogger<WeatherService> _logger = logger;

    /// <summary>
    /// Получает историю погоды сгенерированную для города по ID города. Возвращает null если город не найден.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Weather>> GetWeatherHistoryByCityIdAsync(int id, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Start to getting weather history for a city with ID - {id}.");

        var history = Enumerable.Empty<Weather>();

        try
        {
            history = await _dbContext.Wheathers
                .AsNoTracking()
                .Include(x => x.City)
                .Include(x => x.Temperature)
                .Include(x => x.Wind)
                .Where(x => x.City.Id == id)
                .ToArrayAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error during requesting the city with id - {id}. {ex.Message}");
        }

        if (history is null || !history.Any())
        {
            _logger.LogWarning($"History for the city with ID {id} hasn't been found");
        }
        else
        {
            _logger.LogWarning($"History for the city with ID {id} has been found");
        }

        return history;
    }

    /// <summary>
    /// Получает последнюю сгенерированную погоду по ID города. Возвращает null если город не найден. 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Weather> GetWeatherByCityIdAsync(int id, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Start to getting weather for a city with ID - {id}");

        Weather weather = new Weather();

        try
        {
            weather = await _dbContext.Wheathers
               .AsNoTracking()
               .Include(x => x.City)
               .Include(x => x.Temperature)
               .Include(x => x.Wind)
               .LastOrDefaultAsync(x => x.City.Id == id);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error during requesting the city with id - {id}. {ex.Message}");
        }

        if (weather is null)
        {
            _logger.LogWarning($"The city with ID {id} hasn't been found");
        }

        return weather;
    }

    /// <summary>
    /// Генерирует погоду случайным образом для каждого города в БД.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task RandomGenerateAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Start generating the weather.");

        var random = new Random();

        if (_dbContext.Cities is null || _dbContext.Cities.Count() == 0)
        {
            throw new ArgumentNullException(nameof(_dbContext.Cities));
        }

        foreach (var city in _dbContext.Cities)
        {
            var date = RandomDate();
            //TODO: Fix temperature generating
            var temperatureCount
                = date.Month < march
                ? random.Next(_mokWeatherParams.LowestTemperature, _mokWeatherParams.MiddleTemperature)
                : (date.Month > march && date.Month < october)
                ? random.Next(_mokWeatherParams.MiddleTemperature, _mokWeatherParams.HighestTemperature)
                : random.Next(_mokWeatherParams.LowestTemperature, _mokWeatherParams.MiddleTemperature);

            var wind = new Wind
            {
                Power = random.Next(default, _mokWeatherParams.HighestWindPower),
                WindDirection = WindDirection.Directions[random.Next(WindDirection.Directions.Length)],
            };

            var temperature = new Temperature
            {
                TemperatureAmount = temperatureCount,
            };

            _dbContext.Wheathers.Add(new Weather
            {
                City = city,
                Date = date,
                Temperature = temperature,
                Wind = wind
            });
        }

        _logger.LogInformation($"Generating the weather is finished.");

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error during adding the weather to DB. {ex.Message}");
        }
    }

    /// <summary>
    /// Генерирует случайные дату и время
    /// </summary>
    /// <returns></returns>
    DateTime RandomDate()
    {
        Random gen = new Random();

        var startDate = new DateOnly(
            _mokWeatherParams.LowestDate.Year, 
            _mokWeatherParams.LowestDate.Month, 
            _mokWeatherParams.LowestDate.Day);

        var time = new TimeOnly(
            _mokWeatherParams.Time.Hour,
            _mokWeatherParams.Time.Minute,
            _mokWeatherParams.Time.Second);

        DateTime start = new DateTime(startDate, time);
        int range = (DateTime.Today - start).Days;
        return start.AddDays(gen.Next(range));
    }
}
