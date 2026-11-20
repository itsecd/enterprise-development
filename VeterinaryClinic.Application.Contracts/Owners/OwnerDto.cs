using VeterinaryClinic.Application.Contracts.Appointments;
using VeterinaryClinic.Application.Contracts.Breeds;
using VeterinaryClinic.Application.Contracts.Owners;
using VeterinaryClinic.Application.Contracts.Pets;
using VeterinaryClinic.Application.Contracts.Specializations;
using VeterinaryClinic.Application.Contracts.Veterinarians;
namespace VeterinaryClinic.Application.Contracts.Owners;

/// <summary>
/// Данные владельца в ответе
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="LastName">Фамилия</param>
/// <param name="FirstName">Имя</param>
/// <param name="Patronymic">Отчество, если указано</param>
/// <param name="Phone">Контактный телефон</param>
/// <param name="Address">Контактный адрес</param>
public record OwnerDto(int Id, string LastName, string FirstName, string? Patronymic, string Phone, string Address);
