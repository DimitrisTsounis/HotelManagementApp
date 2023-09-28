using System.Net;
using System.Text.Json;

namespace HotelManagement.API.Middlewares;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger logger;

    public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ErrorDetails details = new()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = e.Source,
                Title = "Server error",
                Detail = "An interval server error has occured."
            };

            string json = JsonSerializer.Serialize(details);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }
    }
}

internal class ErrorDetails
{
    public int Status { get; set; }
    public string Type { get; set; }
    public string Title { get; set; }
    public string Detail { get; set; }
}