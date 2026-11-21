using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace DryCleaner.Api;

/// <summary>
/// Добавляет в OpenAPI-документ схему Bearer-аутентификации.
/// </summary>
internal sealed class BearerSecurityTransformer : IOpenApiDocumentTransformer
{
    /// <summary>
    /// Добавляет в OpenAPI-документ схему Bearer (JWT), чтобы Scalar показывал поле для ввода токена.
    /// </summary>
    /// <param name="document">OpenAPI-документ, в который добавляется схема безопасности.</param>
    /// <param name="context">Контекст трансформации документа.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Завершённая задача.</returns>
    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context,
        CancellationToken cancellationToken)
    {
        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes = new Dictionary<string, IOpenApiSecurityScheme>
        {
            ["Bearer"] = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            }
        };

        document.Security =
        [
            new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("Bearer", document)] = []
            }
        ];

        return Task.CompletedTask;
    }
}