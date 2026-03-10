using Common.Infrastructure.Services.Interfaces;
using MediatR;
using Timezone.Application.Dtos;

namespace Timezone.Application.Features.Queries.GetTimezone;

internal sealed class GetTimezoneQueryHandler(IWeatherApiClient weatherApiClient)
  : IRequestHandler<GetTimezoneQuery, TimezoneDto>
{
  public async Task<TimezoneDto> Handle(GetTimezoneQuery request, CancellationToken cancellationToken)
  {
    var apiResponse = await weatherApiClient.GetTimezoneAsync(request.City);

    return TimezoneDto.MapFrom(apiResponse);
  }
}
