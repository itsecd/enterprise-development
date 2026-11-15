using FitnessGym.Domain.Entities;
using FitnessGym.Domain.Interfaces;
using FitnessGym.Infrastructure.EFCore.Data;
using Microsoft.EntityFrameworkCore;

namespace FitnessGym.Infrastructure.EFCore.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly FitnessGymDbContext _context;

    public ClientRepository(FitnessGymDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Client>> GetAllAsync()
    {
        return await _context.Clients.ToListAsync();
    }

    public async Task<Client?> GetByIdAsync(int id)
    {
        return await _context.Clients.FindAsync(id);
    }

    public async Task<Client> AddAsync(Client entity)
    {
        await _context.Clients.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(Client entity)
    {
        _context.Clients.Update(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client is null) return false;

        _context.Clients.Remove(client);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }
}