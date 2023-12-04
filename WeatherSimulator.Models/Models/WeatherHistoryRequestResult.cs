using WeatherSimulator.Models.DTOs;

namespace WeatherSimulator.Models.Models;

/// <summary>
/// Результаты обработки запроса на получение истории погоды
/// </summary>
public class WeatherHistoryRequestResult : RequestResult
{
    public ICollection<WeatherDTO> WeatherHistory { get; set; } = new HashSet<WeatherDTO>();
}
