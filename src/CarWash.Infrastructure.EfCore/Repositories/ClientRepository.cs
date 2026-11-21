using CarWash.Domain.Entities;
using CarWash.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarWash.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий клиентов в базе данных
/// </summary>
/// <param name="context">Контекст базы данных</param>
public class ClientRepository(CarWashDbContext context) : EfRepository<Client>(context), IClientRepository
{
    /// <inheritdoc />
    protected override IQueryable<Client> Query() => Entities.Include(c => c.Cars);
}
