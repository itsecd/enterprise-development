using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace BeautySalon.ServiceDefaults;

/// <summary>
/// Общие настройки телеметрии и проверки готовности API
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Регистрирует журналы, метрики и трассировку HTTP-запросов для панели Aspire
    /// </summary>
    public static WebApplicationBuilder AddServiceDefaults(this WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks();
        builder.Logging.AddOpenTelemetry(options =>
        {
            options.IncludeFormattedMessage = true;
            options.IncludeScopes = true;
        });
        var telemetry = builder.Services.AddOpenTelemetry()
            .WithMetrics(metrics => metrics.AddAspNetCoreInstrumentation().AddRuntimeInstrumentation())
            .WithTracing(tracing => tracing.AddAspNetCoreInstrumentation(options =>
                options.Filter = context => !context.Request.Path.StartsWithSegments("/health")));

        if (!string.IsNullOrWhiteSpace(builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]))
        {
            telemetry.UseOtlpExporter();
        }

        return builder;
    }

    /// <summary>
    /// Открывает проверку готовности для локального запуска через Aspire
    /// </summary>
    public static WebApplication MapDefaultEndpoints(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapHealthChecks("/health");
        }

        return app;
    }
}