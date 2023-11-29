using Microsoft.EntityFrameworkCore;
using WeatherSimulator.Api;
using WeatherSimulator.Api.Handlers;
using WeatherSimulator.Api.Middleware;
using WeatherSimulator.Data.Context;
using WeatherSimulator.Data.Services;
using WeatherSimulator.Data.Services.Abstractions;
using WeatherSimulator.Models.Configurations;
using WeatherSimulator.Models.Models;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

services.AddLogging(builder => builder.AddSeq(config.GetSection("Seq")));

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddCors();

services.AddDbContext<WeatherDataContext>(opt 
    => opt.UseInMemoryDatabase(config.GetConnectionString("InMemory")));

services.Configure<Settings>(builder.Configuration.GetSection(nameof(Settings)));
services.Configure<MokWeatherParams>(builder.Configuration.GetSection(nameof(MokWeatherParams)));

services.AddHostedService<WeatherCreatorWorker>();

services.AddScoped<IWeatherService, WeatherService>();
services.AddScoped<ICityService, CityService>();
services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

PrepDB.PrepPopulation(app);

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

app.UseMiddleware<RequestLoggingMiddleware>();

app.MapGet("/GetByCityId/{id}",new WeatherHandler().GetWeatherHandler);
app.MapGet("/GetWeatherHistoryByCityId/{id}", new WeatherHandler().GetWeatherHistoryHandler);
app.MapPost("/AddCity/{cityName}", new CityHandler().AddCity);
app.MapGet("/GetCities", new CityHandler().GetCities);

app.Run();
