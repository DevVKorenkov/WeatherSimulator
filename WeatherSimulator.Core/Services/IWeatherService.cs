using WeatherSimulator.Models.Entities;

namespace WeatherSimulator.Core.Services;

public interface IWeatherService
{
    /// <summary>
    /// Генерирует погоду случайным образом для каждого города в БД.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    Task RandomGenerateAsync();
    /// <summary>
    /// Получает последнюю сгенерированную погоду по ID города. Возвращает null если город не найден. 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Weather> GetWeatherByCityIdAsync(int id);
    /// <summary>
    /// Получает историю погоды сгенерированную для города по ID города. Возвращает null если город не найден.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<IEnumerable<Weather>> GetWeatherHistoryByCityIdAsync(int id);
}
