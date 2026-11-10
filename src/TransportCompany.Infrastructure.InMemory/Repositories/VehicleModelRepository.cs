using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Interfaces;
using TransportCompany.Infrastructure.InMemory.Data;

namespace TransportCompany.Infrastructure.InMemory.Repositories;

public class VehicleModelRepository(InMemoryDbContext context) : IVehicleModelRepository
{
    private readonly InMemoryDbContext _context = context;

    public Task<IEnumerable<VehicleModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_context.Models.AsEnumerable());
    }

    public Task<VehicleModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var model = _context.Models.FirstOrDefault(c => c.Id == id);
        return Task.FromResult(model);
    }

    public Task<VehicleModel> AddAsync(VehicleModel entity, CancellationToken cancellationToken = default)
    {
        entity.Id = _context.Models.Count == 0 ? 0 : _context.Models.Max(c => c.Id) + 1;

        _context.Models.Add(entity);
        return Task.FromResult(entity);
    }


    public Task<VehicleModel?> UpdateAsync(VehicleModel entity, CancellationToken cancellationToken = default)
    {
        var existing = _context.Models.FirstOrDefault(c => c.Id == entity.Id);
        if (existing != null)
        {
            existing.BodyType = entity.BodyType;
            existing.BodyVolume = entity.BodyVolume;
            return Task.FromResult<VehicleModel?>(existing);
        }
        return Task.FromResult<VehicleModel?>(null);
    }

    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var existing = _context.Models.FirstOrDefault(c => c.Id == id);
        if (existing != null)
        {
            _context.Models.Remove(existing);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }
}