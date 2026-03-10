using Common.Infrastructure.Services.Interfaces;
using MediatR;
using Astronomy.Application.Dtos;

namespace Astronomy.Application.Features.Queries.GetAstronomy;

internal sealed class GetAstronomyQueryHandler(IWeatherApiClient weatherApiClient)
  : IRequestHandler<GetAstronomyQuery, AstronomyDto>
{
  public async Task<AstronomyDto> Handle(GetAstronomyQuery request, CancellationToken cancellationToken)
  {
    var apiResponse = await weatherApiClient.GetAstronomyAsync(request.City);

    return AstronomyDto.MapFrom(apiResponse);
  }
}
