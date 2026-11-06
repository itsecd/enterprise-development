namespace FoodDelivery.Api;

/// <summary>
/// Приводит подстановочные токены маршрутов к нижнему регистру
/// </summary>
public sealed class LowercaseRouteTransformer : IOutboundParameterTransformer
{
    /// <inheritdoc/>
    public string? TransformOutbound(object? value)
    {
        return value?.ToString()?.ToLowerInvariant();
    }
}
