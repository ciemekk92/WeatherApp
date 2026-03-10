using WeatherApp.Backend.Api.FrameworkExtensions;

namespace WeatherApp.Backend.Api;

public class Program
{
  public static async Task Main(string[] args)
  {
    var webHostBuilder = ConfigureWebApplicationBuilder(args);
    var app = webHostBuilder.Build();

    app.ConfigureWebApplicationMiddleware();

    await app.RunAsync();
  }

  private static WebApplicationBuilder ConfigureWebApplicationBuilder(string[] args)
    => WebApplication.CreateBuilder(args)
      .ConfigureSecretsProvider()
      .ConfigureAsHttpApi()
      .AttachMediatR()
      .ConfigureWeatherApiClient();
}
