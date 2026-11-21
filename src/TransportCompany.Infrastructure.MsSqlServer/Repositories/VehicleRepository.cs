using Microsoft.EntityFrameworkCore;
using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Interfaces;
using TransportCompany.Infrastructure.MsSqlServer.Data;

namespace TransportCompany.Infrastructure.MsSqlServer.Repositories;

/// <summary>
/// Репозиторий транспортных средств
/// </summary>
public class VehicleRepository(TransportCompanyDbContext context) : IVehicleRepository
{
    private readonly TransportCompanyDbContext _context = context;

    public async Task<IEnumerable<Vehicle>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Vehicles
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Vehicle?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Vehicles
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);
    }

    public async Task<Vehicle> AddAsync(Vehicle entity, CancellationToken cancellationToken = default)
    {
        await _context.Vehicles.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Vehicle?> UpdateAsync(Vehicle entity, CancellationToken cancellationToken = default)
    {
        var existing = await _context.Vehicles.FirstOrDefaultAsync(v => v.Id == entity.Id, cancellationToken);
        if (existing == null)
        {
            return null;
        }

        existing.LicensePlate = entity.LicensePlate;
        existing.LoadCapacity = entity.LoadCapacity;
        existing.ModelId = entity.ModelId;

        await _context.SaveChangesAsync(cancellationToken);
        return existing;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var existing = await _context.Vehicles.FirstOrDefaultAsync(v => v.Id == id, cancellationToken);
        if (existing == null)
        {
            return false;
        }

        _context.Vehicles.Remove(existing);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}