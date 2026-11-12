namespace DryCleaner.Application.Contracts.Interfaces;

/// <summary>
/// Определяет стандартные операции CRUD для прикладных сервисов
/// </summary>
public interface IApplicationService<TDto, TCreateUpdateDto, TKey>
{
	/// <summary>
	/// Создаёт новую запись
	/// </summary>
	TDto Create(TCreateUpdateDto dto);

	/// <summary>
	/// Возвращает запись по идентификатору
	/// </summary>
	TDto? Get(TKey id);

	/// <summary>
	/// Возвращает все записи
	/// </summary>
	IList<TDto> GetAll();

	/// <summary>
	/// Обновляет существующую запись
	/// </summary>
	TDto? Update(TCreateUpdateDto dto, TKey id);

	/// <summary>
	/// Удаляет запись
	/// </summary>
	bool Delete(TKey id);
}