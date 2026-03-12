using Common.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WeatherApp.Backend.Api.Middleware;

public sealed class ExceptionHandlingMiddleware(RequestDelegate next)
{
  public async Task InvokeAsync(HttpContext context)
  {
    try
    {
      await next(context);
    }
    catch (InfrastructureBaseException exception)
    {
      var statusCode = GetStatusCode(exception);
      var problemDetails = GetInfrastructureProblemDetails(exception);

      context.Response.StatusCode = statusCode;

      await context.Response.WriteAsJsonAsync(problemDetails);
    }
    catch (ApplicationLogicException exception)
    {
      var statusCode = GetStatusCode(exception);
      var problemDetails = new ProblemDetails
      {
        Status = statusCode,
        Type = nameof(ApplicationLogicException),
        Title = "Application logic error",
        Detail = exception.Message
      };

      context.Response.StatusCode = statusCode;

      await context.Response.WriteAsJsonAsync(problemDetails);
    }
    catch (Exception exception)
    {
      var statusCode = GetStatusCode(exception);
      var problemDetails = new ProblemDetails
      {
        Status = statusCode,
        Type = "InternalServerError",
        Title = "Internal server error",
        Detail = exception.Message
      };

      context.Response.StatusCode = statusCode;

      await context.Response.WriteAsJsonAsync(problemDetails);
    }
  }

  private static ProblemDetails GetInfrastructureProblemDetails(Exception exception)
    => exception switch
    {
      NotFoundException => new ProblemDetails
      {
        Status = StatusCodes.Status404NotFound,
        Type = "NotFound",
        Title = "Not found",
        Detail = exception.Message
      },
      _ => throw exception
    };

  private static int GetStatusCode(Exception exception)
    => exception switch
    {
      ApplicationLogicException => StatusCodes.Status400BadRequest,
      NotFoundException => StatusCodes.Status404NotFound,
      _ => StatusCodes.Status500InternalServerError
    };
}
