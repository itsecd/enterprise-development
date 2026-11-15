using FitnesClub.Domain.Entities;

namespace FitnesClub.Api.Contracts.Members;

/// <summary>
/// Методы расширения для преобразования между доменной сущностью <see cref="Member"/> и DTO клиентов.
/// </summary>
public static class MemberMappingExtensions
{
    /// <summary>
    /// Преобразует доменную сущность клиента в объект передачи данных <see cref="MemberDto"/>.
    /// </summary>
    /// <param name="m">Сущность клиента фитнес-клуба.</param>
    /// <returns>DTO с полными данными клиента, включая персональную информацию и срок действия абонемента.</returns>
    public static MemberDto ToDto(this Member m) => new(
        m.Id,
        m.PassportNumber,
        m.FirstName,
        m.LastName,
        m.Gender,
        m.BirthDate,
        m.PhoneNumber,
        m.MembershipStartDate,
        m.MembershipEndDate
    );

    /// <summary>
    /// Создает новый экземпляр доменной сущности <see cref="Member"/> на основе входящего DTO.
    /// </summary>
    /// <param name="dto">Входные данные для создания клиента клуба.</param>
    /// <returns>Новая сущность клиента с новым идентификатором <see cref="Guid"/>.</returns>
    public static Member ToEntity(this MemberCreateUpdateDto dto) => new()
    {
        Id = Guid.NewGuid(),
        PassportNumber = dto.PassportNumber,
        FirstName = dto.FirstName,
        LastName = dto.LastName,
        Gender = dto.Gender,
        BirthDate = dto.BirthDate,
        PhoneNumber = dto.PhoneNumber,
        MembershipStartDate = dto.MembershipStartDate,
        MembershipEndDate = dto.MembershipEndDate
    };

    /// <summary>
    /// Обновляет персональные данные и период действия абонемента клиента значениями из переданного DTO.
    /// </summary>
    /// <param name="dto">DTO с актуальными данными клиента.</param>
    /// <param name="m">Существующая сущность клиента, поля которой будут перезаписаны.</param>
    public static void UpdateEntity(this MemberCreateUpdateDto dto, Member m)
    {
        m.PassportNumber = dto.PassportNumber;
        m.FirstName = dto.FirstName;
        m.LastName = dto.LastName;
        m.Gender = dto.Gender;
        m.BirthDate = dto.BirthDate;
        m.PhoneNumber = dto.PhoneNumber;
        m.MembershipStartDate = dto.MembershipStartDate;
        m.MembershipEndDate = dto.MembershipEndDate;
    }
}