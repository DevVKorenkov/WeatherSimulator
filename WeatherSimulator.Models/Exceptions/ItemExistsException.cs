namespace WeatherSimulator.Models.Exceptions;

public class ItemExistsException : Exception
{
    public ItemExistsException()
    {
        
    }

    public ItemExistsException(string message) : base(message)
    {
        
    }

    public ItemExistsException(string message, Exception? innerException) : base(message, innerException)
    {

    }
}
