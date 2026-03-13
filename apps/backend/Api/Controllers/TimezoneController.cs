using MediatR;
using Microsoft.AspNetCore.Mvc;
using Timezone.Application.Dtos;
using Timezone.Application.Features.Queries.GetTimezone;

namespace WeatherApp.Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimezoneController(ISender dispatcher) : ControllerBase
{
  [HttpGet("{city}")]
  [ProducesResponseType<TimezoneDto>(StatusCodes.Status200OK)]
  [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
  [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
  [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> GetTimezone(string city)
    => Ok(await dispatcher.Send(new GetTimezoneQuery(city)));
}

