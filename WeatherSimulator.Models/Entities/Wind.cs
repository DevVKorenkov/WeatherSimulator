using WeatherSimulator.Models.Models;

namespace WeatherSimulator.Models.Entities;

public class Wind
{
    public int Id { get; set; }
    public int Power { get; set; }
    public string WindDirection { get; set; } = null!;
}
