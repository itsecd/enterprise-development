namespace DryCleaner.Application.Contracts.ServiceInterfaces;

/// <summary>
/// Определяет контракт для сервисов приложения, поддерживающих операции создания, получения, обновления и удаления
/// объектов.
/// </summary>
/// <typeparam name="TDto">Тип объекта передачи данных (DTO), используемый для вывода.</typeparam>
/// <typeparam name="TCreateUpdateDto">Тип объекта передачи данных (DTO), используемый для создания и обновления.</typeparam>
/// <typeparam name="Tkey">Тип ключа, идентификатора объекта.</typeparam>
public interface IAplicationService<TDto, TCreateUpdateDto, Tkey>
{
    /// <summary>
    /// Создает новый объект и возвращает его представление в виде DTO.
    /// </summary>
    /// <param name="dto">Данные для создания нового объекта.</param>
    /// <returns>Задача, представляющая асинхронную операцию, результатом которой является созданный объект в виде DTO.</returns>
    public Task<TDto> Create(TCreateUpdateDto dto);

    /// <summary>
    /// Возвращает объект DTO по указанному идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор объекта для получения.</param>
    /// <returns>Задача, представляющая асинхронную операцию, результатом которой является объект DTO.</returns>
    public Task<TDto?> Get(Tkey id);

    /// <summary>
    /// Возвращает все объекты в виде DTO.
    /// </summary>
    /// <returns>Задача, представляющая асинхронную операцию, результатом которой является DTO.</returns>
    public Task<IList<TDto>> GetAll();

    /// <summary>
    /// Обновляет сущность по идентификору и возвращает обновлённый объект DTO.
    /// </summary>
    /// <param name="dto">Данные для обновления сущности.</param>
    /// <param name="id">Идентификатор обновляемой сущности.</param>
    /// <returns>Задача, представляющая асинхронную операцию, результатом которой является обновлённый объект DTO.</returns>
    public Task<TDto> Update(TCreateUpdateDto dto, Tkey id);

    /// <summary>
    /// Удаляет сущность по заданному идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемой сущности.</param>
    /// <returns>Задача, представляющая результат операции удаления: true, если удаление выполнено успешно; в противном случае —
    /// false.</returns>
    public Task<bool> Delete(Tkey id);
}
