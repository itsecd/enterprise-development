using DryCleaning.Domain.Models;
using DryCleaning.Infrastructure.Data;

namespace DryCleaning.Infrastructure.Repositories;

public class EfCoreClientRepository(DryCleaningDbContext context) : IClientRepository
{
    public IEnumerable<Client> GetAll() => context.Clients.ToList();
    public Client? GetById(int id) => context.Clients.Find(id);

    public void Add(Client entity)
    {
        context.Clients.Add(entity);
        context.SaveChanges();
    }

    public void Update(Client entity)
    {
        context.Clients.Update(entity);
        context.SaveChanges();
    }

    public void Delete(int id)
    {
        var entity = GetById(id);
        if (entity != null)
        {
            context.Clients.Remove(entity);
            context.SaveChanges();
        }
    }
}
