using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace VetClinic.ServiceDefaults;

/// <summary>
/// Общие настройки Aspire: телеметрия, проверки работоспособности, обнаружение сервисов и устойчивость HTTP-клиентов
/// </summary>
public static class Extensions
{
    private static readonly string _healthEndpointPath = "/health";
    private static readonly string _alivenessEndpointPath = "/alive";

    /// <summary>
    /// Подключить общие настройки Aspire к сервису
    /// </summary>
    /// <typeparam name="TBuilder">Тип построителя приложения</typeparam>
    /// <param name="builder">Построитель приложения</param>
    public static TBuilder AddServiceDefaults<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        builder.ConfigureOpenTelemetry();
        builder.AddDefaultHealthChecks();

        builder.Services.AddServiceDiscovery();
        builder.Services.ConfigureHttpClientDefaults(http =>
        {
            http.AddStandardResilienceHandler();
            http.AddServiceDiscovery();
        });

        return builder;
    }

    /// <summary>
    /// Настроить логи, метрики и трассировку OpenTelemetry
    /// </summary>
    /// <typeparam name="TBuilder">Тип построителя приложения</typeparam>
    /// <param name="builder">Построитель приложения</param>
    public static TBuilder ConfigureOpenTelemetry<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        builder.Logging.AddOpenTelemetry(logging =>
        {
            logging.IncludeFormattedMessage = true;
            logging.IncludeScopes = true;
        });

        builder.Services.AddOpenTelemetry()
            .WithMetrics(metrics => metrics
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddRuntimeInstrumentation())
            .WithTracing(tracing => tracing
                .AddSource(builder.Environment.ApplicationName)
                .AddAspNetCoreInstrumentation(options =>
                    options.Filter = context =>
                        !context.Request.Path.StartsWithSegments(_healthEndpointPath)
                        && !context.Request.Path.StartsWithSegments(_alivenessEndpointPath))
                .AddHttpClientInstrumentation());

        if (!string.IsNullOrWhiteSpace(builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]))
        {
            builder.Services.AddOpenTelemetry().UseOtlpExporter();
        }

        return builder;
    }

    /// <summary>
    /// Зарегистрировать проверку, что процесс отвечает
    /// </summary>
    /// <typeparam name="TBuilder">Тип построителя приложения</typeparam>
    /// <param name="builder">Построитель приложения</param>
    public static TBuilder AddDefaultHealthChecks<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        builder.Services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);

        return builder;
    }

    /// <summary>
    /// Открыть /health и /alive, только в среде разработки
    /// </summary>
    /// <param name="app">Веб-приложение</param>
    public static WebApplication MapDefaultEndpoints(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapHealthChecks(_healthEndpointPath);
            app.MapHealthChecks(_alivenessEndpointPath, new HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains("live")
            });
        }

        return app;
    }
}
