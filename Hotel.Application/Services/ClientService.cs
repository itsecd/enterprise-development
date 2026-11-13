using Hotel.Application.Interfaces;
using Hotel.Application.Mapping;
using Hotel.Contracts.DTO;
using Hotel.Contracts.Request;
using Hotel.Domain.Entities;
using Hotel.Domain.Interfaces;

namespace Hotel.Application.Services;

/// <summary>
/// Сервис для управления клиентами
/// </summary>
public class ClientService(IRepository<Client> repository, IRepository<Booking> bookingRepository)
    : CrudService<Client, ClientRequest, ClientDto>(repository), IClientService
{
    /// <summary>
    /// Создание сущности клиента из запроса
    /// </summary>
    protected override Task<Client> BuildEntityAsync(ClientRequest request, int id)
    {
        return Task.FromResult(new Client
        {
            Id = id,
            LastName = request.LastName,
            FirstName = request.FirstName,
            Patronymic = request.Patronymic,
            BirthDate = request.BirthDate!.Value,
            NumberPassport = request.NumberPassport,
            Citizenship = request.Citizenship
        });
    }

    /// <summary>
    /// Копирование обновленных значений в существующую сущность клиента
    /// </summary>
    protected override void CopyValues(Client source, Client target)
    {
        target.LastName = source.LastName;
        target.FirstName = source.FirstName;
        target.Patronymic = source.Patronymic;
        target.BirthDate = source.BirthDate;
        target.NumberPassport = source.NumberPassport;
        target.Citizenship = source.Citizenship;
    }

    /// <summary>
    /// Преобразование сущности клиента в DTO
    /// </summary>
    protected override ClientDto ToDto(Client entity) => entity.ToDto();

    /// <summary>
    /// Проверка клиента на наличие бронирования перед удалением
    /// </summary>
    protected override async Task CheckDeleteAsync(Client entity)
    {
        var bookings = await bookingRepository.GetAllAsync();
        if (bookings.Any(b => b.Client.Id == entity.Id))
        {
            throw new ArgumentException("Нельзя удалить клиента, у он в настоящее время проживает в гостинице");
        }
    }
}