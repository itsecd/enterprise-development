using Microsoft.EntityFrameworkCore;
using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Interfaces;
using TransportCompany.Infrastructure.MsSqlServer.Data;

namespace TransportCompany.Infrastructure.MsSqlServer.Repositories;

/// <summary>
/// Репозиторий моделей ТС
/// </summary>
public class VehicleModelRepository(TransportCompanyDbContext context) : IVehicleModelRepository
{
    private readonly TransportCompanyDbContext _context = context;

    public async Task<IEnumerable<VehicleModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.VehicleModels
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<VehicleModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.VehicleModels
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
    }

    public async Task<VehicleModel> AddAsync(VehicleModel entity, CancellationToken cancellationToken = default)
    {
        await _context.VehicleModels.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<VehicleModel?> UpdateAsync(VehicleModel entity, CancellationToken cancellationToken = default)
    {
        var existing = await _context.VehicleModels.FirstOrDefaultAsync(m => m.Id == entity.Id, cancellationToken);
        if (existing == null)
        {
            return null;
        }

        existing.BodyType = entity.BodyType;
        existing.BodyVolume = entity.BodyVolume;

        await _context.SaveChangesAsync(cancellationToken);
        return existing;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var existing = await _context.VehicleModels.FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
        if (existing == null)
        {
            return false;
        }

        _context.VehicleModels.Remove(existing);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}