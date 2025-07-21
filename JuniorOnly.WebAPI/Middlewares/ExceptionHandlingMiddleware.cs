using JuniorOnly.Application.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace JuniorOnly.WebAPI.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                httpContext.Response.ContentType = "application/json";
                int statusCode;
                string errorMessage;

                switch (e)
                {
                    case NotFoundException:
                        statusCode = StatusCodes.Status404NotFound;
                        errorMessage = e.Message;
                        break;
                    case ValidationException:
                        statusCode = StatusCodes.Status400BadRequest;
                        errorMessage = e.Message;
                        break;
                    case UnauthorizedAccessException:
                        statusCode = StatusCodes.Status401Unauthorized;
                        errorMessage = e.Message;
                        break;
                    default:
                        statusCode = StatusCodes.Status500InternalServerError;
                        errorMessage = "An unexpected error occurred.";
                        break;
                }

                _logger.LogError(e, "An exception occurred: {Message}", e.Message);

                httpContext.Response.StatusCode = statusCode;
                var response = new { error = errorMessage };
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
