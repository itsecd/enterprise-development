using Microsoft.EntityFrameworkCore;
using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Interfaces;
using TransportCompany.Infrastructure.MsSqlServer.Data;

namespace TransportCompany.Infrastructure.MsSqlServer.Repositories;

/// <summary>
/// Репозиторий водителей
/// </summary>
public class DriverRepository(TransportCompanyDbContext context) : IDriverRepository
{
    private readonly TransportCompanyDbContext _context = context;

    public async Task<IEnumerable<Driver>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Drivers
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Driver?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Drivers
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
    }

    public async Task<Driver> AddAsync(Driver entity, CancellationToken cancellationToken = default)
    {
        await _context.Drivers.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Driver?> UpdateAsync(Driver entity, CancellationToken cancellationToken = default)
    {
        var existing = await _context.Drivers.FirstOrDefaultAsync(d => d.Id == entity.Id, cancellationToken);
        if (existing == null)
        {
            return null;
        }

        existing.PassportNumber = entity.PassportNumber;
        existing.FullName = entity.FullName;
        existing.Experience = entity.Experience;
        existing.License = entity.License;

        await _context.SaveChangesAsync(cancellationToken);
        return existing;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var existing = await _context.Drivers.FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
        if (existing == null)
        {
            return false;
        }

        _context.Drivers.Remove(existing);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}