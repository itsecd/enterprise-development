using BeautySalon.Contracts.Dto;

namespace BeautySalon.Contracts.Interfaces;

/// <summary>
/// Сервис управления клиентами
/// </summary>
public interface IClientService
{
    /// <summary>
    /// Получает список всех клиентов
    /// </summary>
    public Task<IReadOnlyList<ClientDto>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает клиента по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    public Task<ClientDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Создаёт клиента
    /// </summary>
    /// <param name="dto">Данные для создания</param>
    /// <returns>Созданный объект с назначенным идентификатором</returns>
    public Task<ClientDto> CreateAsync(ClientEditDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновляет клиента
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="dto">Новые данные</param>
    /// <returns>Обновлённый объект</returns>
    public Task<ClientDto> UpdateAsync(int id, ClientEditDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет клиента
    /// </summary>
    /// <param name="id">Идентификатор</param>
    public Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}