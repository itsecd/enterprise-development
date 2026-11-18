using FitnessClub.Application.Mapping;
using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Application.Services;

/// <summary>
/// CRUD-сервис клиентов
/// </summary>
public class ClientService(IClientRepository repository)
    : CrudServiceBase<Client, ClientDto, ClientEditDto>(repository), IClientService
{
    /// <summary>
    /// Проверяет бизнес-правила абонемента
    /// </summary>
    /// <exception cref="ArgumentException">Дата начала позже даты окончания</exception>
    protected override Task ValidateAsync(ClientEditDto editDto)
    {
        if (editDto.SubscriptionStart > editDto.SubscriptionEnd)
            throw new ArgumentException("Дата начала абонемента не может быть позже даты окончания");

        return Task.CompletedTask;
    }

    /// <inheritdoc />
    protected override ClientDto MapToDto(Client entity) => entity.ToDto();

    /// <inheritdoc />
    protected override Task<Client> MapToEntityAsync(ClientEditDto editDto) =>
        Task.FromResult(new Client
        {
            PassportNumber = editDto.PassportNumber,
            FullName = editDto.FullName,
            Gender = editDto.Gender is { } gender
                ? gender
                : throw new InvalidOperationException("Пол обязателен"),
            BirthDate = editDto.BirthDate,
            Phone = editDto.Phone,
            SubscriptionStart = editDto.SubscriptionStart,
            SubscriptionEnd = editDto.SubscriptionEnd is { } subscriptionEnd
                ? subscriptionEnd
                : throw new InvalidOperationException("Дата окончания подписки обязательно"),
        });
}