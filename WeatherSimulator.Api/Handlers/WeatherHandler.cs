using AutoMapper;
using Microsoft.Extensions.Options;
using System.Net;
using WeatherSimulator.Core.Services;
using WeatherSimulator.Models.DTOs;
using WeatherSimulator.Models.Models;

namespace WeatherSimulator.Api.Handlers;

/// <summary>
/// Обработчик запросов связанных с погодой
/// </summary>
public class WeatherHandler
{
    private const string NOT_FOUND_MESSAGE = "Information Was not Found";
    private const string ABORTED_MESSAGE = "Request Was Aborted";

    /// <summary>
    /// Возвращает последнюю сгенерированную погоду для города по ID города.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="settings"></param>
    /// <param name="weatherService"></param>
    /// <param name="mapper"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public static async Task<WeatherRequestResult> GetWeatherHandler(
        HttpContext context,
        IOptions<Settings> settings,
        IWeatherService weatherService,
        IMapper mapper,
        int id,
        CancellationToken cancellationToken)
    {
        // Иммитация долгой обработки запроса.
        await Task.Delay(settings.Value.ServerResponseDelayMSec, context.RequestAborted);

        var result = new WeatherRequestResult();

        if (context.RequestAborted.IsCancellationRequested)
        {
            result.HttpStatusCode = HttpStatusCode.OK;
            result.Message = ABORTED_MESSAGE;
        }

        var weather = await weatherService.GetWeatherByCityIdAsync(id, cancellationToken);

        if (weather is null)
        {
            result.HttpStatusCode = HttpStatusCode.NotFound;
            result.Message = NOT_FOUND_MESSAGE;
        }
        else
        {
            result.HttpStatusCode = HttpStatusCode.OK;
            result.Message = $"Weather in {weather.City.Name} on {weather.Date.ToShortDateString()}";
            result.WeatherDTO = mapper.Map<WeatherDTO>(weather);
        }

        return result;
    }

    /// <summary>
    /// Возвращает историю погоды для города по ID города
    /// </summary>
    /// <param name="context"></param>
    /// <param name="weatherService"></param>
    /// <param name="mapper"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public static async Task<WeatherHistoryRequestResult> GetWeatherHistoryHandler(
        HttpContext context,
        IOptions<Settings> settings,
        IWeatherService weatherService, 
        IMapper mapper,
        int id, 
        CancellationToken cancellationToken)
    {
        await Task.Delay(settings.Value.ServerResponseDelayMSec, context.RequestAborted);

        var result = new WeatherHistoryRequestResult();

        if (context.RequestAborted.IsCancellationRequested)
        {
            result.HttpStatusCode = HttpStatusCode.OK;
            result.Message = ABORTED_MESSAGE;
        }

        var weathers = await weatherService.GetWeatherHistoryByCityIdAsync(id, cancellationToken);

        if (weathers is null || !weathers.Any())
        {
            result.HttpStatusCode = HttpStatusCode.NotFound;
            result.Message = NOT_FOUND_MESSAGE;
        }
        else
        {
            result.HttpStatusCode = HttpStatusCode.OK;
            result.Message = $"Weather in {weathers.FirstOrDefault().City.Name}";
            result.WeatherHistory 
                = mapper.Map<ICollection<WeatherDTO>>(weathers)
                .OrderBy(x => x.Date).ToList();
        }

        return result;
    }
}
