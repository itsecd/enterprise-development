namespace DryCleaner.Application.Contracts.Interfaces;

/// <summary>
/// Определяет стандартные операции CRUD для прикладных сервисов
/// </summary>
public interface IApplicationService<TDto, TCreateUpdateDto, TKey>
{
	/// <summary>
	/// Создаёт новую запись
	/// </summary>
	Task<TDto> CreateAsync(TCreateUpdateDto dto);

	/// <summary>
	/// Возвращает запись по идентификатору
	/// </summary>
	Task<TDto?> GetAsync(TKey id);

	/// <summary>
	/// Возвращает все записи
	/// </summary>
	Task<IList<TDto>> GetAllAsync();

	/// <summary>
	/// Обновляет существующую запись
	/// </summary>
	Task<TDto?> UpdateAsync(TCreateUpdateDto dto, TKey id);

	/// <summary>
	/// Удаляет запись
	/// </summary>
	Task<bool> DeleteAsync(TKey id);
}