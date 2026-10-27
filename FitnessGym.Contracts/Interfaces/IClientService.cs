using FitnessGym.Contracts.Dtos;

namespace FitnessGym.Contracts.Interfaces;

/// <summary>
/// Сервис для управления клиентами фитнес-клуба
/// </summary>
public interface IClientService
{
    /// <summary>
    /// Получить список всех клиентов
    /// </summary>
    public Task<IReadOnlyList<ClientDto>> GetAllAsync();

    /// <summary>
    /// Получить клиента по его идентификатору
    /// </summary>
    public Task<ClientDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создать нового клиента
    /// </summary>
    public Task<ClientDto> CreateAsync(ClientDto dto);

    /// <summary>
    /// Обновить данные существующего клиента
    /// </summary>
    public Task<bool> UpdateAsync(int id, ClientDto dto);

    /// <summary>
    /// Удалить клиента по идентификатору
    /// </summary>
    public Task<bool> DeleteAsync(int id);
}