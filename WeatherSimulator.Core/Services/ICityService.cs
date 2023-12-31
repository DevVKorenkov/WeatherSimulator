﻿using WeatherSimulator.Models.Entities;
using WheatherSimulator.Models.DTOs;

namespace WeatherSimulator.Core.Services;

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
    Task AddCityAsync(City city, CancellationToken cancellationToken);
    /// <summary>
    /// Получает все города из БД
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<CityDTO>> GetCitiesAsync(CancellationToken cancellationToken);
}
