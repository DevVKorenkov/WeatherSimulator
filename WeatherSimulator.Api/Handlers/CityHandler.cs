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
    public static async Task<RequestResult> AddCity(
        ICityService cityService, 
        string cityName,
        CancellationToken cancellationToken)
    {
        // TODO: replace all ThrowIfCancellationRequested() if it is needed
        cancellationToken.ThrowIfCancellationRequested();

        var result = new RequestResult();

        //TODO: remove try catch from here
        try
        {
            await cityService.AddCityAsync(new City
            {
                Name = cityName,
            },
            cancellationToken);

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
    public static async Task<IEnumerable<CityDTO>> GetCities(
        ICityService cityService, 
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await cityService.GetCitiesAsync(cancellationToken);
    }
}
