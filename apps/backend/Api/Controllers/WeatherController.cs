using MediatR;
using Microsoft.AspNetCore.Mvc;
using Weather.Application.Dtos;
using Weather.Application.Features.Queries.GetCurrentWeather;

namespace WeatherApp.Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherController(ISender dispatcher) : ControllerBase
{
  [HttpGet("{city}")]
  [ProducesResponseType<CurrentWeatherDto>(StatusCodes.Status200OK)]
  [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
  [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
  [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> GetCurrentWeather(string city)
    => Ok(await dispatcher.Send(new GetCurrentWeatherQuery(city)));
}

