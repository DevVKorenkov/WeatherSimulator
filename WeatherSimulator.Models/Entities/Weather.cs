namespace WeatherSimulator.Models.Entities;

public class Weather
{
    public int Id { get; set; }
    public City City { get; set; } = null!;
    public DateTime Date { get; set; }
    public Temperature Temperature { get; set; } = null!;
    public Wind Wind { get; set; } = null!;
}
