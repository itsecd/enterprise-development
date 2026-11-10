using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Interfaces;
using TransportCompany.Infrastructure.InMemory.Data;

namespace TransportCompany.Infrastructure.InMemory.Repositories;

public class DriverRepository(InMemoryDbContext context) : IDriverRepository
{
    private readonly InMemoryDbContext _context = context;

    public Task<IEnumerable<Driver>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_context.Drivers.AsEnumerable());
    }

    public Task<Driver?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var driver = _context.Drivers.FirstOrDefault(c => c.Id == id);
        return Task.FromResult(driver);
    }

    public Task<Driver> AddAsync(Driver entity, CancellationToken cancellationToken = default)
    {
        entity.Id = _context.Drivers.Count == 0 ? 0 : _context.Drivers.Max(c => c.Id) + 1;

        _context.Drivers.Add(entity);
        return Task.FromResult(entity);
    }

    public Task<Driver?> UpdateAsync(Driver entity, CancellationToken cancellationToken = default)
    {
        var existing = _context.Drivers.FirstOrDefault(c => c.Id == entity.Id);
        if (existing != null)
        {
            existing.PassportNumber = entity.PassportNumber;
            existing.FullName = entity.FullName;
            existing.Experience = entity.Experience;
            existing.License = entity.License;
            return Task.FromResult<Driver?>(existing);
        }
        return Task.FromResult<Driver?>(null);
    }

    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var existing = _context.Drivers.FirstOrDefault(c => c.Id == id);
        if (existing != null)
        {
            _context.Drivers.Remove(existing);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }
}