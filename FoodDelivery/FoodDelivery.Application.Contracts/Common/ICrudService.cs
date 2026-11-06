namespace FoodDelivery.Application.Contracts.Common;

/// <summary>
/// Определяет основные операции с ресурсом приложения
/// </summary>
/// <typeparam name="TDto">Тип данных, возвращаемых приложением </typeparam>
/// <typeparam name="TRequest">Тип данных для создания и изменения ресурса </typeparam>
public interface ICrudService<TDto, TRequest>
    where TDto : class
    where TRequest : class
{
    /// <summary>
    /// Возвращает все ресурсы
    /// </summary>
    /// <returns>Коллекция ресурсов </returns>
    IReadOnlyList<TDto> GetAll();

    /// <summary>
    /// Возвращает ресурс по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор ресурса </param>
    /// <returns>Ресурс или null, если он не найден </returns>
    TDto? GetById(int id);

    /// <summary>
    /// Создаёт ресурс
    /// </summary>
    /// <param name="request">Данные нового ресурса </param>
    /// <returns>Созданный ресурс с назначенным идентификатором </returns>
    TDto Create(TRequest request);

    /// <summary>
    /// Изменяет существующий ресурс
    /// </summary>
    /// <param name="id">Идентификатор ресурса </param>
    /// <param name="request">Новые данные ресурса </param>
    /// <returns>Изменённый ресурс или null, если он не найден </returns>
    TDto? Update(int id, TRequest request);

    /// <summary>
    /// Удаляет ресурс, если он существует 
    /// </summary>
    /// <param name="id">Идентификатор ресурса </param>
    void Delete(int id);
}