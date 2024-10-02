using DispatchTransportControl.Domain.Context;
using DispatchTransportControl.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DispatchTransportControl.Domain.Repository;

/// <summary>
///     Класс реализует интерфейс <see cref="IVehicleModelRepository" />
/// </summary>
public class VehicleModelRepository(TransportDbContext context) : IVehicleModelRepository
{
    /// <inheritdoc />
    public IEnumerable<VehicleModel> GetAll()
    {
        return context.VehicleModels;
    }

    /// <inheritdoc />
    public VehicleModel? GetById(int id)
    {
        return context.VehicleModels.Find(id);
    }

    /// <inheritdoc />
    public VehicleModel Create(VehicleModel entity)
    {
        var res = context.VehicleModels.Add(entity).Entity;
        context.SaveChanges();
        return res;
    }

    /// <inheritdoc />
    public VehicleModel Update(VehicleModel entity)
    {
        var entry = context.Entry(entity);
        entry.State = EntityState.Modified;
        context.SaveChanges();
        return entry.Entity;
    }

    /// <inheritdoc />
    public void Delete(VehicleModel entity)
    {
        context.VehicleModels.Remove(entity);
        context.SaveChanges();
    }
}