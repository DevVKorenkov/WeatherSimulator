using Microsoft.EntityFrameworkCore;
using WeatherSimulator.Api;
using WeatherSimulator.Api.Handlers;
using WeatherSimulator.Core.Services;
using WeatherSimulator.Data.Context;
using WeatherSimulator.Logic.Middleware;
using WeatherSimulator.Logic.Services;
using WeatherSimulator.Models.Configurations;
using WeatherSimulator.Models.Models;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

services.AddLogging(builder => builder.AddSeq(config.GetSection("Seq")));

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddCors();

if(builder.Environment.IsDevelopment())
{
    services.AddDbContext<WeatherDataContext>(opt
    => opt.UseInMemoryDatabase(config.GetConnectionString("InMemory")));
}

services.Configure<Settings>(builder.Configuration.GetSection(nameof(Settings)));
services.Configure<MokWeatherParams>(builder.Configuration.GetSection(nameof(MokWeatherParams)));

services.AddHostedService<WeatherCreatorWorker>();

services.AddScoped<IWeatherService, WeatherService>();
services.AddScoped<ICityService, CityService>();
services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});

// TODO: replace all ThrowIfCancellationRequested() if it is needed in handlers or controllers
app.MapGet("/GetByCityId/{id}", WeatherHandler.GetWeatherHandler);
app.MapGet("/GetWeatherHistoryByCityId/{id}", WeatherHandler.GetWeatherHistoryHandler);
app.MapPost("/AddCity/{cityName}", CityHandler.AddCity);
app.MapGet("/GetCities", CityHandler.GetCities);

app.Run();
