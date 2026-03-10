using MediatR;
using Microsoft.AspNetCore.Mvc;
using Astronomy.Application.Features.Queries.GetAstronomy;

namespace WeatherApp.Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AstronomyController(ISender dispatcher) : ControllerBase
{
  /// <summary>
  /// Gets astronomy data for a city
  /// </summary>
  /// <param name="city">City name (e.g., London, New York)</param>
  [HttpGet("{city}")]
  public async Task<IActionResult> GetAstronomy(string city)
  {
    try
    {
      var query = new GetAstronomyQuery(city);
      var result = await dispatcher.Send(query);
      return Ok(result);
    }
    catch (Exception ex)
    {
      return BadRequest(new { error = ex.Message });
    }
  }
}

