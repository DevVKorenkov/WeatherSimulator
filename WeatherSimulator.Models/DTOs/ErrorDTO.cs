namespace WeatherSimulator.Models.DTOs;

public class ErrorDTO
{
    public required string Message { get; set; }
    public int HttpStatusCode { get; set; }
}
