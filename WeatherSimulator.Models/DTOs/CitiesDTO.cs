using WheatherSimulator.Models.DTOs;

namespace WeatherSimulator.Models.DTOs;

public class CitiesDTO
{
    public ICollection<CityDTO> Cities { get; set; } = null!;

    public override bool Equals(object? obj) =>
        obj is not null &&
        obj is CitiesDTO dTO &&
        EqualityComparer<ICollection<CityDTO>>.Default.Equals(Cities, dTO.Cities) &&
        obj.GetHashCode() == GetHashCode();

    public override int GetHashCode() 
        => Cities.GetHashCode() * Cities.GetHashCode();
}
