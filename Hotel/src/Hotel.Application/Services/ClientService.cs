using System.ComponentModel.DataAnnotations;
using Hotel.Contracts.Dto;
using Hotel.Contracts.Exceptions;
using Hotel.Contracts.Interfaces;
using Hotel.Domain.Enums;
using Hotel.Domain.Interfaces;
using Hotel.Domain.Models;

namespace Hotel.Application.Services;

/// <summary>Сервис клиентов гостиницы и аналитики по их проживаниям.</summary>
/// <param name="clientRepository">Репозиторий клиентов.</param>
/// <param name="bookingRepository">Репозиторий бронирований.</param>
/// <param name="relationshipLock">Защита связанных операций.</param>
public class ClientService(
    IClientRepository clientRepository,
    IBookingRepository bookingRepository,
    IRelationshipLock relationshipLock) : IClientService
{
    private readonly IClientRepository _clientRepository = clientRepository;
    private readonly IBookingRepository _bookingRepository = bookingRepository;
    private readonly IRelationshipLock _relationshipLock = relationshipLock;

    public async Task<IReadOnlyList<ClientDto>> GetAllAsync()
    {
        var clients = await _clientRepository.GetAllAsync();
        return clients.OrderBy(client => client.FullName, StringComparer.Ordinal)
            .Select(MapToDto).ToList();
    }

    public async Task<ClientDto?> GetByIdAsync(int id)
    {
        var client = await _clientRepository.GetByIdAsync(id);
        return client is null ? null : MapToDto(client);
    }

    public async Task<ClientDto> AddAsync(ClientEditDto request)
    {
        ValidateClient(request);
        using var operation = await _relationshipLock.AcquireAsync();

        var clients = await _clientRepository.GetAllAsync();
        EnsurePassportIsUnique(clients, request.PassportNumber, null);

        var client = new Client
        {
            PassportNumber = request.PassportNumber.Trim(),
            FullName = request.FullName.Trim(),
            BirthDate = request.BirthDate.Date,
            Citizenship = request.Citizenship.Trim()
        };

        return MapToDto(await _clientRepository.AddAsync(client));
    }

    public async Task<bool> UpdateAsync(int id, ClientEditDto request)
    {
        ValidateClient(request);
        using var operation = await _relationshipLock.AcquireAsync();

        var existing = await _clientRepository.GetByIdAsync(id);
        if (existing is null)
            return false;

        var clients = await _clientRepository.GetAllAsync();
        EnsurePassportIsUnique(clients, request.PassportNumber, id);

        var updated = new Client
        {
            Id = id,
            PassportNumber = request.PassportNumber.Trim(),
            FullName = request.FullName.Trim(),
            BirthDate = request.BirthDate.Date,
            Citizenship = request.Citizenship.Trim()
        };

        return await _clientRepository.UpdateAsync(updated);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var operation = await _relationshipLock.AcquireAsync();

        if (await _clientRepository.GetByIdAsync(id) is null)
            return false;

        if (await _bookingRepository.HasBookingsForClientAsync(id))
            throw new ResourceConflictException("Нельзя удалить клиента, у которого есть бронирования.");

        return await _clientRepository.DeleteAsync(id);
    }

    public async Task<IReadOnlyList<ClientDto>> GetByRoomCategoryAsync(RoomCategory category)
    {
        if (!Enum.IsDefined(category))
            throw new ValidationException("Указана неизвестная категория номера.");

        var bookings = await _bookingRepository.GetAllAsync();
        return bookings
            .Where(booking => booking.Room.Type.Category == category)
            .Select(booking => booking.Client)
            .DistinctBy(client => client.Id)
            .OrderBy(client => client.FullName, StringComparer.Ordinal)
            .ThenBy(client => client.Id)
            .Select(MapToDto)
            .ToList();
    }

    public async Task<IReadOnlyList<TopClientSpendingDto>> GetTopSpendersAsync()
    {
        var bookings = await _bookingRepository.GetAllAsync();
        return bookings
            .GroupBy(booking => booking.Client.Id)
            .Select(group => new
            {
                Client = group.First().Client,
                TotalSpent = group.Sum(booking => booking.Nights * booking.Room.Type.PricePerNight)
            })
            .OrderByDescending(result => result.TotalSpent)
            .ThenBy(result => result.Client.FullName, StringComparer.Ordinal)
            .ThenBy(result => result.Client.Id)
            .Take(5)
            .Select(result => new TopClientSpendingDto
            {
                Client = MapToDto(result.Client),
                TotalSpent = result.TotalSpent
            })
            .ToList();
    }

    private static void ValidateClient(ClientEditDto request)
    {
        ArgumentNullException.ThrowIfNull(request);
        Validator.ValidateObject(request, new ValidationContext(request), validateAllProperties: true);

        if (string.IsNullOrWhiteSpace(request.PassportNumber) ||
            string.IsNullOrWhiteSpace(request.FullName) ||
            string.IsNullOrWhiteSpace(request.Citizenship))
            throw new ValidationException("Поля клиента не должны состоять только из пробелов.");

        if (request.BirthDate == default)
            throw new ValidationException("Необходимо указать дату рождения.");
        if (request.BirthDate.Date > DateTime.Today)
            throw new ValidationException("Дата рождения не может быть в будущем.");
    }

    private static void EnsurePassportIsUnique(IEnumerable<Client> clients, string passportNumber, int? excludedId)
    {
        var exists = clients.Any(client => client.Id != excludedId &&
            string.Equals(client.PassportNumber, passportNumber.Trim(), StringComparison.OrdinalIgnoreCase));

        if (exists)
            throw new ResourceConflictException("Клиент с таким номером паспорта уже существует.");
    }

    private static ClientDto MapToDto(Client client)
    {
        return new ClientDto
        {
            Id = client.Id,
            PassportNumber = client.PassportNumber,
            FullName = client.FullName,
            BirthDate = client.BirthDate,
            Citizenship = client.Citizenship
        };
    }
}
