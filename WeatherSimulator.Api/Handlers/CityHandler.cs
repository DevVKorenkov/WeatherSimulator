using System.Net;
using WeatherSimulator.Core.Services;
using WeatherSimulator.Models.Entities;
using WeatherSimulator.Models.Models;
using WheatherSimulator.Models.DTOs;

namespace WeatherSimulator.Api.Handlers;

/// <summary>
/// Обработчик запроса связанного с городами
/// </summary>
public class CityHandler
{
    /// <summary>
    /// Добавляет новый город в БД (Возможно добавить только через swagger)
    /// </summary>
    /// <param name="cityService"></param>
    /// <param name="cityName"></param>
    /// <returns></returns>
    public async Task<RequestResult> AddCity(
        ICityService cityService, 
        string cityName)
    {
        var result = new RequestResult();

        try
        {
            await cityService.AddCityAsync(new City
            {
                Name = cityName,
            });

            result.HttpStatusCode = HttpStatusCode.OK;
            result.Message = $"{cityName} City is Successfully added";
        }
        catch (Exception ex)
        {
            result.HttpStatusCode = HttpStatusCode.BadRequest;
            result.Message = ex.Message;
        }

        return result;
    }

    /// <summary>
    /// Возвращает все города из БД
    /// </summary>
    /// <param name="cityService"></param>
    /// <returns></returns>
    public async Task<IEnumerable<CityDTO>> GetCities(ICityService cityService) => await cityService.GetCities();
    
}
