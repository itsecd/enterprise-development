namespace CarWash.Application.Contracts;

/// <summary>
/// Интерфейс службы приложения для CRUD операций
/// </summary>
/// <typeparam name="TDto">DTO для GET запросов</typeparam>
/// <typeparam name="TCreateUpdateDto">DTO для POST/PUT-запросов</typeparam>
/// <typeparam name="TKey">Тип идентификатора</typeparam>
public interface IApplicationService<TDto, TCreateUpdateDto, TKey>
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Создание DTO
    /// </summary>
    TDto Create(TCreateUpdateDto dto);

    /// <summary>
    /// Получение DTO по идентификатору
    /// </summary>
    TDto Get(TKey dtoId);

    /// <summary>
    /// Получение всего списка DTO
    /// </summary>
    List<TDto> GetAll();

    /// <summary>
    /// Обновление DTO
    /// </summary>
    TDto Update(TCreateUpdateDto dto, TKey dtoId);

    /// <summary>
    /// Удаление DTO
    /// </summary>
    void Delete(TKey dtoId);
}
