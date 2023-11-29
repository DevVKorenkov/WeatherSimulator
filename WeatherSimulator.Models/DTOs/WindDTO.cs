using WeatherSimulator.Models.Models;

namespace WeatherSimulator.Models.DTOs;

public class WindDTO
{
    public int Power { get; set; }
    public string WindDirection { get; set; } = null!;
}