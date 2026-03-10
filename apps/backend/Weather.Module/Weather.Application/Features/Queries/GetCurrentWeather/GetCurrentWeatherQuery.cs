using MediatR;
using Weather.Application.Dtos;

namespace Weather.Application.Features.Queries.GetCurrentWeather;

public sealed record GetCurrentWeatherQuery(string City)
  : IRequest<CurrentWeatherDto>;
