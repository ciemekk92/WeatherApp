using MediatR;
using Microsoft.AspNetCore.Mvc;
using Weather.Application.Features.Queries.GetCurrentWeather;

namespace WeatherApp.Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherController(ISender dispatcher) : ControllerBase
{
  /// <summary>
  /// Gets current weather for a city
  /// </summary>
  /// <param name="city">City name (e.g., London, New York)</param>
  [HttpGet("{city}")]
  public async Task<IActionResult> GetCurrentWeather(string city)
  {
    try
    {
      var query = new GetCurrentWeatherQuery(city);
      var result = await dispatcher.Send(query);
      return Ok(result);
    }
    catch (Exception ex)
    {
      return BadRequest(new { error = ex.Message });
    }
  }
}

