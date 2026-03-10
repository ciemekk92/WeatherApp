using System.Reflection;
using FluentValidation;
using Weather.Application;
using Timezone.Application;
using Astronomy.Application;
using Common.Infrastructure.Services.Implementations;
using Common.Infrastructure.Services.Interfaces;

namespace WeatherApp.Backend.Api.FrameworkExtensions;

internal static class ApplicationBuildingExtensions
{
  private static readonly Assembly[] Assemblies =
  [
    typeof(WeatherModuleFeaturesAssemblyMarker).Assembly,
    typeof(TimezoneModuleFeaturesAssemblyMarker).Assembly,
    typeof(AstronomyModuleFeaturesAssemblyMarker).Assembly
  ];

  private static bool ShouldInstallSwagger(this WebApplicationBuilder builder)
    => builder.Environment.ShouldInstallSwagger();

  private static bool ShouldInstallSwagger(this WebApplication app)
    => app.Environment.ShouldInstallSwagger();

  private static bool ShouldInstallSwagger(this IWebHostEnvironment env)
    => env.IsDevelopment();

  internal static WebApplicationBuilder ConfigureAsHttpApi(this WebApplicationBuilder builder)
  {
    builder.Services
      .AddRouting(routes => routes.LowercaseUrls = true);
    // configure CORS if needed

    if (builder.ShouldInstallSwagger())
    {
      builder.Services
        .AddEndpointsApiExplorer()
        .AddSwaggerGen();
    }

    builder.Services.AddControllers();
    builder.Services.AddHttpContextAccessor();

    return builder;
  }

  internal static void ConfigureWebApplicationMiddleware(this WebApplication app)
  {
    if (app.ShouldInstallSwagger())
    {
      app.UseSwagger();
      app.UseSwaggerUI();
      app.UseRouting();
      app.MapControllers();
    }
  }

  internal static WebApplicationBuilder AttachMediatR(this WebApplicationBuilder builder)
  {
    builder.Services.AddMediatR(configuration =>
    {
      configuration.RegisterServicesFromAssemblies(Assemblies);
    });

    builder.Services.AddValidatorsFromAssemblies(Assemblies);

    return builder;
  }

  internal static WebApplicationBuilder ConfigureSecretsProvider(this WebApplicationBuilder builder)
  {
    if (builder.Environment.IsProduction())
    {
      // Register additional Secrets Manager client if in production
    }

    builder.Services.AddSingleton<ISecretsProvider>(provider =>
    {
      var configuration = provider.GetRequiredService<IConfiguration>();
      var environment = provider.GetRequiredService<IHostEnvironment>();

      return SecretsProviderFactory.CreateSecretsProvider(configuration, environment);
    });

    return builder;
  }

  internal static WebApplicationBuilder ConfigureWeatherApiClient(this WebApplicationBuilder builder)
  {
    builder.Services.AddHttpClient<IWeatherApiClient, WeatherApiClient>();

    return builder;
  }
}

