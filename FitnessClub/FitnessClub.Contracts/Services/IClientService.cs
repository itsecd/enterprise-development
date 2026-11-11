using FitnessClub.Contracts.Dtos;

namespace FitnessClub.Contracts.Services;

/// <summary>
/// Определяет операции для работы с клиентами фитнес-клуба.
/// </summary>
public interface IClientService
{
    /// <summary>
    /// Получает всех клиентов.
    /// </summary>
    /// <returns>Коллекция клиентов.</returns>
    public Task<IReadOnlyCollection<ClientDto>> GetAllAsync();

    /// <summary>
    /// Получает клиента по ID.
    /// </summary>
    /// <param name="id">ID клиента.</param>
    /// <returns>Клиент или null, если он не найден.</returns>
    public Task<ClientDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создаёт нового клиента.
    /// </summary>
    /// <param name="dto">Данные нового клиента.</param>
    /// <returns>Созданный клиент.</returns>
    public Task<ClientDto> CreateAsync(CreateClientDto dto);

    /// <summary>
    /// Обновляет данные клиента.
    /// </summary>
    /// <param name="id">ID клиента.</param>
    /// <param name="dto">Новые данные клиента.</param>
    /// <returns>true, если клиент найден и обновлён.</returns>
    public Task<bool> UpdateAsync(int id, UpdateClientDto dto);

    /// <summary>
    /// Удаляет клиента.
    /// </summary>
    /// <param name="id">ID клиента.</param>
    /// <returns>true, если клиент найден и удалён.</returns>
    public Task<bool> DeleteAsync(int id);
}