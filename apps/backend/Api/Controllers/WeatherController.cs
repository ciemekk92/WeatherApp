using MediatR;
using Microsoft.AspNetCore.Mvc;
using Weather.Application.Features.Queries.GetCurrentWeather;

namespace WeatherApp.Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherController(ISender dispatcher) : ControllerBase
{
  [HttpGet("{city}")]
  public async Task<IActionResult> GetCurrentWeather(string city)
    => Ok(await dispatcher.Send(new GetCurrentWeatherQuery(city)));
}

