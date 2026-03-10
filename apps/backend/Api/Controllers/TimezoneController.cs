using MediatR;
using Microsoft.AspNetCore.Mvc;
using Timezone.Application.Features.Queries.GetTimezone;

namespace WeatherApp.Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimezoneController(ISender dispatcher) : ControllerBase
{
  /// <summary>
  /// Gets timezone information for a city
  /// </summary>
  /// <param name="city">City name (e.g., London, New York)</param>
  [HttpGet("{city}")]
  public async Task<IActionResult> GetTimezone(string city)
  {
    try
    {
      var query = new GetTimezoneQuery(city);
      var result = await dispatcher.Send(query);
      return Ok(result);
    }
    catch (Exception ex)
    {
      return BadRequest(new { error = ex.Message });
    }
  }
}

