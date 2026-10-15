namespace FitnesClub.Api.Storage;

/// <summary>
/// Интерфейс репозитория для CRUD операций
/// </summary>
public interface IRepository<TEntity, TKey> 
    where TEntity : class 
    where TKey : struct
{
    public void Create(TEntity entity);
    public List<TEntity> ReadAll();
    public TEntity? Read(TKey id);
    public void Update(TEntity entity);
    public void Delete(TKey id);
}
