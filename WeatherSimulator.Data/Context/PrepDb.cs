using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WeatherSimulator.Core.Services;
using WeatherSimulator.Models.Entities;

namespace WeatherSimulator.Data.Context;

/// <summary>
/// Добавляет города в InMemory базу данных при запуске приложения
/// </summary>
public static class PrepDB
{
    public static void PrepPopulation(IApplicationBuilder applicationBuilder)
    {
        using var services = applicationBuilder.ApplicationServices.CreateScope();
        
            var cities = new List<City>
            {
                new City
                {
                    Id = 1,
                    Name = "Санкт-Петербург",
                },
                new City
                {
                    Id = 2,
                    Name = "Москва",
                },
                new City
                {
                    Id = 3,
                    Name = "Казань",
                }
            };

            SeedData(services.ServiceProvider.GetRequiredService<ICityService>(), cities);
        
    }

    private static async void SeedData(ICityService cityService, IEnumerable<City> cities)
    {
        foreach (var city in cities)
        {
            await cityService.AddCityAsync(city);
        }
    }
}
