namespace Hotel.Application.Contracts;
/// <summary>
/// Базовая служба для CRUD операций
/// </summary>
/// <typeparam name="TResponseDto">DTO сущности для ответа</typeparam>
/// <typeparam name="TCreateUpdateDto">DTO сущности для создания/обновления</typeparam>
public interface IService<TResponseDto, TCreateUpdateDto>
{
    /// <summary>
    /// Получений всех сущностей
    /// </summary>
    /// <param name="ct">Токен для отмены операции</param>
    /// <returns>Коллекция сущностей</returns>
    public Task<IEnumerable<TResponseDto>> GetAllAsync(CancellationToken ct = default);

    /// <summary>
    /// Получение сущности по id
    /// </summary>
    /// <param name="id">Id сущности</param>
    /// <param name="ct">Токен для отмены операции</param>
    /// <returns>Сущность</returns>
    public Task<TResponseDto?> GetByIdAsync(int id, CancellationToken ct = default);

    /// <summary>
    /// Создание сущности
    /// </summary>
    /// <param name="dto">DTO</param>
    /// <param name="ct">Токен для отмены операции</param>
    /// <returns>Создание сущности</returns>
    public Task<TResponseDto> CreateAsync(TCreateUpdateDto dto, CancellationToken ct = default);
    
    /// <summary>
    /// Обнавлённая сущности
    /// </summary>
    /// <param name="id">Id сущности</param>
    /// <param name="dto">DTO</param>
    /// <param name="ct">Токен для отмены операции</param>
    /// <returns>Обнавлённая сущность</returns>
    public Task<TResponseDto?> UpdateAsync(int id, TCreateUpdateDto dto, CancellationToken ct = default);
    
    /// <summary>
    /// Удаление сущности
    /// </summary>
    /// <param name="id">Id сущности</param>
    /// <param name="ct">Токен для отмены операции</param>
    public Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}