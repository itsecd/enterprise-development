namespace VeterinaryClinic.Application.Contracts.Owners;

/// <summary>
/// Данные для создания или обновления владельца
/// </summary>
/// <param name="LastName">Фамилия</param>
/// <param name="FirstName">Имя</param>
/// <param name="Patronymic">Отчество, если указано</param>
/// <param name="Phone">Контактный телефон</param>
/// <param name="Address">Контактный адрес</param>
public record SaveOwnerRequest(string LastName, string FirstName, string? Patronymic, string Phone, string Address);
