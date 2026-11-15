using FitnessGym.Domain.Entities;
using FitnessGym.Domain.Interfaces;
using FitnessGym.Infrastructure.EFCore.Data;
using Microsoft.EntityFrameworkCore;

namespace FitnessGym.Infrastructure.EFCore.Repositories;

public class TrainingSessionRepository : ITrainingSessionRepository
{
    private readonly FitnessGymDbContext _context;

    public TrainingSessionRepository(FitnessGymDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<TrainingSession>> GetAllAsync()
    {
        return await _context.TrainingSessions.ToListAsync();
    }

    public async Task<TrainingSession?> GetByIdAsync(int id)
    {
        return await _context.TrainingSessions.FindAsync(id);
    }

    public async Task<TrainingSession> AddAsync(TrainingSession entity)
    {
        await _context.TrainingSessions.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(TrainingSession entity)
    {
        _context.TrainingSessions.Update(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var session = await _context.TrainingSessions.FindAsync(id);
        if (session is null) return false;

        _context.TrainingSessions.Remove(session);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }
}