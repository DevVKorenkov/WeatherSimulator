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
    public LowestDate LowestDate { get; set; } = null!;
    public Time Time { get; set; } = null!;
}
