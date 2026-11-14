using CarWash.Application.Mapping;
using CarWash.Contracts.Dto;
using CarWash.Contracts.Interfaces;
using CarWash.Domain.Entities;
using CarWash.Domain.Interfaces;

namespace CarWash.Application.Services;

/// <summary>
/// Сервис для работы с клиентами и их автомобилями
/// </summary>
/// <param name="repository">Репозиторий клиентов</param>
/// <param name="carRepository">Репозиторий автомобилей</param>
public class ClientService(IClientRepository repository, ICarRepository carRepository) : IClientService
{
    /// <inheritdoc />
    public async Task<IEnumerable<ClientDto>> GetAllAsync()
    {
        var clients = await repository.GetAllAsync();
        return clients.Select(c => c.ToDto());
    }

    /// <inheritdoc />
    public async Task<ClientDto?> GetByIdAsync(int id)
    {
        var client = await repository.GetByIdAsync(id);
        return client?.ToDto();
    }

    /// <inheritdoc />
    public async Task<ClientDto> CreateAsync(ClientEditDto editDto)
    {
        var client = new Client
        {
            FullName = editDto.FullName,
            Phone = editDto.Phone
        };

        var created = await repository.AddAsync(client);
        return created.ToDto();
    }

    /// <inheritdoc />
    public async Task UpdateAsync(int id, ClientEditDto editDto)
    {
        var client = await repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Клиент не найден");

        client.FullName = editDto.FullName;
        client.Phone = editDto.Phone;
        await repository.UpdateAsync(client);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(int id)
    {
        _ = await repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Клиент не найден");

        await repository.DeleteAsync(id);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<CarDto>?> GetCarsAsync(int clientId)
    {
        var client = await repository.GetByIdAsync(clientId);
        return client?.Cars.Select(c => c.ToDto()).ToList();
    }

    /// <inheritdoc />
    public async Task AddCarAsync(int clientId, int carId)
    {
        var client = await repository.GetByIdAsync(clientId)
            ?? throw new KeyNotFoundException("Клиент не найден");
        var car = await carRepository.GetByIdAsync(carId)
            ?? throw new KeyNotFoundException("Автомобиль не найден");

        if (client.Cars.Any(c => c.Id == carId))
        {
            return;
        }

        var clients = await repository.GetAllAsync();
        if (clients.Any(c => c.Cars.Any(x => x.Id == carId)))
        {
            throw new InvalidOperationException("Автомобиль уже принадлежит другому клиенту");
        }

        client.Cars.Add(car);
        await repository.UpdateAsync(client);
    }

    /// <inheritdoc />
    public async Task RemoveCarAsync(int clientId, int carId)
    {
        var client = await repository.GetByIdAsync(clientId)
            ?? throw new KeyNotFoundException("Клиент не найден");
        var car = client.Cars.FirstOrDefault(c => c.Id == carId)
            ?? throw new KeyNotFoundException("У клиента нет такого автомобиля");

        client.Cars.Remove(car);
        await repository.UpdateAsync(client);
    }
}
