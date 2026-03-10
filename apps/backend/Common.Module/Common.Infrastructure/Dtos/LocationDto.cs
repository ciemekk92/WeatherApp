using Common.Infrastructure.Models.WeatherApi;

namespace Common.Infrastructure.Dtos;

public record LocationDto
{
  public string? Name { get; init; }
  public string? Region { get; init; }
  public string? Country { get; init; }
  public double Lat { get; init; }
  public double Lon { get; init; }
  public string? TzId { get; init; }
  public long LocaltimeEpoch { get; init; }
  public string? Localtime { get; init; }

  public static LocationDto? MapFrom(LocationInfo? source)
  {
    if (source == null) return null;

    return new LocationDto
    {
      Name = source.Name,
      Region = source.Region,
      Country = source.Country,
      Lat = source.Lat,
      Lon = source.Lon,
      TzId = source.TzId,
      LocaltimeEpoch = source.LocaltimeEpoch,
      Localtime = source.Localtime
    };
  }
}
