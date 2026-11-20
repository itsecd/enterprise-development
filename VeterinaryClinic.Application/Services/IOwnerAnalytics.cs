using VeterinaryClinic.Application.Contracts.Appointments;
using VeterinaryClinic.Application.Contracts.Breeds;
using VeterinaryClinic.Application.Contracts.Owners;
using VeterinaryClinic.Application.Contracts.Pets;
using VeterinaryClinic.Application.Contracts.Specializations;
using VeterinaryClinic.Application.Contracts.Veterinarians;
using VeterinaryClinic.Domain.Models;

namespace VeterinaryClinic.Application.Services;

public interface IOwnerAnalytics
{
    public Task<IReadOnlyList<Owner>> OwnersWithMultiplePetsAsync(CancellationToken cancellationToken = default);
}
