using MediatR;
using Astronomy.Application.Dtos;

namespace Astronomy.Application.Features.Queries.GetAstronomy;

public sealed record GetAstronomyQuery(string City)
  : IRequest<AstronomyDto>;

