﻿using Microsoft.Extensions.Options;
using WeatherSimulator.Core.Services;
using WeatherSimulator.Models.Models;

namespace WeatherSimulator.Api;

/// <summary>
/// Вызывает генерацию погоды каждые 30 секунд.
/// </summary>
/// <param name="scopeFactory"></param>
/// <param name="settings"></param>
/// <param name="logger"></param>
public class WeatherCreatorWorker(
    IServiceScopeFactory scopeFactory, 
    IOptions<Settings> settings,
    ILogger<WeatherCreatorWorker> logger) : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
    private readonly Settings _settings = settings.Value;
    private readonly ILogger<WeatherCreatorWorker> _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken cancellation)
    {
        _logger.LogInformation("Creation worker was started");

        var weatherService = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<IWeatherService>();

        _logger.LogInformation("Creation of test weather data was started");

        while (!cancellation.IsCancellationRequested)
        {
            _logger.LogInformation("Start creation of test weather data");

            await weatherService.RandomGenerateAsync(cancellation);

            _logger.LogInformation("Test weather data was created successfully");

            await Task.Delay(TimeSpan.FromSeconds(_settings.CreationDelaySec), cancellation);
        }

        _logger.LogInformation("Creation worker was stopped");
    }
}
