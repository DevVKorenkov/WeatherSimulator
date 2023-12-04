namespace WeatherSimulator.Models.Models;

/// <summary>
/// Параметры для случайной ненерации погоды
/// </summary>
public class MokWeatherParams
{
    public int LowestTemperature { get; set; }
    public int HighestTemperature { get; set; }
    public int MiddleTemperature {get; set; }
    public int HighestWindPower {get; set; }
    public required LowestDate LowestDate { get; set; }
    public required Time Time { get; set; }
}
