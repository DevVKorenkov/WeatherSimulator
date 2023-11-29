using WeatherSimulator.Models.Entities;

namespace WeatherSimulator.Models.DTOs;

public class WeatherDTO
{
    public City City { get; set; } = null!;
    public DateTime Date { get; set; }
    public TemperatureDTO Temperature { get; set; } = null!;
    public WindDTO Wind { get; set; } = null!;
}