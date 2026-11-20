using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace VetClinic.Api.OpenApi;

/// <summary>
/// Добавляет в документ OpenAPI схему Bearer, чтобы в Scalar можно было вставить токен
/// </summary>
public class BearerSecuritySchemeTransformer : IOpenApiDocumentTransformer
{
    /// <summary>
    /// Имя схемы в документе
    /// </summary>
    public const string SchemeName = "Bearer";

    /// <inheritdoc/>
    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();
        document.Components.SecuritySchemes[SchemeName] = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Description = "Токен из POST /api/auth/login"
        };
        return Task.CompletedTask;
    }
}
