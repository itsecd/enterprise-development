namespace TransportCompany.Domain.Interfaces;

/// <summary>
/// Базовый интерфейс для CRUD
/// </summary>
/// <typeparam name="TEntity">Тип сущности</typeparam>
public interface IRepository<TEntity> where TEntity : class
{
    public Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    public Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    public Task<TEntity?> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}