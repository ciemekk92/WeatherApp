using System.Text.Json.Serialization;

namespace Common.Infrastructure.Models.WeatherApi;

public class TimezoneResponse
{
  [JsonPropertyName("location")]
  public LocationInfo? Location { get; set; }
}

public class LocationInfo
{
  [JsonPropertyName("name")]
  public string? Name { get; set; }

  [JsonPropertyName("region")]
  public string? Region { get; set; }

  [JsonPropertyName("country")]
  public string? Country { get; set; }

  [JsonPropertyName("lat")]
  public double Lat { get; set; }

  [JsonPropertyName("lon")]
  public double Lon { get; set; }

  [JsonPropertyName("tz_id")]
  public string? TzId { get; set; }

  [JsonPropertyName("localtime_epoch")]
  public long LocaltimeEpoch { get; set; }

  [JsonPropertyName("localtime")]
  public string? Localtime { get; set; }
}

