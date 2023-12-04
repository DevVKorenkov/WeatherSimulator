namespace WeatherSimulator.Models.DTOs;

public class WindDTO
{
    public int Power { get; set; }
    public string WindDirection { get; set; } = null!;

    public override bool Equals(object? obj) =>
        obj is not null &&
        obj is WindDTO dTO &&
        Power == dTO.Power;

    public override int GetHashCode() => 
        (Power * Power).GetHashCode();
}