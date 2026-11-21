using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Interfaces;
using TransportCompany.Infrastructure.InMemory.Data;

namespace TransportCompany.Infrastructure.InMemory.Repositories;

public class TripRepository(InMemoryDbContext context) : ITripRepository
{
    private readonly InMemoryDbContext _context = context;

    public Task<IEnumerable<Trip>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_context.Trips.AsEnumerable());
    }

    public Task<Trip?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var trip = _context.Trips.FirstOrDefault(c => c.Id == id);
        return Task.FromResult(trip);
    }

    public Task<Trip> AddAsync(Trip entity, CancellationToken cancellationToken = default)
    {
        entity.Id = _context.Trips.Count == 0 ? 0 : _context.Trips.Max(c => c.Id) + 1;

        _context.Trips.Add(entity);
        return Task.FromResult(entity);
    }


    public Task<Trip?> UpdateAsync(Trip entity, CancellationToken cancellationToken = default)
    {
        var existing = _context.Trips.FirstOrDefault(c => c.Id == entity.Id);
        if (existing != null)
        {
            existing.DriverId = entity.DriverId;
            existing.VehicleId = entity.VehicleId;
            existing.ClientId = entity.ClientId;
            existing.Origin = entity.Origin;           
            existing.Destination = entity.Destination; 
            existing.Date = entity.Date;
            existing.CargoWeight = entity.CargoWeight;
            existing.TransportationCost = entity.TransportationCost;
            existing.Status = entity.Status;
            
            return Task.FromResult<Trip?>(existing);
        }
        return Task.FromResult<Trip?>(null);
    }

    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var existing = _context.Trips.FirstOrDefault(c => c.Id == id);
        if (existing != null)
        {
            _context.Trips.Remove(existing);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }
}