using Common.Infrastructure.Dtos;
using Common.Infrastructure.Models.WeatherApi;
using FluentAssertions;

namespace Common.Test.Unit.Dtos;

public class LocationDtoTests
{
  [Fact]
  public void ShouldReturnNullWhenSourceIsNull()
  {
    // When
    var result = LocationDto.MapFrom(null);

    // Then
    result.Should().BeNull();
  }

  [Fact]
  public void ShouldMapAllFieldsCorrectly()
  {
    // Given
    var source = new LocationInfo
    {
      Name = "New York",
      Region = "New York",
      Country = "United States of America",
      Lat = 40.71,
      Lon = -74.01,
      TzId = "America/New_York",
      LocaltimeEpoch = 1700000000,
      Localtime = "2024-01-15 07:00"
    };

    // When
    var result = LocationDto.MapFrom(source);

    // Then
    result.Should().NotBeNull();
    result.Name.Should().Be("New York");
    result.Region.Should().Be("New York");
    result.Country.Should().Be("United States of America");
    result.Lat.Should().Be(40.71);
    result.Lon.Should().Be(-74.01);
    result.TzId.Should().Be("America/New_York");
    result.LocaltimeEpoch.Should().Be(1700000000);
    result.Localtime.Should().Be("2024-01-15 07:00");
  }

  [Fact]
  public void ShouldMapNullableStringFieldsAsNull()
  {
    // Given
    var source = new LocationInfo
    {
      Name = null,
      Region = null,
      Country = null,
      TzId = null,
      Localtime = null
    };

    // When
    var result = LocationDto.MapFrom(source);

    // Then
    result.Should().NotBeNull();
    result.Name.Should().BeNull();
    result.Region.Should().BeNull();
    result.Country.Should().BeNull();
    result.TzId.Should().BeNull();
    result.Localtime.Should().BeNull();
  }
}
