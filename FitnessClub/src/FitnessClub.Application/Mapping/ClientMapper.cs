using FitnessClub.Contracts.Dtos.Read;
using FitnessClub.Contracts.Dtos.Edit;
using FitnessClub.Domain.Entities;

namespace FitnessClub.Application.Mapping;
/// <summary>
/// Преобразует данные клиентов
/// </summary>
public static class ClientMapper
{
    /// <summary>
    /// Преобразует клиента в DTO
    /// </summary>
    public static ClientDto ToDto(this Client client) =>
        new(
            Id: client.Id,
            FullName: client.FullName,
            PassportNumber: client.PassportNumber,
            Gender: client.Gender,
            BirthDate: client.BirthDate,
            PhoneNumber: client.PhoneNumber,
            SubscriptionStart: client.SubscriptionStart,
            SubscriptionEnd: client.SubscriptionEnd);
    /// <summary>
    /// Создаёт клиента из DTO
    /// </summary>
    public static Client ToEntity(this ClientEditDto clientEditDto) =>
        new()
        {
            FullName = clientEditDto.FullName,
            PassportNumber = clientEditDto.PassportNumber,
            Gender = clientEditDto.Gender,
            BirthDate = clientEditDto.BirthDate,
            PhoneNumber = clientEditDto.PhoneNumber,
            SubscriptionStart = clientEditDto.SubscriptionStart,
            SubscriptionEnd = clientEditDto.SubscriptionEnd
        };
    /// <summary>
    /// Обновляет клиента данными из DTO
    /// </summary>
    public static void UpdateEntity(this Client client, ClientEditDto clientEditDto)
    {
        client.FullName = clientEditDto.FullName;
        client.PassportNumber = clientEditDto.PassportNumber;
        client.Gender = clientEditDto.Gender;
        client.BirthDate = clientEditDto.BirthDate;
        client.PhoneNumber = clientEditDto.PhoneNumber;
        client.SubscriptionStart = clientEditDto.SubscriptionStart;
        client.SubscriptionEnd = clientEditDto.SubscriptionEnd; 
    }
}