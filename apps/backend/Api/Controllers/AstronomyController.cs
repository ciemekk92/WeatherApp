using MediatR;
using Microsoft.AspNetCore.Mvc;
using Astronomy.Application.Dtos;
using Astronomy.Application.Features.Queries.GetAstronomy;

namespace WeatherApp.Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AstronomyController(ISender dispatcher) : ControllerBase
{
  [HttpGet("{city}")]
  [ProducesResponseType<AstronomyDto>(StatusCodes.Status200OK)]
  [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
  [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
  [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> GetAstronomy(string city)
    => Ok(await dispatcher.Send(new GetAstronomyQuery(city)));
}
