using System.Reflection;
using FluentValidation;
using Weather.Application;
using Timezone.Application;
using Astronomy.Application;
using Common.Infrastructure.Exceptions;
using Common.Infrastructure.Services.Implementations;
using Common.Infrastructure.Services.Interfaces;
using WeatherApp.Backend.Api.Configuration;

namespace WeatherApp.Backend.Api.FrameworkExtensions;

internal static class ApplicationBuildingExtensions
{
  private const string CorsPolicyName = "ApiCorsPolicy";

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
      .AddRouting(routes => routes.LowercaseUrls = true)
      .AddCors(options =>
      {
        var corsHosts = builder.GetConfigurationSection<CorsSettings>().AllowedOrigins
                        ?? throw new ApplicationConfigurationException("Failed to load CORS settings configuration.");

        options.AddPolicy(CorsPolicyName, corsBuilder =>
        {
          corsBuilder.WithOrigins(corsHosts.ToArray())
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
      });

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
      app.UseRouting()
        .UseCors(CorsPolicyName);
      app.MapControllers();
    }
  }

  internal static T GetConfigurationSection<T>(this WebApplicationBuilder builder)
    => builder.Configuration.GetSection(typeof(T).Name).Get<T>()
       ?? throw new ApplicationConfigurationException(
         $"The configuration section {typeof(T).Name} could not be found.");

  internal static WebApplicationBuilder AttachMediatR(this WebApplicationBuilder builder)
  {
    builder.Services.AddMediatR(configuration => { configuration.RegisterServicesFromAssemblies(Assemblies); });

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
