using WeatherSimulator.Models.DTOs;

namespace WeatherSimulator.Models.Models;

/// <summary>
/// Результат обработки запроса получения последней сгенерированной погоды.
/// </summary>
public class WeatherRequestResult : RequestResult
{
    public WeatherDTO WeatherDTO { get; set; } = null!;
}
