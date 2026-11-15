using FitnessGym.Domain.Entities;
using FitnessGym.Domain.Interfaces;
using FitnessGym.Infrastructure.EFCore.Data;
using Microsoft.EntityFrameworkCore;

namespace FitnessGym.Infrastructure.EFCore.Repositories;

public class TrainerRepository : ITrainerRepository
{
    private readonly FitnessGymDbContext _context;

    public TrainerRepository(FitnessGymDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Trainer>> GetAllAsync()
    {
        return await _context.Trainers.ToListAsync();
    }

    public async Task<Trainer?> GetByIdAsync(int id)
    {
        return await _context.Trainers.FindAsync(id);
    }

    public async Task<Trainer> AddAsync(Trainer entity)
    {
        await _context.Trainers.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(Trainer entity)
    {
        _context.Trainers.Update(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var trainer = await _context.Trainers.FindAsync(id);
        if (trainer is null) return false;

        _context.Trainers.Remove(trainer);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }
}