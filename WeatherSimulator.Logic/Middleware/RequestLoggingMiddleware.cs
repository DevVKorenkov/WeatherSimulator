using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text;

namespace WeatherSimulator.Logic.Middleware;

/// <summary>
/// Логирует все поступающие запросы. Логируются адрес, метод, хедеры и тело запроса.
/// </summary>
/// <param name="next"></param>
/// <param name="logger"></param>
public class RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<RequestLoggingMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        var logStringBuilder = new StringBuilder("The request contains: \n");

        logStringBuilder.AppendLine($"Request path is {context.Request.Path}.");
        logStringBuilder.AppendLine($"Request method is {context.Request.Method}.");

        AddHeadersToLog(context.Request.Headers, logStringBuilder);
        await AddBodyToLog(context.Request, logStringBuilder);

        await Console.Out.WriteLineAsync(logStringBuilder.ToString());

        _logger.LogInformation($"{logStringBuilder}");

        await _next.Invoke(context);
    }

    private void AddHeadersToLog(IHeaderDictionary headers, StringBuilder stringBuilder)
    {
        stringBuilder.AppendLine("Headers:\n");

        foreach (var header in headers)
        {
            stringBuilder.AppendLine($"Header key: {header.Key} - Header value: {header.Value}\n");
        }
    }

    private async Task AddBodyToLog(HttpRequest request, StringBuilder stringBuilder)
    {
        stringBuilder.AppendLine("Body:\n");

        //request.EnableBuffering();

        var bodyStreamReader = new StreamReader(request.Body);
        bodyStreamReader.BaseStream.Seek(0, SeekOrigin.Begin);
        var bodyText = await bodyStreamReader.ReadToEndAsync();

        stringBuilder.AppendLine(bodyText);
    }
}
