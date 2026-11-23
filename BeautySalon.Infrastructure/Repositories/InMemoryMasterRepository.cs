using BeautySalon.Domain.Data;
using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;

namespace BeautySalon.Infrastructure.Repositories;

/// <summary>
/// In memory реализация репозитория мастеров.
/// </summary>
public class InMemoryMasterRepository : IMasterRepository
{
    private readonly List<Master> _masters;

    /// <summary>
    /// Инициализирует репозиторий, копируя данные из SeederData.
    /// </summary>
    public InMemoryMasterRepository()
    {
        _masters = SeederData.Masters.ToList();
    }

    /// <inheritdoc />
    public IEnumerable<Master> GetAll() => _masters;

    /// <inheritdoc />
    public Master? GetById(int id) => _masters.FirstOrDefault(m => m.Id == id);

    /// <inheritdoc />
    public void Add(Master master)
    {
        master.Id = _masters.Count > 0 ? _masters.Max(m => m.Id) + 1 : 1;
        _masters.Add(master);
    }

    /// <inheritdoc />
    public void Update(Master master) { }

    /// <inheritdoc />
    public void Delete(Master master) => _masters.Remove(master);
}