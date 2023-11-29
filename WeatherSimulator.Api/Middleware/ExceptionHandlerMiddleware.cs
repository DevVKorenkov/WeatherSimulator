using System.Text;
using WeatherSimulator.Data.Exceptions;

namespace WeatherSimulator.Api.Middleware;

/// <summary>
/// Компонент записывает статус коды в ответ если возникают ошибки.
/// </summary>
/// <param name="next"></param>
/// <param name="hostEnvironment"></param>
public class ExceptionHandlerMiddleware(RequestDelegate next, IWebHostEnvironment hostEnvironment)
{
    private readonly RequestDelegate _next = next;
    private readonly IWebHostEnvironment _hostEnvironment = hostEnvironment;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (NoContentException e)
        {
            context.Response.StatusCode = e.StatusCode;
        }
        catch (ExceptionWithStatusCode e)
        {
            context.Response.StatusCode = e.StatusCode;
            context.Response.ContentType = "Content-Type: text/plain; charset=UTF-8";
            await context.Response.WriteAsync(e.Message, Encoding.UTF8);
        }
        catch (Exception e)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            if (_hostEnvironment.IsDevelopment())
            {
                context.Response.ContentType = "Content-Type: text/plain; charset=UTF-8";
                await context.Response.WriteAsync(e.ToString(), Encoding.UTF8);
            }
        }
    }
}
