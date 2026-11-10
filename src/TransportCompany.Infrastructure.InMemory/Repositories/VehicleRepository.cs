using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Interfaces;
using TransportCompany.Infrastructure.InMemory.Data;

namespace TransportCompany.Infrastructure.InMemory.Repositories;

public class VehicleRepository(InMemoryDbContext context) : IVehicleRepository
{
    private readonly InMemoryDbContext _context = context;

    public Task<IEnumerable<Vehicle>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_context.Vehicles.AsEnumerable());
    }

    public Task<Vehicle?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var vehicle = _context.Vehicles.FirstOrDefault(c => c.Id == id);
        return Task.FromResult(vehicle);
    }

    public Task<Vehicle> AddAsync(Vehicle entity, CancellationToken cancellationToken = default)
    {
        entity.Id = _context.Vehicles.Count == 0 ? 0 : _context.Vehicles.Max(c => c.Id) + 1;

        _context.Vehicles.Add(entity);
        return Task.FromResult(entity);
    }

    public Task<Vehicle?> UpdateAsync(Vehicle entity, CancellationToken cancellationToken = default)
    {
        var existing = _context.Vehicles.FirstOrDefault(c => c.Id == entity.Id);
        if (existing != null)
        {
            existing.LicensePlate = entity.LicensePlate;
            existing.LoadCapacity = entity.LoadCapacity;
            existing.ModelId = entity.ModelId;
            return Task.FromResult<Vehicle?>(existing);
        }
        return Task.FromResult<Vehicle?>(null);
    }

    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var existing = _context.Vehicles.FirstOrDefault(c => c.Id == id);
        if (existing != null)
        {
            _context.Vehicles.Remove(existing);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }
}