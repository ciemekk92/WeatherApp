using Common.Infrastructure.Models.WeatherApi;
using FluentAssertions;
using Timezone.Application.Dtos;

namespace Timezone.Test.Unit.Dtos;

public class TimezoneDtoTests
{
  [Fact]
  public void ShouldMapLocationFromResponse()
  {
    // Given
    var response = new TimezoneResponse
    {
      Location = new LocationInfo
      {
        Name = "Tokyo",
        Region = "Tokyo",
        Country = "Japan",
        Lat = 35.69,
        Lon = 139.69,
        TzId = "Asia/Tokyo",
        LocaltimeEpoch = 1700000000,
        Localtime = "2024-01-15 21:00"
      }
    };

    // When
    var result = TimezoneDto.MapFrom(response);

    // Then
    result.Should().NotBeNull();
    result.Location.Should().NotBeNull();
    result.Location!.Name.Should().Be("Tokyo");
    result.Location.TzId.Should().Be("Asia/Tokyo");
    result.Location.Lat.Should().Be(35.69);
    result.Location.Lon.Should().Be(139.69);
  }

  [Fact]
  public void ShouldMapNullLocationWhenResponseLocationIsNull()
  {
    // Given
    var response = new TimezoneResponse
    {
      Location = null
    };

    // When
    var result = TimezoneDto.MapFrom(response);

    // Then
    result.Should().NotBeNull();
    result.Location.Should().BeNull();
  }
}
