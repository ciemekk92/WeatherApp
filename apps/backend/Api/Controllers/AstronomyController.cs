using MediatR;
using Microsoft.AspNetCore.Mvc;
using Astronomy.Application.Features.Queries.GetAstronomy;

namespace WeatherApp.Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AstronomyController(ISender dispatcher) : ControllerBase
{
  [HttpGet("{city}")]
  public async Task<IActionResult> GetAstronomy(string city)
    => Ok(await dispatcher.Send(new GetAstronomyQuery(city)));
}
