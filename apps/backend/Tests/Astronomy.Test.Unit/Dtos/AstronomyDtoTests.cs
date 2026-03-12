using Common.Infrastructure.Models.WeatherApi;
using FluentAssertions;
using Astronomy.Application.Dtos;

namespace Astronomy.Test.Unit.Dtos;

public class AstronomyDtoTests
{
  [Fact]
  public void ShouldMapLocationAndAstronomyFromResponse()
  {
    // Given
    var response = new AstronomyResponse
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
      Astronomy = new AstronomyData
      {
        Astro = new Astro
        {
          Sunrise = "07:45 AM",
          Sunset = "04:15 PM",
          Moonrise = "10:30 AM",
          Moonset = "09:45 PM",
          MoonPhase = "Waxing Crescent",
          MoonIllumination = 15,
          IsMoonUp = 1,
          IsSunUp = 1
        }
      }
    };

    // When
    var result = AstronomyDto.MapFrom(response);

    // Then
    result.Location.Should().NotBeNull();
    result.Location!.Name.Should().Be("London");
    result.Astronomy.Should().NotBeNull();
    result.Astronomy!.Astro.Should().NotBeNull();
    result.Astronomy.Astro!.Sunrise.Should().Be("07:45 AM");
  }

  [Fact]
  public void ShouldMapLocationWithNullAstronomy()
  {
    // Given
    var response = new AstronomyResponse
    {
      Location = new LocationInfo { Name = "London" },
      Astronomy = null
    };

    // When
    var result = AstronomyDto.MapFrom(response);

    // Then
    result.Location.Should().NotBeNull();
    result.Astronomy.Should().BeNull();
  }
}

public class AstronomyDataDtoTests
{
  [Fact]
  public void ShouldReturnNullWhenSourceIsNull()
  {
    // When
    var result = AstronomyDataDto.MapFrom(null);

    // Then
    result.Should().BeNull();
  }

  [Fact]
  public void ShouldMapAstroFromSource()
  {
    // Given
    var source = new AstronomyData
    {
      Astro = new Astro
      {
        Sunrise = "06:00 AM",
        Sunset = "06:00 PM",
        MoonPhase = "Full Moon",
        MoonIllumination = 100,
        IsMoonUp = 1,
        IsSunUp = 0
      }
    };

    // When
    var result = AstronomyDataDto.MapFrom(source);

    // Then
    result.Should().NotBeNull();
    result!.Astro.Should().NotBeNull();
    result.Astro!.MoonPhase.Should().Be("Full Moon");
  }
}

public class AstroDtoTests
{
  [Fact]
  public void ShouldReturnNullWhenSourceIsNull()
  {
    // When
    var result = AstroDto.MapFrom(null);

    // Then
    result.Should().BeNull();
  }

  [Fact]
  public void ShouldMapIsMoonUpTrueWhenSourceIsOne()
  {
    // Given
    var source = new Astro { IsMoonUp = 1, IsSunUp = 0 };

    // When
    var result = AstroDto.MapFrom(source);

    // Then
    result!.IsMoonUp.Should().BeTrue();
  }

  [Fact]
  public void ShouldMapIsMoonUpFalseWhenSourceIsZero()
  {
    // Given
    var source = new Astro { IsMoonUp = 0, IsSunUp = 1 };

    // When
    var result = AstroDto.MapFrom(source);

    // Then
    result!.IsMoonUp.Should().BeFalse();
  }

  [Fact]
  public void ShouldMapIsSunUpTrueWhenSourceIsOne()
  {
    // Given
    var source = new Astro { IsSunUp = 1, IsMoonUp = 0 };

    // When
    var result = AstroDto.MapFrom(source);

    // Then
    result!.IsSunUp.Should().BeTrue();
  }

  [Fact]
  public void ShouldMapIsSunUpFalseWhenSourceIsZero()
  {
    // Given
    var source = new Astro { IsSunUp = 0, IsMoonUp = 0 };

    // When
    var result = AstroDto.MapFrom(source);

    // Then
    result!.IsSunUp.Should().BeFalse();
  }

  [Fact]
  public void ShouldMapAllFieldsCorrectly()
  {
    // Given
    var source = new Astro
    {
      Sunrise = "07:30 AM",
      Sunset = "05:45 PM",
      Moonrise = "11:00 AM",
      Moonset = "10:30 PM",
      MoonPhase = "Waning Gibbous",
      MoonIllumination = 85,
      IsMoonUp = 1,
      IsSunUp = 0
    };

    // When
    var result = AstroDto.MapFrom(source);

    // Then
    result.Should().NotBeNull();
    result.Sunrise.Should().Be("07:30 AM");
    result.Sunset.Should().Be("05:45 PM");
    result.Moonrise.Should().Be("11:00 AM");
    result.Moonset.Should().Be("10:30 PM");
    result.MoonPhase.Should().Be("Waning Gibbous");
    result.MoonIllumination.Should().Be(85);
    result.IsMoonUp.Should().BeTrue();
    result.IsSunUp.Should().BeFalse();
  }
}

