namespace DryCleaning.Api.Dtos;

/// <summary>
/// Клиент получает
/// </summary>
public record ClientDto(int Id, string FullName, string PhoneNumber);

/// <summary>
/// Клиент присылает при создании
/// </summary>
public record CreateClientDto(string FullName, string PhoneNumber);

/// <summary>
/// Клиент присылает при обновлении
/// </summary>
public record UpdateClientDto(string FullName, string PhoneNumber);
