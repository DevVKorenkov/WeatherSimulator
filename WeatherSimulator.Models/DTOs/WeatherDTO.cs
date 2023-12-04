using WeatherSimulator.Models.Entities;

namespace WeatherSimulator.Models.DTOs;

public class WeatherDTO
{
    public required City City { get; set; }
    public DateTime Date { get; set; }
    public required TemperatureDTO Temperature { get; set; }
    public required WindDTO Wind { get; set; }

    public bool Equals(object? obj) => 
               obj is not null &&
               obj is WeatherDTO dTO &&
               EqualityComparer<City>.Default.Equals(City, dTO.City) &&
               Date == dTO.Date &&
               EqualityComparer<TemperatureDTO>.Default.Equals(Temperature, dTO.Temperature) &&
               EqualityComparer<WindDTO>.Default.Equals(Wind, dTO.Wind) &&
               obj.GetHashCode() == GetHashCode();

    public override int GetHashCode() => 
        Date.GetHashCode() * Date.GetHashCode();
}