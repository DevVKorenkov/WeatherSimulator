
namespace WeatherSimulator.Models.DTOs;

public class TemperatureDTO
{
    public int Temperature { get; set; }

    public override bool Equals(object? obj) =>
        obj is not null &&
        obj is TemperatureDTO dTO &&
        Temperature == dTO.Temperature &&
        obj.GetHashCode() == GetHashCode();

    public override int GetHashCode() =>  (Temperature * Temperature).GetHashCode();
}