using TransportCompany.Domain.Entities;

namespace TransportCompany.Domain.Interfaces;

/// <summary>
/// Репозиторий рейсов транспортной компании
/// </summary>
public interface ITripRepository : IRepository<Trip>
{
}