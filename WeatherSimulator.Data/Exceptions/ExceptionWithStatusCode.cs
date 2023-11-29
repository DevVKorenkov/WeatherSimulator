namespace WeatherSimulator.Data.Exceptions;

/// <summary>
/// Исключение содержащее статус код
/// </summary>
public class ExceptionWithStatusCode : Exception
{
    public ExceptionWithStatusCode(string message, Exception inner) : base(message, inner)
    {
    }

    public ExceptionWithStatusCode(string message) : base(message)
    {
    }

    public ExceptionWithStatusCode()
    {
    }

    public virtual int StatusCode { get; set; }
}
