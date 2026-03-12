using Common.Infrastructure.Models.WeatherApi;
using FluentAssertions;
using Weather.Application.Dtos;

namespace Weather.Test.Unit.Dtos;

public class CurrentWeatherDtoTests
{
  [Fact]
  public void ShouldMapLocationAndCurrentFromResponse()
  {
    // Given
    var response = new CurrentWeatherResponse
    {
      Location = new LocationInfo
      {
        Name = "London",
        Region = "City of London",
        Country = "United Kingdom",
        Lat = 51.52,
        Lon = -0.11,
        TzId = "Europe/London",
        LocaltimeEpoch = 1700000000,
        Localtime = "2024-01-15 12:00"
      },
      Current = new CurrentWeather
      {
        LastUpdatedEpoch = 1700000000,
        LastUpdated = "2024-01-15 12:00",
        TempC = 10.5,
        TempF = 50.9,
        IsDay = 1,
        Condition = new WeatherCondition { Text = "Partly cloudy", Icon = "//cdn/icon.png", Code = 1003 },
        WindMph = 5.0,
        WindKph = 8.0,
        WindDegree = 180,
        WindDir = "S",
        PressureMb = 1013.0,
        PressureIn = 29.91,
        PrecipMm = 0.0,
        PrecipIn = 0.0,
        Humidity = 75,
        Cloud = 50,
        FeelslikeC = 8.0,
        FeelslikeF = 46.4,
        WindchillC = 7.5,
        WindchillF = 45.5,
        HeatindexC = 10.5,
        HeatindexF = 50.9,
        DewpointC = 6.0,
        DewpointF = 42.8,
        VisKm = 10.0,
        VisMiles = 6.0,
        Uv = 3.0,
        GustMph = 8.0,
        GustKph = 12.9
      }
    };

    // When
    var result = CurrentWeatherDto.MapFrom(response);

    // Then
    result.Location.Should().NotBeNull();
    result.Location!.Name.Should().Be("London");
    result.Current.Should().NotBeNull();
    result.Current!.TempC.Should().Be(10.5);
    result.Current.Humidity.Should().Be(75);
  }
}

public class WeatherDataDtoTests
{
  [Fact]
  public void ShouldMapIsDayTrueWhenSourceIsDayIsOne()
  {
    // Given
    var source = new CurrentWeather { IsDay = 1 };

    // When
    var result = WeatherDataDto.MapFrom(source);

    // Then
    result!.IsDay.Should().BeTrue();
  }

  [Fact]
  public void ShouldMapIsDayFalseWhenSourceIsDayIsZero()
  {
    // Given
    var source = new CurrentWeather { IsDay = 0 };

    // When
    var result = WeatherDataDto.MapFrom(source);

    // Then
    result!.IsDay.Should().BeFalse();
  }

  [Fact]
  public void ShouldReturnNullWhenSourceIsNull()
  {
    // When
    var result = WeatherDataDto.MapFrom(null);

    // Then
    result.Should().BeNull();
  }

  [Fact]
  public void ShouldMapAllWeatherFieldsCorrectly()
  {
    // Given
    var source = new CurrentWeather
    {
      LastUpdatedEpoch = 1700000000,
      LastUpdated = "2024-01-15 12:00",
      TempC = 22.5,
      TempF = 72.5,
      IsDay = 1,
      Condition = new WeatherCondition { Text = "Sunny", Icon = "//cdn/sunny.png", Code = 1000 },
      WindMph = 10.0,
      WindKph = 16.1,
      WindDegree = 270,
      WindDir = "W",
      PressureMb = 1015.0,
      PressureIn = 29.97,
      PrecipMm = 0.5,
      PrecipIn = 0.02,
      Humidity = 60,
      Cloud = 25,
      FeelslikeC = 21.0,
      FeelslikeF = 69.8,
      WindchillC = 20.0,
      WindchillF = 68.0,
      HeatindexC = 23.0,
      HeatindexF = 73.4,
      DewpointC = 14.0,
      DewpointF = 57.2,
      VisKm = 15.0,
      VisMiles = 9.0,
      Uv = 5.0,
      GustMph = 15.0,
      GustKph = 24.1
    };

    // When
    var result = WeatherDataDto.MapFrom(source);

    // Then
    result.Should().NotBeNull();
    result.LastUpdatedEpoch.Should().Be(1700000000);
    result.LastUpdated.Should().Be("2024-01-15 12:00");
    result.TempC.Should().Be(22.5);
    result.TempF.Should().Be(72.5);
    result.IsDay.Should().BeTrue();
    result.Condition.Should().NotBeNull();
    result.WindMph.Should().Be(10.0);
    result.WindKph.Should().Be(16.1);
    result.WindDegree.Should().Be(270);
    result.WindDir.Should().Be("W");
    result.PressureMb.Should().Be(1015.0);
    result.PressureIn.Should().Be(29.97);
    result.PrecipMm.Should().Be(0.5);
    result.PrecipIn.Should().Be(0.02);
    result.Humidity.Should().Be(60);
    result.Cloud.Should().Be(25);
    result.FeelslikeC.Should().Be(21.0);
    result.FeelslikeF.Should().Be(69.8);
    result.WindchillC.Should().Be(20.0);
    result.WindchillF.Should().Be(68.0);
    result.HeatindexC.Should().Be(23.0);
    result.HeatindexF.Should().Be(73.4);
    result.DewpointC.Should().Be(14.0);
    result.DewpointF.Should().Be(57.2);
    result.VisKm.Should().Be(15.0);
    result.VisMiles.Should().Be(9.0);
    result.Uv.Should().Be(5.0);
    result.GustMph.Should().Be(15.0);
    result.GustKph.Should().Be(24.1);
  }
}

public class ConditionDtoTests
{
  [Fact]
  public void ShouldMapAllFieldsCorrectly()
  {
    // Given
    var source = new WeatherCondition
    {
      Text = "Partly cloudy",
      Icon = "//cdn/weather/64x64/day/116.png",
      Code = 1003
    };

    // When
    var result = ConditionDto.MapFrom(source);

    // Then
    result.Should().NotBeNull();
    result.Text.Should().Be("Partly cloudy");
    result.Icon.Should().Be("//cdn/weather/64x64/day/116.png");
    result.Code.Should().Be(1003);
  }

  [Fact]
  public void ShouldReturnNullWhenSourceIsNull()
  {
    // When
    var result = ConditionDto.MapFrom(null);

    // Then
    result.Should().BeNull();
  }
}

