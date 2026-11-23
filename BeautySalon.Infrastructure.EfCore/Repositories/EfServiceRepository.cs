using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;
using BeautySalon.Infrastructure.EfCore.Data;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Infrastructure.EfCore.Repositories;

public class EfServiceRepository(BeautySalonDbContext context) : IServiceRepository
{
    /// <inheritdoc />
    public IEnumerable<Service> GetAll() => context.Services.ToList();

    /// <inheritdoc />
    public Service? GetById(int id) => context.Services.FirstOrDefault(s => s.Id == id);

    /// <inheritdoc />
    public void Add(Service service) 
    { 
        context.Services.Add(service); 
        context.SaveChanges(); 
    }

    /// <inheritdoc />
    public void Update(Service service)
    {
        context.Services.Update(service); 
        context.SaveChanges(); 
    }

    /// <inheritdoc />
    public void Delete(Service service) 
    { 
        context.Services.Remove(service); 
        context.SaveChanges(); 
    }
}