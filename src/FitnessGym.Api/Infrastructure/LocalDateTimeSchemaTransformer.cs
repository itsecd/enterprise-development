using System.Text.Json.Nodes;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace FitnessGym.Api.Infrastructure;

/// <summary>
/// Описывает местное время клуба как строку без зоны вместо RFC 3339 date-time.
/// </summary>
public class LocalDateTimeSchemaTransformer : IOpenApiSchemaTransformer
{
    public Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context,
        CancellationToken cancellationToken)
    {
        if (context.JsonTypeInfo.Type == typeof(DateTime) || context.JsonTypeInfo.Type == typeof(DateTime?))
        {
            schema.Format = null;
            schema.Pattern = @"^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}(?:\.\d{1,7})?$";
            schema.Description = "Местное время клуба, без Z и смещения часового пояса.";
            var example = string.Equals(context.JsonPropertyInfo?.Name, "endsAt", StringComparison.OrdinalIgnoreCase)
                ? "2026-11-08T13:00:00"
                : "2026-11-08T12:00:00";
            schema.Examples = [JsonValue.Create(example)];
        }

        return Task.CompletedTask;
    }
}
