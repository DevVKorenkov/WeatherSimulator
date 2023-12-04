using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using WeatherSimulator.Models.DTOs;

namespace WeatherSimulator.Logic.Middleware;

public class ExceptionHandlingMiddleware(
    RequestDelegate requestDelegate,
    ILogger<ExceptionHandlingMiddleware> logger)
{
    private readonly RequestDelegate _next = requestDelegate;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(OperationCanceledException ex)
        {
            await HandleExceptionAsync(
                context,
                ex.Message,
                HttpStatusCode.BadRequest,
                "Opearation was canceled");
        }
        catch(DbUpdateConcurrencyException ex)
        {
            await HandleExceptionAsync(
                context,
                ex.Message,
                HttpStatusCode.InternalServerError,
                "Database update concurrency error");
        }
        catch(DbUpdateException ex)
        {
            await HandleExceptionAsync(
                context,
                ex.Message,
                HttpStatusCode.InternalServerError,
                "Database update error");
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(
                context, 
                ex.Message, 
                HttpStatusCode.BadRequest, 
                "Something is wrong");
        }
    }

    private async Task HandleExceptionAsync(
        HttpContext context,
        string exMessage,
        HttpStatusCode httpStatusCode,
        string message)
    {
        _logger.LogError(exMessage);

        var response = context.Response;

        response.ContentType = "application/json";
        response.StatusCode = (int)httpStatusCode;

        var errorDto = new ErrorDTO {
            Message = message, 
            HttpStatusCode = (int)httpStatusCode 
        };

        var result = JsonSerializer.Serialize(errorDto);

        await response.WriteAsync(result);
    }
}
