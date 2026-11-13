namespace Hotel.Tests;

/// <summary>
/// DTO для сверки результата теста (FindClientByTypeRoom)
/// </summary>
public record ClientFullName(string LastName, string FirstName, string? Patronymic);