using System.Net;

namespace WeatherSimulator.Models.Models;
// TODO: remove this class

/// <summary>
/// Результат обработки запросов.
/// Мне просто нравится делать свою обертку для результатов.
/// Далее видно, что этот класс является родительским для подобный ему классов. 
/// Можно было использовать паттерн Factory или Factory Method, но в данном случае я решил, что это было бы ненужным усложнением.
/// </summary>
public class RequestResult
{
    public HttpStatusCode HttpStatusCode { get; set; }
    public string? Message { get; set; }
}
