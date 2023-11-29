using WeatherSimulator.Models.Entities;
using WheatherSimulator.Models.DTOs;

namespace WeatherSimulator.Data.Services.Abstractions;

/// <summary>
/// Осуществляет обработку городов
/// </summary>
public interface ICityService
{
    /// <summary>
    /// Добавляет новый город в БД
    /// </summary>
    /// <param name="city"></param>
    /// <returns></returns>
    Task AddCityAsync(City city);
    /// <summary>
    /// Получает все города из БД
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<CityDTO>> GetCities();
}
