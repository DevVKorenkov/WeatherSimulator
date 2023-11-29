using WheatherSimulator.Models.DTOs;

namespace WeatherSimulator.Models.DTOs;

public class CitiesDTO
{
    public ICollection<CityDTO> Cities { get; set; } = null!;
}
