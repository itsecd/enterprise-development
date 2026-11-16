namespace DryCleaning.Api.Dtos;

/// <summary>
/// Клиент получает
/// </summary>
public record ItemDto(int Id, string Name, string Material, int CategoryId, string CategoryName);

/// <summary>
/// Клиент присылает при создании
/// </summary>
public record CreateItemDto(string Name, string Material, int CategoryId);

/// <summary>
/// Клиент присылает при обновлении
/// </summary>
public record UpdateItemDto(string Name, string Material, int CategoryId);
