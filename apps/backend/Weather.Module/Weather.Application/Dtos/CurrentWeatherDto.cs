using Common.Infrastructure.Dtos;
using Common.Infrastructure.Models.WeatherApi;

namespace Weather.Application.Dtos;

public record CurrentWeatherDto
{
  public LocationDto? Location { get; init; }
  public WeatherDataDto? Current { get; init; }

  public static CurrentWeatherDto MapFrom(CurrentWeatherResponse source)
  {
    return new CurrentWeatherDto
    {
      Location = LocationDto.MapFrom(source.Location),
      Current = WeatherDataDto.MapFrom(source.Current)
    };
  }
}

public record WeatherDataDto
{
  public long LastUpdatedEpoch { get; init; }
  public string? LastUpdated { get; init; }
  public double TempC { get; init; }
  public double TempF { get; init; }
  public bool IsDay { get; init; }
  public ConditionDto? Condition { get; init; }
  public double WindMph { get; init; }
  public double WindKph { get; init; }
  public int WindDegree { get; init; }
  public string? WindDir { get; init; }
  public double PressureMb { get; init; }
  public double PressureIn { get; init; }
  public double PrecipMm { get; init; }
  public double PrecipIn { get; init; }
  public int Humidity { get; init; }
  public int Cloud { get; init; }
  public double FeelslikeC { get; init; }
  public double FeelslikeF { get; init; }
  public double WindchillC { get; init; }
  public double WindchillF { get; init; }
  public double HeatindexC { get; init; }
  public double HeatindexF { get; init; }
  public double DewpointC { get; init; }
  public double DewpointF { get; init; }
  public double VisKm { get; init; }
  public double VisMiles { get; init; }
  public double Uv { get; init; }
  public double GustMph { get; init; }
  public double GustKph { get; init; }

  public static WeatherDataDto? MapFrom(CurrentWeather? source)
  {
    if (source == null) return null;

    return new WeatherDataDto
    {
      LastUpdatedEpoch = source.LastUpdatedEpoch,
      LastUpdated = source.LastUpdated,
      TempC = source.TempC,
      TempF = source.TempF,
      IsDay = source.IsDay == 1,
      Condition = ConditionDto.MapFrom(source.Condition),
      WindMph = source.WindMph,
      WindKph = source.WindKph,
      WindDegree = source.WindDegree,
      WindDir = source.WindDir,
      PressureMb = source.PressureMb,
      PressureIn = source.PressureIn,
      PrecipMm = source.PrecipMm,
      PrecipIn = source.PrecipIn,
      Humidity = source.Humidity,
      Cloud = source.Cloud,
      FeelslikeC = source.FeelslikeC,
      FeelslikeF = source.FeelslikeF,
      WindchillC = source.WindchillC,
      WindchillF = source.WindchillF,
      HeatindexC = source.HeatindexC,
      HeatindexF = source.HeatindexF,
      DewpointC = source.DewpointC,
      DewpointF = source.DewpointF,
      VisKm = source.VisKm,
      VisMiles = source.VisMiles,
      Uv = source.Uv,
      GustMph = source.GustMph,
      GustKph = source.GustKph
    };
  }
}

public record ConditionDto
{
  public string? Text { get; init; }
  public string? Icon { get; init; }
  public int Code { get; init; }

  public static ConditionDto? MapFrom(WeatherCondition? source)
  {
    if (source == null) return null;

    return new ConditionDto
    {
      Text = source.Text,
      Icon = source.Icon,
      Code = source.Code
    };
  }
}
