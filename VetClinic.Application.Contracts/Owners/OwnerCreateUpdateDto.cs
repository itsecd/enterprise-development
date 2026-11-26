namespace VetClinic.Application.Contracts.Owners;

/// <summary>
/// DTO входящих данных для внесения или изменения информации о владельцах
/// </summary>
/// <param name="FullName">ФИО владельца</param>
/// <param name="Phone">Телефон владельца</param>
/// <param name="Adress">Адрес владельца</param>
public record OwnerCreateUpdateDto(string FullName, string Phone, string Adress);
