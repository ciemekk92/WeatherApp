using MediatR;
using Timezone.Application.Dtos;

namespace Timezone.Application.Features.Queries.GetTimezone;

public sealed record GetTimezoneQuery(string City)
  : IRequest<TimezoneDto>;

