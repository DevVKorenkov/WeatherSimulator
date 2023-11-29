using Microsoft.AspNetCore.Http;

namespace WeatherSimulator.Data.Exceptions;

/// <summary>
/// Исключение содержащее статус код "контент не найден"
/// </summary>
public class NoContentException : ExceptionWithStatusCode
{
    public override int StatusCode { get; set; } = StatusCodes.Status204NoContent;
}
