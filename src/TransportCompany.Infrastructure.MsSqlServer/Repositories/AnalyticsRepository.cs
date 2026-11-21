using Microsoft.EntityFrameworkCore;
using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Enums;
using TransportCompany.Domain.Interfaces;
using TransportCompany.Domain.Models;
using TransportCompany.Infrastructure.MsSqlServer.Data;

namespace TransportCompany.Infrastructure.MsSqlServer.Repositories;

/// <summary>
/// Репозиторий аналитических запросов (MS SQL)
/// </summary>
public class AnalyticsRepository(TransportCompanyDbContext context) : IAnalyticsRepository
{
    private readonly TransportCompanyDbContext _context = context;

    public async Task<IReadOnlyList<Client>> GetClientsByVehicleModelAsync(int modelId, CancellationToken cancellationToken = default)
    {
        var clientIds =
            from t in _context.Trips.AsNoTracking()
            join v in _context.Vehicles on t.VehicleId equals v.Id
            where v.ModelId == modelId
            select t.ClientId;

        return await _context.Clients
            .AsNoTracking()
            .Where(c => clientIds.Contains(c.Id))
            .OrderBy(c => c.Name)
            .ThenBy(c => c.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Vehicle>> GetVehiclesInTransitAsync(CancellationToken cancellationToken = default)
    {
        var inTransitVehicleIds = _context.Trips
            .AsNoTracking()
            .Where(t => t.Status == TripStatus.InTransit)
            .Select(t => t.VehicleId);

        return await _context.Vehicles
            .AsNoTracking()
            .Where(v => inTransitVehicleIds.Contains(v.Id))
            .OrderBy(v => v.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<DriverTripCount>> GetTopDriversAsync(int take, CancellationToken cancellationToken = default)
    {
        var query =
            from t in _context.Trips.AsNoTracking()
            join d in _context.Drivers on t.DriverId equals d.Id
            group t by new { d.Id, d.FullName } into g
            orderby g.Count() descending, g.Key.Id
            select new DriverTripCount(g.Key.Id, g.Key.FullName, g.Count());

        return await query.Take(take).ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<VehicleTripCount>> GetTripCountsAsync(CancellationToken cancellationToken = default)
    {
        var query = _context.Vehicles
            .AsNoTracking()
            .LeftJoin(
                _context.Trips,
                v => v.Id,
                t => t.VehicleId,
                (v, t) => new { v.Id, v.LicensePlate, Trip = t })
            .GroupBy(x => new { x.Id, x.LicensePlate })
            .OrderBy(g => g.Key.Id)
            .Select(g => new VehicleTripCount(
                g.Key.Id,
                g.Key.LicensePlate,
                g.Count(x => x.Trip != null)));

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ClientTripCost>> GetTopClientsByCostAsync(int take, CancellationToken cancellationToken = default)
    {
        var query =
            from t in _context.Trips.AsNoTracking()
            join c in _context.Clients on t.ClientId equals c.Id
            group t by new { c.Id, c.Name } into g
            orderby (g.Sum(t => t.TransportationCost) ?? 0m) descending, g.Key.Id
            select new ClientTripCost(
                g.Key.Id,
                g.Key.Name,
                g.Sum(t => t.TransportationCost) ?? 0m);

        return await query.Take(take).ToListAsync(cancellationToken);
    }
}