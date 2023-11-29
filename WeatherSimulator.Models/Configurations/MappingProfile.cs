using AutoMapper;
using WeatherSimulator.Models.DTOs;
using WeatherSimulator.Models.Entities;
using WheatherSimulator.Models.DTOs;

namespace WeatherSimulator.Models.Configurations;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<City, CityDTO>();
        CreateMap<Weather, WeatherDTO>();
        CreateMap<Temperature, TemperatureDTO>()
            .ForMember(x => x.Temperature, opt => opt.MapFrom(t => t.TemperatureAmount));
        CreateMap<Wind, WindDTO>();
    }
}
