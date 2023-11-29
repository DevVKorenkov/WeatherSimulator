namespace WeatherSimulator.Models.Models;

/// <summary>
/// Настройки.
/// В данном случае содержат настройки интервала генерации погоды и время иммитации долгой обработки запроса.
/// </summary>
public class Settings
{
    public int CreationDelaySec { get; set; }
    public int ServerResponseDelayMSec { get; set; }
}
