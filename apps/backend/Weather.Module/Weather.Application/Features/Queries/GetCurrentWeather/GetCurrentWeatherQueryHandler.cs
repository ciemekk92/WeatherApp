using Common.Infrastructure.Services.Interfaces;
using MediatR;
using Weather.Application.Dtos;

namespace Weather.Application.Features.Queries.GetCurrentWeather;

internal sealed class GetCurrentWeatherQueryHandler(IWeatherApiClient weatherApiClient)
  : IRequestHandler<GetCurrentWeatherQuery, CurrentWeatherDto>
{
  public async Task<CurrentWeatherDto> Handle(GetCurrentWeatherQuery request, CancellationToken cancellationToken)
  {
    var apiResponse = await weatherApiClient.GetCurrentWeatherAsync(request.City);

    return CurrentWeatherDto.MapFrom(apiResponse);
  }
}
