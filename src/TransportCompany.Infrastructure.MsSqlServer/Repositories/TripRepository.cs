using Microsoft.EntityFrameworkCore;
using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Interfaces;
using TransportCompany.Infrastructure.MsSqlServer.Data;

namespace TransportCompany.Infrastructure.MsSqlServer.Repositories;

/// <summary>
/// Репозиторий рейсов
/// </summary>
public class TripRepository(TransportCompanyDbContext context) : ITripRepository
{
    private readonly TransportCompanyDbContext _context = context;

    public async Task<IEnumerable<Trip>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Trips
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Trip?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Trips
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<Trip> AddAsync(Trip entity, CancellationToken cancellationToken = default)
    {
        await _context.Trips.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Trip?> UpdateAsync(Trip entity, CancellationToken cancellationToken = default)
    {
        var existing = await _context.Trips.FirstOrDefaultAsync(t => t.Id == entity.Id, cancellationToken);
        if (existing == null)
        {
            return null;
        }

        existing.DriverId = entity.DriverId;
        existing.VehicleId = entity.VehicleId;
        existing.ClientId = entity.ClientId;
        existing.Origin = entity.Origin;
        existing.Destination = entity.Destination;
        existing.Date = entity.Date;
        existing.CargoWeight = entity.CargoWeight;
        existing.TransportationCost = entity.TransportationCost;
        existing.Status = entity.Status;

        await _context.SaveChangesAsync(cancellationToken);
        return existing;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var existing = await _context.Trips.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        if (existing == null)
        {
            return false;
        }

        _context.Trips.Remove(existing);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}