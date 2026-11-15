using FitnessGym.Domain.Entities;
using FitnessGym.Domain.Interfaces;
using FitnessGym.Infrastructure.EFCore.Data;
using Microsoft.EntityFrameworkCore;

namespace FitnessGym.Infrastructure.EFCore.Repositories;

public class SpecializationRepository : ISpecializationRepository
{
    private readonly FitnessGymDbContext _context;

    public SpecializationRepository(FitnessGymDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Specialization>> GetAllAsync()
    {
        return await _context.Specializations.ToListAsync();
    }

    public async Task<Specialization?> GetByIdAsync(int id)
    {
        return await _context.Specializations.FindAsync(id);
    }

    public async Task<Specialization> AddAsync(Specialization entity)
    {
        await _context.Specializations.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(Specialization entity)
    {
        _context.Specializations.Update(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var spec = await _context.Specializations.FindAsync(id);
        if (spec is null) return false;

        _context.Specializations.Remove(spec);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }
}