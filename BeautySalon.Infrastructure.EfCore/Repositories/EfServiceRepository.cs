using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;
using BeautySalon.Infrastructure.EfCore.Data;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Infrastructure.EfCore.Repositories;

public class EfServiceRepository(BeautySalonDbContext context) : IServiceRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<Service>> GetAllAsync() => await context.Services.ToListAsync();

    /// <inheritdoc />
    public async Task<Service?> GetByIdAsync(int id) => await context.Services.FirstOrDefaultAsync(s => s.Id == id);

    /// <inheritdoc />
    public async Task AddAsync(Service entity) 
    { 
        context.Services.Add(entity); 
        await context.SaveChangesAsync(); 
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Service entity) 
    { 
        context.Services.Update(entity); 
        await context.SaveChangesAsync(); 
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Service entity) 
    { 
        context.Services.Remove(entity); 
        await context.SaveChangesAsync(); 
    }
}