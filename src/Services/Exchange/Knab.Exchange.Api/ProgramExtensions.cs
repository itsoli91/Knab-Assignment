using System.Text.Json.Serialization;
using Knab.Exchange.Api.Configs;
using Knab.Exchange.Api.Filters;
using Knab.Exchange.Domain.Crypto;
using Knab.Exchange.Domain.Exchange;
using Knab.Exchange.Infrastructure.Common.Configurations;
using Knab.Exchange.Infrastructure.Repositories.CryptoPriceRepository;
using Knab.Exchange.Infrastructure.Repositories.CryptoPriceRepository.External;
using Knab.Exchange.Infrastructure.Repositories.ExchangeRateRepository;
using Knab.Exchange.Infrastructure.Repositories.ExchangeRateRepository.External;
using Knab.Exchange.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.IdentityModel.Logging;
using Refit;
using Serilog;
using Serilog.Enrichers.Span;

namespace Knab.Exchange.Api;

public static class ProgramExtensions
{
    public static void AddCustomSerilog(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithEnvironmentName()
            .Enrich.WithSpan()
            .WriteTo.Console()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

        builder.Host.UseSerilog();
    }

    public static void AddCoreApiVersioning(this WebApplicationBuilder builder)
    {
        builder.AddVersioning();
        builder.AddSwaggerVersioning();
    }

    private static void AddVersioning(this WebApplicationBuilder builder)
    {
        var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();

        var setupDefaultApiVersion = new ApiVersion(appSettings.MajorVersion, appSettings.MinorVersion);

        builder.Services.AddApiVersioning(
            setup =>
            {
                setup.DefaultApiVersion = setupDefaultApiVersion;
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.ReportApiVersions = true;
            });

        builder.Services.AddVersionedApiExplorer(
            setup =>
            {
                setup.DefaultApiVersion = setupDefaultApiVersion;
                setup.GroupNameFormat = "'v'VVV";
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.SubstituteApiVersionInUrl = true;
            });
    }

    private static void AddSwaggerVersioning(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen();
        builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
    }

    public static void AddCustomCors(this WebApplicationBuilder builder)
    {
        //add cors
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("default",
                b =>
                {
                    b.WithOrigins("http://localhost:3000");
                    b.AllowAnyHeader();
                    b.AllowAnyMethod();
                });
        });
    }

    public static void AddCustomConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
    }

    public static void AddCustomController(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers(config => { config.Filters.Add<HttpGlobalExceptionFilter>(); })
            .AddJsonOptions(
                options =>
                {
                    // show enum value in swagger.
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
    }

    public static void AddCustomServices(this WebApplicationBuilder builder)
    {
        var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();

        builder.Services
            .AddRefitClient<ICoinMarketCapApi>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(appSettings.CoinMarketCapApiBaseUrl);
                c.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", appSettings.CoinMarketCapApiKey);
            });

        builder.Services
            .AddRefitClient<IExchangeRatesApi>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(appSettings.ExchangeRatesApiBaseUrl);
                c.DefaultRequestHeaders.Add("apiKey", appSettings.ExchangeRatesApiKey);
            });

        builder.Services.AddScoped<IExchangeRateRepository, UsdExchangeRateRepository>();
        builder.Services.AddScoped<ICryptoPriceRepository, CoinMarketCapRepository>();
        builder.Services.AddScoped<ICryptoPriceService, CryptoPriceService>();
    }


    public static void AddCustomAuthentication(this WebApplicationBuilder builder)
    {
        var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

        if (builder.Environment.IsDevelopment())
        {
            IdentityModelEventSource.ShowPII = true;
        }

        builder.Services.AddAuthentication(
            options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(
            o =>
            {
                o.Authority = jwtSettings.Authority;
                o.RequireHttpsMetadata = jwtSettings.RequireHttpsMetadata;
                o.TokenValidationParameters.ValidateAudience = jwtSettings.ValidateAudience;
                o.TokenValidationParameters.ValidAudiences = jwtSettings.ValidAudiences;
                o.TokenValidationParameters.ValidateIssuer = jwtSettings.ValidateIssuer;
                o.TokenValidationParameters.ValidIssuers = jwtSettings.ValidIssuers;
                o.TokenValidationParameters.ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey;
                o.TokenValidationParameters.RequireSignedTokens = jwtSettings.RequireSignedTokens;
                //o.TokenValidationParameters.IssuerSigningKey = new JsonWebKey(jwtSettings.IssuerSigningKey.ToString());
            });
    }

    public static void UseSwaggerWithVersioning(this WebApplication webApplication)
    {
        var provider = webApplication.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        webApplication.UseSwagger();

        webApplication.UseSwaggerUI(
            options =>
            {
                foreach (var description in provider.ApiVersionDescriptions.Reverse())
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }
            });
    }
}