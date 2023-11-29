using Microsoft.EntityFrameworkCore;
using WeatherSimulator.Models.Entities;

namespace WeatherSimulator.Data.Context;

/// <summary>
/// Контекст базы данных погоды
/// </summary>
public class WeatherDataContext : DbContext
{
    public DbSet<City> Cities { get; set; }
    public DbSet<Weather> Wheathers { get; set; }

    public WeatherDataContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }
}
