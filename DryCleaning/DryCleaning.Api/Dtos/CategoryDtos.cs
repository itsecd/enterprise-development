namespace DryCleaning.Api.Dtos;

/// <summary>
/// Клиент получает
/// </summary>
public record CategoryDto(int Id, string Name, string CleaningType, decimal Price);

/// <summary>
/// Клиент присылает при создании
/// </summary>
public record CreateCategoryDto(string Name, string CleaningType, decimal Price);

/// <summary>
/// Клиент присылает при обновлении
/// </summary>
public record UpdateCategoryDto(string Name, string CleaningType, decimal Price);
