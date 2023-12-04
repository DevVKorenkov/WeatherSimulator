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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<City>().HasData(
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
            });
    }
}
