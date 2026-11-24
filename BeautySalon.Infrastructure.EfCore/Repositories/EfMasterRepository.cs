using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;
using BeautySalon.Infrastructure.EfCore.Data;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Infrastructure.EfCore.Repositories;

public class EfMasterRepository(BeautySalonDbContext context) : IMasterRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<Master>> GetAllAsync() => await context.Masters.ToListAsync();

    /// <inheritdoc />
    public async Task<Master?> GetByIdAsync(int id) => await context.Masters.FirstOrDefaultAsync(m => m.Id == id);

    /// <inheritdoc />
    public async Task AddAsync(Master entity) 
    { 
        context.Masters.Add(entity); 
        await context.SaveChangesAsync(); 
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Master entity)
    { 
        context.Masters.Update(entity); 
        await context.SaveChangesAsync(); 
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Master entity) 
    { 
        context.Masters.Remove(entity); 
        await context.SaveChangesAsync(); 
    }
}